
namespace PRTGNetFlowUpdater
{
    using System;
    using System.Windows.Forms;

    public partial class Form1 : Form
    {
        PRTGXMLConfig conf;

        public Form1()
        {
            InitializeComponent();
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                conf = new PRTGXMLConfig();
                conf.ConfigFileName = openFileDialog.FileName;
                conf.LoadXMLConfigFile(trvConfig.Nodes);
            }
        }

        private void OnTreeNodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Text.StartsWith("ChannelDef_"))
            {
                if(conf != null)
                {
                    string def = conf.GetChannelDef(e.Node.Text.Substring("ChannelDef_".Length));
                    txtDisplay.Text = def;
                }
            }
        }
    }
}
