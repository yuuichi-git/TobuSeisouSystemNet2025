/*
 * 2025-08-14
 */
using System.Data;

using Dao;

using FarPoint.Win.Spread;

using Vo;

namespace Seisou {
    public partial class OracleAllTable : Form {
        /*
         * Dao
         */
        private OracleAllTableDao _oracleAllTableDao;
        /*
         * Vo
         */
        private ConnectionVo _connectionVo;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionVo"></param>
        /// <param name="screen"></param>
        public OracleAllTable(ConnectionVo connectionVo, Screen screen) {
            /*
             * Dao
             */
            _oracleAllTableDao = new(connectionVo);
            /*
             * Vo
             */
            _connectionVo = connectionVo;
            /*
             * InitializeControl
             */
            InitializeComponent();
            /*
             * MenuStrip
             */
            List<string> listString = new() {
                "ToolStripMenuItemFile",
                "ToolStripMenuItemExit",
                "ToolStripMenuItemHelp"
            };
            this.MenuStripEx1.ChangeEnable(listString);
            /*
             * TreeView
             */
            this.TreeViewEx1.AddParentNodes(_oracleAllTableDao.GetUserTables());
            /*
             * Spread
             */
            this.InitializeSheetView(this.SheetViewList);
            /*
             * StatusStrip
             */
            this.StatusStripEx1.ToolStripStatusLabelDetail.Text = string.Empty;
            /*
             * Event
             */
            this.MenuStripEx1.Event_MenuStripEx_ToolStripMenuItem_Click += ToolStripMenuItem_Click;
            this.TreeViewEx1.NodeMouseDoubleClick += this.TreeView1_NodeMouseDoubleClick;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItem_Click(object sender, EventArgs e) {
            switch (((ToolStripMenuItem)sender).Name) {
                case "ToolStripMenuItemExit":
                    this.Close();
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TreeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e) {
            if (_connectionVo.OracleConnection.State == ConnectionState.Open) {
                SetSheetViewColumns(_oracleAllTableDao.GetColumns("SEISOU", e.Node.Name));

            } else {

            }
        }

        /// <summary>
        /// Spread��Column��ݒ肷��
        /// </summary>
        /// <param name="listColumnName"></param>
        private void SetSheetViewColumns(List<string> listColumnName) {
            this.SheetViewList.Rows.Clear();
            this.SheetViewList.Columns.Clear();
            int columnNumber = 0;
            foreach (string columnName in listColumnName) {
                this.SheetViewList.Columns.Add(columnNumber, 1);                                                    // ���ǉ����܂�
                this.SheetViewList.ColumnHeader.Columns[columnNumber].Font = new Font("Yu Gothic UI", 9);           // ��w�b�_��Font
                this.SheetViewList.ColumnHeader.Columns[columnNumber].Width = 100;                                  // ��w�b�_�̕���ύX���܂�
                this.SheetViewList.Columns[columnNumber].Label = columnName;

                columnNumber++;
            }
        }

        /// <summary>
        /// Spread��SheetView������������
        /// </summary>
        /// <param name="sheetView"></param>
        /// <returns></returns>
        private void InitializeSheetView(SheetView sheetView) {
            this.SpreadList.AllowDragDrop = false;                                                                  // DrugDrop���֎~����
            this.SpreadList.PaintSelectionHeader = false;                                                           // �w�b�_�̑I����Ԃ����Ȃ�
            this.SpreadList.TabStrip.DefaultSheetTab.Font = new Font("Yu Gothic UI", 9);
            sheetView.Rows.Clear();
            sheetView.Columns.Clear();

            sheetView.ColumnHeader.Rows[0].Height = 26;                                                             // Column�w�b�_�̍���
            sheetView.GrayAreaBackColor = Color.White;
            sheetView.HorizontalGridLine = new GridLine(GridLineType.None);
            sheetView.RowHeader.Columns[0].Font = new Font("Yu Gothic UI", 9);                                      // �s�w�b�_��Font
            sheetView.RowHeader.Columns[0].Width = 48;                                                              // �s�w�b�_�̕���ύX���܂�
            sheetView.VerticalGridLine = new GridLine(GridLineType.Flat, Color.LightGray);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OracleAllTable_FormClosing(object sender, FormClosingEventArgs e) {

        }
    }
}
