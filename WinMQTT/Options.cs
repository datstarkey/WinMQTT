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
    public partial class Options : Form
    {
        public Options()
        {
            InitializeComponent();

            this.ServerBox.Text = TrayContext.Options.Server;
            this.PortBox.Text = TrayContext.Options.Port.ToString();
            this.UsernameBox.Text = TrayContext.Options.Username;
            this.PasswordBox.Text = TrayContext.Options.Password;
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (IsDigitsOnly(this.PortBox.Text))
            {
                TrayContext.Options.Server = ServerBox.Text;
                TrayContext.Options.Port = int.Parse(PortBox.Text);
                TrayContext.Options.Username = UsernameBox.Text;
                TrayContext.Options.Password = PasswordBox.Text;
                this.Hide();
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