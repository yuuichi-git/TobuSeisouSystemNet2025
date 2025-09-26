/*
 * 2025-07-31
 */
namespace ControlEx {
    public partial class TreeViewEx : TreeView {

        /// <summary>
        /// コンストラクター
        /// </summary>
        public TreeViewEx() {
            /*
             * InitializeControl
             */
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pe"></param>
        protected override void OnPaint(PaintEventArgs pe) {
            base.OnPaint(pe);
        }

        /// <summary>
        /// 親ノードを追加する
        /// </summary>
        /// <param name="listNode"></param>
        public void AddParentNodes(List<string> listNode) {
            this.Nodes.Clear();                                 // 全てのNodesをClearする
            foreach (string node in listNode) {
                TreeNode treeNode = new();                      // Node作成
                treeNode.Name = node;
                treeNode.Text = node;
                this.Nodes.Add(treeNode);                       // Node追加
            }
        }

        public void AddChildNodes() {

        }
    }
}
