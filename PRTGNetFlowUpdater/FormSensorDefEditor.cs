using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace PRTGNetFlowUpdater
{
    public partial class FormSensorDefEditor : Form
    {
        private string sensorDef;

        public FormSensorDefEditor()
        {
            InitializeComponent();
        }

        public string SensorDef
        {
            get
            {
                return this.sensorDef;
            }

            set
            {
                this.sensorDef = value;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void FormSensorDefEditor_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void OnFormShown(object sender, EventArgs e)
        {
            if (this.SensorDef != string.Empty)
            {
                IDictionary<int, ChannelDefinition> channels = ParseSensorDef(this.SensorDef);
                foreach(int id in channels.Keys)
                {
                    ChannelDefinition cd = channels[id];
                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(this.dataGridViewRules, new object[] { cd.ChannelID, cd.Name, "Custom", cd.Rule });
                    row.ContextMenuStrip = this.contextMenuDataGrid;
                    this.dataGridViewRules.Rows.Add(row);
                }
            }
        }

        private IDictionary<int, ChannelDefinition> ParseSensorDef(string sensor)
        {
            Regex rulePattern = new Regex(@"^(\d+):\s*(\S+)\s+(.*)", RegexOptions.Singleline);

            string[] chans = sensor.Trim().Split(new char[] { '#' }, StringSplitOptions.RemoveEmptyEntries) ;

            Dictionary<int, ChannelDefinition> dic = new Dictionary<int, ChannelDefinition>();

            foreach(string s in chans)
            {
                ChannelDefinition cd = new ChannelDefinition();
                Match m = rulePattern.Match(s);
                if(m.Success)
                {
                    cd.ChannelID = Convert.ToInt32(m.Groups[1].Value);
                    cd.Name = m.Groups[2].Value;
                    cd.Rule = m.Groups[3].Value;
                    dic[cd.ChannelID] = cd;
                }
            }

            return dic;
        }
    }
}
