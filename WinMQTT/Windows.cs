using AudioSwitcher.AudioApi.CoreAudio;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinMQTT
{
    public static class Windows
    {
        private static readonly CoreAudioDevice DefaultPlaybackDevice = new CoreAudioController().DefaultPlaybackDevice;
        private static double Volume { get; set; } = 0;

        [DllImport("user32")]
        public static extern bool ExitWindowsEx(uint uFlags, uint dwReason);

        [DllImport("user32")]
        public static extern void LockWorkStation();

        [DllImport("PowrProf.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool SetSuspendState(bool hibernate, bool forceCritical, bool disableWakeEvent);


        public static async void ProcessRequest(string topic, string payload)
        {
            switch (topic)
            {
                case "windows/status":
                    await SendStatus();
                    break;

                case "windows/volume/status":
                    await SendVolume();
                    break;

                case "windows/screens/status":
                    await SendScreens();
                    break;

                case "windows/sendkeys":
                    KeyStroke(payload);
                    break;

                case "windows/actions":
                    Actions(payload);
                    break;

                case "windows/volume":
                    ChangeVolume(payload);
                    break;

                default:
                    break;
            }
        }

        public static async Task SendStatus(string status = "true") =>
            await MQTT.Publish($"windows/{Environment.MachineName}", status);

        private static async Task SendVolume() =>
            await MQTT.Publish($"windows/{Environment.MachineName}volume", GetVolume());

        private static async Task SendScreens() =>
            await MQTT.Publish($"windows/{Environment.MachineName}/screens", GetMonitors());

        public static void Actions(string payload)
        {
            switch (payload)
            {
                case "sleep":
                    SetSuspendState(false, true, true);
                    break;

                case "hibernate":
                    SetSuspendState(true, true, true);
                    break;

                case "shutdown":
                    Process.Start("shutdown", "/s /t 0");
                    break;

                case "restart":
                    Process.Start("shutdown", "/r /t 0");
                    break;

                case "logout":
                    ExitWindowsEx(0, 0);
                    break;

                case "lock":
                    LockWorkStation();
                    break;

                case "exit":
                    Application.Exit();
                    break;

                default:
                    break;
            }
        }

        private static void KeyStroke(string input)
        {
            try
            {
                SendKeys.SendWait(input);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error = {e}");
            }
        }

        private static void ChangeVolume(string volume)
        {
            try
            {
                double.TryParse(volume, out double v);
                DefaultPlaybackDevice.Volume = v;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error = {e}");
            }
        }

        private static string GetVolume()
        {
            try
            {
                Volume = Math.Round(DefaultPlaybackDevice.Volume);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error = {e}");
            }

            return Volume.ToString();
        }

        private static string GetMonitors()
        {
            try
            {
                var screens = Screen.AllScreens;
                return screens.Length.ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error = {e}");
                return "0";
            }
        }
    }
}