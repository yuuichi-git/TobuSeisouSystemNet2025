/*
 * 2024-11-24
 */
namespace ControlEx {
    public partial class StockBoxPanel : FlowLayoutPanel {
        /*
         * デリゲート
         */
        public event DragEventHandler StockBoxPanel_OnDragDrop = delegate { };
        public event DragEventHandler StockBoxPanel_OnDragEnter = delegate { };
        public event DragEventHandler StockBoxPanel_OnDragOver = delegate { };
        public StockBoxPanel() {
            /*
             * InitializeControl
             */
            InitializeComponent();
            this.AllowDrop = true;
            this.AutoScroll = true;
            this.Dock = DockStyle.Fill;
            this.Margin = new Padding(0);
            this.Name = "StockBoxPanelBase";
            this.Padding = new Padding(0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pe"></param>
        protected override void OnPaint(PaintEventArgs pe) {
            base.OnPaint(pe);
        }

        protected override void OnDragDrop(DragEventArgs e) {
            StockBoxPanel_OnDragDrop.Invoke(this, e);
        }

        protected override void OnDragEnter(DragEventArgs e) {
            StockBoxPanel_OnDragEnter.Invoke(this, e);
        }

        protected override void OnDragOver(DragEventArgs e) {
            StockBoxPanel_OnDragOver.Invoke(this, e);
        }

    }
}
