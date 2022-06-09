
namespace WindowsFormsApp1.UI
{
    partial class Log_in
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
            System.Windows.Forms.Label loginLabel;
            System.Windows.Forms.Label passwordLine;
            System.Windows.Forms.Button button1;
            System.Windows.Forms.LinkLabel linkLabel1;
            this.loginTextBox = new System.Windows.Forms.TextBox();
            this.passwordTextBox = new System.Windows.Forms.TextBox();
            loginLabel = new System.Windows.Forms.Label();
            passwordLine = new System.Windows.Forms.Label();
            button1 = new System.Windows.Forms.Button();
            linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // loginLabel
            // 
            loginLabel.AutoSize = true;
            loginLabel.Location = new System.Drawing.Point(7, 15);
            loginLabel.Name = "loginLabel";
            loginLabel.Size = new System.Drawing.Size(47, 17);
            loginLabel.TabIndex = 0;
            loginLabel.Text = "Login:";
            // 
            // passwordLine
            // 
            passwordLine.AutoSize = true;
            passwordLine.Location = new System.Drawing.Point(7, 41);
            passwordLine.Name = "passwordLine";
            passwordLine.Size = new System.Drawing.Size(73, 17);
            passwordLine.TabIndex = 2;
            passwordLine.Text = "Password:";
            // 
            // button1
            // 
            button1.Location = new System.Drawing.Point(12, 66);
            button1.Name = "button1";
            button1.Size = new System.Drawing.Size(75, 31);
            button1.TabIndex = 4;
            button1.Text = "Log in";
            button1.UseVisualStyleBackColor = true;
            button1.Click += new System.EventHandler(this.logInButton_Click);
            // 
            // linkLabel1
            // 
            linkLabel1.AutoSize = true;
            linkLabel1.Location = new System.Drawing.Point(93, 73);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new System.Drawing.Size(113, 17);
            linkLabel1.TabIndex = 5;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "Forgot password";
            // 
            // loginTextBox
            // 
            this.loginTextBox.Location = new System.Drawing.Point(86, 10);
            this.loginTextBox.Name = "loginTextBox";
            this.loginTextBox.Size = new System.Drawing.Size(100, 22);
            this.loginTextBox.TabIndex = 1;
            // 
            // passwordTextBox
            // 
            this.passwordTextBox.Location = new System.Drawing.Point(86, 38);
            this.passwordTextBox.Name = "passwordTextBox";
            this.passwordTextBox.PasswordChar = '*';
            this.passwordTextBox.Size = new System.Drawing.Size(100, 22);
            this.passwordTextBox.TabIndex = 3;
            // 
            // Log_in
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(214, 107);
            this.Controls.Add(linkLabel1);
            this.Controls.Add(button1);
            this.Controls.Add(this.passwordTextBox);
            this.Controls.Add(passwordLine);
            this.Controls.Add(this.loginTextBox);
            this.Controls.Add(loginLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Log_in";
            this.Text = "Log in";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox loginTextBox;
        private System.Windows.Forms.TextBox passwordTextBox;
    }
}