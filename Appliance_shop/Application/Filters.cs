using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    interface Filter
    {
        void Initialize(ref Point startPosition, ref Panel panel);
        string GetRequest();
    }

    class CountableFilter : Filter
    {
        private Label _name;
        private List<CheckBox> _dataBoxes;
        private Label Name { get => _name; set => _name = value; }
        private List<CheckBox> DataBoxes { get => _dataBoxes; set => _dataBoxes = value; }
        public CountableFilter()
        {
            _name = new Label();
            _dataBoxes = new List<CheckBox>();
        }
        public CountableFilter(string name, List<string> data) : this()
        {
            Name.AutoSize = true;
            Name.Name = name + "NameLabel";
            Name.TabIndex = 0;
            Name.Text = name;
            for (int i = 0; i < data.Count; i++)
            {
                DataBoxes.Add(new CheckBox());
                DataBoxes[i].AutoSize = true;
                DataBoxes[i].Name = data[i] + "CheckBox";
                DataBoxes[i].TabIndex = 0;
                DataBoxes[i].Text = data[i];
                DataBoxes[i].UseVisualStyleBackColor = true;
            }
        }
        public void SetName(string newName)
        {
            Name.Name = newName + "NameLabel";
            Name.Text = newName;
        }
        public void SetData(List<string> newData)
        {
            DataBoxes.Clear();
            for (int i = 0; i < newData.Count; i++)
            {
                DataBoxes.Add(new CheckBox());
                DataBoxes[i].AutoSize = true;
                DataBoxes[i].Name = newData[i] + "CheckBox";
                DataBoxes[i].TabIndex = 0;
                DataBoxes[i].Text = newData[i];
                DataBoxes[i].UseVisualStyleBackColor = true;
            }
        }
        public void Initialize(ref Point startPosition, ref Panel panel)
        {
            Name.Location = startPosition;
            startPosition += new Size(6, Name.Size.Height + 1);
            panel.Controls.Add(Name);
            for (int i = 0; i < DataBoxes.Count; i++)
            {
                DataBoxes[i].Location = startPosition;
                panel.Controls.Add(DataBoxes[i]);
                startPosition += new Size(0, DataBoxes[i].Size.Height + 1);
            }
            startPosition += new Size(-6, 5);
        }
        public string GetRequest()
        {
            string request = "(";
            foreach (var dataBox in DataBoxes)
            {
                if (dataBox.Checked)
                    request += Name.Text + " = \"" + dataBox.Text + "\" OR ";
            }
            if (request != "(") request = request.Remove(request.Length - 4) + ")";
            else request = "";
            return request;
        }
    }
}
