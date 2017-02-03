
namespace PRTGNetFlowUpdater
{
    using System.Xml;
    using System.Xml.Linq;
    using System.Windows.Forms;

    class PRTGXMLConfig
    {
        private string configFileName;

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

            TreeNode currentNode = null;

            XDocument xdoc = XDocument.Load(this.ConfigFileName);

            foreach (XElement xel in xdoc.Root.Element("basenode").Element("nodes").Element("group").Element("nodes").Elements("probenode"))
            {
                ParseProbeNode(nodes, xel);
            }

            /*
            using (XmlReader reader = XmlReader.Create(this.ConfigFileName))
            {
                nodes.Clear();
                reader.MoveToContent();

                if(reader.NodeType == XmlNodeType.Element && reader.Name == "root")
                {
                    currentNode = nodes.Add("root");
                }
                else
                {
                    return;
                }

                reader.ReadToDescendant("basenode");
                reader.ReadToDescendant("nodes");
                reader.ReadToDescendant("group");
                reader.ReadToDescendant("nodes");

                reader.ReadToDescendant("probenode");
                XmlReader tempReader = reader.ReadSubtree();
                tempReader.ReadToDescendant("data");
                tempReader.ReadToDescendant("name");
                currentNode.Nodes.Add(tempReader.ReadElementContentAsString());


            }
            */
        }

        void ParseProbeNode(TreeNodeCollection nodes, XElement probeXEl)
        {
            TreeNode probeNode = nodes.Add(((string)probeXEl.Element("data").Element("name")).Trim());

            foreach (XElement deviceXEl in probeXEl.Elements("device"))
            {
                ParseDeviceNode(probeNode.Nodes, deviceXEl);
            }

            foreach (XElement groupXEl in probeXEl.Elements("group"))
            {
                ParseGroupNode(probeNode.Nodes, groupXEl);
            }
        }

        int ParseDeviceNode(TreeNodeCollection nodes, XElement deviceXEl)
        {
            TreeNode deviceNode = new TreeNode((string)deviceXEl.Element("data").Element("name"));
            int netFlowSensorCount = 0;
            foreach (XElement sensorXEl in deviceXEl.Element("nodes").Elements("sensor"))
            {
                if(((string)sensorXEl.Element("sensorkind")).Equals("netflow9custom"))
                {
                    netFlowSensorCount++;
                    deviceNode.Nodes.Add((string)sensorXEl.Element("flowchannel"));
                }
            }

            if(netFlowSensorCount > 0)
            {
                nodes.Add(deviceNode);
            }

            return netFlowSensorCount;
        }

        void ParseGroupNode(TreeNodeCollection nodes, XElement groupXEl)
        {
            TreeNode groupNode = new TreeNode((string)groupXEl.Element("data").Element("name"));

            foreach(XElement deviceXEl in groupXEl.)

        }
    }
}
