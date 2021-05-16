using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;

namespace WinMQTT
{
    public partial class OptionsPage : Form
    {
        private readonly Properties.Settings _settings = Properties.Settings.Default;
        private static readonly string StartupKey = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run";
        private static readonly string StartupValue = "WinMQTT";

        public OptionsPage()
        {
            InitializeComponent();

            this.ServerBox.Text = _settings.Server;
            this.PortBox.Text = _settings.Port.ToString();
            this.UsernameBox.Text = _settings.Username;
            this.PasswordBox.Text = _settings.Password;
            this.MachineName.Text = Environment.MachineName;
            Startup.Checked = _settings.Startup;
        }


        private async void SaveButton_Click(object sender, EventArgs e)
        {
            if (IsDigitsOnly(this.PortBox.Text))
            {
                _settings.Server = ServerBox.Text;
                _settings.Port = int.Parse(PortBox.Text);
                _settings.Username = UsernameBox.Text;
                _settings.Password = PasswordBox.Text;
                _settings.Startup = Startup.Checked;
                _settings.Save();
                SetStartup(_settings.Startup);
                MQTT.Restart();
                Close();
            }
            else
                MessageBox.Show("Error", "Port can only be numbers", MessageBoxButtons.OK);
        }

        private static bool IsDigitsOnly(string str)
        {
            return str.All(c => c >= '0' && c <= '9');
        }

        private void OptionsPage_Load(object sender, EventArgs e)
        {
        }

        private static void SetStartup(bool value)
        {
            using (var key = Registry.CurrentUser.OpenSubKey(StartupKey, true))
                if (value)
                    key.SetValue(StartupValue, Application.ExecutablePath);
                else
                    key.DeleteValue(StartupValue);
        }
    }
}