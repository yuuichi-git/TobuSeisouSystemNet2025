﻿/*
 * 2024-09-23
 */
namespace ControlEx {
    public partial class MenuStripEx : MenuStrip {
        /*
         * ToolStripMenuItem
         */
        private ToolStripMenuItem toolStripMenuItemFile = new("ファイル");

        private ToolStripMenuItem toolStripMenuItemEdit = new("編集");

        private ToolStripMenuItem toolStripMenuItemExit = new("アプリケーションを終了する");

        private ToolStripMenuItem toolStripMenuItemInitialize = new("初期化");
        private ToolStripMenuItem toolStripMenuItemInitializeBord = new("配車ボードを初期化する");

        private ToolStripMenuItem toolStripMenuItemDataBase = new("データベース");
        private ToolStripMenuItem toolStripMenuItemDataBaseLocal = new("ローカルデータベースへ接続する");

        private ToolStripMenuItem toolStripMenuItemPrint = new("印刷");

        private ToolStripMenuItem toolStripMenuItemHelp = new("ヘルプ");
        /*
         * 変数
         */
        private bool _ToolStripMenuItemDataBaseLocalFlag = false;

        /// <summary>
        /// コンストラクター
        /// </summary>
        public MenuStripEx() {
            /*
             * Initialize
             */
            InitializeComponent();
            this.Dock = DockStyle.Top;
            this.CreateItem();
        }

        /// <summary>
        /// OnPaint
        /// </summary>
        /// <param name="pe"></param>
        protected override void OnPaint(PaintEventArgs pe) {
            base.OnPaint(pe);
        }

        /// <summary>
        /// ToolStripMenuItemを作成
        /// </summary>
        private void CreateItem() {
            /*
             * ファイル
             */
            toolStripMenuItemFile.Name = "ToolStripMenuItemFile";
            this.Items.Add(toolStripMenuItemFile);

            toolStripMenuItemExit.Name = "ToolStripMenuItemExit";
            toolStripMenuItemExit.Click += ToolStripMenuItem_Click;
            toolStripMenuItemFile.DropDownItems.Add(toolStripMenuItemExit);
            /*
             * 編集
             */
            toolStripMenuItemEdit.Name = "ToolStripMenuItemEdit";
            this.Items.Add(toolStripMenuItemEdit);
            /*
             * 初期化
             */
            toolStripMenuItemInitialize.Name = "ToolStripMenuItemInitialize";
            this.Items.Add(toolStripMenuItemInitialize);

            toolStripMenuItemInitializeBord.Name = "ToolStripMenuItemInitializeBord";
            toolStripMenuItemInitializeBord.Click += ToolStripMenuItem_Click;
            toolStripMenuItemInitialize.DropDownItems.Add(toolStripMenuItemInitializeBord);
            /*
             * データベース
             */
            toolStripMenuItemDataBase.Name = "ToolStripMenuItemDataBase";
            this.Items.Add(toolStripMenuItemDataBase);

            toolStripMenuItemDataBaseLocal.Checked = ToolStripMenuItemDataBaseLocalFlag;
            toolStripMenuItemDataBaseLocal.CheckOnClick = true;
            toolStripMenuItemDataBaseLocal.Name = "ToolStripMenuItemDataBaseLocal";
            toolStripMenuItemDataBaseLocal.Click += ToolStripMenuItem_Click;
            toolStripMenuItemDataBase.DropDownItems.Add(toolStripMenuItemDataBaseLocal);
            /*
             * 印刷
             */
            toolStripMenuItemPrint.Name = "ToolStripMenuItemPrint";
            this.Items.Add(toolStripMenuItemPrint);
            /*
             * ヘルプ
             */
            toolStripMenuItemHelp.Name = "ToolStripMenuItemHelp";
            this.Items.Add(toolStripMenuItemHelp);
        }

        /// <summary>
        /// ToolStripMenuItemのEnableを設定
        /// </summary>
        /// <param name="listString"></param>
        public void ChangeEnable(List<string> listString) {
            foreach (ToolStripMenuItem item in this.Items) {
                item.Visible = listString.Contains(item.Name);
                foreach (ToolStripMenuItem dropDownItem in item.DropDownItems) {
                    dropDownItem.Enabled = listString.Contains(dropDownItem.Name);
                }
            }
        }

        /// <summary>
        /// ToolStripMenuItem_Clickを親へ渡す
        /// </summary>
        public event EventHandler Event_MenuStripEx_ToolStripMenuItem_Click = delegate { };
        private void ToolStripMenuItem_Click(object sender, EventArgs e) {
            Event_MenuStripEx_ToolStripMenuItem_Click.Invoke(sender, e);
        }

        /// <summary>
        /// True:Localに接続 False:Networkに接続
        /// </summary>
        public bool ToolStripMenuItemDataBaseLocalFlag {
            get => this._ToolStripMenuItemDataBaseLocalFlag;
            set => this._ToolStripMenuItemDataBaseLocalFlag = value;
        }
    }
}