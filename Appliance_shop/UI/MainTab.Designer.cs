
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
            this.approvalButton = new System.Windows.Forms.Button();
            this.pagePanel = new System.Windows.Forms.Panel();
            this.previousPageButton = new System.Windows.Forms.Button();
            this.nextPageButton = new System.Windows.Forms.Button();
            this.pageLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.TableView)).BeginInit();
            this.pagePanel.SuspendLayout();
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
            this.TableView.Size = new System.Drawing.Size(776, 378);
            this.TableView.TabIndex = 0;
            this.TableView.RowHeaderMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.TableView_RowHeaderMouseDoubleClick);
            // 
            // menuStrip
            // 
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(800, 24);
            this.menuStrip.TabIndex = 1;
            this.menuStrip.Text = "menuStrip1";
            // 
            // findByTextBox
            // 
            this.findByTextBox.Location = new System.Drawing.Point(643, 31);
            this.findByTextBox.Name = "findByTextBox";
            this.findByTextBox.Size = new System.Drawing.Size(145, 22);
            this.findByTextBox.TabIndex = 2;
            this.findByTextBox.Visible = false;
            // 
            // findByComboBox
            // 
            this.findByComboBox.FormattingEnabled = true;
            this.findByComboBox.Location = new System.Drawing.Point(556, 31);
            this.findByComboBox.Name = "findByComboBox";
            this.findByComboBox.Size = new System.Drawing.Size(81, 24);
            this.findByComboBox.TabIndex = 3;
            this.findByComboBox.Visible = false;
            // 
            // findByLabel
            // 
            this.findByLabel.AutoSize = true;
            this.findByLabel.Location = new System.Drawing.Point(492, 34);
            this.findByLabel.Name = "findByLabel";
            this.findByLabel.Size = new System.Drawing.Size(58, 17);
            this.findByLabel.TabIndex = 4;
            this.findByLabel.Text = "Find by:";
            this.findByLabel.Visible = false;
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
            // approvalButton
            // 
            this.approvalButton.Location = new System.Drawing.Point(693, 443);
            this.approvalButton.Name = "approvalButton";
            this.approvalButton.Size = new System.Drawing.Size(95, 33);
            this.approvalButton.TabIndex = 7;
            this.approvalButton.Text = "button1";
            this.approvalButton.UseVisualStyleBackColor = true;
            this.approvalButton.Visible = false;
            this.approvalButton.Click += new System.EventHandler(this.approvalButton_Click);
            // 
            // pagePanel
            // 
            this.pagePanel.Controls.Add(this.previousPageButton);
            this.pagePanel.Controls.Add(this.nextPageButton);
            this.pagePanel.Controls.Add(this.pageLabel);
            this.pagePanel.Location = new System.Drawing.Point(291, 443);
            this.pagePanel.Name = "pagePanel";
            this.pagePanel.Size = new System.Drawing.Size(231, 27);
            this.pagePanel.TabIndex = 8;
            // 
            // previousPageButton
            // 
            this.previousPageButton.Location = new System.Drawing.Point(3, 2);
            this.previousPageButton.Name = "previousPageButton";
            this.previousPageButton.Size = new System.Drawing.Size(72, 23);
            this.previousPageButton.TabIndex = 1;
            this.previousPageButton.Text = "Previous";
            this.previousPageButton.UseVisualStyleBackColor = true;
            this.previousPageButton.Click += new System.EventHandler(this.previousPageButton_Click);
            // 
            // nextPageButton
            // 
            this.nextPageButton.Location = new System.Drawing.Point(155, 3);
            this.nextPageButton.Name = "nextPageButton";
            this.nextPageButton.Size = new System.Drawing.Size(73, 23);
            this.nextPageButton.TabIndex = 0;
            this.nextPageButton.Text = "Next";
            this.nextPageButton.UseVisualStyleBackColor = true;
            this.nextPageButton.Click += new System.EventHandler(this.nextButton_Click);
            // 
            // pageLabel
            // 
            this.pageLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pageLabel.Location = new System.Drawing.Point(0, 0);
            this.pageLabel.Name = "pageLabel";
            this.pageLabel.Size = new System.Drawing.Size(231, 27);
            this.pageLabel.TabIndex = 2;
            this.pageLabel.Text = "0";
            this.pageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Appliances
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(800, 477);
            this.Controls.Add(this.pagePanel);
            this.Controls.Add(this.approvalButton);
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
            this.pagePanel.ResumeLayout(false);
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
        private System.Windows.Forms.Button approvalButton;
        private System.Windows.Forms.Panel pagePanel;
        private System.Windows.Forms.Button previousPageButton;
        private System.Windows.Forms.Button nextPageButton;
        private System.Windows.Forms.Label pageLabel;
    }
}

