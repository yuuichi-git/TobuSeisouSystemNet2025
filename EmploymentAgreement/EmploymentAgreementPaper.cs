/*
 * 2024-11-10
 */
using System.Drawing.Printing;

using Common;

using Dao;

using Vo;

namespace EmploymentAgreement {
    public partial class EmploymentAgreementPaper : Form {
        private readonly DateTime _defaultDateTime = new(1900, 01, 01);
        /*
         * インスタンス作成
         */
        private readonly DateUtility _dateUtility = new();
        private readonly PrintUtility _printUtility = new();
        private readonly StampUtility _stampUtility = new();
        /*
         * Dao
         */
        private BelongsMasterDao _belongsMasterDao;
        private JobDescriptionMasterDao _jobDescriptionMasterDao;
        private StaffMasterDao _staffMasterDao;
        /*
         * Vo
         */
        private ConnectionVo _connectionVo;
        private EmploymentAgreementVo _employmentAgreementVo;
        private StaffMasterVo _staffMasterVo;
        /*
         * Dictionary
         */
        private readonly Dictionary<int, string> _dictionaryBelongs = new();
        private readonly Dictionary<int, string> _dictionaryJobDescription = new();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionVo"></param>
        /// <param name="staffMasterVo"></param>
        /// <param name="employmentAgreementVo"></param>
        public EmploymentAgreementPaper(ConnectionVo connectionVo, int code, int staffCode, EmploymentAgreementVo employmentAgreementVo) {
            /*
             * Dao
             */
            _belongsMasterDao = new(connectionVo);
            _jobDescriptionMasterDao = new(connectionVo);
            _staffMasterDao = new(connectionVo);
            /*
             * Vo
             */
            _connectionVo = connectionVo;
            _staffMasterVo = _staffMasterDao.SelectOneStaffMaster(staffCode);
            _employmentAgreementVo = employmentAgreementVo;
            /*
             * Dictionary
             */
            foreach (BelongsMasterVo belongsMasterVo in _belongsMasterDao.SelectAllBelongsMaster())
                _dictionaryBelongs.Add(belongsMasterVo.Code, belongsMasterVo.Name);
            foreach (JobDescriptionMasterVo jobDescriptionMasterVo in _jobDescriptionMasterDao.SelectAllJobDescriptionMaster())
                _dictionaryJobDescription.Add(jobDescriptionMasterVo.Code, jobDescriptionMasterVo.Name);
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
            // プリンターの一覧を取得後、通常使うプリンター名をセットする
            _printUtility.SetAllPrinterForComboBoxEx(ComboBoxExPrinter);

            this.ComboBoxExBaseAddress.Text = _employmentAgreementVo.BaseLocation;
            this.LabelExCurrentAddress.Text = _staffMasterVo.CurrentAddress;

            /// <summary>
            /// 契約書識別コード
            /// 10:長期雇用契約（新産別）
            /// 11:長期雇用契約（自運労運転士）
            /// 12:長期雇用契約（自運労作業員）
            /// 13:短期雇用契約
            /// 20:継続アルバイト契約
            /// 21:体験アルバイト契約
            /// 22:嘱託雇用契約社員
            /// 23:パートタイマー
            /// 30:誓約書
            /// 40:失墜行為確認書
            /// 50:満了一カ月前通知
            switch (code) {
                case 10: // 長期雇用契約（新産別）
                    this.SpreadList.ActiveSheetIndex = 4;
                    this.PutContractExpirationLongJob新産別();
                    break;
                case 11: // 長期雇用契約（自運労運転士）
                    this.SpreadList.ActiveSheetIndex = 5;
                    this.PutContractExpirationLongJob自運労運転士();
                    break;
                case 12: // 長期雇用契約（自運労作業員）
                    this.SpreadList.ActiveSheetIndex = 6;
                    this.PutContractExpirationLongJob自運労作業員();
                    break;
                case 13: // 短期雇用契約

                    break;
                case 20: // 継続アルバイト契約
                    this.SpreadList.ActiveSheetIndex = 1;
                    this.PutContractExpirationPartTimeJob();
                    break;
                case 21: // 体験アルバイト契約
                    this.SpreadList.ActiveSheetIndex = 0;
                    this.PutExpirationJob();
                    break;
                case 22: // 嘱託雇用契約社員
                    this.SpreadList.ActiveSheetIndex = 2;
                    this.PutContractExpirationPartTimeEmployee();
                    break;
                case 23: // パートタイマー
                    this.SpreadList.ActiveSheetIndex = 3;
                    this.PutContractExpirationPartTimer();
                    break;
                case 30: // 誓約書
                    this.SpreadList.ActiveSheetIndex = 7;
                    this.PutContractExpirationWrittenPledge();
                    break;
                case 40: // 失墜行為確認書

                    break;
                case 50: // 満了一カ月前通知

                    break;

            }
        }

