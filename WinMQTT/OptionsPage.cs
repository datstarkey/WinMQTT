using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinMQTT
{
    public partial class OptionsPage : Form
    {
        private Properties.Settings settings = Properties.Settings.Default;

        public OptionsPage()
        {
            InitializeComponent();

            this.ServerBox.Text = settings.Server;
            this.PortBox.Text = settings.Port.ToString();
            this.UsernameBox.Text = settings.Username;
            this.PasswordBox.Text = settings.Password;

            TrayContext.Mqtt.Stop();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (IsDigitsOnly(this.PortBox.Text))
            {
                settings.Server = ServerBox.Text;
                settings.Port = int.Parse(PortBox.Text);
                settings.Username = UsernameBox.Text;
                settings.Password = PasswordBox.Text;
                settings.Save();
                TrayContext.Mqtt.Start();
                this.Close();
            }
            else
                MessageBox.Show("Error", "Port can only be numbers", MessageBoxButtons.OK);
        }

        private static bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }
    }
}