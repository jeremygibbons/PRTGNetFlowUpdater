using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace PRTGNetFlowUpdater
{
    public partial class FormSensorDefEditor : Form
    {
        public string SensorDef { get; set; }

        private TemplateManager templManager;

        public FormSensorDefEditor(TemplateManager tm)
        {
            InitializeComponent();
            this.templManager = tm;
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

        private void addTemplateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TemplateEditor te = new TemplateEditor(this.templManager);
            DialogResult dr = te.ShowDialog();
        }

        private void EditTemplateToolStripMenuItem_MouseHover(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = sender as ToolStripMenuItem;
            if(sender == null)
            {
                return;
            }

            menuItem.DropDownItems.Clear();

            IEnumerable<RuleTemplate> rules = this.templManager.Templates;
            foreach (RuleTemplate rule in rules)
            {
                menuItem.DropDownItems.Add(rule.TemplateName);
            }
        }
    }
}
