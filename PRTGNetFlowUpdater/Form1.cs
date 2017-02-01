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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                PRTGXMLConfig conf = new PRTGXMLConfig();
                conf.ConfigFileName = openFileDialog.FileName;
                conf.LoadXMLConfigFile(trvConfig.Nodes);
            }
        }
    }
}
