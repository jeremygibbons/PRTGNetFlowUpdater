// <copyright file="FormSensorDefEditor.cs" company="None">
//    <para>
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to
// deal in the Software without restriction, including without limitation the 
// rights to use, copy, modify, merge, publish, distribute, sublicense, and/or
// sell copies of the Software, and to permit persons to whom the Software is 
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS
// IN THE SOFTWARE.
//
// Copyright (c) 2017 Jeremy Gibbons. All rights reserved
// </copyright>

namespace PRTGNetFlowUpdater
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;

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
                foreach (int id in channels.Keys)
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
                if (m.Success)
                {
                    cd.ChannelID = Convert.ToInt32(m.Groups[1].Value);
                    cd.Name = m.Groups[2].Value;
                    cd.Rule = m.Groups[3].Value;
                    dic[cd.ChannelID] = cd;
                }
            }

            return dic;
        }

        private void AddTemplateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TemplateEditor te = new TemplateEditor(this.templManager);
            te.ShowDialog();
        }

        private void EditTemplateToolStripMenuItem_MouseHover(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = sender as ToolStripMenuItem;

            if (sender == null)
            {
                return;
            }

            menuItem.DropDownItems.Clear();

            IEnumerable<RuleTemplate> rules = this.templManager.Templates;
            foreach (RuleTemplate rule in rules)
            {
                menuItem.DropDownItems.Add(rule.TemplateName, null, TemplateEditMenuItem_Click);
            }
        }

        private void TemplateEditMenuItem_Click(object sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem mi)
            {
                TemplateEditor te = new TemplateEditor(mi.Text, this.templManager);
                te.ShowDialog();
            }
        }

        private void RemoveTemplateToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = sender as ToolStripMenuItem;

            if (sender == null)
            {
                return;
            }

            menuItem.DropDownItems.Clear();

            IEnumerable<RuleTemplate> rules = this.templManager.Templates;
            foreach (RuleTemplate rule in rules)
            {
                menuItem.DropDownItems.Add(rule.TemplateName, null, TemplateRemoveMenuItem_Click);
            }
        }

        private void TemplateRemoveMenuItem_Click(object sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem mi)
            {
                templManager.DeleteRule(mi.Text);
            }
        }
    }
}
