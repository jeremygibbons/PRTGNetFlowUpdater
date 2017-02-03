
namespace PRTGNetFlowUpdater
{
    using System.Collections.Generic;
    using System.Xml.Linq;
    using System.Windows.Forms;

    class PRTGXMLConfig
    {
        private string configFileName;

        private Dictionary<string, TreeNode> flowChannels = new Dictionary<string, TreeNode>();

        public PRTGXMLConfig()
        {

        }

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

        public void LoadXMLConfigFile(TreeNodeCollection nodes)
        {
            if (this.ConfigFileName.Equals(""))
            {
                return;
            }

            XDocument xdoc = XDocument.Load(this.ConfigFileName);

            foreach (XElement xel in xdoc.Root.Element("basenode").Element("nodes").Element("group").Element("nodes").Elements("probenode"))
            {
                ParseProbeNode(nodes, xel);
            }
        }

        int ParseProbeNode(TreeNodeCollection nodes, XElement probeXEl)
        {
            TreeNode probeNode = new TreeNode(((string)probeXEl.Element("data").Element("name")).Trim());

            int numChildren = 0;

            foreach (XElement deviceXEl in probeXEl.Element("nodes").Elements("device"))
            {
                numChildren += ParseDeviceNode(probeNode.Nodes, deviceXEl);
            }

            foreach (XElement groupXEl in probeXEl.Element("nodes").Elements("group"))
            {
                numChildren += ParseGroupNode(probeNode.Nodes, groupXEl);
            }

            if(numChildren > 0)
            {
                nodes.Add(probeNode);
            }

            return numChildren;
        }

        int ParseDeviceNode(TreeNodeCollection nodes, XElement deviceXEl)
        {
            TreeNode deviceNode = new TreeNode(((string)deviceXEl.Element("data").Element("name")).Trim());
            int netFlowSensorCount = 0;
            foreach (XElement sensorXEl in deviceXEl.Element("nodes").Elements("sensor"))
            {
                if(((string)sensorXEl.Element("data").Element("sensorkind")).Trim().Equals("netflow9custom"))
                {
                    netFlowSensorCount++;
                    deviceNode.Nodes.Add(((string)sensorXEl.Element("data").Element("flowchannel")).Trim());
                }
            }

            if(netFlowSensorCount > 0)
            {
                nodes.Add(deviceNode);
            }

            return netFlowSensorCount;
        }

        int ParseGroupNode(TreeNodeCollection nodes, XElement groupXEl)
        {
            TreeNode groupNode = new TreeNode(((string)groupXEl.Element("data").Element("name")).Trim());

            int numChildren = 0;

            foreach (XElement subGroupXEl in groupXEl.Element("nodes").Elements("group"))
            {
                numChildren += ParseGroupNode(groupNode.Nodes, subGroupXEl);
            }

            foreach (XElement deviceXEl in groupXEl.Element("nodes").Elements("device"))
            {
                numChildren += ParseDeviceNode(groupNode.Nodes, deviceXEl);
            }

            if(numChildren > 0)
            {
                nodes.Add(groupNode);
            }

            return numChildren;
        }
    }
}
