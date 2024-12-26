/*
 * 2024-11-24
 */
namespace ControlEx {
    public partial class StockBoxPanel : FlowLayoutPanel {
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
    }
}
