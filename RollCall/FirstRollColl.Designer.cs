namespace RollCall {
    partial class FirstRollColl {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FirstRollColl));
            TableLayoutPanelExBase = new CcControl.CcTableLayoutPanel();
            MenuStripEx1 = new CcControl.CcMenuStrip();
            StatusStripEx1 = new CcControl.CcStatusStrip();
            PanelExUp = new CcControl.CcPanel();
            ButtonExUpdate = new CcControl.CcButton();
            CheckBoxEx1 = new CcControl.CcCheckBox();
            groupBoxEx2 = new CcControl.CcGroupBox();
            ComboBoxEx8 = new CcControl.CcComboBox();
            ComboBoxEx7 = new CcControl.CcComboBox();
            labelEx9 = new CcControl.CcLabel();
            labelEx8 = new CcControl.CcLabel();
            groupBoxEx1 = new CcControl.CcGroupBox();
            ComboBoxExWeather = new CcControl.CcComboBox();
            labelEx7 = new CcControl.CcLabel();
            ComboBoxEx5 = new CcControl.CcComboBox();
            labelEx6 = new CcControl.CcLabel();
            ComboBoxEx4 = new CcControl.CcComboBox();
            labelEx5 = new CcControl.CcLabel();
            ComboBoxEx3 = new CcControl.CcComboBox();
            labelEx4 = new CcControl.CcLabel();
            ComboBoxEx2 = new CcControl.CcComboBox();
            labelEx3 = new CcControl.CcLabel();
            ComboBoxEx1 = new CcControl.CcComboBox();
            labelEx2 = new CcControl.CcLabel();
            DateTimePickerExOperationDate = new CcControl.CcDateTime();
            labelEx1 = new CcControl.CcLabel();
            SpreadFirstRollCall = new FarPoint.Win.Spread.FpSpread(FarPoint.Win.Spread.LegacyBehaviors.None, resources.GetObject("TableLayoutPanelExBase.Controls"));
            SheetViewFirstRollCall = SpreadFirstRollCall.GetSheet(0);
            SheetViewPartTimeStaff = SpreadFirstRollCall.GetSheet(1);
            SheetViewFullStaff = SpreadFirstRollCall.GetSheet(2);
            TableLayoutPanelExBase.SuspendLayout();
            PanelExUp.SuspendLayout();
            groupBoxEx2.SuspendLayout();
            groupBoxEx1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)SpreadFirstRollCall).BeginInit();
            SuspendLayout();
            // 
            // TableLayoutPanelExBase
            // 
            TableLayoutPanelExBase.ColumnCount = 1;
            TableLayoutPanelExBase.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            TableLayoutPanelExBase.Controls.Add(MenuStripEx1, 0, 0);
            TableLayoutPanelExBase.Controls.Add(StatusStripEx1, 0, 3);
            TableLayoutPanelExBase.Controls.Add(PanelExUp, 0, 1);
            TableLayoutPanelExBase.Controls.Add(SpreadFirstRollCall, 0, 2);
            TableLayoutPanelExBase.Dock = DockStyle.Fill;
            TableLayoutPanelExBase.Location = new Point(0, 0);
            TableLayoutPanelExBase.Name = "TableLayoutPanelExBase";
            TableLayoutPanelExBase.RowCount = 4;
            TableLayoutPanelExBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            TableLayoutPanelExBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 170F));
            TableLayoutPanelExBase.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            TableLayoutPanelExBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            TableLayoutPanelExBase.Size = new Size(1904, 1041);
            TableLayoutPanelExBase.TabIndex = 0;
            // 
            // MenuStripEx1
            // 
            MenuStripEx1.Location = new Point(0, 0);
            MenuStripEx1.Name = "MenuStripEx1";
            MenuStripEx1.Size = new Size(1904, 24);
            MenuStripEx1.TabIndex = 0;
            MenuStripEx1.Text = "menuStripEx1";
            MenuStripEx1.ToolStripMenuItemDataBaseLocalFlag = false;
            // 
            // StatusStripEx1
            // 
            StatusStripEx1.Location = new Point(0, 1019);
            StatusStripEx1.Name = "StatusStripEx1";
            StatusStripEx1.Size = new Size(1904, 22);
            StatusStripEx1.TabIndex = 1;
            StatusStripEx1.Text = "statusStripEx1";
            // 
            // PanelExUp
            // 
            PanelExUp.Controls.Add(ButtonExUpdate);
            PanelExUp.Controls.Add(CheckBoxEx1);
            PanelExUp.Controls.Add(groupBoxEx2);
            PanelExUp.Controls.Add(groupBoxEx1);
            PanelExUp.Controls.Add(DateTimePickerExOperationDate);
            PanelExUp.Controls.Add(labelEx1);
            PanelExUp.Dock = DockStyle.Fill;
            PanelExUp.Location = new Point(3, 27);
            PanelExUp.Name = "PanelExUp";
            PanelExUp.Size = new Size(1898, 164);
            PanelExUp.TabIndex = 2;
            // 
            // ButtonExUpdate
            // 
            ButtonExUpdate.ForeColor = SystemColors.ControlText;
            ButtonExUpdate.Location = new Point(1660, 64);
            ButtonExUpdate.Name = "ButtonExUpdate";
            ButtonExUpdate.SetTextDirectionVertical = "";
            ButtonExUpdate.Size = new Size(176, 36);
            ButtonExUpdate.TabIndex = 5;
            ButtonExUpdate.Text = "配車表を作成する";
            ButtonExUpdate.UseVisualStyleBackColor = true;
            ButtonExUpdate.Click += ButtonEx_Click;
            // 
            // CheckBoxEx1
            // 
            CheckBoxEx1.AutoSize = true;
            CheckBoxEx1.Location = new Point(1456, 72);
            CheckBoxEx1.Name = "CheckBoxEx1";
            CheckBoxEx1.Size = new Size(161, 19);
            CheckBoxEx1.TabIndex = 4;
            CheckBoxEx1.Text = "記録済みの項目を読み込む";
            CheckBoxEx1.UseVisualStyleBackColor = true;
            CheckBoxEx1.CheckedChanged += CheckBoxEx1_CheckedChanged;
            // 
            // groupBoxEx2
            // 
            groupBoxEx2.Controls.Add(ComboBoxEx8);
            groupBoxEx2.Controls.Add(ComboBoxEx7);
            groupBoxEx2.Controls.Add(labelEx9);
            groupBoxEx2.Controls.Add(labelEx8);
            groupBoxEx2.Location = new Point(324, 72);
            groupBoxEx2.Name = "groupBoxEx2";
            groupBoxEx2.Size = new Size(1076, 84);
            groupBoxEx2.TabIndex = 3;
            groupBoxEx2.TabStop = false;
            groupBoxEx2.Text = "指示事項";
            // 
            // ComboBoxEx8
            // 
            ComboBoxEx8.FormattingEnabled = true;
            ComboBoxEx8.Items.AddRange(new object[] { "法定速度遵守  ", "車間距離の保持", "追い越し注意  ", "行違い注意    ", "スリップ注意  ", "路肩注意      ", "優先交通権の確認 ", "踏切注意         ", "発進時の前後左右の確認 ", "信号黄色は止まれの合図       ", "カーブ・交差点注意 ", "通行区分厳守   ", "横断歩道注意   ", "歩行者・自転車に注意 ", "連続運転・無理な運行の禁止 ", "運転中の携帯電話使用厳禁  ", "シートベルトの着用 ", "積載状況の確認と記録 ", "確実な積み付け ", "雨天・霧発生時のライト点灯", "老人と子供に注意", "適時適切な休憩・休息", "適時適切な報告の実施", "危険予知の励行", "事故予測の励行", "問題意識の保持", "「思いやり」「譲り合い」の励行", "「だろう」運転禁止", "「かもしれない」運転の励行", "「ながら」運転の禁止" });
            ComboBoxEx8.Location = new Point(84, 52);
            ComboBoxEx8.Name = "ComboBoxEx8";
            ComboBoxEx8.Size = new Size(980, 23);
            ComboBoxEx8.TabIndex = 15;
            ComboBoxEx8.Text = "〇〇〇〇〇〇";
            // 
            // ComboBoxEx7
            // 
            ComboBoxEx7.FormattingEnabled = true;
            ComboBoxEx7.Items.AddRange(new object[] { "法定速度遵守  ", "車間距離の保持", "追い越し注意  ", "行違い注意    ", "スリップ注意  ", "路肩注意      ", "優先交通権の確認 ", "踏切注意         ", "発進時の前後左右の確認 ", "信号黄色は止まれの合図       ", "カーブ・交差点注意 ", "通行区分厳守   ", "横断歩道注意   ", "歩行者・自転車に注意 ", "連続運転・無理な運行の禁止 ", "運転中の携帯電話使用厳禁  ", "シートベルトの着用 ", "積載状況の確認と記録 ", "確実な積み付け ", "雨天・霧発生時のライト点灯", "老人と子供に注意", "適時適切な休憩・休息", "適時適切な報告の実施", "危険予知の励行", "事故予測の励行", "問題意識の保持", "「思いやり」「譲り合い」の励行", "「だろう」運転禁止", "「かもしれない」運転の励行", "「ながら」運転の禁止" });
            ComboBoxEx7.Location = new Point(84, 24);
            ComboBoxEx7.Name = "ComboBoxEx7";
            ComboBoxEx7.Size = new Size(980, 23);
            ComboBoxEx7.TabIndex = 13;
            ComboBoxEx7.Text = "〇〇〇〇〇〇";
            // 
            // labelEx9
            // 
            labelEx9.AutoSize = true;
            labelEx9.Location = new Point(16, 56);
            labelEx9.Name = "labelEx9";
            labelEx9.Size = new Size(62, 15);
            labelEx9.TabIndex = 14;
            labelEx9.Text = "その他事項";
            // 
            // labelEx8
            // 
            labelEx8.AutoSize = true;
            labelEx8.Location = new Point(16, 28);
            labelEx8.Name = "labelEx8";
            labelEx8.Size = new Size(55, 15);
            labelEx8.TabIndex = 13;
            labelEx8.Text = "指示事項";
            // 
            // groupBoxEx1
            // 
            groupBoxEx1.Controls.Add(ComboBoxExWeather);
            groupBoxEx1.Controls.Add(labelEx7);
            groupBoxEx1.Controls.Add(ComboBoxEx5);
            groupBoxEx1.Controls.Add(labelEx6);
            groupBoxEx1.Controls.Add(ComboBoxEx4);
            groupBoxEx1.Controls.Add(labelEx5);
            groupBoxEx1.Controls.Add(ComboBoxEx3);
            groupBoxEx1.Controls.Add(labelEx4);
            groupBoxEx1.Controls.Add(ComboBoxEx2);
            groupBoxEx1.Controls.Add(labelEx3);
            groupBoxEx1.Controls.Add(ComboBoxEx1);
            groupBoxEx1.Controls.Add(labelEx2);
            groupBoxEx1.Location = new Point(324, 12);
            groupBoxEx1.Name = "groupBoxEx1";
            groupBoxEx1.Size = new Size(1076, 52);
            groupBoxEx1.TabIndex = 2;
            groupBoxEx1.TabStop = false;
            groupBoxEx1.Text = "点呼執行者";
            // 
            // ComboBoxExWeather
            // 
            ComboBoxExWeather.FormattingEnabled = true;
            ComboBoxExWeather.Items.AddRange(new object[] { "晴れ", "曇り", "小雨", "雨", "雪" });
            ComboBoxExWeather.Location = new Point(964, 20);
            ComboBoxExWeather.Name = "ComboBoxExWeather";
            ComboBoxExWeather.Size = new Size(100, 23);
            ComboBoxExWeather.TabIndex = 12;
            ComboBoxExWeather.Text = "〇〇〇〇〇〇";
            // 
            // labelEx7
            // 
            labelEx7.AutoSize = true;
            labelEx7.Location = new Point(928, 24);
            labelEx7.Name = "labelEx7";
            labelEx7.Size = new Size(31, 15);
            labelEx7.TabIndex = 11;
            labelEx7.Text = "天気";
            // 
            // ComboBoxEx5
            // 
            ComboBoxEx5.FormattingEnabled = true;
            ComboBoxEx5.Items.AddRange(new object[] { "波潟", "川名", "酒井", "青木" });
            ComboBoxEx5.Location = new Point(788, 20);
            ComboBoxEx5.Name = "ComboBoxEx5";
            ComboBoxEx5.Size = new Size(100, 23);
            ComboBoxEx5.TabIndex = 10;
            ComboBoxEx5.Text = "〇〇〇〇〇〇";
            // 
            // labelEx6
            // 
            labelEx6.AutoSize = true;
            labelEx6.Location = new Point(752, 24);
            labelEx6.Name = "labelEx6";
            labelEx6.Size = new Size(31, 15);
            labelEx6.TabIndex = 9;
            labelEx6.Text = "三郷";
            // 
            // ComboBoxEx4
            // 
            ComboBoxEx4.FormattingEnabled = true;
            ComboBoxEx4.Items.AddRange(new object[] { "新井", "波潟", "石原", "今村", "辻", "百瀨", "岸波", "青木" });
            ComboBoxEx4.Location = new Point(612, 20);
            ComboBoxEx4.Name = "ComboBoxEx4";
            ComboBoxEx4.Size = new Size(100, 23);
            ComboBoxEx4.TabIndex = 8;
            ComboBoxEx4.Text = "〇〇〇〇〇〇";
            // 
            // labelEx5
            // 
            labelEx5.AutoSize = true;
            labelEx5.Location = new Point(544, 24);
            labelEx5.Name = "labelEx5";
            labelEx5.Size = new Size(63, 15);
            labelEx5.TabIndex = 7;
            labelEx5.Text = "本社(帰２)";
            // 
            // ComboBoxEx3
            // 
            ComboBoxEx3.FormattingEnabled = true;
            ComboBoxEx3.Items.AddRange(new object[] { "新井", "波潟", "石原", "今村", "辻", "百瀨", "岸波", "青木" });
            ComboBoxEx3.Location = new Point(436, 20);
            ComboBoxEx3.Name = "ComboBoxEx3";
            ComboBoxEx3.Size = new Size(100, 23);
            ComboBoxEx3.TabIndex = 6;
            ComboBoxEx3.Text = "〇〇〇〇〇〇";
            // 
            // labelEx4
            // 
            labelEx4.AutoSize = true;
            labelEx4.Location = new Point(368, 24);
            labelEx4.Name = "labelEx4";
            labelEx4.Size = new Size(63, 15);
            labelEx4.TabIndex = 5;
            labelEx4.Text = "本社(帰１)";
            // 
            // ComboBoxEx2
            // 
            ComboBoxEx2.FormattingEnabled = true;
            ComboBoxEx2.Items.AddRange(new object[] { "新井", "波潟", "石原", "今村", "辻", "百瀨", "岸波", "青木" });
            ComboBoxEx2.Location = new Point(260, 20);
            ComboBoxEx2.Name = "ComboBoxEx2";
            ComboBoxEx2.Size = new Size(100, 23);
            ComboBoxEx2.TabIndex = 6;
            ComboBoxEx2.Text = "〇〇〇〇〇〇";
            // 
            // labelEx3
            // 
            labelEx3.AutoSize = true;
            labelEx3.Location = new Point(192, 24);
            labelEx3.Name = "labelEx3";
            labelEx3.Size = new Size(63, 15);
            labelEx3.TabIndex = 5;
            labelEx3.Text = "本社(出２)";
            // 
            // ComboBoxEx1
            // 
            ComboBoxEx1.FormattingEnabled = true;
            ComboBoxEx1.Items.AddRange(new object[] { "新井", "波潟", "石原", "今村", "辻", "百瀨", "岸波", "青木" });
            ComboBoxEx1.Location = new Point(84, 20);
            ComboBoxEx1.Name = "ComboBoxEx1";
            ComboBoxEx1.Size = new Size(100, 23);
            ComboBoxEx1.TabIndex = 4;
            ComboBoxEx1.Text = "〇〇〇〇〇〇";
            // 
            // labelEx2
            // 
            labelEx2.AutoSize = true;
            labelEx2.Location = new Point(16, 24);
            labelEx2.Name = "labelEx2";
            labelEx2.Size = new Size(63, 15);
            labelEx2.TabIndex = 3;
            labelEx2.Text = "本社(出１)";
            // 
            // DateTimePickerExOperationDate
            // 
            DateTimePickerExOperationDate.CultureFlag = false;
            DateTimePickerExOperationDate.CustomFormat = " 明治33年01月01日(月曜日)";
            DateTimePickerExOperationDate.Format = DateTimePickerFormat.Custom;
            DateTimePickerExOperationDate.Location = new Point(112, 76);
            DateTimePickerExOperationDate.Name = "DateTimePickerExOperationDate";
            DateTimePickerExOperationDate.Size = new Size(184, 23);
            DateTimePickerExOperationDate.TabIndex = 1;
            DateTimePickerExOperationDate.Value = new DateTime(1900, 1, 1, 0, 0, 0, 0);
            // 
            // labelEx1
            // 
            labelEx1.AutoSize = true;
            labelEx1.Location = new Point(52, 80);
            labelEx1.Name = "labelEx1";
            labelEx1.Size = new Size(55, 15);
            labelEx1.TabIndex = 0;
            labelEx1.Text = "配車日付";
            // 
            // SpreadFirstRollCall
            // 
            SpreadFirstRollCall.AccessibleDescription = "SpreadFirstRollCall, 配車表, Row 0, Column 0";
            SpreadFirstRollCall.Dock = DockStyle.Fill;
            SpreadFirstRollCall.Font = new Font("ＭＳ Ｐゴシック", 11F);
            SpreadFirstRollCall.Location = new Point(3, 197);
            SpreadFirstRollCall.Name = "SpreadFirstRollCall";
            SpreadFirstRollCall.Size = new Size(1898, 817);
            SpreadFirstRollCall.TabIndex = 3;
            // 
            // FirstRollColl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1904, 1041);
            Controls.Add(TableLayoutPanelExBase);
            MainMenuStrip = MenuStripEx1;
            Name = "FirstRollColl";
            Text = "FirstRollColl";
            FormClosing += FirstRollColl_FormClosing;
            TableLayoutPanelExBase.ResumeLayout(false);
            TableLayoutPanelExBase.PerformLayout();
            PanelExUp.ResumeLayout(false);
            PanelExUp.PerformLayout();
            groupBoxEx2.ResumeLayout(false);
            groupBoxEx2.PerformLayout();
            groupBoxEx1.ResumeLayout(false);
            groupBoxEx1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)SpreadFirstRollCall).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private CcControl.CcTableLayoutPanel TableLayoutPanelExBase;
        private CcControl.CcMenuStrip MenuStripEx1;
        private CcControl.CcStatusStrip StatusStripEx1;
        private CcControl.CcPanel PanelExUp;
        private FarPoint.Win.Spread.FpSpread SpreadFirstRollCall;
        private CcControl.CcLabel labelEx1;
        private CcControl.CcGroupBox groupBoxEx1;
        private CcControl.CcDateTime DateTimePickerExOperationDate;
        private CcControl.CcGroupBox groupBoxEx2;
        private CcControl.CcComboBox ComboBoxExWeather;
        private CcControl.CcLabel labelEx7;
        private CcControl.CcComboBox ComboBoxEx5;
        private CcControl.CcLabel labelEx6;
        private CcControl.CcComboBox ComboBoxEx4;
        private CcControl.CcLabel labelEx5;
        private CcControl.CcComboBox ComboBoxEx3;
        private CcControl.CcLabel labelEx4;
        private CcControl.CcComboBox ComboBoxEx2;
        private CcControl.CcLabel labelEx3;
        private CcControl.CcComboBox ComboBoxEx1;
        private CcControl.CcLabel labelEx2;
        private CcControl.CcComboBox ComboBoxEx8;
        private CcControl.CcComboBox ComboBoxEx7;
        private CcControl.CcLabel labelEx9;
        private CcControl.CcLabel labelEx8;
        private CcControl.CcButton ButtonExUpdate;
        private CcControl.CcCheckBox CheckBoxEx1;
        private FarPoint.Win.Spread.SheetView SheetViewFirstRollCall;
        private FarPoint.Win.Spread.SheetView SheetViewPartTimeStaff;
        private FarPoint.Win.Spread.SheetView SheetViewFullStaff;
    }
}