namespace PRTGNetFlowUpdater
{
    partial class FormSensorDefEditor
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
            this.dataGridViewRules = new System.Windows.Forms.DataGridView();
            this.colRuleNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRuleName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRuleType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.colRuleDef = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuDataGrid = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.templatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editTemplatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addTemplateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeTemplateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewRules)).BeginInit();
            this.contextMenuDataGrid.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridViewRules
            // 
            this.dataGridViewRules.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewRules.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colRuleNum,
            this.colRuleName,
            this.colRuleType,
            this.colRuleDef});
            this.dataGridViewRules.Location = new System.Drawing.Point(1, 25);
            this.dataGridViewRules.Name = "dataGridViewRules";
            this.dataGridViewRules.RowHeadersVisible = false;
            this.dataGridViewRules.Size = new System.Drawing.Size(851, 279);
            this.dataGridViewRules.TabIndex = 0;
            this.dataGridViewRules.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // colRuleNum
            // 
            this.colRuleNum.HeaderText = "N°";
            this.colRuleNum.Name = "colRuleNum";
            // 
            // colRuleName
            // 
            this.colRuleName.HeaderText = "Name";
            this.colRuleName.Name = "colRuleName";
            // 
            // colRuleType
            // 
            this.colRuleType.HeaderText = "Rule Type";
            this.colRuleType.Items.AddRange(new object[] {
            "Custom",
            "Template: ZScaler",
            "Template: SMB"});
            this.colRuleType.Name = "colRuleType";
            // 
            // colRuleDef
            // 
            this.colRuleDef.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colRuleDef.HeaderText = "Rule Definition";
            this.colRuleDef.Name = "colRuleDef";
            this.colRuleDef.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colRuleDef.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // contextMenuDataGrid
            // 
            this.contextMenuDataGrid.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem2});
            this.contextMenuDataGrid.Name = "contextMenuDataGrid";
            this.contextMenuDataGrid.Size = new System.Drawing.Size(227, 48);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(226, 22);
            this.toolStripMenuItem1.Text = "Replace from template";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(226, 22);
            this.toolStripMenuItem2.Text = "Add channel template below";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(625, 310);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(106, 30);
            this.button1.TabIndex = 1;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(737, 310);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(106, 30);
            this.button2.TabIndex = 2;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 343);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(855, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.templatesToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(855, 24);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // templatesToolStripMenuItem
            // 
            this.templatesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editTemplatesToolStripMenuItem,
            this.addTemplateToolStripMenuItem,
            this.removeTemplateToolStripMenuItem});
            this.templatesToolStripMenuItem.Name = "templatesToolStripMenuItem";
            this.templatesToolStripMenuItem.Size = new System.Drawing.Size(73, 20);
            this.templatesToolStripMenuItem.Text = "Templates";
            // 
            // editTemplatesToolStripMenuItem
            // 
            this.editTemplatesToolStripMenuItem.Name = "editTemplatesToolStripMenuItem";
            this.editTemplatesToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.editTemplatesToolStripMenuItem.Text = "Edit template";
            // 
            // addTemplateToolStripMenuItem
            // 
            this.addTemplateToolStripMenuItem.Name = "addTemplateToolStripMenuItem";
            this.addTemplateToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.addTemplateToolStripMenuItem.Text = "Add template";
            this.addTemplateToolStripMenuItem.Click += new System.EventHandler(this.addTemplateToolStripMenuItem_Click);
            // 
            // removeTemplateToolStripMenuItem
            // 
            this.removeTemplateToolStripMenuItem.Name = "removeTemplateToolStripMenuItem";
            this.removeTemplateToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.removeTemplateToolStripMenuItem.Text = "Remove template";
            // 
            // FormSensorDefEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(855, 365);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dataGridViewRules);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormSensorDefEditor";
            this.Text = " ";
            this.Load += new System.EventHandler(this.FormSensorDefEditor_Load);
            this.Shown += new System.EventHandler(this.OnFormShown);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewRules)).EndInit();
            this.contextMenuDataGrid.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewRules;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRuleNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRuleName;
        private System.Windows.Forms.DataGridViewComboBoxColumn colRuleType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRuleDef;
        private System.Windows.Forms.ContextMenuStrip contextMenuDataGrid;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem templatesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editTemplatesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addTemplateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeTemplateToolStripMenuItem;
    }
}