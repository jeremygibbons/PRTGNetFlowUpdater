namespace PRTGNetFlowUpdater
{
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using System.Windows.Forms.VisualStyles;

    public class TriStateCBTreeView : TreeView
    {

        // ~~~ fields ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        ImageList _ilStateImages;
        bool _bUseTriState;
        bool _bCheckBoxesVisible;
        bool _bPreventCheckEvent;

        // ~~~ constructor ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        /// &lt;summary>
        /// Creates a new instance
        /// of this control.
        /// &lt;/summary>
        public TriStateCBTreeView()
            : base()
        {
            _ilStateImages = new ImageList();                            // first we create our state image
            CheckBoxState cbsState = CheckBoxState.UncheckedNormal;      // list and pre-init check state.

            for (int i = 0; i <= 2; i++) {                                                                // let's iterate each tri-state
                Bitmap bmpCheckBox = new Bitmap(16, 16);                   // creating a new checkbox bitmap
                Graphics gfxCheckBox = Graphics.FromImage(bmpCheckBox);      // and getting graphics object from
                switch (i)                                                  // it...
                {
                    case 0: cbsState = CheckBoxState.UncheckedNormal; break;
                    case 1: cbsState = CheckBoxState.CheckedNormal; break;
                    case 2: cbsState = CheckBoxState.MixedNormal; break;
                }
                CheckBoxRenderer.DrawCheckBox(gfxCheckBox, new Point(2, 2), cbsState);  // ...rendering
                gfxCheckBox.Save();                                         // the checkbox and
                _ilStateImages.Images.Add(bmpCheckBox);                      // adding to state image list.
            }

            _bUseTriState = true;
        }

        // ~~~ properties ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        /// &lt;summary>
        /// Gets or sets to display
        /// checkboxes in the tree
        /// view.
        /// &lt;/summary>
        [Category("Appearance")]
        [Description("Sets tree view to display checkboxes or not.")]
        [DefaultValue(false)]
        public new bool CheckBoxes
        {
            get { return _bCheckBoxesVisible; }
            set
            {
                _bCheckBoxesVisible = value;
                base.CheckBoxes = _bCheckBoxesVisible;
                this.StateImageList = _bCheckBoxesVisible ? _ilStateImages : null;
            }
        }

        [Browsable(false)]
        public new ImageList StateImageList
        {
            get { return base.StateImageList; }
            set { base.StateImageList = value; }
        }

        /// &lt;summary>
        /// Gets or sets to support
        /// tri-state in the checkboxes
        /// or not.
        /// &lt;/summary>
        [Category("Appearance")]
        [Description("Sets tree view to use tri-state checkboxes or not.")]
        [DefaultValue(true)]
        public bool CheckBoxesTriState
        {
            get { return _bUseTriState; }
            set { _bUseTriState = value; }
        }

        // ~~~ functions ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        /// <summary>
        /// Changes nodes state.
        /// </summary>
        protected void SetParentState(TreeNode tNode)
        {
            TreeNode ParentNode = tNode.Parent;
            if (ParentNode != null)
            {
                try
                {
                    if (tNode.StateImageIndex == 2)
                    {                   // if the current node has a mixed state
                        ParentNode.Checked = false;                     // then its parent is set to the same state
                        ParentNode.StateImageIndex = 2;
                        return;
                    }
                    int CheckedCount = 0;
                    int UnCheckedCount = 0;
                    foreach (TreeNode ChildNode in ParentNode.Nodes)
                    {  // we count the checked and unchecked states
                        if (ChildNode.StateImageIndex <= 0)             // of each node at the current level
                            UnCheckedCount++;
                        else if (ChildNode.StateImageIndex == 1)
                            CheckedCount++;
                        if (ChildNode.StateImageIndex == 2 ||           // if one node has a mixed state or there
                           (CheckedCount > 0 && UnCheckedCount > 0))
                        {  // are checked and unchecked states, then
                            ParentNode.Checked = false;                 // the parent node is set to a mixed state
                            ParentNode.StateImageIndex = 2;
                            return;
                        }
                    }
                    if (UnCheckedCount > 0)
                    {
                        ParentNode.Checked = false;
                        ParentNode.StateImageIndex = 0;
                    }
                    else if (CheckedCount > 0)
                    {
                        ParentNode.Checked = true;
                        ParentNode.StateImageIndex = 1;
                    }
                }
                finally
                {
                    SetParentState(ParentNode);                         // the parent node becomes the current node
                }
            }
        }
        protected void SetChildrenState(TreeNode tNode, bool RootNode)
        {
            if (!RootNode)
            {
                tNode.Checked = (tNode.Parent.StateImageIndex == 1);    // the child state is inherited
                tNode.StateImageIndex = tNode.Parent.StateImageIndex;   // from the parent state
            }
            foreach (TreeNode ChildNode in tNode.Nodes)
                SetChildrenState(ChildNode, false);
        }
        public void SetState(TreeNode tNode, int NewState)
        {
            if (NewState < 0 || NewState > 2)
                NewState = 0;
            tNode.Checked = (NewState == 1);
            if (tNode.Checked == (NewState == 1))
            {                     // we verify if the checked state has
                tNode.StateImageIndex = NewState;                       // not been canceled in a BeforeCheck event

                _bPreventCheckEvent = true;

                SetParentState(tNode);
                SetChildrenState(tNode, true);

                _bPreventCheckEvent = false;
            }
        }

        /// &lt;summary>
        /// Initializes the nodes state.
        /// &lt;/summary>

        public void InitializeStates(TreeNodeCollection tNodes)
        {
            foreach (TreeNode tnCurrent in tNodes)
            {                   // set tree state image
                if (tnCurrent.StateImageIndex == -1)
                {                 // to each child node...
                    _bPreventCheckEvent = true;

                    if (tnCurrent.Parent != null)
                    {
                        tnCurrent.Checked = tnCurrent.Parent.Checked;
                        tnCurrent.StateImageIndex = tnCurrent.Parent.StateImageIndex;
                    }
                    else
                        tnCurrent.StateImageIndex = tnCurrent.Checked ? 1 : 0;

                    _bPreventCheckEvent = false;
                }
                InitializeStates(tnCurrent.Nodes);
            }
        }
        public void InitializeCBImages()
        {
            if (!CheckBoxes)                   // nothing to do here if
                return;                       // checkboxes are hidden.

            base.CheckBoxes = false;               // hide normal checkboxes...

            InitializeStates(this.Nodes);
        }

        /// &lt;summary>
        /// Refreshes this control.
        /// &lt;/summary>

        public override void Refresh()
        {
            base.Refresh();

            InitializeCBImages();
        }

        // ~~~ events ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        protected override void OnLayout(LayoutEventArgs levent)
        {
            base.OnLayout(levent);

            InitializeCBImages();
        }

        protected override void OnAfterExpand(TreeViewEventArgs e)
        {
            if (CheckBoxes)
                InitializeStates(e.Node.Nodes);

            base.OnAfterExpand(e);
        }

        public delegate void AutoCheckEventHandler(object sender, TreeViewEventArgs e);
        public event AutoCheckEventHandler AutoCheck;

        protected override void OnBeforeCheck(TreeViewCancelEventArgs e)
        {
            if (_bPreventCheckEvent)
                return;

            base.OnBeforeCheck(e);
        }

        protected override void OnAfterCheck(TreeViewEventArgs e)
        {
            if (_bPreventCheckEvent)
            {
                if (AutoCheck != null)
                    AutoCheck(this, e);
                return;
            }

            base.OnAfterCheck(e);
        }

        protected override void OnNodeMouseClick(TreeNodeMouseClickEventArgs e)
        {
            base.OnNodeMouseClick(e);

            int iSpacing = ImageList == null ? 0 : 20;      // if user clicked area
            if (e.X > e.Node.Bounds.Left - iSpacing ||      // *not* used by the state
                e.X < e.Node.Bounds.Left - (iSpacing + 14) ||    // image we can leave here.
                 e.Button != MouseButtons.Left) {
                return;
            }

            SetState(e.Node, e.Node.Checked ? 0 : 1);
        }
    }
}