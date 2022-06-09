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
        private int _page = 0;
        private Table _table;
        public Table Table { get => _table; set => _table = value; }
        public Appliances()
        {
            InitializeComponent();
            Table = new ApplianceTable();
            InitializeForm();
        }
        public Appliances(Table t)
        {
            InitializeComponent();
            Table = t;
            InitializeForm();
        }
        private void InitializeForm()
        {
            Table.GetDataFromRepository(_page);
            tableViewInitialize(Table);
            filterThingsInitialize(Table.Filters);
            InitializeByRole();
        }
        private void filterButton_Click(object sender, EventArgs e)
        {
            filterPanel.Visible = true;
        }

        private void filterEndButton_Click(object sender, EventArgs e)
        {
            filterData();
            filterPanel.Visible = false;
        }

        private void filterData()
        {
            _page = 0;
            Table.GetDataFromRepository(_page);
            tableViewInitialize(Table);
        }
        private void filterThingsInitialize(IEnumerable<Filter> filters)
        {
            this.Controls.RemoveByKey("filterStartButton");
            filterPanel.Controls.Clear();
            filterPanel.Visible = false;
            if (filters.Count() == 0) return;
            Button filterStartButton = new Button();
            filterStartButton.Location = new System.Drawing.Point(8, 24);
            filterStartButton.Name = "filterStartButton";
            filterStartButton.Size = new System.Drawing.Size(75, 23);
            filterStartButton.TabIndex = 5;
            filterStartButton.Text = "Filter";
            filterStartButton.UseVisualStyleBackColor = true;
            filterStartButton.Click += new System.EventHandler(this.filterButton_Click);
            this.Controls.Add(filterStartButton);
            Point pos = new Point(9, 8);
            foreach (var filter in filters)
            {
                filter.Initialize(ref pos, ref filterPanel);
            }
            Button filterEndButton = new Button();
            filterEndButton.Dock = DockStyle.Bottom;
            filterEndButton.Name = "filterEndButton";
            filterEndButton.TabIndex = 4;
            filterEndButton.Text = "Filter";
            filterEndButton.UseVisualStyleBackColor = true;
            filterEndButton.Click += new System.EventHandler(filterEndButton_Click);
            filterPanel.Controls.Add(filterEndButton);
        }
        private void tableViewInitialize(Table table)
        {
            TableView.Columns.Clear();
            findByComboBox.Items.Clear();
            for (int i = 0; i < table.ColumnsNames.Count; i++)
            {
                TableView.Columns.Add(table.ColumnsNames[i], table.ColumnsNames[i]);
                TableView.Columns[i].SortMode = DataGridViewColumnSortMode.Programmatic;
            }
            foreach (var name in table.ColumnsNames)
            {
                findByComboBox.Items.Add(name);
            }
            for (int i = 0; i < table.Count; i++)
            {
                TableView.Rows.Add();
                for (int j = 0; j < table.ColumnsNames.Count; j++)
                {
                    TableView[j, i].Value = table[i, j];
                }
            }
        }
        private void InitializeByRole()
        {
            menuStrip.Items.Clear();
            switch (ActiveUser.Instance.Role)
            {
                case "Guest":
                    {
                        InitializeAsGuest();
                        break;
                    }
                case "User":
                    {
                        InitializeAsUser();
                        break;
                    }
                case "Admin":
                    {
                        InitializeAsAdmin();
                        break;
                    }
            }
        }
        private void InitializeAsGuest()
        {
            ToolStripMenuItem appliancesToolStripMenuItem = new ToolStripMenuItem();
            ToolStripMenuItem logInToolStripMenuItem = new ToolStripMenuItem();
            ToolStripMenuItem registerToolStripMenuItem = new ToolStripMenuItem();
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            appliancesToolStripMenuItem,
            logInToolStripMenuItem,
            registerToolStripMenuItem});
            //
            // appliancesToolStripMenuItem
            //
            appliancesToolStripMenuItem.Name = "appliancesToolStripMenuItem";
            appliancesToolStripMenuItem.Size = new System.Drawing.Size(96, 24);
            appliancesToolStripMenuItem.Text = "Appliances";
            appliancesToolStripMenuItem.Click += new System.EventHandler(this.applianceToolStripMenuItem_Click);
            //
            // logInToolStripMenuItem
            //
            logInToolStripMenuItem.Name = "logInToolStripMenuItem";
            logInToolStripMenuItem.Size = new System.Drawing.Size(64, 24);
            logInToolStripMenuItem.Text = "Log in";
            logInToolStripMenuItem.Click += new System.EventHandler(this.logInToolStripMenuItem_Click);
            //
            // registerToolStripMenuItem
            //
            registerToolStripMenuItem.Name = "registerToolStripMenuItem";
            registerToolStripMenuItem.Size = new System.Drawing.Size(77, 24);
            registerToolStripMenuItem.Text = "Register";
            registerToolStripMenuItem.Click += new System.EventHandler(this.registerToolStripMenuItem_Click);
        }
        private void InitializeAsUser()
        {
            ToolStripMenuItem appliancesToolStripMenuItem = new ToolStripMenuItem();
            ToolStripMenuItem shopinglistToolStripMenuItem = new ToolStripMenuItem();
            ToolStripMenuItem changeProfileToolStripMenuItem = new ToolStripMenuItem();
            ToolStripMenuItem logOutToolStripMenuItem = new ToolStripMenuItem();
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            appliancesToolStripMenuItem,
            shopinglistToolStripMenuItem,
            changeProfileToolStripMenuItem,
            logOutToolStripMenuItem});
            //
            // appliancesToolStripMenuItem
            //
            appliancesToolStripMenuItem.Name = "applianceToolStripMenuItem";
            appliancesToolStripMenuItem.Size = new System.Drawing.Size(90, 24);
            appliancesToolStripMenuItem.Text = "Appliance";
            appliancesToolStripMenuItem.Click += new System.EventHandler(this.applianceToolStripMenuItem_Click);
            //
            // shopinglistToolStripMenuItem
            //
            shopinglistToolStripMenuItem.Name = "shopinglistToolStripMenuItem";
            shopinglistToolStripMenuItem.Size = new System.Drawing.Size(101, 24);
            shopinglistToolStripMenuItem.Text = "Shoping list";
            shopinglistToolStripMenuItem.Click += new System.EventHandler(this.shopingListToolStripMenuItem_Click);
            //
            // changeProfileToolStripMenuItem
            //
            changeProfileToolStripMenuItem.Name = "changeProfileToolStripMenuItem";
            changeProfileToolStripMenuItem.Size = new System.Drawing.Size(121, 24);
            changeProfileToolStripMenuItem.Text = "Change profile";
            changeProfileToolStripMenuItem.Click += new System.EventHandler(this.changeProfileStripMenuItem_Click);
            //
            // logOutToolStripMenuItem
            //
            logOutToolStripMenuItem.Name = "logOutToolStripMenuItem";
            logOutToolStripMenuItem.Size = new System.Drawing.Size(74, 24);
            logOutToolStripMenuItem.Text = "Log out";
            logOutToolStripMenuItem.Click += new System.EventHandler(this.logOutToolStripMenuItem_Click);

        }
        private void InitializeAsAdmin()
        {
            ToolStripMenuItem adminToolStripMenuItem = new ToolStripMenuItem();
            ToolStripMenuItem getFinancialInfoToolStripMenuItem = new ToolStripMenuItem();
            ToolStripMenuItem seeUserListToolStripMenuItem = new ToolStripMenuItem();
            ToolStripMenuItem addSuplyToolStripMenuItem = new ToolStripMenuItem();
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            adminToolStripMenuItem});
            // 
            // adminToolStripMenuItem
            // 
            adminToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            getFinancialInfoToolStripMenuItem,
            seeUserListToolStripMenuItem,
            addSuplyToolStripMenuItem});
            adminToolStripMenuItem.Name = "adminToolStripMenuItem";
            adminToolStripMenuItem.Size = new System.Drawing.Size(67, 24);
            adminToolStripMenuItem.Text = "Admin";
            // 
            // getFinancialInfoToolStripMenuItem
            // 
            getFinancialInfoToolStripMenuItem.Name = "getFinancialInfoToolStripMenuItem";
            getFinancialInfoToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            getFinancialInfoToolStripMenuItem.Text = "Get financial info";
            getFinancialInfoToolStripMenuItem.Click += new System.EventHandler(this.getFinancialInfoToolStripMenuItem_Click);
            // 
            // seeUserListToolStripMenuItem
            // 
            seeUserListToolStripMenuItem.Name = "seeUserListToolStripMenuItem";
            seeUserListToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            seeUserListToolStripMenuItem.Text = "See user list";
            seeUserListToolStripMenuItem.Click += new System.EventHandler(this.seeUserListToolStripMenuItem_Click);
            // 
            // addSuplyToolStripMenuItem
            // 
            addSuplyToolStripMenuItem.Name = "addSuplyToolStripMenuItem";
            addSuplyToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            addSuplyToolStripMenuItem.Text = "Add suply";
            addSuplyToolStripMenuItem.Click += new System.EventHandler(this.addSuplyToolStripMenuItem_Click);
            //
            // Call to add user control elements
            //
            InitializeAsUser();
        }
        private void shopingListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Table = new ShopingListTable();
            InitializeForm();
            for (int i = 0; i < TableView.Columns.Count; i++)
            {
                TableView.Columns[i].SortMode = DataGridViewColumnSortMode.Automatic;
            }

        }
        private void applianceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Table = new ApplianceTable();
            InitializeForm();
            TableView.ColumnHeaderMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.TableView_ColumnHeaderMouseDoubleClick);
        }
        private void logInToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UI.Log_in logIn = new UI.Log_in();
            this.Visible = false;
            logIn.ShowDialog(this);
            Table = new ApplianceTable();
            InitializeForm();
            this.Visible = true;
        }
        private void registerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UI.Registration registration = new UI.Registration();
            this.Visible = false;
            registration.ShowDialog(this);
            Table = new ApplianceTable();
            InitializeForm();
            this.Visible = true;
        }
        private void changeProfileStripMenuItem_Click(object sender, EventArgs e)
        {
            UI.ChangeProfile changeProfile = new UI.ChangeProfile();
            this.Visible = false;
            changeProfile.ShowDialog(this);
            Table = new ApplianceTable();
            InitializeForm();
            this.Visible = true;

        }
        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DB.User user = new DB.User();
            user.RoleName = "Guest";
            ActiveUser.Instance.User = user;
            InitializeForm();
        }
        private void getFinancialInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
        private void seeUserListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Table = new UserTable();
            InitializeForm();
            this.TableView.ColumnHeaderMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.TableView_ColumnHeaderMouseDoubleClick);
        }
        private void addSuplyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
        private void TableView_ColumnHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            bool desc = false;
            var dirrection = TableView.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection;
            _page = 0;
            if(dirrection == SortOrder.Ascending)
            {
                Table.OrderBy = TableView.Columns[e.ColumnIndex].Name + " desc ";
                desc = true;
            }
            else
            {
                Table.OrderBy = TableView.Columns[e.ColumnIndex].Name;
            }
            Table.GetDataFromRepository(_page);
            tableViewInitialize(Table);
            if(desc)
                TableView.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = SortOrder.Descending;
            else
                TableView.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = SortOrder.Ascending;

        }
    }
}
