/*
 * 2026-05-28
 * 休みの管理を行うフォーム
 */
using CcControl;

using Dao;

using Vo;

namespace PaidLeave {
    public partial class PaidLeaveList : Form {
        /*
         * 指定された FlowLayoutPanel 内の全コントロールを安全に削除するメソッド。
         * 
         * 【重要ポイント】
         * 1. Controls.Clear() ではコントロールのハンドルが破棄されず、メモリリークの原因になる。
         *    → 必ず Dispose() を明示的に呼び出す必要がある。
         * 
         * 2. FlowLayoutPanel はコントロール削除のたびにレイアウト再計算・再描画が走るため、
         *    大量のコントロールを削除すると画面が激しくちらつく。
         *    → SuspendLayout() と WM_SETREDRAW で描画を一時停止し、最後にまとめて再描画する。
         * 
         * 3. コントロールは「後ろから」Dispose することが重要。
         *    → インデックスがずれず、安全に削除できる。
         */
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, bool wParam, int lParam);
        private const int WM_SETREDRAW = 0x000B;

        private CcFlowLayoutPanel[] arrayCcFlowLayoutPanels = new CcFlowLayoutPanel[5];
        private readonly DateTime _defaultDateTime = new(1900, 01, 01);
        private StaffLabel _parentStaffLabel;
        private int _timeOffCode;
        /*
         * Dao
         */
        private StaffMasterDao _staffMasterDao;
        private TimeOffMasterDao _timeOffMasterDao;
        /*
         * Vo
         */
        private ConnectionVo _connectionVo;
        private List<StaffMasterVo> _listStaffMasterVo;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="connectionVo"></param>
        /// <param name="screen"></param>
        public PaidLeaveList(ConnectionVo connectionVo, Screen screen) {
            /*
             * Dao
             */
            _connectionVo = connectionVo;
            _staffMasterDao = new(_connectionVo);
            /*
             * Vo
             */
            _listStaffMasterVo = _staffMasterDao.SelectAllStaffMaster(null, null, null, false);
            _timeOffMasterDao = new(connectionVo);
            /*
             * InitializeControl
             */
            InitializeComponent();
            /*
             * 配列にFlowLayoutPanelを格納
             */
            arrayCcFlowLayoutPanels[0] = this.CcFlowLayoutPanel1;
            arrayCcFlowLayoutPanels[1] = this.CcFlowLayoutPanel2;
            arrayCcFlowLayoutPanels[2] = this.CcFlowLayoutPanel3;
            arrayCcFlowLayoutPanels[3] = this.CcFlowLayoutPanel4;
            arrayCcFlowLayoutPanels[4] = this.CcFlowLayoutPanel5;

            this.CcFlowLayoutPanel1.DisplayText = "有給休暇";
            this.CcFlowLayoutPanel2.DisplayText = "指名休暇";
            this.CcFlowLayoutPanel3.DisplayText = "欠勤";
            this.CcFlowLayoutPanel4.DisplayText = "代休";
            this.CcFlowLayoutPanel5.DisplayText = "その他";

            /*
             * MenuStrip
             */
            List<string> listString = new() {"ToolStripMenuItemFile",
                                             "ToolStripMenuItemExit",
                                             "ToolStripMenuItemHelp"
            };
            this.CcMenuStrip1.ChangeEnable(listString);
            /*
             * CcFlowLayoutPanelStockの件数を初期化
             */
            this.CcLabelRecordCount.Text = "レコード数：0件";
            /*
             * CcMonthCalendarを初期化
             */
            this.CcLabelDate.Text = DateTime.Now.ToString("yyyy年MM月dd日 (dddd)");
            this.CcMonthCalendar1.TodayDate = DateTime.Now.Date;

            this.SetControls(this.CcMonthCalendar1.TodayDate);

            this.CcStatusStrip1.ToolStripStatusLabelDetail.Text = "InitializeSuccess";
            /*
             * Eventを登録する
             */
            this.CcMenuStrip1.Event_MenuStripEx_ToolStripMenuItem_Click += ToolStripMenuItem_Click;
        }

