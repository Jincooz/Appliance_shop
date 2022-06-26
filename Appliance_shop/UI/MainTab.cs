using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//TODO: Delete this
using MySql.Data.MySqlClient;

namespace WindowsFormsApp1
{
    public partial class Appliances : Form
    {
        private int _page = 1;
        private Table _table;
        public Table Table { get => _table; set => _table = value; }
        public int Page { get => _page; set => _page = value; }
        public Appliances()
        {
            InitializeComponent();
            ApplianceToolStripMenuItem_Click(this, new EventArgs());
            InitializeForm();
            InitializeByRole();
        }
        public Appliances(Table t)
        {
            InitializeComponent();
            Table = t;
            InitializeForm();
            InitializeByRole();
        }
        private void InitializePagePanel()
        {
            previousPageButton.Visible = true;
            nextPageButton.Visible = true;
            if (Page == 1) previousPageButton.Visible = false;
            if (Page >= Table.Size) nextPageButton.Visible = false;
            pageLabel.Text = (Page).ToString();
        }
        private void InitializeForm()
        {
            TableViewInitialize(Table);
            FilterThingsInitialize(Table.Filters);
        }
        private void FilterButton_Click(object sender, EventArgs e)
        {
            filterPanel.Visible = true;
        }
        private void FilterEndButton_Click(object sender, EventArgs e)
        {
            FilterData();
            filterPanel.Visible = false;
        }
        private void FilterData()
        {
            Page = 1;
            TableViewInitialize(Table);
        }
        private void FilterThingsInitialize(IEnumerable<Filter> filters)
        {
            this.Controls.RemoveByKey("filterStartButton");
            filterPanel.Controls.Clear();
            filterPanel.Visible = false;
            if (filters.Count() == 0) return;
            Button filterStartButton = new Button
            {
                Location = new System.Drawing.Point(8, 24),
                Name = "filterStartButton",
                Size = new System.Drawing.Size(75, 23),
                TabIndex = 5,
                Text = "Filter",
                UseVisualStyleBackColor = true
            };
            filterStartButton.Click += new System.EventHandler(this.FilterButton_Click);
            this.Controls.Add(filterStartButton);
            Point pos = new Point(9, 8);
            foreach (var filter in filters)
            {
                filter.Initialize(ref pos, ref filterPanel);
            }
            Button filterEndButton = new Button
            {
                Dock = DockStyle.Bottom,
                Name = "filterEndButton",
                TabIndex = 4,
                Text = "Filter",
                UseVisualStyleBackColor = true
            };
            filterEndButton.Click += new System.EventHandler(FilterEndButton_Click);
            filterPanel.Controls.Add(filterEndButton);
        }
        private void TableViewInitialize(Table table)
        {
            Table.GetDataFromRepository(Page-1);
            InitializePagePanel();
            TableView.Columns.Clear();
            findByComboBox.Items.Clear();
            for (int i = 0; i < table.ColumnsNames.Count; i++)
            {
                TableView.Columns.Add(table.ColumnsNames[i], table.ColumnsNames[i]);
            }
            TableView.Columns.Add("Hiden column", "Hiden column");
            TableView.Columns["Hiden column"].Visible = false;
            foreach (var name in table.ColumnsNames)
            {
                findByComboBox.Items.Add(name);
            }
            for (int i = 0; i < table.Count; i++)
            {
                TableView.Rows.Add();
                TableView["Hiden column", i].Value = (i);
                for (int j = 0; j < table.ColumnsNames.Count; j++)
                {
                    TableView[j, i].Value = table[i, j];
                }
            }
        }
        private void InitializeByRole()
        {
            menuStrip.Items.Clear();
            if(ActiveUser.Instance.Rights.ShowAdminToolStripMenuItem)
            {
                ToolStripMenuItem adminToolStripMenuItem = new ToolStripMenuItem
                {
                    Name = "adminToolStripMenuItem",
                    Size = new System.Drawing.Size(67, 24),
                    Text = "Admin"
                };
                menuStrip.Items.Add(adminToolStripMenuItem);
                if (ActiveUser.Instance.Rights.ShowGetFinancialInfoToolStripMenuItem)
                {
                    ToolStripMenuItem getFinancialInfoToolStripMenuItem = new ToolStripMenuItem
                    {
                        Name = "getFinancialInfoToolStripMenuItem",
                        Size = new System.Drawing.Size(224, 26),
                        Text = "Get financial info"
                    };
                    getFinancialInfoToolStripMenuItem.Click += new System.EventHandler(this.GetFinancialInfoToolStripMenuItem_Click);
                    adminToolStripMenuItem.DropDownItems.Add(getFinancialInfoToolStripMenuItem);
                }
                if(ActiveUser.Instance.Rights.ShowSeeUserListToolStripMenuItem)
                {
                    ToolStripMenuItem seeUserListToolStripMenuItem = new ToolStripMenuItem
                    {
                        Name = "seeUserListToolStripMenuItem",
                        Size = new System.Drawing.Size(224, 26),
                        Text = "See user list"
                    };
                    seeUserListToolStripMenuItem.Click += new System.EventHandler(this.SeeUserListToolStripMenuItem_Click);
                    adminToolStripMenuItem.DropDownItems.Add(seeUserListToolStripMenuItem);
                }
                if(ActiveUser.Instance.Rights.ShowAddSuplyToolStripMenuItem)
                {
                    ToolStripMenuItem addSuplyToolStripMenuItem = new ToolStripMenuItem
                    {
                        Name = "addSuplyToolStripMenuItem",
                        Size = new System.Drawing.Size(224, 26),
                        Text = "Add suply"
                    };
                    addSuplyToolStripMenuItem.Click += new System.EventHandler(this.AddSuplyToolStripMenuItem_Click);
                    adminToolStripMenuItem.DropDownItems.Add(addSuplyToolStripMenuItem);
                }
            }
            if(ActiveUser.Instance.Rights.ShowAppliancesToolStripMenuItem)
            {
                ToolStripMenuItem appliancesToolStripMenuItem = new ToolStripMenuItem
                {
                    Name = "applianceToolStripMenuItem",
                    Size = new System.Drawing.Size(90, 24),
                    Text = "Appliance"
                };
                appliancesToolStripMenuItem.Click += new System.EventHandler(this.ApplianceToolStripMenuItem_Click);
                this.menuStrip.Items.Add(appliancesToolStripMenuItem);
            }
            if(ActiveUser.Instance.Rights.ShowShopinglistToolStripMenuItem)
            {
                ToolStripMenuItem shopinglistToolStripMenuItem = new ToolStripMenuItem
                {
                    Name = "shopinglistToolStripMenuItem",
                    Size = new System.Drawing.Size(101, 24),
                    Text = "Shoping list"
                };
                shopinglistToolStripMenuItem.Click += new System.EventHandler(this.ShopingListToolStripMenuItem_Click);
                this.menuStrip.Items.Add(shopinglistToolStripMenuItem);
            }
            if(ActiveUser.Instance.Rights.ShowChangeProfileToolStripMenuItem)
            {
                ToolStripMenuItem changeProfileToolStripMenuItem = new ToolStripMenuItem
                {
                    Name = "changeProfileToolStripMenuItem",
                    Size = new System.Drawing.Size(121, 24),
                    Text = "Change profile"
                };
                changeProfileToolStripMenuItem.Click += new System.EventHandler(this.ChangeProfileStripMenuItem_Click);
                this.menuStrip.Items.Add(changeProfileToolStripMenuItem);
            }
            if(ActiveUser.Instance.Rights.ShowLogOutToolStripMenuItem)
            {
                ToolStripMenuItem logOutToolStripMenuItem = new ToolStripMenuItem
                {
                    Name = "logOutToolStripMenuItem",
                    Size = new System.Drawing.Size(74, 24),
                    Text = "Log out"
                };
                logOutToolStripMenuItem.Click += new System.EventHandler(this.LogOutToolStripMenuItem_Click);
                this.menuStrip.Items.Add(logOutToolStripMenuItem);
            }
            if(ActiveUser.Instance.Rights.ShowLogInToolStripMenuItem)
            {
                ToolStripMenuItem logInToolStripMenuItem = new ToolStripMenuItem
                {
                    Name = "logInToolStripMenuItem",
                    Size = new System.Drawing.Size(64, 24),
                    Text = "Log in"
                };
                logInToolStripMenuItem.Click += new System.EventHandler(this.LogInToolStripMenuItem_Click);
                this.menuStrip.Items.Add(logInToolStripMenuItem);
            }
            if(ActiveUser.Instance.Rights.ShowRegisterToolStripMenuItem)
            {
                ToolStripMenuItem registerToolStripMenuItem = new ToolStripMenuItem
                {
                    Name = "registerToolStripMenuItem",
                    Size = new System.Drawing.Size(77, 24),
                    Text = "Register"
                };
                registerToolStripMenuItem.Click += new System.EventHandler(this.RegisterToolStripMenuItem_Click);
                this.menuStrip.Items.Add(registerToolStripMenuItem);
            }
        }
        private void ShopingListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Table = new ShopingListTable();
            InitializeForm();
            approvalButton.Visible = true;
            approvalButton.Text = "Buy";
            if(ActiveUser.Instance.User.ShopingList.Size == 0)
            {
                approvalButton.Enabled = false;
            }
            else
            {
                approvalButton.Enabled = true;
            }
            pagePanel.Visible = false;
            for (int i = 0; i < TableView.Columns.Count; i++)
            {
                TableView.Columns[i].SortMode = DataGridViewColumnSortMode.Automatic;
            }
            TableView.ColumnHeaderMouseDoubleClick -= new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.TableView_ColumnHeaderMouseDoubleClick);

        }
        private void ApplianceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Table = new ApplianceTable();
            approvalButton.Visible = false;
            InitializeForm();
            pagePanel.Visible = true;
            for (int i = 0; i < TableView.Columns.Count; i++)
            {
                TableView.Columns[i].SortMode = DataGridViewColumnSortMode.Programmatic;
            }
            TableView.ColumnHeaderMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.TableView_ColumnHeaderMouseDoubleClick);
        }
        private void GetFinancialInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UI.FinanciaInfo financiaInfo = new UI.FinanciaInfo();
            this.Visible = false;
            financiaInfo.ShowDialog(this);
            this.Visible = true;
        }
        private void SeeUserListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Table = new UserTable(); 
            approvalButton.Visible = false;
            pagePanel.Visible = true;
            InitializeForm();
            for (int i = 0; i < TableView.Columns.Count; i++)
            {
                TableView.Columns[i].SortMode = DataGridViewColumnSortMode.Programmatic;
            }
            TableView.ColumnHeaderMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.TableView_ColumnHeaderMouseDoubleClick);
        }
        private void AddSuplyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UI.SupplyOrder supplyOrder = new UI.SupplyOrder();
            this.Visible = false;
            supplyOrder.ShowDialog(this);
            ApplianceToolStripMenuItem_Click(this,new EventArgs());
            InitializeByRole();
            InitializeForm();
            this.Visible = true;
        }
        private void LogInToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UI.Log_in logIn = new UI.Log_in();
            this.Visible = false;
            logIn.ShowDialog(this);
            ApplianceToolStripMenuItem_Click(this, new EventArgs());
            InitializeByRole();
            InitializeForm();
            this.Visible = true;
        }
        private void RegisterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UI.Registration registration = new UI.Registration();
            this.Visible = false;
            registration.ShowDialog(this);
            ApplianceToolStripMenuItem_Click(this, new EventArgs());
            InitializeByRole();
            InitializeForm();
            this.Visible = true;
        }
        private void ChangeProfileStripMenuItem_Click(object sender, EventArgs e)
        {
            UI.ChangeProfile changeProfile = new UI.ChangeProfile();
            this.Visible = false;
            changeProfile.ShowDialog(this);
            ApplianceToolStripMenuItem_Click(this, new EventArgs());
            InitializeForm();
            this.Visible = true;

        }
        private void LogOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DB.User user = new DB.User
            {
                RoleName = "guest"
            };
            user.Rights = new DB.Rights(user.RoleName);
            ApplianceToolStripMenuItem_Click(this, new EventArgs());
            ActiveUser.Instance.User = user;
            InitializeByRole();
            InitializeForm();
        }
        private void TableView_ColumnHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            bool desc = false;
            var dirrection = TableView.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection;
            Page = 1;
            if(dirrection == SortOrder.Ascending)
            {
                Table.OrderBy = TableView.Columns[e.ColumnIndex].Name + " desc ";
                desc = true;
            }
            else
            {
                Table.OrderBy = TableView.Columns[e.ColumnIndex].Name;
            }
            TableViewInitialize(Table);
            if(desc)
                TableView.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = SortOrder.Descending;
            else
                TableView.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = SortOrder.Ascending;

        }
        private void TableView_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (!ActiveUser.Instance.Rights.ActiveTableRowHeader) return;
            int index = Convert.ToInt32(TableView["Hiden column", e.RowIndex].Value);
            Table.Repository.TableRowClicked(index);
            InitializeForm();
        }
        private void nextButton_Click(object sender, EventArgs e)
        {
            Page++;
            TableViewInitialize(Table);
        }

        private void previousPageButton_Click(object sender, EventArgs e)
        {
            Page--;
            TableViewInitialize(Table);
        }

        private void approvalButton_Click(object sender, EventArgs e)
        {
            UI.GetCard getCard = new UI.GetCard();
            this.Visible = false;
            getCard.ShowDialog();
            this.Visible = true;
            if (!getCard.Aprove)
                return;
            Table.Repository.ActionButtonClick();
            InitializeForm();
        }
    }
}
