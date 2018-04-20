namespace CS408_PROJ_STEP1_SERVERSIDE
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
            this.portInputBox = new System.Windows.Forms.TextBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.listenButton = new System.Windows.Forms.Button();
            this.disconnectButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // portInputBox
            // 
            this.portInputBox.Location = new System.Drawing.Point(49, 80);
            this.portInputBox.Name = "portInputBox";
            this.portInputBox.Size = new System.Drawing.Size(338, 38);
            this.portInputBox.TabIndex = 0;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(49, 147);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(721, 357);
            this.richTextBox1.TabIndex = 1;
            this.richTextBox1.Text = "";
            // 
            // listenButton
            // 
            this.listenButton.Location = new System.Drawing.Point(423, 38);
            this.listenButton.Name = "listenButton";
            this.listenButton.Size = new System.Drawing.Size(347, 80);
            this.listenButton.TabIndex = 2;
            this.listenButton.Text = "LISTEN";
            this.listenButton.UseVisualStyleBackColor = true;
            this.listenButton.Click += new System.EventHandler(this.listenButton_Click);
            // 
            // disconnectButton
            // 
            this.disconnectButton.Location = new System.Drawing.Point(49, 528);
            this.disconnectButton.Name = "disconnectButton";
            this.disconnectButton.Size = new System.Drawing.Size(721, 100);
            this.disconnectButton.TabIndex = 3;
            this.disconnectButton.Text = "DISCONNECT";
            this.disconnectButton.UseVisualStyleBackColor = true;
            this.disconnectButton.Click += new System.EventHandler(this.disconnectButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(81, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(271, 32);
            this.label1.TabIndex = 4;
            this.label1.Text = "Enter Port Number : ";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(806, 676);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.disconnectButton);
            this.Controls.Add(this.listenButton);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.portInputBox);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox portInputBox;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button listenButton;
        private System.Windows.Forms.Button disconnectButton;
        private System.Windows.Forms.Label label1;

        //this.portInputBox.AutoSize = false;
        //this.portInputBox.Size = new System.Drawing.Size(142, 27);
    }
}

