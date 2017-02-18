// <copyright file="TriStateTreeView.cs" company="None">
// 
// Code originally posted in March 2012 by "Fred_Informatix" on
// https://www.codeproject.com/Articles/340565/Simple-Tri-State-TreeView
// 
// The code is covered by the CPOL license as described here:
// https://www.codeproject.com/info/cpol10.aspx
// </copyright>

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

        /// <summary>
        /// Creates a new instance
        /// of this control.
        /// <summary>
        public TriStateCBTreeView()
            : base()
        {
            // first we create our state image
            // list and pre-init check state.
            _ilStateImages = new ImageList();                              
            CheckBoxState cbsState = CheckBoxState.UncheckedNormal;

            // let's iterate each tri-state
            // creating a new checkbox bitmap
            // and getting graphics object from it...
            for (int i = 0; i <= 2; i++) {                                 
                Bitmap bmpCheckBox = new Bitmap(16, 16);                   
                Graphics gfxCheckBox = Graphics.FromImage(bmpCheckBox);    
                switch (i)
                {
                    case 0: cbsState = CheckBoxState.UncheckedNormal; break;
                    case 1: cbsState = CheckBoxState.CheckedNormal; break;
                    case 2: cbsState = CheckBoxState.MixedNormal; break;
                }

                // ...rendering the checkbox and adding to state image list.
                CheckBoxRenderer.DrawCheckBox(gfxCheckBox, new Point(2, 2), cbsState);
                gfxCheckBox.Save();
                _ilStateImages.Images.Add(bmpCheckBox);
            }

            _bUseTriState = true;
        }

        // ~~~ properties ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        /// <summary>
        /// Gets or sets to display checkboxes in the tree view.
        /// </summary>
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

        /// <summary>
        /// Gets or sets to support tri-state in the checkboxes or not.
        /// </summary>
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
                    // if the current node has a mixed state
                    // then its parent is set to the same state
                    if (tNode.StateImageIndex == 2)
                    {                   
                        ParentNode.Checked = false;
                        ParentNode.StateImageIndex = 2;
                        return;
                    }

                    int CheckedCount = 0;
                    int UnCheckedCount = 0;

                    foreach (TreeNode ChildNode in ParentNode.Nodes)
                    {
                        // we count the checked and unchecked states
                        // of each node at the current level
                        if (ChildNode.StateImageIndex <= 0)
                        {            
                            UnCheckedCount++;
                        }
                        else if (ChildNode.StateImageIndex == 1)
                        {
                            CheckedCount++;
                        }

                        // if one node has a mixed state or there
                        // are checked and unchecked states, then
                        // the parent node is set to a mixed state
                        if (ChildNode.StateImageIndex == 2 ||           
                           (CheckedCount > 0 && UnCheckedCount > 0))
                        {  
                            ParentNode.Checked = false;                 
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
                    // the parent node becomes the current node
                    SetParentState(ParentNode);                         
                }
            }
        }

        protected void SetChildrenState(TreeNode tNode, bool RootNode)
        {
            if (!RootNode)
            {
                // the child state is inherited from the parent state
                tNode.Checked = (tNode.Parent.StateImageIndex == 1);    
                tNode.StateImageIndex = tNode.Parent.StateImageIndex;
            }

            foreach (TreeNode ChildNode in tNode.Nodes)
            {
                SetChildrenState(ChildNode, false);
            }
        }

        public void SetState(TreeNode tNode, int NewState)
        {
            if (NewState < 0 || NewState > 2)
            {
                NewState = 0;
            }

            tNode.Checked = (NewState == 1);

            // we verify if the checked state has
            // not been canceled in a BeforeCheck event
            if (tNode.Checked == (NewState == 1))
            {                     
                tNode.StateImageIndex = NewState;                       

                _bPreventCheckEvent = true;

                SetParentState(tNode);
                SetChildrenState(tNode, true);

                _bPreventCheckEvent = false;
            }
        }

        /// <summary>
        /// Initializes the nodes state.
        /// </summary>
        public void InitializeStates(TreeNodeCollection tNodes)
        {
            foreach (TreeNode tnCurrent in tNodes)
            {
                // set tree state image to each child node...
                if (tnCurrent.StateImageIndex == -1)
                {
                    _bPreventCheckEvent = true;

                    if (tnCurrent.Parent != null)
                    {
                        tnCurrent.Checked = tnCurrent.Parent.Checked;
                        tnCurrent.StateImageIndex = tnCurrent.Parent.StateImageIndex;
                    }
                    else
                    {
                        tnCurrent.StateImageIndex = tnCurrent.Checked ? 1 : 0;
                    }

                    _bPreventCheckEvent = false;
                }

                InitializeStates(tnCurrent.Nodes);
            }
        }

        public void InitializeCBImages()
        {
            // nothing to do here if checkboxes are hidden.
            if (!CheckBoxes)
            {
                return;
            }

            // hide normal checkboxes...
            base.CheckBoxes = false;               

            InitializeStates(this.Nodes);
        }

        /// <summary>
        /// Refreshes this control.
        /// </summary>
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
                {
                    AutoCheck(this, e);
                }

                return;
            }

            base.OnAfterCheck(e);
        }

        protected override void OnNodeMouseClick(TreeNodeMouseClickEventArgs e)
        {
            base.OnNodeMouseClick(e);

            int iSpacing = ImageList == null ? 0 : 20;
            
            // if user clicked area *not* used by the state
            // image we can leave here.
            if (e.X > e.Node.Bounds.Left - iSpacing ||      
                e.X < e.Node.Bounds.Left - (iSpacing + 14) ||    
                 e.Button != MouseButtons.Left)
            {
                return;
            }

            SetState(e.Node, e.Node.Checked ? 0 : 1);
        }
    }
}