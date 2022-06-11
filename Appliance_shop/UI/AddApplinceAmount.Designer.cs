
namespace WindowsFormsApp1.UI
{
    partial class AddApplinceAmount
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
            this.AproveButton = new System.Windows.Forms.Button();
            this.amountTextBox = new System.Windows.Forms.TextBox();
            this.InfoLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // AproveButton
            // 
            this.AproveButton.Location = new System.Drawing.Point(115, 35);
            this.AproveButton.Name = "AproveButton";
            this.AproveButton.Size = new System.Drawing.Size(65, 25);
            this.AproveButton.TabIndex = 0;
            this.AproveButton.Text = "Aprove";
            this.AproveButton.UseVisualStyleBackColor = true;
            this.AproveButton.Click += new System.EventHandler(this.AproveButton_Click);
            // 
            // amountTextBox
            // 
            this.amountTextBox.Location = new System.Drawing.Point(186, 12);
            this.amountTextBox.Name = "amountTextBox";
            this.amountTextBox.Size = new System.Drawing.Size(100, 22);
            this.amountTextBox.TabIndex = 1;
            this.amountTextBox.TextChanged += new System.EventHandler(this.amountTextBox_TextChanged);
            // 
            // InfoLabel
            // 
            this.InfoLabel.AutoSize = true;
            this.InfoLabel.Location = new System.Drawing.Point(12, 15);
            this.InfoLabel.Name = "InfoLabel";
            this.InfoLabel.Size = new System.Drawing.Size(168, 17);
            this.InfoLabel.TabIndex = 2;
            this.InfoLabel.Text = "How many you want buy?";
            // 
            // AddApplinceAmount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(311, 69);
            this.Controls.Add(this.InfoLabel);
            this.Controls.Add(this.amountTextBox);
            this.Controls.Add(this.AproveButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "AddApplinceAmount";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button AproveButton;
        private System.Windows.Forms.TextBox amountTextBox;
        private System.Windows.Forms.Label InfoLabel;
    }
}