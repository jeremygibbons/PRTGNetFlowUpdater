
namespace PRTGNetFlowUpdater.TreeNodes
{
    using System.Windows.Forms;

    public class NetflowSensorTreeNode : PRTGTreeNode
    {
        private int channelDefinitionID = -1;

        public NetflowSensorTreeNode(string id, int channelID) : base(id, "ChannelDef_" + channelID)
        {
            this.ChannelDefinitionID = channelID;
        }

        public int ChannelDefinitionID
        {
            get
            {
                return this.channelDefinitionID;
            }

            set
            {
                this.channelDefinitionID = value;
            }
        }
    }
}
