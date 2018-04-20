namespace CS408_PROJSTEP1_CLIENTSIDE2
{
    partial class Form1
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
            this.ipaddressText = new System.Windows.Forms.TextBox();
            this.usernameText = new System.Windows.Forms.TextBox();
            this.portnumberText = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.richTextBox = new System.Windows.Forms.RichTextBox();
            this.connectButton = new System.Windows.Forms.Button();
            this.disconnectButton = new System.Windows.Forms.Button();
            this.listplayersButton = new System.Windows.Forms.Button();
            this.InviteButton = new System.Windows.Forms.Button();
            this.SurrenderButton = new System.Windows.Forms.Button();
            this.RejectButton = new System.Windows.Forms.Button();
            this.AcceptButton = new System.Windows.Forms.Button();
            this.inviteBox = new System.Windows.Forms.TextBox();
            this.guessButton = new System.Windows.Forms.Button();
            this.guessTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // ipaddressText
            // 
            this.ipaddressText.Location = new System.Drawing.Point(12, 147);
            this.ipaddressText.Name = "ipaddressText";
            this.ipaddressText.Size = new System.Drawing.Size(270, 38);
            this.ipaddressText.TabIndex = 0;
            // 
            // usernameText
            // 
            this.usernameText.Location = new System.Drawing.Point(12, 242);
            this.usernameText.Name = "usernameText";
            this.usernameText.Size = new System.Drawing.Size(270, 38);
            this.usernameText.TabIndex = 1;
            // 
            // portnumberText
            // 
            this.portnumberText.Location = new System.Drawing.Point(12, 53);
            this.portnumberText.Name = "portnumberText";
            this.portnumberText.Size = new System.Drawing.Size(270, 38);
            this.portnumberText.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(196, 32);
            this.label1.TabIndex = 3;
            this.label1.Text = "Port Number : ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 104);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(174, 32);
            this.label2.TabIndex = 4;
            this.label2.Text = "IP Address : ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(27, 198);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(167, 32);
            this.label3.TabIndex = 5;
            this.label3.Text = "Username : ";
            // 
            // richTextBox
            // 
            this.richTextBox.Location = new System.Drawing.Point(314, 40);
            this.richTextBox.Name = "richTextBox";
            this.richTextBox.Size = new System.Drawing.Size(590, 514);
            this.richTextBox.TabIndex = 6;
            this.richTextBox.Text = "";
            // 
            // connectButton
            // 
            this.connectButton.Location = new System.Drawing.Point(12, 315);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(270, 67);
            this.connectButton.TabIndex = 7;
            this.connectButton.Text = "Connect";
            this.connectButton.UseVisualStyleBackColor = true;
            this.connectButton.Click += new System.EventHandler(this.connectButton_Click);
            // 
            // disconnectButton
            // 
            this.disconnectButton.Location = new System.Drawing.Point(12, 402);
            this.disconnectButton.Name = "disconnectButton";
            this.disconnectButton.Size = new System.Drawing.Size(270, 67);
            this.disconnectButton.TabIndex = 8;
            this.disconnectButton.Text = "Disconnect";
            this.disconnectButton.UseVisualStyleBackColor = true;
            this.disconnectButton.Click += new System.EventHandler(this.disconnectButton_Click);
            // 
            // listplayersButton
            // 
            this.listplayersButton.Location = new System.Drawing.Point(12, 487);
            this.listplayersButton.Name = "listplayersButton";
            this.listplayersButton.Size = new System.Drawing.Size(271, 67);
            this.listplayersButton.TabIndex = 9;
            this.listplayersButton.Text = "List Player";
            this.listplayersButton.UseVisualStyleBackColor = true;
            this.listplayersButton.Click += new System.EventHandler(this.listplayersButton_Click);
            // 
            // InviteButton
            // 
            this.InviteButton.Location = new System.Drawing.Point(12, 650);
            this.InviteButton.Name = "InviteButton";
            this.InviteButton.Size = new System.Drawing.Size(271, 52);
            this.InviteButton.TabIndex = 10;
            this.InviteButton.Text = "Invite";
            this.InviteButton.UseVisualStyleBackColor = true;
            this.InviteButton.Click += new System.EventHandler(this.InviteButton_Click);
            // 
            // SurrenderButton
            // 
            this.SurrenderButton.Location = new System.Drawing.Point(12, 724);
            this.SurrenderButton.Name = "SurrenderButton";
            this.SurrenderButton.Size = new System.Drawing.Size(270, 57);
            this.SurrenderButton.TabIndex = 11;
            this.SurrenderButton.Text = "Surrender";
            this.SurrenderButton.UseVisualStyleBackColor = true;
            this.SurrenderButton.Click += new System.EventHandler(this.SurrenderButton_Click);
            // 
            // RejectButton
            // 
            this.RejectButton.Location = new System.Drawing.Point(534, 591);
            this.RejectButton.Name = "RejectButton";
            this.RejectButton.Size = new System.Drawing.Size(370, 111);
            this.RejectButton.TabIndex = 12;
            this.RejectButton.Text = "Reject";
            this.RejectButton.UseVisualStyleBackColor = true;
            this.RejectButton.Click += new System.EventHandler(this.RejectButton_Click);
            // 
            // AcceptButton
            // 
            this.AcceptButton.Location = new System.Drawing.Point(328, 591);
            this.AcceptButton.Name = "AcceptButton";
            this.AcceptButton.Size = new System.Drawing.Size(182, 111);
            this.AcceptButton.TabIndex = 13;
            this.AcceptButton.Text = "Accept";
            this.AcceptButton.UseVisualStyleBackColor = true;
            this.AcceptButton.Click += new System.EventHandler(this.AcceptButton_Click);
            // 
            // inviteBox
            // 
            this.inviteBox.Location = new System.Drawing.Point(12, 591);
            this.inviteBox.Name = "inviteBox";
            this.inviteBox.Size = new System.Drawing.Size(271, 38);
            this.inviteBox.TabIndex = 14;
            // 
            // guessButton
            // 
            this.guessButton.Location = new System.Drawing.Point(637, 728);
            this.guessButton.Name = "guessButton";
            this.guessButton.Size = new System.Drawing.Size(267, 53);
            this.guessButton.TabIndex = 15;
            this.guessButton.Text = "Guess";
            this.guessButton.UseVisualStyleBackColor = true;
            this.guessButton.Click += new System.EventHandler(this.guessButton_Click);
            // 
            // guessTextBox
            // 
            this.guessTextBox.Location = new System.Drawing.Point(328, 728);
            this.guessTextBox.Name = "guessTextBox";
            this.guessTextBox.Size = new System.Drawing.Size(278, 38);
            this.guessTextBox.TabIndex = 16;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(938, 804);
            this.Controls.Add(this.guessTextBox);
            this.Controls.Add(this.guessButton);
            this.Controls.Add(this.inviteBox);
            this.Controls.Add(this.AcceptButton);
            this.Controls.Add(this.RejectButton);
            this.Controls.Add(this.SurrenderButton);
            this.Controls.Add(this.InviteButton);
            this.Controls.Add(this.listplayersButton);
            this.Controls.Add(this.disconnectButton);
            this.Controls.Add(this.connectButton);
            this.Controls.Add(this.richTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.portnumberText);
            this.Controls.Add(this.usernameText);
            this.Controls.Add(this.ipaddressText);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox ipaddressText;
        private System.Windows.Forms.TextBox usernameText;
        private System.Windows.Forms.TextBox portnumberText;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RichTextBox richTextBox;
        private System.Windows.Forms.Button connectButton;
        private System.Windows.Forms.Button disconnectButton;
        private System.Windows.Forms.Button listplayersButton;
        private System.Windows.Forms.Button InviteButton;
        private System.Windows.Forms.Button SurrenderButton;
        private System.Windows.Forms.Button RejectButton;
        private System.Windows.Forms.Button AcceptButton;
        private System.Windows.Forms.TextBox inviteBox;
        private System.Windows.Forms.Button guessButton;
        private System.Windows.Forms.TextBox guessTextBox;
    }
}

