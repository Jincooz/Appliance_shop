
namespace WindowsFormsApp1
{
    partial class Appliances
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.TableView = new System.Windows.Forms.DataGridView();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.findByTextBox = new System.Windows.Forms.TextBox();
            this.findByComboBox = new System.Windows.Forms.ComboBox();
            this.findByLabel = new System.Windows.Forms.Label();
            this.filterPanel = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.TableView)).BeginInit();
            this.SuspendLayout();
            // 
            // TableView
            // 
            this.TableView.AllowUserToAddRows = false;
            this.TableView.AllowUserToDeleteRows = false;
            this.TableView.AllowUserToResizeRows = false;
            this.TableView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.TableView.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.TableView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TableView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.TableView.Location = new System.Drawing.Point(12, 59);
            this.TableView.Name = "TableView";
            this.TableView.ReadOnly = true;
            this.TableView.RowHeadersWidth = 51;
            this.TableView.RowTemplate.Height = 24;
            this.TableView.Size = new System.Drawing.Size(776, 407);
            this.TableView.TabIndex = 0;
            // 
            // menuStrip
            // 
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(800, 30);
            this.menuStrip.TabIndex = 1;
            this.menuStrip.Text = "menuStrip1";
            // 
            // findByTextBox
            // 
            this.findByTextBox.Location = new System.Drawing.Point(643, 31);
            this.findByTextBox.Name = "findByTextBox";
            this.findByTextBox.Size = new System.Drawing.Size(145, 22);
            this.findByTextBox.TabIndex = 2;
            // 
            // findByComboBox
            // 
            this.findByComboBox.FormattingEnabled = true;
            this.findByComboBox.Location = new System.Drawing.Point(556, 31);
            this.findByComboBox.Name = "findByComboBox";
            this.findByComboBox.Size = new System.Drawing.Size(81, 24);
            this.findByComboBox.TabIndex = 3;
            // 
            // findByLabel
            // 
            this.findByLabel.AutoSize = true;
            this.findByLabel.Location = new System.Drawing.Point(492, 34);
            this.findByLabel.Name = "findByLabel";
            this.findByLabel.Size = new System.Drawing.Size(58, 17);
            this.findByLabel.TabIndex = 4;
            this.findByLabel.Text = "Find by:";
            // 
            // filterPanel
            // 
            this.filterPanel.AutoScroll = true;
            this.filterPanel.Location = new System.Drawing.Point(0, 30);
            this.filterPanel.Name = "filterPanel";
            this.filterPanel.Size = new System.Drawing.Size(199, 446);
            this.filterPanel.TabIndex = 6;
            this.filterPanel.Visible = false;
            // 
            // Appliances
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(800, 477);
            this.Controls.Add(this.filterPanel);
            this.Controls.Add(this.findByLabel);
            this.Controls.Add(this.findByComboBox);
            this.Controls.Add(this.findByTextBox);
            this.Controls.Add(this.TableView);
            this.Controls.Add(this.menuStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip;
            this.MaximizeBox = false;
            this.Name = "Appliances";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.TableView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView TableView;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.TextBox findByTextBox;
        private System.Windows.Forms.Panel filterPanel;
        private System.Windows.Forms.ComboBox findByComboBox;
        private System.Windows.Forms.Label findByLabel;
    }
}

