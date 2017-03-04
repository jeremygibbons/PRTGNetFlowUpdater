using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PRTGNetFlowUpdater
{
    public partial class TemplateEditor : Form
    {
        private string templateName;

        private string appRuleName;

        private string appRule;

        public TemplateEditor()
        {
            InitializeComponent();
        }

        public TemplateEditor(string templName, string appName, string rule)
        {
            this.TemplateName = templName;
            this.AppRuleName = appName;
            this.AppRule = rule;
        }

        public string TemplateName
        {
            get
            {
                return this.templateName;
            }

            set
            {
                this.templateName = value;
            }
        }

        public string AppRuleName
        {
            get
            {
                return this.appRuleName;
            }

            set
            {
                this.appRuleName = value;
            }
        }

        public string AppRule
        {
            get
            {
                return this.appRule;
            }

            set
            {
                this.appRule = value;
            }
        }

        private void TemplateEditor_Load(object sender, EventArgs e)
        {
            txtTemplateName.Text = this.TemplateName;
            txtAppName.Text = this.AppRuleName;
            txtRule.Text = this.AppRule;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
