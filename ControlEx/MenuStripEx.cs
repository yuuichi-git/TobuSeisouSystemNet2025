﻿/*
 * 2024-09-23
 */
namespace ControlEx {
    public partial class MenuStripEx : MenuStrip {
        /*
         * デリゲート
         */
        public event EventHandler Event_MenuStripEx_ToolStripMenuItem_Click = delegate { };
        /*
         * ToolStripMenuItem
         */
        private readonly ToolStripMenuItem toolStripMenuItemFile = new("ファイル");
        private readonly ToolStripMenuItem toolStripMenuItemExit = new("アプリケーションを終了する");

        private readonly ToolStripMenuItem toolStripMenuItemEdit = new("編集");
        private readonly ToolStripMenuItem toolStripMenuItemInsertNewRecord = new("新規レコード作成");
        private readonly ToolStripMenuItem toolStripMenuItemUpdateTaitou = new("台東資源収集量入力");

        private readonly ToolStripMenuItem toolStripMenuItemInitialize = new("初期化");
        private readonly ToolStripMenuItem toolStripMenuItemInitializeBord = new("配車ボードを初期化する");

        private readonly ToolStripMenuItem toolStripMenuItemDataBase = new("データベース");
        private readonly ToolStripMenuItem toolStripMenuItemDataBaseLocal = new("ローカルデータベースへ接続する");

        private readonly ToolStripMenuItem toolStripMenuItemExport = new("エクスポート");
        private readonly ToolStripMenuItem toolStripMenuItemExportExcel = new("xls形式ファイルをエクスポートします");
        private readonly ToolStripMenuItem toolStripMenuItemExportCSV = new("免許証登録用ファイルをエクスポートします");

        private readonly ToolStripMenuItem toolStripMenuItemPrint = new("印刷");
        private readonly ToolStripMenuItem toolStripMenuItemPrintA4 = new("A4で印刷する");
        private readonly ToolStripMenuItem toolStripMenuItemPrintB5 = new("B5で印刷する");
        private readonly ToolStripMenuItem toolStripMenuItemPrintB5Dialog = new("B5で印刷する(Dialog)");

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

            toolStripMenuItemInsertNewRecord.Name = "ToolStripMenuItemInsertNewRecord";
            toolStripMenuItemInsertNewRecord.Click += ToolStripMenuItem_Click;
            toolStripMenuItemEdit.DropDownItems.Add(toolStripMenuItemInsertNewRecord);

            toolStripMenuItemUpdateTaitou.Name = "ToolStripMenuItemUpdateTaitou";
            toolStripMenuItemUpdateTaitou.Click += ToolStripMenuItem_Click;
            toolStripMenuItemEdit.DropDownItems.Add(toolStripMenuItemUpdateTaitou);
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
             * エクスポート
             */
            toolStripMenuItemExport.Name = "ToolStripMenuItemExport";
            this.Items.Add(toolStripMenuItemExport);

            toolStripMenuItemExportExcel.Name = "ToolStripMenuItemExportExcel";
            toolStripMenuItemExportExcel.Click += ToolStripMenuItem_Click;
            toolStripMenuItemExport.DropDownItems.Add(toolStripMenuItemExportExcel);

            toolStripMenuItemExportCSV.Name = "ToolStripMenuItemExportCSV";
            toolStripMenuItemExportCSV.Click += ToolStripMenuItem_Click;
            toolStripMenuItemExport.DropDownItems.Add(toolStripMenuItemExportCSV);
            /*
             * 印刷
             */
            toolStripMenuItemPrint.Name = "ToolStripMenuItemPrint";
            this.Items.Add(toolStripMenuItemPrint);
            toolStripMenuItemPrintA4.Name = "ToolStripMenuItemPrintA4";
            toolStripMenuItemPrintA4.Click += ToolStripMenuItem_Click;
            toolStripMenuItemPrint.DropDownItems.Add(toolStripMenuItemPrintA4);
            toolStripMenuItemPrintB5.Name = "ToolStripMenuItemPrintB5";
            toolStripMenuItemPrintB5.Click += ToolStripMenuItem_Click;
            toolStripMenuItemPrint.DropDownItems.Add(toolStripMenuItemPrintB5);
            toolStripMenuItemPrintB5Dialog.Name = "ToolStripMenuItemPrintB5Dialog";
            toolStripMenuItemPrintB5Dialog.Click += ToolStripMenuItem_Click;
            toolStripMenuItemPrint.DropDownItems.Add(toolStripMenuItemPrintB5Dialog);
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
