namespace WinMQTT
{
    partial class OptionsPage
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OptionsPage));
            this.label1 = new System.Windows.Forms.Label();
            this.ServerBox = new System.Windows.Forms.TextBox();
            this.PortBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.UsernameBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.PasswordBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SaveButton = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.MachineName = new System.Windows.Forms.Label();
            this.Startup = new System.Windows.Forms.CheckBox();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(47, 69);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Server IP";
            // 
            // ServerBox
            // 
            this.ServerBox.Location = new System.Drawing.Point(113, 66);
            this.ServerBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ServerBox.Name = "ServerBox";
            this.ServerBox.Size = new System.Drawing.Size(116, 23);
            this.ServerBox.TabIndex = 1;
            // 
            // PortBox
            // 
            this.PortBox.AccessibleName = "PortBox";
            this.PortBox.Location = new System.Drawing.Point(113, 96);
            this.PortBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.PortBox.Name = "PortBox";
            this.PortBox.Size = new System.Drawing.Size(45, 23);
            this.PortBox.TabIndex = 3;
            this.PortBox.Text = "1883";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(76, 99);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Port";
            // 
            // UsernameBox
            // 
            this.UsernameBox.AccessibleName = "UsernameBox";
            this.UsernameBox.Location = new System.Drawing.Point(113, 126);
            this.UsernameBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.UsernameBox.Name = "UsernameBox";
            this.UsernameBox.Size = new System.Drawing.Size(116, 23);
            this.UsernameBox.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(42, 129);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "Username";
            // 
            // PasswordBox
            // 
            this.PasswordBox.AccessibleName = "PasswordBox";
            this.PasswordBox.Location = new System.Drawing.Point(113, 156);
            this.PasswordBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.PasswordBox.Name = "PasswordBox";
            this.PasswordBox.PasswordChar = '*';
            this.PasswordBox.Size = new System.Drawing.Size(116, 23);
            this.PasswordBox.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(44, 159);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 15);
            this.label4.TabIndex = 6;
            this.label4.Text = "Password";
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(113, 212);
            this.SaveButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(88, 27);
            this.SaveButton.TabIndex = 8;
            this.SaveButton.Text = "Save";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 36);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 15);
            this.label5.TabIndex = 9;
            this.label5.Text = "Machine Name";
            // 
            // MachineName
            // 
            this.MachineName.AccessibleName = "MachineName";
            this.MachineName.AutoSize = true;
            this.MachineName.Location = new System.Drawing.Point(113, 36);
            this.MachineName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.MachineName.Name = "MachineName";
            this.MachineName.Size = new System.Drawing.Size(76, 15);
            this.MachineName.TabIndex = 10;
            this.MachineName.Text = "{someName}";
            // 
            // Startup
            // 
            this.Startup.AutoSize = true;
            this.Startup.Location = new System.Drawing.Point(113, 186);
            this.Startup.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Startup.Name = "Startup";
            this.Startup.Size = new System.Drawing.Size(103, 19);
            this.Startup.TabIndex = 11;
            this.Startup.Text = "Run At Startup";
            this.Startup.UseVisualStyleBackColor = true;
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            // 
            // OptionsPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(287, 253);
            this.Controls.Add(this.Startup);
            this.Controls.Add(this.MachineName);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.PasswordBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.UsernameBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.PortBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ServerBox);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OptionsPage";
            this.Text = "Options";
            this.Load += new System.EventHandler(this.OptionsPage_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox ServerBox;
        private System.Windows.Forms.TextBox PortBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox UsernameBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox PasswordBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label MachineName;
        private System.Windows.Forms.CheckBox Startup;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
    }
}