        /// <summary>
        /// 体験アルバイト契約
        /// </summary>
        private void PutExpirationJob() {
            // 【労働者】住所
            this.SheetView体験期間契約.Cells[5, 23].Text = _staffMasterVo.CurrentAddress;
            // 【労働者】フリガナ
            this.SheetView体験期間契約.Cells[7, 23].Text = _staffMasterVo.OtherNameKana;
            // 【労働者】生年月日
            this.SheetView体験期間契約.Cells[10, 23].Text = string.Concat(StaffMasterVo.BirthDate.ToString("yyyy年MM月dd日"), " (", _dateUtility.GetAge(_staffMasterVo.BirthDate), "歳)");
            // 雇用形態
            this.SheetView体験期間契約.Cells[11, 8].Text = _dictionaryBelongs[_staffMasterVo.Belongs];
            // 契約期間
            this.SheetView体験期間契約.Cells[12, 9].Text = _employmentAgreementVo.ContractExpirationPeriodString;
            // 就業の場所
            this.SheetView体験期間契約.Cells[19, 9].Text = _employmentAgreementVo.BaseLocation;
            // 従事すべき業務の内容
            this.SheetView体験期間契約.Cells[20, 9].Text = _dictionaryJobDescription[_employmentAgreementVo.JobDescription];
            // 始業・就業の時刻
            this.SheetView体験期間契約.Cells[22, 9].Text = _employmentAgreementVo.WorkTime;
            // 休憩時間
            this.SheetView体験期間契約.Cells[24, 9].Text = _employmentAgreementVo.BreakTime;
            // 給料区分
            this.SheetView体験期間契約.Cells[28, 9].Text = _employmentAgreementVo.PayDetail;
            // 給料金額
            this.SheetView体験期間契約.Cells[28, 30].Value = _employmentAgreementVo.Pay != 0 ? _employmentAgreementVo.Pay : "---";
            // 交通費区分
            this.SheetView体験期間契約.Cells[29, 9].Text = _employmentAgreementVo.TravelCostDetail;
            // 交通費
            this.SheetView体験期間契約.Cells[29, 30].Value = _employmentAgreementVo.TravelCost;

            // 印影
            this.SheetView体験期間契約.Cells[8, 33].Value = _stampUtility.CreateStamp(_staffMasterVo.StampPicture);
        }

