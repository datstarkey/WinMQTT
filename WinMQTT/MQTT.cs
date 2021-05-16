using Microsoft.Win32;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinMQTT.Extensions;
using Timer = System.Windows.Forms.Timer;

namespace WinMQTT
{
    public static class MQTT
    {
        private static readonly IMqttClient MqttClient = new MqttFactory().CreateMqttClient();
        public static bool Running = false;
        public static CancellationTokenSource TokenSource = new CancellationTokenSource();

        public static Timer SendStatus = new Timer()
        {
            Enabled = false,
            Interval = 5000,
        };


        public static async Task Start()
        {
            Running = true;
            var machineName = Environment.MachineName;

            var token = TokenSource.Token;

            SendStatus.Tick -= UpdateMachineStatus;
            SendStatus.Tick += UpdateMachineStatus;
            SendStatus.Enabled = true;


            Debug.WriteLine("Starting MQTT Client");
            var server = Properties.Settings.Default.Server;
            var port = Properties.Settings.Default.Port;
            var username = Properties.Settings.Default.Username;
            var password = Properties.Settings.Default.Password;

            var options = new MqttClientOptionsBuilder()
                .WithClientId($"Windows {machineName}")
                .WithTcpServer(server, port)
                .WithWillMessage(new MqttApplicationMessageBuilder()
                    .WithTopic($"windows/{machineName}")
                    .WithPayload("false")
                    .Build())
                .WithCredentials(username, password)
                .WithCleanSession()
                .Build();


            await MqttClient.ConnectAsync(options, token);

            Debug.WriteLine("### CONNECTED WITH SERVER ###");

            TrayContext.SendNotification("WinMQTT Connected to the broker");

            await MqttClient.SubscribeAsync(Subscribe($"windows/{machineName}/sendkeys"));
            await MqttClient.SubscribeAsync(Subscribe($"windows/{machineName}/actions"));
            await MqttClient.SubscribeAsync(Subscribe($"windows/{machineName}/volume"));
            await MqttClient.SubscribeAsync(Subscribe($"windows/{machineName}/volume/status"));
            await MqttClient.SubscribeAsync(Subscribe($"windows/{machineName}/screens/status"));
            await MqttClient.SubscribeAsync(Subscribe($"windows/{machineName}/status"));


            MqttClient.UseDisconnectedHandler(async e =>
            {
                Debug.WriteLine("### DISCONNECTED FROM SERVER ###");
                TrayContext.SendNotification("WinMQTT Disconnected from the broker, attempting reconnect");

                if (Running)
                {
                    try
                    {
                        await MqttClient.ConnectAsync(options, token);
                        TrayContext.SendNotification("WinMQTT Reconnected to the broker");
                    }
                    catch
                    {
                        Debug.WriteLine("### RECONNECTING FAILED ###");
                        await Task.Delay(3000, token);
                    }
                }
            });

            MqttClient.UseApplicationMessageReceivedHandler(e =>
            {
                try
                {
                    var payload = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
                    var topic = e.ApplicationMessage.Topic;
                    Debug.WriteLine("### RECEIVED APPLICATION MESSAGE ###");
                    Debug.WriteLine($"+ Payload = {payload}");
                    Debug.WriteLine($"+ Topic = {topic}");
                    Windows.ProcessRequest(topic.RemoveMachineName(), payload);
                }
                catch (Exception error)
                {
                    Debug.WriteLine("Unable to Read Message");
                    Debug.WriteLine($"Error = {error}");
                }
            });
        }

        private static async void UpdateMachineStatus(object sender, EventArgs e)
        {
            await Windows.SendStatus();
        }


        public static async Task Restart()
        {
            Debug.WriteLine("Restarting MQTT Client");
            if (Running)
                await Stop(false);

            await Start();
        }

        public static async Task Stop(bool sendStop = true)
        {
            Debug.WriteLine("Stopping MQTT Client");
            Running = false;
            SendStatus.Enabled = false;


            if (TokenSource.Token.CanBeCanceled)
            {
                TokenSource.Cancel();
                TokenSource = new CancellationTokenSource();
            }

            if (sendStop)
                await Windows.SendStatus("false");

            if (MqttClient.IsConnected)
                await MqttClient.DisconnectAsync();
        }


        private static MqttTopicFilter Subscribe(string topic)
        {
            return new MqttTopicFilterBuilder()
                .WithAtMostOnceQoS()
                .WithTopic(topic)
                .Build();
        }

        public static async Task Publish(string topic, string payload)
        {
            try
            {
                if (MqttClient.IsConnected)
                {
                    Console.WriteLine($"Publishing Message {payload} on topic {topic}");
                    var message = new MqttApplicationMessageBuilder().WithTopic(topic).WithPayload(payload)
                        .WithAtMostOnceQoS()
                        .Build();
                    await MqttClient.PublishAsync(message);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error = {e}");
            }
        }
    }
}