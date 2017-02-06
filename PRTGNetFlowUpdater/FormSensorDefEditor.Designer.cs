﻿namespace PRTGNetFlowUpdater
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.colRuleNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRuleType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.colRuleDef = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colRuleNum,
            this.colRuleType,
            this.colRuleDef});
            this.dataGridView1.Location = new System.Drawing.Point(1, 43);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(851, 261);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // colRuleNum
            // 
            this.colRuleNum.HeaderText = "N°";
            this.colRuleNum.Name = "colRuleNum";
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
            // FormSensorDefEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(855, 367);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "FormSensorDefEditor";
            this.Text = "Sensor Definition Editor";
            this.Load += new System.EventHandler(this.FormSensorDefEditor_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRuleNum;
        private System.Windows.Forms.DataGridViewComboBoxColumn colRuleType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRuleDef;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}