        /// <summary>
        /// 長期アルバイト契約
        /// </summary>
        private void PutContractExpirationPartTimeJob() {
            // 【労働者】住所
            this.SheetViewアルバイト契約.Cells[5, 23].Text = _staffMasterVo.CurrentAddress;
            // 【労働者】フリガナ
            this.SheetViewアルバイト契約.Cells[7, 23].Text = _staffMasterVo.OtherNameKana;
            // 【労働者】生年月日
            this.SheetViewアルバイト契約.Cells[10, 23].Text = string.Concat(StaffMasterVo.BirthDate.ToString("yyyy年MM月dd日"), " (", _dateUtility.GetAge(_staffMasterVo.BirthDate), "歳)");
            // 雇用形態
            this.SheetViewアルバイト契約.Cells[11, 8].Text = _dictionaryBelongs[_staffMasterVo.Belongs];
            // 契約期間
            this.SheetViewアルバイト契約.Cells[12, 9].Text = _employmentAgreementVo.ContractExpirationPeriodString;
            // 就業の場所
            this.SheetViewアルバイト契約.Cells[20, 9].Text = _employmentAgreementVo.BaseLocation;
            // 従事すべき業務の内容
            this.SheetViewアルバイト契約.Cells[21, 9].Text = _dictionaryJobDescription[_employmentAgreementVo.JobDescription];
            // 始業・就業の時刻
            this.SheetViewアルバイト契約.Cells[23, 9].Text = _employmentAgreementVo.WorkTime;
            // 休憩時間
            this.SheetViewアルバイト契約.Cells[25, 9].Text = _employmentAgreementVo.BreakTime;
            // 給料区分
            this.SheetViewアルバイト契約.Cells[29, 9].Text = _employmentAgreementVo.PayDetail;
            // 給料金額
            this.SheetViewアルバイト契約.Cells[29, 30].Value = _employmentAgreementVo.Pay != 0 ? _employmentAgreementVo.Pay : "---";
            // 交通費区分
            this.SheetViewアルバイト契約.Cells[30, 9].Text = _employmentAgreementVo.TravelCostDetail;
            // 交通費
            this.SheetViewアルバイト契約.Cells[30, 30].Value = _employmentAgreementVo.TravelCost;

            // 印影
            this.SheetViewアルバイト契約.Cells[8, 33].Value = _stampUtility.CreateStamp(_staffMasterVo.StampPicture);
        }

        /// <summary>
        /// 嘱託雇用契約社員
        /// </summary>
        private void PutContractExpirationPartTimeEmployee() {
            // 【労働者】住所
            this.SheetView嘱託雇用契約社員.Cells[5, 23].Text = _staffMasterVo.CurrentAddress;
            // 【労働者】フリガナ
            this.SheetView嘱託雇用契約社員.Cells[7, 23].Text = _staffMasterVo.OtherNameKana;
            // 【労働者】生年月日
            this.SheetView嘱託雇用契約社員.Cells[10, 23].Text = string.Concat(StaffMasterVo.BirthDate.ToString("yyyy年MM月dd日"), " (", _dateUtility.GetAge(_staffMasterVo.BirthDate), "歳)");
            // 雇用形態
            this.SheetView嘱託雇用契約社員.Cells[11, 8].Text = _dictionaryBelongs[_staffMasterVo.Belongs];
            // 契約期間
            this.SheetView嘱託雇用契約社員.Cells[12, 9].Text = _employmentAgreementVo.ContractExpirationPeriodString;
            // 就業の場所
            this.SheetView嘱託雇用契約社員.Cells[20, 9].Text = _employmentAgreementVo.BaseLocation;
            // 従事すべき業務の内容
            this.SheetView嘱託雇用契約社員.Cells[21, 9].Text = _dictionaryJobDescription[_employmentAgreementVo.JobDescription];
            // 始業・就業の時刻
            this.SheetView嘱託雇用契約社員.Cells[23, 9].Text = _employmentAgreementVo.WorkTime;
            // 休憩時間
            this.SheetView嘱託雇用契約社員.Cells[25, 9].Text = _employmentAgreementVo.BreakTime;
            // 給料区分
            this.SheetView嘱託雇用契約社員.Cells[32, 9].Text = _employmentAgreementVo.PayDetail;
            // 給料金額
            this.SheetView嘱託雇用契約社員.Cells[32, 30].Value = _employmentAgreementVo.Pay != 0 ? _employmentAgreementVo.Pay : "---";
            // 交通費区分
            this.SheetView嘱託雇用契約社員.Cells[35, 9].Text = _employmentAgreementVo.PayDetail;
            // 交通費
            this.SheetView嘱託雇用契約社員.Cells[35, 30].Value = _employmentAgreementVo.TravelCost != 0 ? _employmentAgreementVo.TravelCost : "---";

            // 印影
            this.SheetView嘱託雇用契約社員.Cells[8, 33].Value = _stampUtility.CreateStamp(_staffMasterVo.StampPicture);
        }

