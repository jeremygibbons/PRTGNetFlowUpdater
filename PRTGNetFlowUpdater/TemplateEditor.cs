using System;
using System.Windows.Forms;

namespace PRTGNetFlowUpdater
{
    public partial class TemplateEditor : Form
    {
        private TemplateManager templateManager;

        public string TemplateName { get; set; }

        public string AppRuleName { get; set; }

        public string AppRule { get; set; }

        public TemplateEditor(TemplateManager tm)
        {
            InitializeComponent();
            this.templateManager = tm;
        }

        public TemplateEditor(string templName, string appName, string rule, TemplateManager tm)
        {
            InitializeComponent();
            this.TemplateName = templName;
            this.AppRuleName = appName;
            this.AppRule = rule;

            this.templateManager = tm;
        }
        
        private void TemplateEditor_Load(object sender, EventArgs e)
        {
            txtTemplateName.Text = this.TemplateName;
            txtAppName.Text = this.AppRuleName;
            txtRule.Text = this.AppRule;
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            RuleTemplate rt = new RuleTemplate(txtTemplateName.Text, txtAppName.Text, txtRule.Text);
            if(rt.IsValidRule())
            {
                this.templateManager.AddRule(rt);
                this.Close();
            }
            else
            {
                MessageBox.Show(rt.GetError());
            }
        }
    }
}