        /// <summary>
        /// 指定日のレコードを取得し、CcFlowLayoutPanel1～5にStaffLabelを配置する
        /// </summary>
        /// <param name="targetDate"></param>
        private void SetControls(DateTime targetDate) {
            RemoveControls(CcFlowLayoutPanel1);
            RemoveControls(CcFlowLayoutPanel2);
            RemoveControls(CcFlowLayoutPanel3);
            RemoveControls(CcFlowLayoutPanel4);
            RemoveControls(CcFlowLayoutPanel5);

            List<TimeOffMasterVo> listTimeOffMasterVo = _timeOffMasterDao.SelectAllTimeOffMaster(targetDate.Date);                    // 指定日のレコードを取得する
            if (listTimeOffMasterVo.Count == 0)
                return;

            List<StaffMasterVo> listStaffMasterVo = _staffMasterDao.SelectSomeStaffMaster(listTimeOffMasterVo.Select(x => x.StaffCode).ToArray());

            foreach (TimeOffMasterVo timeOffMasterVo in listTimeOffMasterVo.OrderBy(x => x.Code)) {
                this.arrayCcFlowLayoutPanels[timeOffMasterVo.Code - 1].Controls.Add(GetOneStaffLabel(listStaffMasterVo.Find(x => x.StaffCode == timeOffMasterVo.StaffCode)));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonEx_Click(object sender, EventArgs e) {
            switch (((CcButton)sender).Name) {
                case "CcButtonFullTime":
                    try {
                        this.RemoveControls(CcFlowLayoutPanelStock);
                        this.CcFlowLayoutPanelStock.Controls.AddRange(GetArrayStaffLabel(_listStaffMasterVo, this.GetAllStaffLabel(), "CcButtonFullTime"));
                        this.CcStatusStrip1.ToolStripStatusLabelDetail.Text = "社員等で初期化しました";
                    } catch (Exception exception) {
                        MessageBox.Show(exception.Message);
                    }
                    break;
                case "CcButtonPartTime":
                    try {
                        this.RemoveControls(CcFlowLayoutPanelStock);
                        this.CcFlowLayoutPanelStock.Controls.AddRange(GetArrayStaffLabel(_listStaffMasterVo, this.GetAllStaffLabel(), "CcButtonPartTime"));
                        this.CcStatusStrip1.ToolStripStatusLabelDetail.Text = "アルバイトで初期化しました";
                    } catch (Exception exception) {
                        MessageBox.Show(exception.Message);
                    }
                    break;
                case "CcButtonLongTime":
                    try {
                        this.RemoveControls(CcFlowLayoutPanelStock);
                        this.CcFlowLayoutPanelStock.Controls.AddRange(GetArrayStaffLabel(_listStaffMasterVo, this.GetAllStaffLabel(), "CcButtonLongTime"));
                        this.CcStatusStrip1.ToolStripStatusLabelDetail.Text = "労供長期で初期化しました";
                    } catch (Exception exception) {
                        MessageBox.Show(exception.Message);
                    }
                    break;
                case "CcButtonShortTime":
                    try {
                        this.RemoveControls(CcFlowLayoutPanelStock);
                        this.CcFlowLayoutPanelStock.Controls.AddRange(GetArrayStaffLabel(_listStaffMasterVo, this.GetAllStaffLabel(), "CcButtonShortTime"));
                        this.CcStatusStrip1.ToolStripStatusLabelDetail.Text = "労供短期で初期化しました";
                    } catch (Exception exception) {
                        MessageBox.Show(exception.Message);
                    }
                    break;
                case "CcButtonTemporaryWorker":
                    try {
                        this.RemoveControls(CcFlowLayoutPanelStock);
                        this.CcFlowLayoutPanelStock.Controls.AddRange(GetArrayStaffLabel(_listStaffMasterVo, this.GetAllStaffLabel(), "CcButtonTemporaryWorker"));
                        this.CcStatusStrip1.ToolStripStatusLabelDetail.Text = "派遣で初期化しました";
                    } catch (Exception exception) {
                        MessageBox.Show(exception.Message);
                    }
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="listStaffMasterVo">AllStaffMasterVo</param>
        /// <param name="excludeListStaffMasterVo">CcFlowLayoutPanel2に配置されているStaffLabel</param>
        /// <param name="key"></param>
        /// <returns></returns>
        public StaffLabel[] GetArrayStaffLabel(List<StaffMasterVo> listStaffMasterVo, List<StaffMasterVo> excludeListStaffMasterVo, string key) {
            List<StaffMasterVo> newListStaffMasterVo = listStaffMasterVo.Where(x => !CreateStaffCodeList(excludeListStaffMasterVo).Contains(x.StaffCode)).ToList();
            switch (key) {
                case "CcButtonFullTime":                // 社員
                    newListStaffMasterVo = newListStaffMasterVo.FindAll(x => (x.Belongs == 10 || x.Belongs == 11 || x.Belongs == 14 || x.Belongs == 15) && x.RetirementFlag == false);
                    break;
                case "CcButtonPartTime":                // アルバイト
                    newListStaffMasterVo = newListStaffMasterVo.FindAll(x => x.Belongs == 12 && x.RetirementFlag == false);
                    break;
                case "CcButtonLongTime":                // 長期
                    newListStaffMasterVo = newListStaffMasterVo.FindAll(x => x.Belongs == 22 && (x.JobForm == 20 || x.JobForm == 22) && x.RetirementFlag == false);
                    break;
                case "CcButtonShortTime":               // 短期
                    newListStaffMasterVo = newListStaffMasterVo.FindAll(x => x.Belongs == 22 && (x.JobForm == 21 || x.JobForm == 23) && x.RetirementFlag == false);
                    break;
                case "CcButtonTemporaryWorker":         // 派遣
                    newListStaffMasterVo = newListStaffMasterVo.FindAll(x => x.Belongs == 13 && x.RetirementFlag == false);
                    break;
            }
            this.CcLabelRecordCount.Text = $"レコード数：{newListStaffMasterVo.Count}件";

            StaffLabel[] _arrayControl = new StaffLabel[newListStaffMasterVo.Count];
            int i = 0;
            foreach (StaffMasterVo staffMasterVo in newListStaffMasterVo.OrderBy(x => x.NameKana)) {
                _arrayControl[i] = GetOneStaffLabel(staffMasterVo);
                i++;
            }
            return _arrayControl;
        }

        /// <summary>
        /// CcFlowLayoutPanel2に配置されているStaffLabelを走査する
        /// </summary>
        /// <returns></returns>
        public List<StaffMasterVo> GetAllStaffLabel() {
            List<StaffMasterVo> listStaffMasterVo = new();

            // 対象となる FlowLayoutPanel を配列でまとめる
            CcFlowLayoutPanel[] flowLayoutPanel = { this.CcFlowLayoutPanel1,
                                                    this.CcFlowLayoutPanel2,
                                                    this.CcFlowLayoutPanel3,
                                                    this.CcFlowLayoutPanel4,
                                                    this.CcFlowLayoutPanel5 };
            foreach (CcFlowLayoutPanel panel in flowLayoutPanel) {
                foreach (Control control in panel.Controls) {
                    if (control is StaffLabel staffLabel)
                        listStaffMasterVo.Add(staffLabel.StaffMasterVo);
                }
            }
            return listStaffMasterVo;
        }

        private List<int> CreateStaffCodeList(List<StaffMasterVo> listStaffMasterVo) {
            List<int> list = new();
            foreach (StaffMasterVo staffMasterVo in listStaffMasterVo)
                list.Add(staffMasterVo.StaffCode);
            return list;
        }

        public StaffLabel GetOneStaffLabel(StaffMasterVo staffMasterVo) {
            StaffLabel staffLabel = new(staffMasterVo);
            staffLabel.ParentControl = null;
            staffLabel.OccupationCode = 99;
            staffLabel.Memo = string.Empty;
            staffLabel.MemoFlag = false;
            staffLabel.ProxyFlag = false;
            staffLabel.RollCallFlag = false;
            staffLabel.RollCallYmdHms = _defaultDateTime;
            //// Eventを登録
            staffLabel.StaffLabel_ContextMenuStrip_Opened += ContextMenuStrip_Opened;
            staffLabel.StaffLabel_ToolStripMenuItem_Click += ToolStripMenuItem_Click;
            //staffLabel.StaffLabel_OnMouseClick += OnMouseClick;
            //staffLabel.StaffLabel_OnMouseDoubleClick += OnMouseDoubleClick;
            staffLabel.StaffLabel_OnMouseDown += OnMouseDown;
            //staffLabel.MouseMove += OnMouseMove;
            //staffLabel.MouseUp += OnMouseUp;
            return staffLabel;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContextMenuStrip_Opened(object sender, EventArgs e) {
            switch (((ContextMenuStrip)sender).SourceControl) {
                case StaffLabel staffLabel:
                    switch (staffLabel.Parent.Name) {
                        case "CcFlowLayoutPanelStock":
                            staffLabel.SetToolStripMenuItemEnables(null);                                           // ToolStripMenuItemを有効・無効にする
                            /*
                             * ClickされたStaffLabelを退避する
                             * ClickされたCodeを退避する
                             */
                            _parentStaffLabel = null;
                            _timeOffCode = 0;
                            break;
                        case "CcFlowLayoutPanel1":
                        case "CcFlowLayoutPanel2":
                        case "CcFlowLayoutPanel3":
                        case "CcFlowLayoutPanel4":
                        case "CcFlowLayoutPanel5":
                            staffLabel.SetToolStripMenuItemEnables(new string[] { "ToolStripMenuItemStaffMemo" }); // ToolStripMenuItemStaffMemoを有効にする
                            /*
                             * ClickされたStaffLabelを退避する
                             * ClickされたCodeを退避する
                             */
                            _parentStaffLabel = staffLabel;
                            _timeOffCode = int.Parse(((CcFlowLayoutPanel)staffLabel.Parent).Tag.ToString());
                            break;
                    }
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItem_Click(object sender, EventArgs e) {
            switch (((ToolStripMenuItem)sender).Name) {
                case "ToolStripMenuItemStaffMemo":
                    Remark remark = new(_connectionVo,
                                        CcMonthCalendar1.SelectionStart.Date,                       // 指定日
                                        _timeOffCode,                                               // コード
                                        _parentStaffLabel.StaffMasterVo.StaffCode);                 // 社員コード
                    remark.ShowDialog();
                    break;
                case "ToolStripMenuItemExit":
                    Close();
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CcMonthCalendar1_DateSelected(object sender, DateRangeEventArgs e) {
            this.CcLabelDate.Text = e.Start.ToString("yyyy年MM月dd日 (dddd)");
            /*
             * 指定日が変更されたとき、CcFlowLayoutPanelStockに配置されているStaffLabelを削除する
             * 削除しないと重複する可能性がある
             */
            RemoveControls(CcFlowLayoutPanelStock);
            this.CcLabelRecordCount.Text = "レコード数：0件";
            /*
             * 指定日のレコードを取得し、CcFlowLayoutPanel1～5にStaffLabelを配置する
             */
            this.SetControls(e.Start.Date);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ccFlowLayoutPanel"></param>
        public void RemoveControls(CcFlowLayoutPanel ccFlowLayoutPanel) {
            SendMessage(ccFlowLayoutPanel.Handle, WM_SETREDRAW, false, 0);                  // 描画停止

            try {
                ccFlowLayoutPanel.SuspendLayout();                                          // レイアウトの一時停止
                for (int i = ccFlowLayoutPanel.Controls.Count - 1; 0 <= i; i--)
                    ccFlowLayoutPanel.Controls[i].Dispose();
            } finally {
                ccFlowLayoutPanel.ResumeLayout(true);                                       // レイアウトの再開

                SendMessage(ccFlowLayoutPanel.Handle, WM_SETREDRAW, true, 0);               // 描画再開

                ccFlowLayoutPanel.Invalidate();
                ccFlowLayoutPanel.Update();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PaidLeaveList_FormClosing(object sender, FormClosingEventArgs e) {

        }

        /*
         * 
         * Drag & Drop
         * 
         */
        private CcFlowLayoutPanel _beforeParentControl = null;
        private CcFlowLayoutPanel _afterParentControl = null;

        /// <summary>
        /// StaffLabelをOnMouseDownしたとき、ドラッグアンドドロップの開始処理を行う
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnMouseDown(object sender, MouseEventArgs e) {
            _beforeParentControl = (CcFlowLayoutPanel)((StaffLabel)sender).Parent;                          // Drag前の親コントロールを保存
            if (e.Button == MouseButtons.Left)
                ((StaffLabel)sender).DoDragDrop(sender, DragDropEffects.Move);
        }

        private void CcFlowLayoutPanel_DragEnter(object sender, DragEventArgs e) {

        }

        private void CcFlowLayoutPanel_DragOver(object sender, DragEventArgs e) {
            e.Effect = DragDropEffects.Move;
        }

        private void CcFlowLayoutPanel_DragLeave(object sender, EventArgs e) {

        }

        private void CcFlowLayoutPanel_DragDrop(object sender, DragEventArgs e) {
            _afterParentControl = (CcFlowLayoutPanel)sender;                                                // Drop後の親コントロールを保存
            this.CcStatusStrip1.ToolStripStatusLabelDetail.Text = $"移動元：{_beforeParentControl.Name} → 移動先：{_afterParentControl.Name}";

            if (object.ReferenceEquals(_beforeParentControl, _afterParentControl)) {
                this.CcStatusStrip1.ToolStripStatusLabelDetail.Text = "同じ場所にドロップされました";
            } else {
                DateTime targetDate = this.CcMonthCalendar1.TodayDate.Date;                                                     // TargetDateはCcMonthCalendar1のTodayDateを使用する
                StaffLabel staffLabel = (StaffLabel)e.Data.GetData(typeof(StaffLabel));                                         // ドロップされたStaffLabelを取得する

                _beforeParentControl.Controls.Remove(staffLabel);
                switch (_beforeParentControl.Name) {
                    case "CcFlowLayoutPanelStock":

                        break;
                    case "CcFlowLayoutPanel1":
                    case "CcFlowLayoutPanel2":
                    case "CcFlowLayoutPanel3":
                    case "CcFlowLayoutPanel4":
                    case "CcFlowLayoutPanel5":

                        break;

                }

                _afterParentControl.Controls.Add(staffLabel);
                switch (_afterParentControl.Name) {
                    case "CcFlowLayoutPanelStock":
                        this.CcStatusStrip1.ToolStripStatusLabelDetail.Text = "SQL Delete";
                        try {
                            if (_timeOffMasterDao.ExistenceTimeOffMaster(targetDate, staffLabel.StaffMasterVo.StaffCode)) {
                                _timeOffMasterDao.DeleteOneTimeOffMaster(targetDate, staffLabel.StaffMasterVo.StaffCode);
                                this.CcStatusStrip1.ToolStripStatusLabelDetail.Text = "１件のレコードを削除しました";
                            } else {
                                this.CcStatusStrip1.ToolStripStatusLabelDetail.Text = "Deleteするレコードが見つかりません";
                            }
                        } catch (Exception exception) {
                            MessageBox.Show(exception.Message);
                        }
                        break;
                    case "CcFlowLayoutPanel1":
                    case "CcFlowLayoutPanel2":
                    case "CcFlowLayoutPanel3":
                    case "CcFlowLayoutPanel4":
                    case "CcFlowLayoutPanel5":
                        this.CcStatusStrip1.ToolStripStatusLabelDetail.Text = "SQL Insert OR Update";
                        try {
                            if (_timeOffMasterDao.ExistenceTimeOffMaster(targetDate, staffLabel.StaffMasterVo.StaffCode)) {
                                _timeOffMasterDao.UpdateOneTimeOffMaster(targetDate,
                                                                         int.Parse(_afterParentControl.Tag.ToString()),
                                                                         staffLabel.StaffMasterVo.StaffCode);
                                this.CcStatusStrip1.ToolStripStatusLabelDetail.Text = "１件のレコードをUPDATEしました";
                            } else {

                                _timeOffMasterDao.InsertOneTimeOffMaster(targetDate,
                                                                         int.Parse(_afterParentControl.Tag.ToString()),
                                                                         staffLabel.StaffMasterVo.StaffCode);
                                this.CcStatusStrip1.ToolStripStatusLabelDetail.Text = "１件のレコードをINSERTしました";
                            }
                        } catch (Exception exception) {
                            MessageBox.Show(exception.Message);
                        }
                        break;
                }
            }
        }
    }
}