        /// <summary>
        /// パートタイマー
        /// </summary>
        private void PutContractExpirationPartTimer() {
            // 【労働者】住所
            this.SheetViewパートタイマー.Cells[5, 23].Text = _staffMasterVo.CurrentAddress;
            // 【労働者】フリガナ
            this.SheetViewパートタイマー.Cells[7, 23].Text = _staffMasterVo.OtherNameKana;
            // 【労働者】生年月日
            this.SheetViewパートタイマー.Cells[10, 23].Text = string.Concat(StaffMasterVo.BirthDate.ToString("yyyy年MM月dd日"), " (", _dateUtility.GetAge(_staffMasterVo.BirthDate), "歳)");
            // 雇用形態
            this.SheetViewパートタイマー.Cells[11, 8].Text = _dictionaryBelongs[_staffMasterVo.Belongs];
            // 契約期間
            this.SheetViewパートタイマー.Cells[12, 9].Text = _employmentAgreementVo.ContractExpirationPeriodString;
            // 就業の場所
            this.SheetViewパートタイマー.Cells[19, 9].Text = _employmentAgreementVo.BaseLocation;
            // 従事すべき業務の内容
            this.SheetViewパートタイマー.Cells[20, 9].Text = _dictionaryJobDescription[_employmentAgreementVo.JobDescription];
            // 始業・就業の時刻
            this.SheetViewパートタイマー.Cells[22, 9].Text = _employmentAgreementVo.WorkTime;
            // 休憩時間
            this.SheetViewパートタイマー.Cells[24, 9].Text = _employmentAgreementVo.BreakTime;
            // 給料区分
            this.SheetViewパートタイマー.Cells[28, 9].Text = _employmentAgreementVo.PayDetail;
            // 給料金額
            this.SheetViewパートタイマー.Cells[28, 30].Value = _employmentAgreementVo.Pay != 0 ? _employmentAgreementVo.Pay : "---";

            // 印影
            this.SheetViewパートタイマー.Cells[8, 33].Value = _stampUtility.CreateStamp(_staffMasterVo.StampPicture);
        }

        /// <summary>
        /// 継続就労契約書（新産別）
        /// </summary>
        private void PutContractExpirationLongJob新産別() {
            // 氏名カナ
            this.SheetView長期雇用契約新産別.Cells[1, 1].Text = _staffMasterVo.OtherNameKana;
            // 契約期間
            this.SheetView長期雇用契約新産別.Cells[25, 12].Text = _employmentAgreementVo.ContractExpirationPeriodString;
            // 契約日
            this.SheetView長期雇用契約新産別.Cells[28, 2].Text = _dateUtility.GetDateTimeNowJp(DateTime.Now.Date);

            // 印影
            this.SheetView長期雇用契約新産別.Cells[45, 28].Value = _stampUtility.CreateStamp(_staffMasterVo.StampPicture);
        }

        /// <summary>
        /// 継続就労契約書（自運労運転士）
        /// </summary>
        private void PutContractExpirationLongJob自運労運転士() {
            // 氏名カナ
            this.SheetView長期雇用契約自運労運転士.Cells[1, 1].Text = _staffMasterVo.OtherNameKana;
            // 契約期間
            this.SheetView長期雇用契約自運労運転士.Cells[25, 12].Text = _employmentAgreementVo.ContractExpirationPeriodString;
            // 契約日
            this.SheetView長期雇用契約自運労運転士.Cells[28, 2].Text = _dateUtility.GetDateTimeNowJp(DateTime.Now.Date);

            // 印影
            this.SheetView長期雇用契約自運労運転士.Cells[45, 28].Value = _stampUtility.CreateStamp(_staffMasterVo.StampPicture);
        }

