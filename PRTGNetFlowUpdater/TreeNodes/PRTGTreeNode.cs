namespace PRTGNetFlowUpdater.TreeNodes
{
    using System.Windows.Forms;

    public class PRTGTreeNode : TreeNode
    {
        private string id;

        public PRTGTreeNode(string id, string text) : base(text)
        {
            this.Id = id;
            
        }

        public string Id
        {
            get
            {
                return this.id;
            }
            set
            {
                this.id = value;
            }
        }
    }
}
