using Newtonsoft.Json;
using System;
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
        public static OptionsModel Options;
        private static MQTT mqtt;
        public static Options OptionsPage;

        public TrayContext()
        {
            Application.ApplicationExit += new EventHandler(this.OnApplicationExit);
            InitializeComponent();
            Main();
        }

        private void Main()
        {
            LoadOptions();
            TrayIcon.Visible = true;
            mqtt = new MQTT();

            if (Options.Server.Length > 0)
                mqtt.Start();
            else
                OpenOptions();
        }

        private static void SettingsChanged(object sender, EventArgs e)
        {
            if (OptionsPage.Visible)
                mqtt.Stop();
            else
            {
                Properties.Settings.Default.MqttSettings = JsonConvert.SerializeObject(Options);
                Properties.Settings.Default.Save();
                mqtt.Start();
            }
        }

        private void LoadOptions()
        {
            if (Properties.Settings.Default.MqttSettings != "")
                Options = JsonConvert.DeserializeObject<OptionsModel>(Properties.Settings.Default.MqttSettings);
            else
                Options = new OptionsModel();
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

            TrayIconContextMenu.Items.AddRange(new ToolStripItem[] { OptionsItem, CloseMenuItem,  });
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

            Debug.WriteLine("Tray Made");
        }

        private void OpenSettingsEvent(object sender, EventArgs e)
        {
            OpenOptions();
        }

        public static void OpenOptions()
        {
            if (OptionsPage == null)
            {
                OptionsPage = new Options();
                OptionsPage.VisibleChanged += SettingsChanged;
            }

            OptionsPage.Show();
        }

        private void OnApplicationExit(object sender, EventArgs e) => TrayIcon.Visible = false;

        private void CloseMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you really want to close", "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                Application.Exit();
        }
    }
}