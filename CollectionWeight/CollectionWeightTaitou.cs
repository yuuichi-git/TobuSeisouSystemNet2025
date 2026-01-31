/*
 * 2024-12-30
 */
using ControlEx;

using Dao;

using Vo;

namespace Collection {
    public partial class CollectionWeightTaitou : Form {
        /*
         * Dao
         */
        private CollectionTaitouDao _CollectionWeightTaitouDao;
        /*
         * Vo
         */
        private ConnectionVo _connectionVo;
        private CollectionWeightTaitouVo _collectionWeightTaitouVo;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionVo"></param>
        /// <param name="operationDate"></param>
        public CollectionWeightTaitou(ConnectionVo connectionVo, DateTime operationDate) {
            /*
             * Dao
             */
            _CollectionWeightTaitouDao = new(connectionVo);
            /*
             * Vo
             */
            _connectionVo = connectionVo;
            _collectionWeightTaitouVo = new();
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
            MenuStripEx1.ChangeEnable(listString);

            this.InitializeControl();
            this.StatusStripEx1.ToolStripStatusLabelDetail.Text = "Initialize Success";
            /*
             * Eventを登録する
             */
            MenuStripEx1.Event_MenuStripEx_ToolStripMenuItem_Click += ToolStripMenuItem_Click;
            // ここで値をセットして発火させる
            this.DateTimePickerExOperationDate.SetValue(operationDate);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonExUpdate_Click(object sender, EventArgs e) {
            CollectionWeightTaitouVo collectionWeightTaitouVo = new();
            collectionWeightTaitouVo.OperationDate = this.DateTimePickerExOperationDate.GetDate();
            collectionWeightTaitouVo.Weight1Total = (int)this.NumericUpDownEx1.Value;
            collectionWeightTaitouVo.Weight2Total = (int)this.NumericUpDownEx2.Value;
            collectionWeightTaitouVo.Weight3Total = (int)this.NumericUpDownEx3.Value;
            collectionWeightTaitouVo.Weight4Total = (int)this.NumericUpDownEx4.Value;
            collectionWeightTaitouVo.Weight5Total = (int)this.NumericUpDownEx5.Value;
            collectionWeightTaitouVo.Weight6Total = (int)this.NumericUpDownEx6.Value;
            collectionWeightTaitouVo.Weight7Total = (int)this.NumericUpDownEx7.Value;
            collectionWeightTaitouVo.Weight8Total = (int)this.NumericUpDownEx8.Value;
            collectionWeightTaitouVo.Weight9Total = (int)this.NumericUpDownEx9.Value;
            if (_CollectionWeightTaitouDao.ExistenceCollectionWeightTaitou(this.DateTimePickerExOperationDate.GetDate())) {
                try {
                    int count = _CollectionWeightTaitouDao.UpdateOneCollectionWeightTaitou(collectionWeightTaitouVo);
                    this.StatusStripEx1.ToolStripStatusLabelDetail.Text = string.Concat(count, " 件のレコードが更新されました。");
                } catch (Exception exception) {
                    MessageBox.Show(exception.Message);
                }
            } else {
                try {
                    int count = _CollectionWeightTaitouDao.InsertOneCollectionWeightTaitou(collectionWeightTaitouVo);
                    this.StatusStripEx1.ToolStripStatusLabelDetail.Text = string.Concat(count, " 件のレコードが更新されました。");
                } catch (Exception exception) {
                    MessageBox.Show(exception.Message);
                }
            }
            this.Close();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItem_Click(object sender, EventArgs e) {
            switch (((ToolStripMenuItem)sender).Name) {
                case "ToolStripMenuItemExit":
                    Close();
                    break;

            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void InitializeControl() {
            this.NumericUpDownEx1.Value = 0;
            this.NumericUpDownEx2.Value = 0;
            this.NumericUpDownEx3.Value = 0;
            this.NumericUpDownEx4.Value = 0;
            this.NumericUpDownEx5.Value = 0;
            this.NumericUpDownEx6.Value = 0;
            this.NumericUpDownEx7.Value = 0;
            this.NumericUpDownEx8.Value = 0;
            this.NumericUpDownEx9.Value = 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DateTimePickerExOperationDate_ValueChanged(object sender, EventArgs e) {
            this.InitializeControl();
            if (_CollectionWeightTaitouDao.ExistenceCollectionWeightTaitou(((CcDateTime)sender).GetDate())) {
                try {
                    _collectionWeightTaitouVo = _CollectionWeightTaitouDao.SelectOneCollectionWeightTaitou(this.DateTimePickerExOperationDate.GetDate());
                    this.NumericUpDownEx1.Value = _collectionWeightTaitouVo.Weight1Total;
                    this.NumericUpDownEx2.Value = _collectionWeightTaitouVo.Weight2Total;
                    this.NumericUpDownEx3.Value = _collectionWeightTaitouVo.Weight3Total;
                    this.NumericUpDownEx4.Value = _collectionWeightTaitouVo.Weight4Total;
                    this.NumericUpDownEx5.Value = _collectionWeightTaitouVo.Weight5Total;
                    this.NumericUpDownEx6.Value = _collectionWeightTaitouVo.Weight6Total;
                    this.NumericUpDownEx7.Value = _collectionWeightTaitouVo.Weight7Total;
                    this.NumericUpDownEx8.Value = _collectionWeightTaitouVo.Weight8Total;
                    this.NumericUpDownEx9.Value = _collectionWeightTaitouVo.Weight9Total;
                } catch (Exception exception) {
                    MessageBox.Show(exception.Message);
                }
            }
        }
    }
}
