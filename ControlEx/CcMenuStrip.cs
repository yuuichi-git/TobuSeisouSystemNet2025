/*
 * 2024-09-23
 */
namespace CcControl {
    public partial class CcMenuStrip: MenuStrip {
        /*
         * デリゲート
         */
        public event EventHandler Event_MenuStripEx_ToolStripMenuItem_Click = delegate { };
        /*
         * 変数
         */
        private bool _ToolStripMenuItemDataBaseLocalFlag = false;

        /// <summary>
        /// コンストラクター
        /// </summary>
        public CcMenuStrip() {
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
             * File
             */
            ToolStripMenuItem toolStripMenuItemFile = CreateMenuItem("ToolStripMenuItemFile","ファイル",null,this.Items);
            CreateMenuItem("ToolStripMenuItemExit", "終了", ToolStripMenuItem_Click, toolStripMenuItemFile.DropDownItems);
            /*
             * Edit
             */
            ToolStripMenuItem toolStripMenuItemEdit =CreateMenuItem("ToolStripMenuItemEdit","編集",null,this.Items);
            CreateMenuItem("ToolStripMenuItemInsertNewRecord", "新規レコード作成", ToolStripMenuItem_Click, toolStripMenuItemEdit.DropDownItems);
            CreateMenuItem("ToolStripMenuItemUpdateTaitou", "台東資源収集量入力", ToolStripMenuItem_Click, toolStripMenuItemEdit.DropDownItems);
            /*
             * Initialize
             */
            ToolStripMenuItem toolStripMenuItemInitialize =CreateMenuItem("ToolStripMenuItemInitialize","初期化",null,this.Items);
            CreateMenuItem("ToolStripMenuItemInitializeBord", "配車ボードを初期化する", ToolStripMenuItem_Click, toolStripMenuItemInitialize.DropDownItems);
            /*
             * DataBase
             */
            ToolStripMenuItem toolStripMenuItemDataBase =CreateMenuItem("ToolStripMenuItemDataBase","データベース",null,this.Items);
            ToolStripMenuItem toolStripMenuItemDataBaseLocal =CreateMenuItem("ToolStripMenuItemDataBaseLocal","ローカルデータベースへ接続",ToolStripMenuItem_Click,toolStripMenuItemDataBase.DropDownItems);
            toolStripMenuItemDataBaseLocal.Checked = ToolStripMenuItemDataBaseLocalFlag;
            toolStripMenuItemDataBaseLocal.CheckOnClick = true;
            /*
             * Export
             */
            ToolStripMenuItem toolStripMenuItemExport =CreateMenuItem("ToolStripMenuItemExport","エクスポート",null,this.Items);
            CreateMenuItem("ToolStripMenuItemExportExcel", "xls形式ファイルをエクスポート", ToolStripMenuItem_Click, toolStripMenuItemExport.DropDownItems);
            CreateMenuItem("ToolStripMenuItemExportCSV", "csv形式ファイルをエクスポート", ToolStripMenuItem_Click, toolStripMenuItemExport.DropDownItems);
            /*
             * PDF
             */
            ToolStripMenuItem toolStripMenuItemPDF =CreateMenuItem("ToolStripMenuItemPDF","PDF",null,this.Items);
            CreateMenuItem("ToolStripMenuItemPDFOpen", "PDFファイルを開く", ToolStripMenuItem_Click, toolStripMenuItemPDF.DropDownItems);
            /*
             * Print
             */
            ToolStripMenuItem toolStripMenuItemPrint =CreateMenuItem("ToolStripMenuItemPrint","Print",null,this.Items);
            CreateMenuItem("ToolStripMenuItemPrintA4", "A4で印刷する", ToolStripMenuItem_Click, toolStripMenuItemPrint.DropDownItems);
            CreateMenuItem("ToolStripMenuItemPrintA4CarDetail", "A4で印刷する(車検証＋記録事項)", ToolStripMenuItem_Click, toolStripMenuItemPrint.DropDownItems);
            CreateMenuItem("ToolStripMenuItemPrintB4", "B4で印刷する", ToolStripMenuItem_Click, toolStripMenuItemPrint.DropDownItems);
            CreateMenuItem("ToolStripMenuItemPrintB5", "B5で印刷する", ToolStripMenuItem_Click, toolStripMenuItemPrint.DropDownItems);
            CreateMenuItem("ToolStripMenuItemPrintB5Dialog", "B5で印刷する(Dialog)", ToolStripMenuItem_Click, toolStripMenuItemPrint.DropDownItems);
            /*
             * Help
             */
            CreateMenuItem("ToolStripMenuItemHelp", "ヘルプ", null, this.Items);
        }

        private ToolStripMenuItem CreateMenuItem(string name, string text, EventHandler handler, ToolStripItemCollection parent) {
            ToolStripMenuItem item = new(text);
            item.Name = name;

            if(handler != null) {
                item.Click += handler;
            }

            parent.Add(item);
            return item;
        }

        /// <summary>
        /// ToolStripMenuItemのEnableを設定
        /// 2026-02-11 修正
        /// </summary>
        /// <param name="listString"></param>
        public void ChangeEnable(List<string> listString) {
            foreach(ToolStripItem parentItem in this.Items) {
                if(parentItem is not ToolStripMenuItem parentMenu)
                    continue;
                /*
                 * 親メニューの表示・活性制御
                 */
                bool parentVisible = listString.Contains(parentMenu.Name);
                parentMenu.Visible = parentVisible;

                foreach(ToolStripItem childItem in parentMenu.DropDownItems) {
                    if(childItem is not ToolStripMenuItem childMenu)
                        continue;
                    /*
                     * 子メニューの表示・活性制御
                     */
                    bool childEnabled = listString.Contains(childMenu.Name);
                    childMenu.Visible = childEnabled;
                    childMenu.Enabled = childEnabled;
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