        /// <summary>
        /// 継続就労契約書（自運労作業員）
        /// </summary>
        private void PutContractExpirationLongJob自運労作業員() {
            // 氏名カナ
            this.SheetView長期雇用契約自運労作業員.Cells[1, 1].Text = _staffMasterVo.OtherNameKana;
            // 契約期間
            this.SheetView長期雇用契約自運労作業員.Cells[25, 12].Text = _employmentAgreementVo.ContractExpirationPeriodString;
            // 契約日
            this.SheetView長期雇用契約自運労作業員.Cells[28, 2].Text = _dateUtility.GetDateTimeNowJp(DateTime.Now.Date);

            // 印影
            this.SheetView長期雇用契約自運労作業員.Cells[45, 28].Value = _stampUtility.CreateStamp(_staffMasterVo.StampPicture);
        }

        /// <summary>
        /// 誓約書
        /// </summary>
        private void PutContractExpirationWrittenPledge() {
            // 氏名カナ
            this.SheetView誓約書.Cells[1, 1].Text = _staffMasterVo.OtherNameKana;
            // 契約日
            this.SheetView誓約書.Cells[40, 17].Text = _dateUtility.GetDateTimeNowJp(DateTime.Now.Date);

            // 印影
            this.SheetView誓約書.Cells[42, 30].Value = _stampUtility.CreateStamp(_staffMasterVo.StampPicture);
        }

        /*
         * Print
         */
        private PrintDocument _printDocument = new();
        private void ButtonExPrint_Click(object sender, EventArgs e) {
            // Eventを登録
            _printDocument.PrintPage += new PrintPageEventHandler(PrintDocument_PrintPage);
            //// 出力先プリンタを指定します。
            //_printDocument.PrinterSettings.PrinterName = this.HComboBoxExPrinterName.Text;
            // 用紙の向きを設定(横：true、縦：false)
            _printDocument.DefaultPageSettings.Landscape = false;
            /*
             * プリンタがサポートしている用紙サイズを調べる
             */
            foreach (PaperSize paperSize in _printDocument.PrinterSettings.PaperSizes) {
                // A4用紙に設定する
                if (paperSize.Kind == PaperKind.A4) {
                    _printDocument.DefaultPageSettings.PaperSize = paperSize;
                    break;
                }
            }
            // 印刷部数を指定します。
            _printDocument.PrinterSettings.Copies = 1;
            // 片面印刷に設定します。
            _printDocument.PrinterSettings.Duplex = Duplex.Default;
            // カラー印刷に設定します。
            _printDocument.PrinterSettings.DefaultPageSettings.Color = true;
            // 印刷する
            _printDocument.Print();
        }
        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e) {
            int sheetNumber = SpreadList.ActiveSheetIndex;
            // 印刷ページ（1ページ目）の描画を行う
            Rectangle rectangle = new(e.PageBounds.X, e.PageBounds.Y, e.PageBounds.Width, e.PageBounds.Height);
            // e.Graphicsへ出力(page パラメータは、０からではなく１から始まります)
            SpreadList.OwnerPrintDraw(e.Graphics, rectangle, sheetNumber, 1);
            // 印刷終了を指定
            e.HasMorePages = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBoxExBaseAddress_SelectedIndexChanged(object sender, EventArgs e) {
            // 【使用者】事業場所在地
            this.SpreadList.ActiveSheet.Cells[5, 6].Text = this.ComboBoxExBaseAddress.Text;
        }

        private void EmploymentAgreementPaper_Load(object sender, EventArgs e) {

        }

        /*
         * Setter Getter
         */
        public StaffMasterVo StaffMasterVo {
            get => this._staffMasterVo;
            set => this._staffMasterVo = value;
        }
    }
}
