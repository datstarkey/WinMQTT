using System;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace WinMQTT
{
    internal class TrayContext : ApplicationContext
    {
        private NotifyIcon TrayIcon;
        private ContextMenuStrip TrayIconContextMenu;
        private ToolStripMenuItem CloseMenuItem;
        private ToolStripMenuItem OptionsItem;
        public static MQTT Mqtt;
        private Properties.Settings settngs = Properties.Settings.Default;

        public TrayContext()
        {
            Application.ApplicationExit += new EventHandler(this.OnApplicationExit);
            InitializeComponent();
            Main();
        }

        private void Main()
        {
            TrayIcon.Visible = true;
            Mqtt = new MQTT();

            if (String.IsNullOrEmpty(settngs.Server))
            {
                var optionsPage = new OptionsPage();
                optionsPage.Show();
            }
            else
            {
                Mqtt.Start();
            }
        }

        private void InitializeComponent()
        {
            TrayIcon = new NotifyIcon();

            TrayIconContextMenu = new ContextMenuStrip();
            CloseMenuItem = new ToolStripMenuItem();
            OptionsItem = new ToolStripMenuItem();
            TrayIconContextMenu.SuspendLayout();

            TrayIcon.Icon = Properties.Resources.TrayIcon;

            TrayIcon.Text = "Windows MQTT Client Executor";

            TrayIconContextMenu.Items.AddRange(new ToolStripItem[] { OptionsItem, CloseMenuItem, });
            TrayIconContextMenu.Name = "TrayIconContextMenu";
            TrayIconContextMenu.Size = new Size(153, 70);

            CloseMenuItem.Name = "CloseMenuItem";
            CloseMenuItem.Size = new Size(152, 22);
            CloseMenuItem.Text = "Close the tray icon program";
            CloseMenuItem.Click += CloseMenuItem_Click;

            OptionsItem.Name = "OpenSettingsItem";
            OptionsItem.Size = new Size(152, 22);
            OptionsItem.Text = "Open Settings";
            OptionsItem.Click += OpenSettingsEvent;

            TrayIconContextMenu.ResumeLayout(false);
            TrayIcon.ContextMenuStrip = TrayIconContextMenu;
        }

        private void OpenSettingsEvent(object sender, EventArgs e)
        {
            var optionsPage = new OptionsPage();
            optionsPage.Show();
        }

        private void OnApplicationExit(object sender, EventArgs e) => TrayIcon.Visible = false;

        private void CloseMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you really want to close", "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                Application.Exit();
        }
    }
}