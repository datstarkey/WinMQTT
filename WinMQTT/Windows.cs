using AudioSwitcher.AudioApi.CoreAudio;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinMQTT
{
    public class Windows
    {
        private CoreAudioDevice defaultPlaybackDevice = new CoreAudioController().DefaultPlaybackDevice;
        private double volume = 0;
        private MQTT mqtt;

        [DllImport("user32")]
        public static extern bool ExitWindowsEx(uint uFlags, uint dwReason);

        [DllImport("user32")]
        public static extern void LockWorkStation();

        [DllImport("PowrProf.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool SetSuspendState(bool hiberate, bool forceCritical, bool disableWakeEvent);

        public Windows(MQTT MQTT)
        {
            mqtt = MQTT;
        }

        public async void ProcessRequest(string topic, string payload)
        {
            switch (topic)
            {
                case "windows/status":
                    await SendStatus();
                    break;

                case "windows/sendkeys":
                    KeyStroke(payload);
                    break;

                case "windows/actions":
                    Actions(payload);
                    break;

                case "windows/volume":
                    Volume(payload);
                    break;

                default:
                    break;
            }
        }

        private async Task SendStatus()
        {
            await mqtt.Publish("windows", "On");
        }

        public void Actions(string payload)
        {
            try
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
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                Debug.WriteLine("not valid input");
            }
        }

        public void KeyStroke(string input)
        {
            try
            {
                SendKeys.SendWait(input);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                Debug.WriteLine("not valid input");
            }
        }

        public void Volume(string volume)
        {
            try
            {
                double.TryParse(volume, out double v);
                Debug.WriteLine(v);
                defaultPlaybackDevice.SetVolumeAsync(v);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                Debug.WriteLine("not valid input");
            }
        }

        public string GetVolume()
        {
            try
            {
                volume = Math.Round(defaultPlaybackDevice.Volume);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }

            return volume.ToString();
        }

        public string GetMonitors()
        {
            try
            {
                var screens = Screen.AllScreens;
                return screens.Length.ToString();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                Debug.WriteLine("not valid input");
                return "0";
            }
        }
    }
}