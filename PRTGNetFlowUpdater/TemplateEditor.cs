// <copyright file="TemplateEditor.cs" company="None">
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
    using System.Windows.Forms;

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

        public TemplateEditor(string templName, TemplateManager tm)
        {
            InitializeComponent();

            this.templateManager = tm;
            RuleTemplate rt = tm.GetRule(templName);

            this.TemplateName = templName;
            this.AppRuleName = rt.AppName;
            this.AppRule = rt.AppRule;   
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
                if (this.templateManager.GetRule(rt.TemplateName) == null)
                {
                    this.templateManager.AddRule(rt);
                }
                else
                {
                    this.templateManager.ModifyRule(rt);
                }

                this.Close();
            }
            else
            {
                MessageBox.Show(rt.GetError());
            }
        }
    }
}
