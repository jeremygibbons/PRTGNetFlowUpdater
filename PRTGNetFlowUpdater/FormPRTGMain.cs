// <copyright file="Form1.cs" company="None">
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
    using PRTGNetFlowUpdater.TreeNodes;

    /// <summary>
    /// Main form class for the application
    /// </summary>
    public partial class FormPRTGMain : Form
    {
        /// <summary>
        /// The PRTGXMLConfig instance used to parse the config file.
        /// </summary>
        private PRTGXMLConfig conf;

        /// <summary>
        /// Initializes a new instance of the <see cref="FormPRTGMain"/> class.
        /// </summary>
        public FormPRTGMain()
        {
            this.InitializeComponent();
        }

        private void LoadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.openFileDialog.ShowDialog() == DialogResult.OK)
            {
                this.conf = new PRTGXMLConfig();
                this.conf.ConfigFileName = this.openFileDialog.FileName;
                this.conf.LoadXMLConfigFile(this.trvConfig.Nodes);
            }
        }

        private void OnTreeNodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                if (e.Node.Text.StartsWith("ChannelDef_"))
                {
                    if (this.conf != null)
                    {
                        int id = 0;
                        string idstr = e.Node.Text.Substring("ChannelDef_".Length);
                        try
                        {
                            id = Convert.ToInt32(idstr);
                        }
                        catch (FormatException fe)
                        {
                            return;
                            //return "Channel Definition Not Found. Format Error: " + fe.Message;
                        }

                        string def = this.conf.GetChannelDef(id);
                        this.txtDisplay.Text = def;
                    }
                }
                else
                {
                    this.txtDisplay.Text = e.Node.Text;
                }
            }
        }

        private void valueViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            trvConfig.Nodes.Clear();
            foreach(ChannelDefTreeNode c in conf.Channels)
            {
                trvConfig.Nodes.Add(c);
            }
        }

        private void treeViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            trvConfig.Nodes.Clear();
            foreach (ProbeTreeNode p in conf.Probes)
            {
                trvConfig.Nodes.Add(p);
            }
        }

        private void OnTreeNodeMouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                TreeNode node = trvConfig.GetNodeAt(e.Location);

                //trvConfig.SelectedNode = node;

                if (node is GroupTreeNode)
                {
                    ctxMenuGroupNode.Show(this.trvConfig, e.Location);
                }
                else if (node is DeviceTreeNode)
                {
                    ctxMenuDeviceNode.Show(this.trvConfig, e.Location);
                }
                else if (node is NetflowSensorTreeNode)
                {
                    ctxMenuSensorNode.Show(this.trvConfig, e.Location);
                }
            }
        }

        private void OnTreeViewAfterNodeSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Text.StartsWith("ChannelDef_"))
            {
                if (this.conf != null)
                {
                    int id = 0;
                    string idstr = e.Node.Text.Substring("ChannelDef_".Length);
                    try
                    {
                        id = Convert.ToInt32(idstr);
                    }
                    catch (FormatException fe)
                    {
                        return;
                        //return "Channel Definition Not Found. Format Error: " + fe.Message;
                    }

                    string def = this.conf.GetChannelDef(id);
                    this.txtDisplay.Text = def;
                }
            }
            else
            {
                this.txtDisplay.Text = e.Node.Text;
            }
        }

        private void OnTreeViewAfterCheck(object sender, TreeViewEventArgs e)
        {
            // The code only executes if the user caused the checked state to change.
            if (e.Action != TreeViewAction.Unknown)
            {
                if (e.Node.Nodes.Count > 0)
                {
                    /* Calls the CheckAllChildNodes method, passing in the current 
                    Checked value of the TreeNode whose checked state changed. */
                    this.CheckAllChildNodes(e.Node, e.Node.Checked);
                }
            }
        }

        // Updates all child tree nodes recursively.
        private void CheckAllChildNodes(TreeNode treeNode, bool nodeChecked)
        {
            foreach (TreeNode node in treeNode.Nodes)
            {
                node.Checked = nodeChecked;
                if (node.Nodes.Count > 0)
                {
                    // If the current node has child nodes, call the CheckAllChildsNodes method recursively.
                    this.CheckAllChildNodes(node, nodeChecked);
                }
            }
        }
    }
}
