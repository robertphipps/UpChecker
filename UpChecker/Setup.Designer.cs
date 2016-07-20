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
            this.goButton.Location = new System.Drawing.Point(15, 243);
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
            // Setup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(280, 291);
            this.Controls.Add(this.rebootClientsRadio);
            this.Controls.Add(this.pingClientsRadio);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.goButton);
            this.Controls.Add(this.inputBox);
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
    }
}

