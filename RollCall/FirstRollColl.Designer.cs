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
            this.TableLayoutPanelExBase = new ControlEx.TableLayoutPanelEx();
            this.MenuStripEx1 = new ControlEx.MenuStripEx();
            this.StatusStripEx1 = new ControlEx.StatusStripEx();
            this.PanelExUp = new ControlEx.PanelEx();
            this.ButtonExUpdate = new ControlEx.ButtonEx();
            this.CheckBoxEx1 = new ControlEx.CheckBoxEx();
            this.groupBoxEx2 = new ControlEx.GroupBoxEx();
            this.ComboBoxEx8 = new ControlEx.ComboBoxEx();
            this.ComboBoxEx7 = new ControlEx.ComboBoxEx();
            this.labelEx9 = new ControlEx.LabelEx();
            this.labelEx8 = new ControlEx.LabelEx();
            this.groupBoxEx1 = new ControlEx.GroupBoxEx();
            this.ComboBoxExWeather = new ControlEx.ComboBoxEx();
            this.labelEx7 = new ControlEx.LabelEx();
            this.ComboBoxEx5 = new ControlEx.ComboBoxEx();
            this.labelEx6 = new ControlEx.LabelEx();
            this.ComboBoxEx4 = new ControlEx.ComboBoxEx();
            this.labelEx5 = new ControlEx.LabelEx();
            this.ComboBoxEx3 = new ControlEx.ComboBoxEx();
            this.labelEx4 = new ControlEx.LabelEx();
            this.ComboBoxEx2 = new ControlEx.ComboBoxEx();
            this.labelEx3 = new ControlEx.LabelEx();
            this.ComboBoxEx1 = new ControlEx.ComboBoxEx();
            this.labelEx2 = new ControlEx.LabelEx();
            this.DateTimePickerExOperationDate = new ControlEx.DateTimePickerEx();
            this.labelEx1 = new ControlEx.LabelEx();
            this.SpreadFirstRollCall = new FarPoint.Win.Spread.FpSpread(FarPoint.Win.Spread.LegacyBehaviors.None, resources.GetObject("TableLayoutPanelExBase.Controls"));
            this.SheetViewFirstRollCall = this.SpreadFirstRollCall.GetSheet(0);
            this.SheetViewPartTime = this.SpreadFirstRollCall.GetSheet(1);
            this.TableLayoutPanelExBase.SuspendLayout();
            this.PanelExUp.SuspendLayout();
            this.groupBoxEx2.SuspendLayout();
            this.groupBoxEx1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)this.SpreadFirstRollCall).BeginInit();
            this.SuspendLayout();
            // 
            // TableLayoutPanelExBase
            // 
            this.TableLayoutPanelExBase.ColumnCount = 1;
            this.TableLayoutPanelExBase.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            this.TableLayoutPanelExBase.Controls.Add(this.MenuStripEx1, 0, 0);
            this.TableLayoutPanelExBase.Controls.Add(this.StatusStripEx1, 0, 3);
            this.TableLayoutPanelExBase.Controls.Add(this.PanelExUp, 0, 1);
            this.TableLayoutPanelExBase.Controls.Add(this.SpreadFirstRollCall, 0, 2);
            this.TableLayoutPanelExBase.Dock = DockStyle.Fill;
            this.TableLayoutPanelExBase.Location = new Point(0, 0);
            this.TableLayoutPanelExBase.Name = "TableLayoutPanelExBase";
            this.TableLayoutPanelExBase.RowCount = 4;
            this.TableLayoutPanelExBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            this.TableLayoutPanelExBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 170F));
            this.TableLayoutPanelExBase.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            this.TableLayoutPanelExBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            this.TableLayoutPanelExBase.Size = new Size(1904, 1041);
            this.TableLayoutPanelExBase.TabIndex = 0;
            // 
            // MenuStripEx1
            // 
            this.MenuStripEx1.Location = new Point(0, 0);
            this.MenuStripEx1.Name = "MenuStripEx1";
            this.MenuStripEx1.Size = new Size(1904, 24);
            this.MenuStripEx1.TabIndex = 0;
            this.MenuStripEx1.Text = "menuStripEx1";
            this.MenuStripEx1.ToolStripMenuItemDataBaseLocalFlag = false;
            // 
            // StatusStripEx1
            // 
            this.StatusStripEx1.Location = new Point(0, 1019);
            this.StatusStripEx1.Name = "StatusStripEx1";
            this.StatusStripEx1.Size = new Size(1904, 22);
            this.StatusStripEx1.TabIndex = 1;
            this.StatusStripEx1.Text = "statusStripEx1";
            // 
            // PanelExUp
            // 
            this.PanelExUp.Controls.Add(this.ButtonExUpdate);
            this.PanelExUp.Controls.Add(this.CheckBoxEx1);
            this.PanelExUp.Controls.Add(this.groupBoxEx2);
            this.PanelExUp.Controls.Add(this.groupBoxEx1);
            this.PanelExUp.Controls.Add(this.DateTimePickerExOperationDate);
            this.PanelExUp.Controls.Add(this.labelEx1);
            this.PanelExUp.Dock = DockStyle.Fill;
            this.PanelExUp.Location = new Point(3, 27);
            this.PanelExUp.Name = "PanelExUp";
            this.PanelExUp.Size = new Size(1898, 164);
            this.PanelExUp.TabIndex = 2;
            // 
            // ButtonExUpdate
            // 
            this.ButtonExUpdate.Location = new Point(1660, 64);
            this.ButtonExUpdate.Name = "ButtonExUpdate";
            this.ButtonExUpdate.SetTextDirectionVertical = "";
            this.ButtonExUpdate.Size = new Size(176, 36);
            this.ButtonExUpdate.TabIndex = 5;
            this.ButtonExUpdate.Text = "配車表を作成する";
            this.ButtonExUpdate.UseVisualStyleBackColor = true;
            this.ButtonExUpdate.Click += this.ButtonEx_Click;
            // 
            // CheckBoxEx1
            // 
            this.CheckBoxEx1.AutoSize = true;
            this.CheckBoxEx1.Location = new Point(1456, 72);
            this.CheckBoxEx1.Name = "CheckBoxEx1";
            this.CheckBoxEx1.Size = new Size(161, 19);
            this.CheckBoxEx1.TabIndex = 4;
            this.CheckBoxEx1.Text = "記録済みの項目を読み込む";
            this.CheckBoxEx1.UseVisualStyleBackColor = true;
            this.CheckBoxEx1.CheckedChanged += this.CheckBoxEx1_CheckedChanged;
            // 
            // groupBoxEx2
            // 
            this.groupBoxEx2.Controls.Add(this.ComboBoxEx8);
            this.groupBoxEx2.Controls.Add(this.ComboBoxEx7);
            this.groupBoxEx2.Controls.Add(this.labelEx9);
            this.groupBoxEx2.Controls.Add(this.labelEx8);
            this.groupBoxEx2.Location = new Point(324, 72);
            this.groupBoxEx2.Name = "groupBoxEx2";
            this.groupBoxEx2.Size = new Size(1076, 84);
            this.groupBoxEx2.TabIndex = 3;
            this.groupBoxEx2.TabStop = false;
            this.groupBoxEx2.Text = "指示事項";
            // 
            // ComboBoxEx8
            // 
            this.ComboBoxEx8.FormattingEnabled = true;
            this.ComboBoxEx8.Items.AddRange(new object[] { "法定速度遵守  ", "車間距離の保持", "追い越し注意  ", "行違い注意    ", "スリップ注意  ", "路肩注意      ", "優先交通権の確認 ", "踏切注意         ", "発進時の前後左右の確認 ", "信号黄色は止まれの合図       ", "カーブ・交差点注意 ", "通行区分厳守   ", "横断歩道注意   ", "歩行者・自転車に注意 ", "連続運転・無理な運行の禁止 ", "運転中の携帯電話使用厳禁  ", "シートベルトの着用 ", "積載状況の確認と記録 ", "確実な積み付け ", "雨天・霧発生時のライト点灯", "老人と子供に注意", "適時適切な休憩・休息", "適時適切な報告の実施", "危険予知の励行", "事故予測の励行", "問題意識の保持", "「思いやり」「譲り合い」の励行", "「だろう」運転禁止", "「かもしれない」運転の励行", "「ながら」運転の禁止" });
            this.ComboBoxEx8.Location = new Point(84, 52);
            this.ComboBoxEx8.Name = "ComboBoxEx8";
            this.ComboBoxEx8.Size = new Size(980, 23);
            this.ComboBoxEx8.TabIndex = 15;
            this.ComboBoxEx8.Text = "〇〇〇〇〇〇";
            // 
            // ComboBoxEx7
            // 
            this.ComboBoxEx7.FormattingEnabled = true;
            this.ComboBoxEx7.Items.AddRange(new object[] { "法定速度遵守  ", "車間距離の保持", "追い越し注意  ", "行違い注意    ", "スリップ注意  ", "路肩注意      ", "優先交通権の確認 ", "踏切注意         ", "発進時の前後左右の確認 ", "信号黄色は止まれの合図       ", "カーブ・交差点注意 ", "通行区分厳守   ", "横断歩道注意   ", "歩行者・自転車に注意 ", "連続運転・無理な運行の禁止 ", "運転中の携帯電話使用厳禁  ", "シートベルトの着用 ", "積載状況の確認と記録 ", "確実な積み付け ", "雨天・霧発生時のライト点灯", "老人と子供に注意", "適時適切な休憩・休息", "適時適切な報告の実施", "危険予知の励行", "事故予測の励行", "問題意識の保持", "「思いやり」「譲り合い」の励行", "「だろう」運転禁止", "「かもしれない」運転の励行", "「ながら」運転の禁止" });
            this.ComboBoxEx7.Location = new Point(84, 24);
            this.ComboBoxEx7.Name = "ComboBoxEx7";
            this.ComboBoxEx7.Size = new Size(980, 23);
            this.ComboBoxEx7.TabIndex = 13;
            this.ComboBoxEx7.Text = "〇〇〇〇〇〇";
            // 
            // labelEx9
            // 
            this.labelEx9.AutoSize = true;
            this.labelEx9.Location = new Point(16, 56);
            this.labelEx9.Name = "labelEx9";
            this.labelEx9.Size = new Size(62, 15);
            this.labelEx9.TabIndex = 14;
            this.labelEx9.Text = "その他事項";
            // 
            // labelEx8
            // 
            this.labelEx8.AutoSize = true;
            this.labelEx8.Location = new Point(16, 28);
            this.labelEx8.Name = "labelEx8";
            this.labelEx8.Size = new Size(55, 15);
            this.labelEx8.TabIndex = 13;
            this.labelEx8.Text = "指示事項";
            // 
            // groupBoxEx1
            // 
            this.groupBoxEx1.Controls.Add(this.ComboBoxExWeather);
            this.groupBoxEx1.Controls.Add(this.labelEx7);
            this.groupBoxEx1.Controls.Add(this.ComboBoxEx5);
            this.groupBoxEx1.Controls.Add(this.labelEx6);
            this.groupBoxEx1.Controls.Add(this.ComboBoxEx4);
            this.groupBoxEx1.Controls.Add(this.labelEx5);
            this.groupBoxEx1.Controls.Add(this.ComboBoxEx3);
            this.groupBoxEx1.Controls.Add(this.labelEx4);
            this.groupBoxEx1.Controls.Add(this.ComboBoxEx2);
            this.groupBoxEx1.Controls.Add(this.labelEx3);
            this.groupBoxEx1.Controls.Add(this.ComboBoxEx1);
            this.groupBoxEx1.Controls.Add(this.labelEx2);
            this.groupBoxEx1.Location = new Point(324, 12);
            this.groupBoxEx1.Name = "groupBoxEx1";
            this.groupBoxEx1.Size = new Size(1076, 52);
            this.groupBoxEx1.TabIndex = 2;
            this.groupBoxEx1.TabStop = false;
            this.groupBoxEx1.Text = "点呼執行者";
            // 
            // ComboBoxExWeather
            // 
            this.ComboBoxExWeather.FormattingEnabled = true;
            this.ComboBoxExWeather.Items.AddRange(new object[] { "晴れ", "曇り", "小雨", "雨", "雪" });
            this.ComboBoxExWeather.Location = new Point(964, 20);
            this.ComboBoxExWeather.Name = "ComboBoxExWeather";
            this.ComboBoxExWeather.Size = new Size(100, 23);
            this.ComboBoxExWeather.TabIndex = 12;
            this.ComboBoxExWeather.Text = "〇〇〇〇〇〇";
            // 
            // labelEx7
            // 
            this.labelEx7.AutoSize = true;
            this.labelEx7.Location = new Point(928, 24);
            this.labelEx7.Name = "labelEx7";
            this.labelEx7.Size = new Size(31, 15);
            this.labelEx7.TabIndex = 11;
            this.labelEx7.Text = "天気";
            // 
            // ComboBoxEx5
            // 
            this.ComboBoxEx5.FormattingEnabled = true;
            this.ComboBoxEx5.Items.AddRange(new object[] { "波潟", "川名", "酒井", "青木" });
            this.ComboBoxEx5.Location = new Point(788, 20);
            this.ComboBoxEx5.Name = "ComboBoxEx5";
            this.ComboBoxEx5.Size = new Size(100, 23);
            this.ComboBoxEx5.TabIndex = 10;
            this.ComboBoxEx5.Text = "〇〇〇〇〇〇";
            // 
            // labelEx6
            // 
            this.labelEx6.AutoSize = true;
            this.labelEx6.Location = new Point(752, 24);
            this.labelEx6.Name = "labelEx6";
            this.labelEx6.Size = new Size(31, 15);
            this.labelEx6.TabIndex = 9;
            this.labelEx6.Text = "三郷";
            // 
            // ComboBoxEx4
            // 
            this.ComboBoxEx4.FormattingEnabled = true;
            this.ComboBoxEx4.Items.AddRange(new object[] { "新井", "波潟", "石原", "今村", "辻", "百瀨" });
            this.ComboBoxEx4.Location = new Point(612, 20);
            this.ComboBoxEx4.Name = "ComboBoxEx4";
            this.ComboBoxEx4.Size = new Size(100, 23);
            this.ComboBoxEx4.TabIndex = 8;
            this.ComboBoxEx4.Text = "〇〇〇〇〇〇";
            // 
            // labelEx5
            // 
            this.labelEx5.AutoSize = true;
            this.labelEx5.Location = new Point(544, 24);
            this.labelEx5.Name = "labelEx5";
            this.labelEx5.Size = new Size(63, 15);
            this.labelEx5.TabIndex = 7;
            this.labelEx5.Text = "本社(帰２)";
            // 
            // ComboBoxEx3
            // 
            this.ComboBoxEx3.FormattingEnabled = true;
            this.ComboBoxEx3.Items.AddRange(new object[] { "新井", "波潟", "石原", "今村", "辻", "百瀨" });
            this.ComboBoxEx3.Location = new Point(436, 20);
            this.ComboBoxEx3.Name = "ComboBoxEx3";
            this.ComboBoxEx3.Size = new Size(100, 23);
            this.ComboBoxEx3.TabIndex = 6;
            this.ComboBoxEx3.Text = "〇〇〇〇〇〇";
            // 
            // labelEx4
            // 
            this.labelEx4.AutoSize = true;
            this.labelEx4.Location = new Point(368, 24);
            this.labelEx4.Name = "labelEx4";
            this.labelEx4.Size = new Size(63, 15);
            this.labelEx4.TabIndex = 5;
            this.labelEx4.Text = "本社(帰１)";
            // 
            // ComboBoxEx2
            // 
            this.ComboBoxEx2.FormattingEnabled = true;
            this.ComboBoxEx2.Items.AddRange(new object[] { "新井", "波潟", "石原", "今村", "辻", "百瀨" });
            this.ComboBoxEx2.Location = new Point(260, 20);
            this.ComboBoxEx2.Name = "ComboBoxEx2";
            this.ComboBoxEx2.Size = new Size(100, 23);
            this.ComboBoxEx2.TabIndex = 6;
            this.ComboBoxEx2.Text = "〇〇〇〇〇〇";
            // 
            // labelEx3
            // 
            this.labelEx3.AutoSize = true;
            this.labelEx3.Location = new Point(192, 24);
            this.labelEx3.Name = "labelEx3";
            this.labelEx3.Size = new Size(63, 15);
            this.labelEx3.TabIndex = 5;
            this.labelEx3.Text = "本社(出２)";
            // 
            // ComboBoxEx1
            // 
            this.ComboBoxEx1.FormattingEnabled = true;
            this.ComboBoxEx1.Items.AddRange(new object[] { "新井", "波潟", "石原", "今村", "辻", "百瀨" });
            this.ComboBoxEx1.Location = new Point(84, 20);
            this.ComboBoxEx1.Name = "ComboBoxEx1";
            this.ComboBoxEx1.Size = new Size(100, 23);
            this.ComboBoxEx1.TabIndex = 4;
            this.ComboBoxEx1.Text = "〇〇〇〇〇〇";
            // 
            // labelEx2
            // 
            this.labelEx2.AutoSize = true;
            this.labelEx2.Location = new Point(16, 24);
            this.labelEx2.Name = "labelEx2";
            this.labelEx2.Size = new Size(63, 15);
            this.labelEx2.TabIndex = 3;
            this.labelEx2.Text = "本社(出１)";
            // 
            // DateTimePickerExOperationDate
            // 
            this.DateTimePickerExOperationDate.CultureFlag = false;
            this.DateTimePickerExOperationDate.CustomFormat = " 明治33年01月01日(月曜日)";
            this.DateTimePickerExOperationDate.Format = DateTimePickerFormat.Custom;
            this.DateTimePickerExOperationDate.Location = new Point(112, 76);
            this.DateTimePickerExOperationDate.Name = "DateTimePickerExOperationDate";
            this.DateTimePickerExOperationDate.Size = new Size(184, 23);
            this.DateTimePickerExOperationDate.TabIndex = 1;
            this.DateTimePickerExOperationDate.Value = new DateTime(1900, 1, 1, 0, 0, 0, 0);
            // 
            // labelEx1
            // 
            this.labelEx1.AutoSize = true;
            this.labelEx1.Location = new Point(52, 80);
            this.labelEx1.Name = "labelEx1";
            this.labelEx1.Size = new Size(55, 15);
            this.labelEx1.TabIndex = 0;
            this.labelEx1.Text = "配車日付";
            // 
            // SpreadFirstRollCall
            // 
            this.SpreadFirstRollCall.AccessibleDescription = "SpreadFirstRollCall, Sheet2, Row 0, Column 0";
            this.SpreadFirstRollCall.Dock = DockStyle.Fill;
            this.SpreadFirstRollCall.Font = new Font("ＭＳ Ｐゴシック", 11F, FontStyle.Regular, GraphicsUnit.Point);
            this.SpreadFirstRollCall.Location = new Point(3, 197);
            this.SpreadFirstRollCall.Name = "SpreadFirstRollCall";
            this.SpreadFirstRollCall.Size = new Size(1898, 817);
            this.SpreadFirstRollCall.TabIndex = 3;
            // 
            // FirstRollColl
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(1904, 1041);
            this.Controls.Add(this.TableLayoutPanelExBase);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.MenuStripEx1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FirstRollColl";
            this.Text = "FirstRollColl";
            this.FormClosing += this.FirstRollColl_FormClosing;
            this.TableLayoutPanelExBase.ResumeLayout(false);
            this.TableLayoutPanelExBase.PerformLayout();
            this.PanelExUp.ResumeLayout(false);
            this.PanelExUp.PerformLayout();
            this.groupBoxEx2.ResumeLayout(false);
            this.groupBoxEx2.PerformLayout();
            this.groupBoxEx1.ResumeLayout(false);
            this.groupBoxEx1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)this.SpreadFirstRollCall).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private ControlEx.TableLayoutPanelEx TableLayoutPanelExBase;
        private ControlEx.MenuStripEx MenuStripEx1;
        private ControlEx.StatusStripEx StatusStripEx1;
        private ControlEx.PanelEx PanelExUp;
        private FarPoint.Win.Spread.FpSpread SpreadFirstRollCall;
        private ControlEx.LabelEx labelEx1;
        private ControlEx.GroupBoxEx groupBoxEx1;
        private ControlEx.DateTimePickerEx DateTimePickerExOperationDate;
        private ControlEx.GroupBoxEx groupBoxEx2;
        private ControlEx.ComboBoxEx ComboBoxExWeather;
        private ControlEx.LabelEx labelEx7;
        private ControlEx.ComboBoxEx ComboBoxEx5;
        private ControlEx.LabelEx labelEx6;
        private ControlEx.ComboBoxEx ComboBoxEx4;
        private ControlEx.LabelEx labelEx5;
        private ControlEx.ComboBoxEx ComboBoxEx3;
        private ControlEx.LabelEx labelEx4;
        private ControlEx.ComboBoxEx ComboBoxEx2;
        private ControlEx.LabelEx labelEx3;
        private ControlEx.ComboBoxEx ComboBoxEx1;
        private ControlEx.LabelEx labelEx2;
        private ControlEx.ComboBoxEx ComboBoxEx8;
        private ControlEx.ComboBoxEx ComboBoxEx7;
        private ControlEx.LabelEx labelEx9;
        private ControlEx.LabelEx labelEx8;
        private ControlEx.ButtonEx ButtonExUpdate;
        private ControlEx.CheckBoxEx CheckBoxEx1;
        private FarPoint.Win.Spread.SheetView SheetViewFirstRollCall;
        private FarPoint.Win.Spread.SheetView SheetViewPartTime;
    }
}