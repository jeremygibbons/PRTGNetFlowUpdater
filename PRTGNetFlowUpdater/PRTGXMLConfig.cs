// <copyright file="PRTGXMLConfig.cs" company="None">
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
    using System.Windows.Forms;
    using System.Xml.Linq;

    public class PRTGXMLConfig
    {
        private string configFileName;

        private int flowChannelCount = 0;
        private Dictionary<string, TreeNode> flowChannels = new Dictionary<string, TreeNode>();
        private Dictionary<string, int> flowChannelDefToNum = new Dictionary<string, int>();
        private Dictionary<int, string> flowChannelNumToDef = new Dictionary<int, string>();


        public PRTGXMLConfig()
        {
        }

        /// <summary>
        /// Gets or sets the path for the configuration file
        /// </summary>
        public string ConfigFileName
        {
            get
            {
                return this.configFileName;
            }

            set
            {
                this.configFileName = value;
            }
        }

        /// <summary>
        /// Loads a PRTG Configuration.dat XML file
        /// </summary>
        /// <param name="nodes">
        /// The TreeNodeCollection to which the configuration items are to be added
        /// </param>
        public void LoadXMLConfigFile(TreeNodeCollection nodes)
        {
            if (this.ConfigFileName.Equals(string.Empty))
            {
                return;
            }

            XDocument xdoc = XDocument.Load(this.ConfigFileName);

            foreach (XElement xel in xdoc.Root.Element("basenode").Element("nodes").Element("group").Element("nodes").Elements("probenode"))
            {
                this.ParseProbeNode(nodes, xel);
            }
        }

        /// <summary>
        /// Gets the channel definition for a given channel definition ID.
        /// </summary>
        /// <param name="channelDefID"></param>
        /// <returns></returns>
        internal string GetChannelDef(int id)
        {
            if (flowChannelNumToDef.ContainsKey(id))
            {
                return flowChannelNumToDef[id];
            }

            return "Channel Definition Not Found. No such id: " + id;
        }

        /// <summary>
        /// Parses a "probenode" configuration node.
        /// </summary>
        /// <param name="nodes">The TreeNodeCollection to which config items are to be added.</param>
        /// <param name="probeXEl">The XElement containing the probenode</param>
        /// <returns>The number of netflow sensors in this probenode</returns>
        private int ParseProbeNode(TreeNodeCollection nodes, XElement probeXEl)
        {
            TreeNode probeNode = new TreeNode(((string)probeXEl.Element("data").Element("name")).Trim());

            int numChildren = 0;

            foreach (XElement deviceXEl in probeXEl.Element("nodes").Elements("device"))
            {
                numChildren += this.ParseDeviceNode(probeNode.Nodes, deviceXEl);
            }

            foreach (XElement groupXEl in probeXEl.Element("nodes").Elements("group"))
            {
                numChildren += this.ParseGroupNode(probeNode.Nodes, groupXEl);
            }

            if (numChildren > 0)
            {
                nodes.Add(probeNode);
            }

            return numChildren;
        }

        /// <summary>
        /// Parses a "device" node. A device node contains sensors of various types in its "nodes" child node.
        /// The aim here is to retrieve the netflow sensors and parse their channel definitions.
        /// </summary>
        /// <param name="nodes">The TreeNodeCollection the devices should be added to.</param>
        /// <param name="deviceXEl">The XElement containing the device node to be parsed</param>
        /// <returns></returns>
        private int ParseDeviceNode(TreeNodeCollection nodes, XElement deviceXEl)
        {
            TreeNode deviceNode = new TreeNode(((string)deviceXEl.Element("data").Element("name")).Trim());
            int netFlowSensorCount = 0;
            foreach (XElement sensorXEl in deviceXEl.Element("nodes").Elements("sensor"))
            {
                if (((string)sensorXEl.Element("data").Element("sensorkind")).Trim().Equals("netflow9custom"))
                {
                    netFlowSensorCount++;
                    // Note: PRTG seems to use \r as a line separator in netflow channel defs.
                    // We convert them to CRLF in order for display to be handled correctly in the GUI.
                    string netflowChannelDef = ((string)sensorXEl.Element("data").Element("flowchannel")).Trim().Replace("\r", Environment.NewLine);
                    int channelDefNum = this.AddChannelDef(netflowChannelDef);
                    deviceNode.Nodes.Add("ChannelDef_" + channelDefNum);
                }
            }

            if (netFlowSensorCount > 0)
            {
                nodes.Add(deviceNode);
            }

            return netFlowSensorCount;
        }

        /// <summary>
        /// Parses a "group" node, which contains either further groups, or devices
        /// </summary>
        /// <param name="nodes">The TreeNodeCollection to which parsed nodes should be added</param>
        /// <param name="groupXEl">The XElement to parse</param>
        /// <returns>The number of netflow sensors under this group node</returns>
        private int ParseGroupNode(TreeNodeCollection nodes, XElement groupXEl)
        {
            TreeNode groupNode = new TreeNode(((string)groupXEl.Element("data").Element("name")).Trim());

            int numChildren = 0;

            foreach (XElement subGroupXEl in groupXEl.Element("nodes").Elements("group"))
            {
                numChildren += this.ParseGroupNode(groupNode.Nodes, subGroupXEl);
            }

            foreach (XElement deviceXEl in groupXEl.Element("nodes").Elements("device"))
            {
                numChildren += this.ParseDeviceNode(groupNode.Nodes, deviceXEl);
            }

            if (numChildren > 0)
            {
                nodes.Add(groupNode);
            }

            return numChildren;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="netflowChannelDef">The string representation of a NetFlow channel definition.</param>
        /// <remarks>
        /// It is the responsibility of the caller to make any adjustments to the channel definition,
        /// e.g. trimming whitespace.
        /// </remarks>
        /// <returns>The index of the channel definition</returns>
        private int AddChannelDef(string netflowChannelDef)
        {
            if(this.flowChannelDefToNum.ContainsKey(netflowChannelDef))
            {
                return this.flowChannelDefToNum[netflowChannelDef];
            }
            else
            {
                this.flowChannelCount++;
                this.flowChannelDefToNum[netflowChannelDef] = this.flowChannelCount;
                this.flowChannelNumToDef[this.flowChannelCount] = netflowChannelDef;
                return this.flowChannelCount;
            }
        }
    }
}
