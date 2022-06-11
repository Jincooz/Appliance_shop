
namespace WindowsFormsApp1.UI
{
    partial class ChangeUserRole
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
            System.Windows.Forms.Button banButtun;
            System.Windows.Forms.Button aproveButton;
            System.Windows.Forms.Label rowLabel;
            this.roleComboBox = new System.Windows.Forms.ComboBox();
            banButtun = new System.Windows.Forms.Button();
            aproveButton = new System.Windows.Forms.Button();
            rowLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // banButtun
            // 
            banButtun.Location = new System.Drawing.Point(15, 37);
            banButtun.Name = "banButtun";
            banButtun.Size = new System.Drawing.Size(75, 23);
            banButtun.TabIndex = 0;
            banButtun.Text = "Ban";
            banButtun.UseVisualStyleBackColor = true;
            banButtun.Click += new System.EventHandler(this.banButtun_Click);
            // 
            // aproveButton
            // 
            aproveButton.Location = new System.Drawing.Point(109, 36);
            aproveButton.Name = "aproveButton";
            aproveButton.Size = new System.Drawing.Size(74, 25);
            aproveButton.TabIndex = 1;
            aproveButton.Text = "Aprove";
            aproveButton.UseVisualStyleBackColor = true;
            aproveButton.Click += new System.EventHandler(this.aproveButton_Click);
            // 
            // rowLabel
            // 
            rowLabel.AutoSize = true;
            rowLabel.Location = new System.Drawing.Point(12, 12);
            rowLabel.Name = "rowLabel";
            rowLabel.Size = new System.Drawing.Size(67, 17);
            rowLabel.TabIndex = 3;
            rowLabel.Text = "New role:";
            // 
            // roleComboBox
            // 
            this.roleComboBox.FormattingEnabled = true;
            this.roleComboBox.Location = new System.Drawing.Point(85, 9);
            this.roleComboBox.Name = "roleComboBox";
            this.roleComboBox.Size = new System.Drawing.Size(100, 24);
            this.roleComboBox.TabIndex = 4;
            // 
            // ChangeUserRole
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(195, 69);
            this.Controls.Add(this.roleComboBox);
            this.Controls.Add(rowLabel);
            this.Controls.Add(aproveButton);
            this.Controls.Add(banButtun);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "ChangeUserRole";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox roleComboBox;
    }
}