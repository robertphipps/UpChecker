namespace UpChecker
{
    partial class Setup
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
            this.inputBox = new System.Windows.Forms.TextBox();
            this.goButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.pingClientsRadio = new System.Windows.Forms.RadioButton();
            this.rebootClientsRadio = new System.Windows.Forms.RadioButton();
            this.checkClientsRadio = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.checkProcessRadio = new System.Windows.Forms.RadioButton();
            this.startProcessRadio = new System.Windows.Forms.RadioButton();
            this.logOffRadio = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // inputBox
            // 
            this.inputBox.Location = new System.Drawing.Point(12, 39);
            this.inputBox.Multiline = true;
            this.inputBox.Name = "inputBox";
            this.inputBox.Size = new System.Drawing.Size(256, 132);
            this.inputBox.TabIndex = 0;
            // 
            // goButton
            // 
            this.goButton.Location = new System.Drawing.Point(15, 352);
            this.goButton.Name = "goButton";
            this.goButton.Size = new System.Drawing.Size(256, 36);
            this.goButton.TabIndex = 1;
            this.goButton.Text = "Go";
            this.goButton.UseVisualStyleBackColor = true;
            this.goButton.Click += new System.EventHandler(this.goButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(220, 26);
            this.label1.TabIndex = 2;
            this.label1.Text = "Please type DNS name or IP addresses, one \r\non each line";
            // 
            // pingClientsRadio
            // 
            this.pingClientsRadio.AutoSize = true;
            this.pingClientsRadio.Location = new System.Drawing.Point(15, 178);
            this.pingClientsRadio.Name = "pingClientsRadio";
            this.pingClientsRadio.Size = new System.Drawing.Size(79, 17);
            this.pingClientsRadio.TabIndex = 3;
            this.pingClientsRadio.TabStop = true;
            this.pingClientsRadio.Text = "Ping clients";
            this.pingClientsRadio.UseVisualStyleBackColor = true;
            // 
            // rebootClientsRadio
            // 
            this.rebootClientsRadio.AutoSize = true;
            this.rebootClientsRadio.Location = new System.Drawing.Point(15, 201);
            this.rebootClientsRadio.Name = "rebootClientsRadio";
            this.rebootClientsRadio.Size = new System.Drawing.Size(93, 17);
            this.rebootClientsRadio.TabIndex = 3;
            this.rebootClientsRadio.TabStop = true;
            this.rebootClientsRadio.Text = "Reboot clients";
            this.rebootClientsRadio.UseVisualStyleBackColor = true;
            // 
            // checkClientsRadio
            // 
            this.checkClientsRadio.AutoSize = true;
            this.checkClientsRadio.Location = new System.Drawing.Point(15, 224);
            this.checkClientsRadio.Name = "checkClientsRadio";
            this.checkClientsRadio.Size = new System.Drawing.Size(162, 17);
            this.checkClientsRadio.TabIndex = 3;
            this.checkClientsRadio.TabStop = true;
            this.checkClientsRadio.Text = "Check for directory on clients";
            this.checkClientsRadio.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(166, 391);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "v4 (alpha) - RP 2016";
            // 
            // checkProcessRadio
            // 
            this.checkProcessRadio.AutoSize = true;
            this.checkProcessRadio.Location = new System.Drawing.Point(15, 247);
            this.checkProcessRadio.Name = "checkProcessRadio";
            this.checkProcessRadio.Size = new System.Drawing.Size(159, 17);
            this.checkProcessRadio.TabIndex = 3;
            this.checkProcessRadio.TabStop = true;
            this.checkProcessRadio.Text = "Check for process on clients";
            this.checkProcessRadio.UseVisualStyleBackColor = true;
            // 
            // startProcessRadio
            // 
            this.startProcessRadio.AutoSize = true;
            this.startProcessRadio.Location = new System.Drawing.Point(15, 270);
            this.startProcessRadio.Name = "startProcessRadio";
            this.startProcessRadio.Size = new System.Drawing.Size(135, 17);
            this.startProcessRadio.TabIndex = 5;
            this.startProcessRadio.TabStop = true;
            this.startProcessRadio.Text = "Start process on clients";
            this.startProcessRadio.UseVisualStyleBackColor = true;
            // 
            // logOffRadio
            // 
            this.logOffRadio.AutoSize = true;
            this.logOffRadio.Location = new System.Drawing.Point(15, 293);
            this.logOffRadio.Name = "logOffRadio";
            this.logOffRadio.Size = new System.Drawing.Size(209, 17);
            this.logOffRadio.TabIndex = 6;
            this.logOffRadio.TabStop = true;
            this.logOffRadio.Text = "Log off any console sessions on clients";
            this.logOffRadio.UseVisualStyleBackColor = true;
            // 
            // Setup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(280, 413);
            this.Controls.Add(this.logOffRadio);
            this.Controls.Add(this.startProcessRadio);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.checkProcessRadio);
            this.Controls.Add(this.checkClientsRadio);
            this.Controls.Add(this.rebootClientsRadio);
            this.Controls.Add(this.pingClientsRadio);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.goButton);
            this.Controls.Add(this.inputBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Setup";
            this.Text = "UpChecker";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox inputBox;
        private System.Windows.Forms.Button goButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton pingClientsRadio;
        private System.Windows.Forms.RadioButton rebootClientsRadio;
        private System.Windows.Forms.RadioButton checkClientsRadio;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton checkProcessRadio;
        private System.Windows.Forms.RadioButton startProcessRadio;
        private System.Windows.Forms.RadioButton logOffRadio;
    }
}

