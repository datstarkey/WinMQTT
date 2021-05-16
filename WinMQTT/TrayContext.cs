using System;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinMQTT
{
    internal class TrayContext : ApplicationContext
    {
        private NotifyIcon _trayIcon;
        private ContextMenuStrip _trayIconContextMenu;
        private ToolStripMenuItem _closeMenuItem;
        private ToolStripMenuItem _optionsItem;
        private readonly Properties.Settings _settings = Properties.Settings.Default;

        public TrayContext()
        {
            Application.ApplicationExit += new EventHandler(this.OnApplicationExit);
            InitializeComponent();
            Main();
        }

        private async Task Main()
        {
            _trayIcon.Visible = true;

            if (string.IsNullOrEmpty(_settings.Server))
            {
                new OptionsPage().Show();
            }
            else
            {
                await MQTT.Start();
            }
        }

        private void InitializeComponent()
        {
            _trayIcon = new NotifyIcon();

            _trayIconContextMenu = new ContextMenuStrip();
            _closeMenuItem = new ToolStripMenuItem();
            _optionsItem = new ToolStripMenuItem();
            _trayIconContextMenu.SuspendLayout();

            _trayIcon.Icon = Properties.Resources.TrayIcon;

            _trayIcon.Text = "Windows MQTT Client Executor";

            _trayIconContextMenu.Items.AddRange(new ToolStripItem[] {_optionsItem, _closeMenuItem,});
            _trayIconContextMenu.Name = "_trayIconContextMenu";
            _trayIconContextMenu.Size = new Size(153, 70);

            _closeMenuItem.Name = "_closeMenuItem";
            _closeMenuItem.Size = new Size(152, 22);
            _closeMenuItem.Text = "Close the tray icon program";
            _closeMenuItem.Click += CloseMenuItem_Click;

            _optionsItem.Name = "OpenSettingsItem";
            _optionsItem.Size = new Size(152, 22);
            _optionsItem.Text = "Open Settings";
            _optionsItem.Click += OpenSettingsEvent;

            _trayIconContextMenu.ResumeLayout(false);
            _trayIcon.ContextMenuStrip = _trayIconContextMenu;
        }

        private void OpenSettingsEvent(object sender, EventArgs e)
        {
            new OptionsPage().Show();
        }

        private void OnApplicationExit(object sender, EventArgs e) => _trayIcon.Visible = false;

        private void CloseMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you really want to close", "Are you sure?", MessageBoxButtons.YesNo,
                MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                Application.Exit();
        }
    }
}