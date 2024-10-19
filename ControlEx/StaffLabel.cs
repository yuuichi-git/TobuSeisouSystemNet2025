/*
 * 2024-10-14
 */
using Vo;

namespace ControlEx {
    public partial class StaffLabel : Label {
        private bool _cursorEnterFlag = false;
        /*
         * Vo
         */
        private StaffMasterVo _staffMasterVo;
        /*
         * Labelのサイズ
         */
        private const float _panelWidth = 70;
        private const float _panelHeight = 116;

        /// <summary>
        /// Constractor
        /// </summary>
        /// <param name="staffMasterVo"></param>
        public StaffLabel(StaffMasterVo staffMasterVo) {
            /*
             * Vo
             */
            _staffMasterVo = staffMasterVo;
            /*
             * InitializeControl
             */
            InitializeComponent();
            this.AllowDrop = false;
            this.BackColor = Color.Transparent;
            this.BorderStyle = BorderStyle.None;
            this.Height = (int)_panelHeight;
            this.Margin = new Padding(2);
            this.Name = "StaffLabel";
            this.Padding = new(0);
            this.Width = (int)_panelWidth;
            /*
             * Event
             */
            this.MouseClick += StaffLabel_MouseClick;
            this.MouseDoubleClick += StaffLabel_MouseDoubleClick;
            this.MouseEnter += StaffLabel_MouseEnter;
            this.MouseLeave += StaffLabel_MouseLeave;
            this.MouseMove += StaffLabel_MouseMove;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pe"></param>
        protected override void OnPaint(PaintEventArgs pe) {
            base.OnPaint(pe);
            /*
             * 背景画像
             */
            pe.Graphics.DrawImage(ByteArrayToImage(Properties.Resources.CarLabelImage), 0, 0, _panelWidth, _panelHeight);
            /*
             * カーソル関係
             */
            if (CursorEnterFlag)
                pe.Graphics.DrawImage(ByteArrayToImage(Properties.Resources.Filter), 0, 0, _panelWidth, _panelHeight);
        }

        /// <summary>
        /// バイト配列をImageオブジェクトに変換
        /// </summary>
        /// <param name="arrayByte"></param>
        /// <returns></returns>
        public Image ByteArrayToImage(byte[] arrayByte) {
            ImageConverter imageConverter = new();
            return (Image)imageConverter.ConvertFrom(arrayByte);
        }

        /*
         * Event
         */
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StaffLabel_MouseClick(object sender, MouseEventArgs e) {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StaffLabel_MouseDoubleClick(object sender, MouseEventArgs e) {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StaffLabel_MouseEnter(object sender, EventArgs e) {
            CursorEnterFlag = true;
            Refresh();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StaffLabel_MouseLeave(object sender, EventArgs e) {
            CursorEnterFlag = false;
            Refresh();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StaffLabel_MouseMove(object sender, MouseEventArgs e) {
        }

        /*
         * プロパティ
         */
        /// <summary>
        /// StaffMasterVo
        /// </summary>
        public StaffMasterVo StaffMasterVo {
            get => this._staffMasterVo;
            set => this._staffMasterVo = value;
        }
        /// <summary>
        /// True:カーソルが乗っている False:カーソルが外れている
        /// </summary>
        public bool CursorEnterFlag {
            get => this._cursorEnterFlag;
            set => this._cursorEnterFlag = value;
        }
        
    }
}
