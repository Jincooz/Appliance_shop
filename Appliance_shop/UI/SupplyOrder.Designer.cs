
namespace WindowsFormsApp1.UI
{
    partial class SupplyOrder
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
            this.TableView = new System.Windows.Forms.DataGridView();
            this.approvalButton = new System.Windows.Forms.Button();
            this.AddButton = new System.Windows.Forms.Button();
            this.AmountTextBox = new System.Windows.Forms.TextBox();
            this.EANMaskedTextBox = new System.Windows.Forms.MaskedTextBox();
            this.EANLabel = new System.Windows.Forms.Label();
            this.AmountLabel = new System.Windows.Forms.Label();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.TitleLabel = new System.Windows.Forms.Label();
            this.CategoryLabel = new System.Windows.Forms.Label();
            this.TrademarkLabel = new System.Windows.Forms.Label();
            this.PriceLabel = new System.Windows.Forms.Label();
            this.GuarantyLabel = new System.Windows.Forms.Label();
            this.TitleTextBox = new System.Windows.Forms.TextBox();
            this.CategoryTextBox = new System.Windows.Forms.TextBox();
            this.TrademarkTextBox = new System.Windows.Forms.TextBox();
            this.PriceTextBox = new System.Windows.Forms.TextBox();
            this.GuarantyTextBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.TableView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
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
            this.TableView.Location = new System.Drawing.Point(271, 12);
            this.TableView.Name = "TableView";
            this.TableView.ReadOnly = true;
            this.TableView.RowHeadersWidth = 51;
            this.TableView.RowTemplate.Height = 24;
            this.TableView.Size = new System.Drawing.Size(549, 378);
            this.TableView.TabIndex = 1;
            // 
            // approvalButton
            // 
            this.approvalButton.Location = new System.Drawing.Point(725, 396);
            this.approvalButton.Name = "approvalButton";
            this.approvalButton.Size = new System.Drawing.Size(95, 29);
            this.approvalButton.TabIndex = 8;
            this.approvalButton.Text = "Commit operation";
            this.approvalButton.UseVisualStyleBackColor = true;
            this.approvalButton.Visible = false;
            this.approvalButton.Click += new System.EventHandler(this.approvalButton_Click);
            // 
            // AddButton
            // 
            this.AddButton.Location = new System.Drawing.Point(77, 221);
            this.AddButton.Name = "AddButton";
            this.AddButton.Size = new System.Drawing.Size(95, 29);
            this.AddButton.TabIndex = 9;
            this.AddButton.Text = "Add";
            this.AddButton.UseVisualStyleBackColor = true;
            this.AddButton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // AmountTextBox
            // 
            this.AmountTextBox.Location = new System.Drawing.Point(99, 40);
            this.AmountTextBox.Name = "AmountTextBox";
            this.AmountTextBox.Size = new System.Drawing.Size(136, 22);
            this.AmountTextBox.TabIndex = 10;
            this.AmountTextBox.TextChanged += new System.EventHandler(this.AmountTextBox_TextChanged);
            // 
            // EANMaskedTextBox
            // 
            this.EANMaskedTextBox.Location = new System.Drawing.Point(99, 12);
            this.EANMaskedTextBox.Mask = "0/000000/000000";
            this.EANMaskedTextBox.Name = "EANMaskedTextBox";
            this.EANMaskedTextBox.Size = new System.Drawing.Size(136, 22);
            this.EANMaskedTextBox.TabIndex = 11;
            this.EANMaskedTextBox.TextMaskFormat = System.Windows.Forms.MaskFormat.IncludePrompt;
            // 
            // EANLabel
            // 
            this.EANLabel.AutoSize = true;
            this.EANLabel.Location = new System.Drawing.Point(12, 15);
            this.EANLabel.Name = "EANLabel";
            this.EANLabel.Size = new System.Drawing.Size(40, 17);
            this.EANLabel.TabIndex = 12;
            this.EANLabel.Text = "EAN:";
            // 
            // AmountLabel
            // 
            this.AmountLabel.AutoSize = true;
            this.AmountLabel.Location = new System.Drawing.Point(12, 43);
            this.AmountLabel.Name = "AmountLabel";
            this.AmountLabel.Size = new System.Drawing.Size(60, 17);
            this.AmountLabel.TabIndex = 13;
            this.AmountLabel.Text = "Amount:";
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // TitleLabel
            // 
            this.TitleLabel.AutoSize = true;
            this.TitleLabel.Location = new System.Drawing.Point(12, 71);
            this.TitleLabel.Name = "TitleLabel";
            this.TitleLabel.Size = new System.Drawing.Size(39, 17);
            this.TitleLabel.TabIndex = 14;
            this.TitleLabel.Text = "Title:";
            this.TitleLabel.Visible = false;
            // 
            // CategoryLabel
            // 
            this.CategoryLabel.AutoSize = true;
            this.CategoryLabel.Location = new System.Drawing.Point(12, 99);
            this.CategoryLabel.Name = "CategoryLabel";
            this.CategoryLabel.Size = new System.Drawing.Size(69, 17);
            this.CategoryLabel.TabIndex = 15;
            this.CategoryLabel.Text = "Category:";
            this.CategoryLabel.Visible = false;
            // 
            // TrademarkLabel
            // 
            this.TrademarkLabel.AutoSize = true;
            this.TrademarkLabel.Location = new System.Drawing.Point(12, 127);
            this.TrademarkLabel.Name = "TrademarkLabel";
            this.TrademarkLabel.Size = new System.Drawing.Size(81, 17);
            this.TrademarkLabel.TabIndex = 16;
            this.TrademarkLabel.Text = "Trademark:";
            this.TrademarkLabel.Visible = false;
            // 
            // PriceLabel
            // 
            this.PriceLabel.AutoSize = true;
            this.PriceLabel.Location = new System.Drawing.Point(12, 156);
            this.PriceLabel.Name = "PriceLabel";
            this.PriceLabel.Size = new System.Drawing.Size(82, 17);
            this.PriceLabel.TabIndex = 17;
            this.PriceLabel.Text = "Suply price:";
            this.PriceLabel.Visible = false;
            // 
            // GuarantyLabel
            // 
            this.GuarantyLabel.AutoSize = true;
            this.GuarantyLabel.Location = new System.Drawing.Point(12, 184);
            this.GuarantyLabel.Name = "GuarantyLabel";
            this.GuarantyLabel.Size = new System.Drawing.Size(67, 34);
            this.GuarantyLabel.TabIndex = 18;
            this.GuarantyLabel.Text = "Guaranty\r\ndays:";
            this.GuarantyLabel.Visible = false;
            // 
            // TitleTextBox
            // 
            this.TitleTextBox.Location = new System.Drawing.Point(99, 68);
            this.TitleTextBox.Name = "TitleTextBox";
            this.TitleTextBox.Size = new System.Drawing.Size(136, 22);
            this.TitleTextBox.TabIndex = 20;
            this.TitleTextBox.Visible = false;
            // 
            // CategoryTextBox
            // 
            this.CategoryTextBox.Location = new System.Drawing.Point(99, 96);
            this.CategoryTextBox.Name = "CategoryTextBox";
            this.CategoryTextBox.Size = new System.Drawing.Size(136, 22);
            this.CategoryTextBox.TabIndex = 21;
            this.CategoryTextBox.Visible = false;
            // 
            // TrademarkTextBox
            // 
            this.TrademarkTextBox.Location = new System.Drawing.Point(99, 124);
            this.TrademarkTextBox.Name = "TrademarkTextBox";
            this.TrademarkTextBox.Size = new System.Drawing.Size(136, 22);
            this.TrademarkTextBox.TabIndex = 22;
            this.TrademarkTextBox.Visible = false;
            // 
            // PriceTextBox
            // 
            this.PriceTextBox.Location = new System.Drawing.Point(99, 153);
            this.PriceTextBox.Name = "PriceTextBox";
            this.PriceTextBox.Size = new System.Drawing.Size(136, 22);
            this.PriceTextBox.TabIndex = 23;
            this.PriceTextBox.Visible = false;
            this.PriceTextBox.TextChanged += new System.EventHandler(this.PriceTextBox_TextChanged);
            // 
            // GuarantyTextBox
            // 
            this.GuarantyTextBox.Location = new System.Drawing.Point(99, 191);
            this.GuarantyTextBox.Name = "GuarantyTextBox";
            this.GuarantyTextBox.Size = new System.Drawing.Size(136, 22);
            this.GuarantyTextBox.TabIndex = 24;
            this.GuarantyTextBox.Visible = false;
            this.GuarantyTextBox.TextChanged += new System.EventHandler(this.GuarantyTextBox_TextChanged);
            // 
            // SupplyOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(832, 437);
            this.Controls.Add(this.GuarantyTextBox);
            this.Controls.Add(this.PriceTextBox);
            this.Controls.Add(this.TrademarkTextBox);
            this.Controls.Add(this.CategoryTextBox);
            this.Controls.Add(this.TitleTextBox);
            this.Controls.Add(this.GuarantyLabel);
            this.Controls.Add(this.PriceLabel);
            this.Controls.Add(this.TrademarkLabel);
            this.Controls.Add(this.CategoryLabel);
            this.Controls.Add(this.TitleLabel);
            this.Controls.Add(this.AmountLabel);
            this.Controls.Add(this.EANLabel);
            this.Controls.Add(this.EANMaskedTextBox);
            this.Controls.Add(this.AmountTextBox);
            this.Controls.Add(this.AddButton);
            this.Controls.Add(this.approvalButton);
            this.Controls.Add(this.TableView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "SupplyOrder";
            this.Text = "SupplyOrder";
            ((System.ComponentModel.ISupportInitialize)(this.TableView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView TableView;
        private System.Windows.Forms.Button approvalButton;
        private System.Windows.Forms.Button AddButton;
        private System.Windows.Forms.TextBox AmountTextBox;
        private System.Windows.Forms.MaskedTextBox EANMaskedTextBox;
        private System.Windows.Forms.Label EANLabel;
        private System.Windows.Forms.Label AmountLabel;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.TextBox GuarantyTextBox;
        private System.Windows.Forms.TextBox PriceTextBox;
        private System.Windows.Forms.TextBox TrademarkTextBox;
        private System.Windows.Forms.TextBox CategoryTextBox;
        private System.Windows.Forms.TextBox TitleTextBox;
        private System.Windows.Forms.Label GuarantyLabel;
        private System.Windows.Forms.Label PriceLabel;
        private System.Windows.Forms.Label TrademarkLabel;
        private System.Windows.Forms.Label CategoryLabel;
        private System.Windows.Forms.Label TitleLabel;
    }
}