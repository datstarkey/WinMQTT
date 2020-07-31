using Microsoft.Win32;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using System;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinMQTT
{
    public class MQTT
    {
        private MqttFactory factory;
        private IMqttClient mqttClient;
        private Windows windows;
        private OptionsPage optionsPage;

        public MQTT()
        {
            windows = new Windows(this);
            factory = new MqttFactory();
            mqttClient = factory.CreateMqttClient();
        }

        public async void Start()
        {
            try
            {
                Console.WriteLine("Starting MQTT Client");
                var server = Properties.Settings.Default.Server;
                var port = Properties.Settings.Default.Port;
                var username = Properties.Settings.Default.Username;
                var password = Properties.Settings.Default.Password;

                var options = new MqttClientOptionsBuilder().WithClientId("Windows Dekstop").WithTcpServer(server, port).WithCleanSession().Build();
                await mqttClient.ConnectAsync(options, CancellationToken.None);
                Console.WriteLine("### CONNECTED WITH SERVER ###");

                await mqttClient.SubscribeAsync(new TopicFilterBuilder().WithTopic("windows/sendkeys").Build());
                await mqttClient.SubscribeAsync(new TopicFilterBuilder().WithTopic("windows/actions").Build());
                await mqttClient.SubscribeAsync(new TopicFilterBuilder().WithTopic("windows/volume").Build());
                await mqttClient.SubscribeAsync(new TopicFilterBuilder().WithTopic("windows/volume/status").Build());
                await mqttClient.SubscribeAsync(new TopicFilterBuilder().WithTopic("windows/screens/status").Build());
                await mqttClient.SubscribeAsync(new TopicFilterBuilder().WithTopic("windows/status").Build());

                mqttClient.UseDisconnectedHandler(async e =>
                {
                    Debug.WriteLine("### DISCONNECTED FROM SERVER ###");
                    await Task.Delay(1000);
                    Start();
                });

                mqttClient.UseApplicationMessageReceivedHandler(e =>
                {
                    try
                    {
                        var payload = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
                        var topic = e.ApplicationMessage.Topic;
                        Console.WriteLine("### RECEIVED APPLICATION MESSAGE ###");
                        Console.WriteLine($"+ Payload = {payload}");
                        Console.WriteLine($"+ Topic = {topic}");
                        windows.ProcessRequest(topic, payload);
                    }
                    catch (Exception error)
                    {
                        Console.WriteLine("Unable to Read Message");
                        Console.WriteLine($"Error = {error}");
                    }
                });
            }
            catch
            {
                Console.WriteLine($"Error = could not connect");
                MessageBox.Show("Incorrect Ip / Port");
                optionsPage.Show();
            }
        }

        public async void Stop()
        {
            if (mqttClient.IsConnected)
            {
                mqttClient.UseDisconnectedHandler(e =>
               {
                   Console.WriteLine("Stopping MQTT Client");
               });
                await mqttClient.DisconnectAsync();
            }
        }

        public async Task Publish(string topic, string payload)
        {
            try
            {
                if (mqttClient.IsConnected)
                {
                    try
                    {
                        var message = new MqttApplicationMessageBuilder().WithTopic(topic).WithPayload(payload).Build();
                        await mqttClient.PublishAsync(message);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Error = {e}");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error = {e}");
            }
        }
    }
}