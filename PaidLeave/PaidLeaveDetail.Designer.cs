namespace PaidLeave {
    partial class PaidLeaveDetail {
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
            ccLabel1 = new CcControl.CcLabel();
            ccLabel2 = new CcControl.CcLabel();
            CcLabelStaffName = new CcControl.CcLabel();
            ccLabel3 = new CcControl.CcLabel();
            ccLabel4 = new CcControl.CcLabel();
            ccLabel5 = new CcControl.CcLabel();
            CcLabelPaidLeaveReferenceDate = new CcControl.CcLabel();
            CcLabelPaidLeaveCommencementDate = new CcControl.CcLabel();
            CcNumericUpDownGrantedDays = new CcControl.CcNumericUpDown();
            CcLabelNumberOfWorkingFullDays = new CcControl.CcLabel();
            CcButtonUpdate = new CcControl.CcButton();
            ccLabel6 = new CcControl.CcLabel();
            CcLabelYearsOfService = new CcControl.CcLabel();
            CcNumericUpDownYearsOfService = new CcControl.CcNumericUpDown();
            CcLabelNumberOfWorkingHalfDays = new CcControl.CcLabel();
            CcLabel休日日数１年間 = new CcControl.CcLabel();
            CcLabel出勤率半年間 = new CcControl.CcLabel();
            CcTextBox1 = new CcControl.CcTextBox();
            CcLabel所定労働日数半年間 = new CcControl.CcLabel();
            CcLabel休日日数半年間 = new CcControl.CcLabel();
            CcLabel所定労働日数１年間 = new CcControl.CcLabel();
            CcLabel出勤率１年間 = new CcControl.CcLabel();
            CcGroupBox半年間 = new CcControl.CcGroupBox();
            CcGroupBox１年間 = new CcControl.CcGroupBox();
            ((System.ComponentModel.ISupportInitialize)CcNumericUpDownGrantedDays).BeginInit();
            ((System.ComponentModel.ISupportInitialize)CcNumericUpDownYearsOfService).BeginInit();
            CcGroupBox半年間.SuspendLayout();
            CcGroupBox１年間.SuspendLayout();
            SuspendLayout();
            // 
            // ccLabel1
            // 
            ccLabel1.Font = new Font("Yu Gothic UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 128);
            ccLabel1.Location = new Point(8, 8);
            ccLabel1.Name = "ccLabel1";
            ccLabel1.Size = new Size(556, 28);
            ccLabel1.TabIndex = 0;
            ccLabel1.Text = "起算日の登録と修正";
            ccLabel1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // ccLabel2
            // 
            ccLabel2.Location = new Point(16, 312);
            ccLabel2.Name = "ccLabel2";
            ccLabel2.Size = new Size(96, 20);
            ccLabel2.TabIndex = 1;
            ccLabel2.Text = "従事者名：";
            ccLabel2.TextAlign = ContentAlignment.MiddleRight;
            // 
            // CcLabelStaffName
            // 
            CcLabelStaffName.Location = new Point(116, 312);
            CcLabelStaffName.Name = "CcLabelStaffName";
            CcLabelStaffName.Size = new Size(156, 20);
            CcLabelStaffName.TabIndex = 2;
            CcLabelStaffName.Text = "辻　祐一";
            CcLabelStaffName.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // ccLabel3
            // 
            ccLabel3.Location = new Point(16, 368);
            ccLabel3.Name = "ccLabel3";
            ccLabel3.Size = new Size(96, 20);
            ccLabel3.TabIndex = 3;
            ccLabel3.Text = "有給基準日：";
            ccLabel3.TextAlign = ContentAlignment.MiddleRight;
            // 
            // ccLabel4
            // 
            ccLabel4.Location = new Point(16, 396);
            ccLabel4.Name = "ccLabel4";
            ccLabel4.Size = new Size(96, 20);
            ccLabel4.TabIndex = 4;
            ccLabel4.Text = "直近の起算日：";
            ccLabel4.TextAlign = ContentAlignment.MiddleRight;
            // 
            // ccLabel5
            // 
            ccLabel5.Location = new Point(16, 424);
            ccLabel5.Name = "ccLabel5";
            ccLabel5.Size = new Size(96, 20);
            ccLabel5.TabIndex = 5;
            ccLabel5.Text = "有給付与日数：";
            ccLabel5.TextAlign = ContentAlignment.MiddleRight;
            // 
            // CcLabelPaidLeaveReferenceDate
            // 
            CcLabelPaidLeaveReferenceDate.Location = new Point(116, 368);
            CcLabelPaidLeaveReferenceDate.Name = "CcLabelPaidLeaveReferenceDate";
            CcLabelPaidLeaveReferenceDate.Size = new Size(156, 20);
            CcLabelPaidLeaveReferenceDate.TabIndex = 6;
            CcLabelPaidLeaveReferenceDate.Text = "2026/10/10";
            CcLabelPaidLeaveReferenceDate.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // CcLabelPaidLeaveCommencementDate
            // 
            CcLabelPaidLeaveCommencementDate.Location = new Point(116, 396);
            CcLabelPaidLeaveCommencementDate.Name = "CcLabelPaidLeaveCommencementDate";
            CcLabelPaidLeaveCommencementDate.Size = new Size(156, 20);
            CcLabelPaidLeaveCommencementDate.TabIndex = 7;
            CcLabelPaidLeaveCommencementDate.Text = "2026/10/10";
            CcLabelPaidLeaveCommencementDate.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // CcNumericUpDownGrantedDays
            // 
            CcNumericUpDownGrantedDays.Font = new Font("Yu Gothic UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 128);
            CcNumericUpDownGrantedDays.Location = new Point(116, 420);
            CcNumericUpDownGrantedDays.Maximum = new decimal(new int[] { 20, 0, 0, 0 });
            CcNumericUpDownGrantedDays.Name = "CcNumericUpDownGrantedDays";
            CcNumericUpDownGrantedDays.Size = new Size(48, 25);
            CcNumericUpDownGrantedDays.TabIndex = 8;
            CcNumericUpDownGrantedDays.TextAlign = HorizontalAlignment.Right;
            // 
            // CcLabelNumberOfWorkingFullDays
            // 
            CcLabelNumberOfWorkingFullDays.ForeColor = Color.Black;
            CcLabelNumberOfWorkingFullDays.Location = new Point(8, 24);
            CcLabelNumberOfWorkingFullDays.Name = "CcLabelNumberOfWorkingFullDays";
            CcLabelNumberOfWorkingFullDays.Size = new Size(548, 20);
            CcLabelNumberOfWorkingFullDays.TabIndex = 9;
            CcLabelNumberOfWorkingFullDays.Text = "(１年間) 2026/12/12～2027/12/12までのの勤務日数：";
            CcLabelNumberOfWorkingFullDays.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // CcButtonUpdate
            // 
            CcButtonUpdate.ForeColor = SystemColors.ControlText;
            CcButtonUpdate.Location = new Point(80, 648);
            CcButtonUpdate.Name = "CcButtonUpdate";
            CcButtonUpdate.SetTextDirectionVertical = "";
            CcButtonUpdate.Size = new Size(396, 30);
            CcButtonUpdate.TabIndex = 10;
            CcButtonUpdate.Text = "UPDATE";
            CcButtonUpdate.UseVisualStyleBackColor = true;
            CcButtonUpdate.Click += CcButtonUpdate_Click;
            // 
            // ccLabel6
            // 
            ccLabel6.Location = new Point(16, 340);
            ccLabel6.Name = "ccLabel6";
            ccLabel6.Size = new Size(96, 20);
            ccLabel6.TabIndex = 11;
            ccLabel6.Text = "勤続年数：";
            ccLabel6.TextAlign = ContentAlignment.MiddleRight;
            // 
            // CcLabelYearsOfService
            // 
            CcLabelYearsOfService.Location = new Point(168, 340);
            CcLabelYearsOfService.Name = "CcLabelYearsOfService";
            CcLabelYearsOfService.Size = new Size(104, 20);
            CcLabelYearsOfService.TabIndex = 12;
            CcLabelYearsOfService.Text = "年6か月";
            CcLabelYearsOfService.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // CcNumericUpDownYearsOfService
            // 
            CcNumericUpDownYearsOfService.Font = new Font("Yu Gothic UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 128);
            CcNumericUpDownYearsOfService.Location = new Point(116, 336);
            CcNumericUpDownYearsOfService.Maximum = new decimal(new int[] { 20, 0, 0, 0 });
            CcNumericUpDownYearsOfService.Name = "CcNumericUpDownYearsOfService";
            CcNumericUpDownYearsOfService.Size = new Size(48, 25);
            CcNumericUpDownYearsOfService.TabIndex = 13;
            CcNumericUpDownYearsOfService.TextAlign = HorizontalAlignment.Right;
            // 
            // CcLabelNumberOfWorkingHalfDays
            // 
            CcLabelNumberOfWorkingHalfDays.ForeColor = Color.Black;
            CcLabelNumberOfWorkingHalfDays.Location = new Point(8, 24);
            CcLabelNumberOfWorkingHalfDays.Name = "CcLabelNumberOfWorkingHalfDays";
            CcLabelNumberOfWorkingHalfDays.Size = new Size(548, 20);
            CcLabelNumberOfWorkingHalfDays.TabIndex = 14;
            CcLabelNumberOfWorkingHalfDays.Text = "(半年間) 2026/12/12～2027/12/12までのの勤務日数：";
            CcLabelNumberOfWorkingHalfDays.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // CcLabel休日日数１年間
            // 
            CcLabel休日日数１年間.ForeColor = Color.Black;
            CcLabel休日日数１年間.Location = new Point(8, 72);
            CcLabel休日日数１年間.Name = "CcLabel休日日数１年間";
            CcLabel休日日数１年間.Size = new Size(548, 20);
            CcLabel休日日数１年間.TabIndex = 15;
            CcLabel休日日数１年間.Text = "休日・祭日・指定休日：";
            CcLabel休日日数１年間.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // CcLabel出勤率半年間
            // 
            CcLabel出勤率半年間.ForeColor = Color.Black;
            CcLabel出勤率半年間.Location = new Point(8, 96);
            CcLabel出勤率半年間.Name = "CcLabel出勤率半年間";
            CcLabel出勤率半年間.Size = new Size(548, 20);
            CcLabel出勤率半年間.TabIndex = 16;
            CcLabel出勤率半年間.Text = "出勤率：";
            CcLabel出勤率半年間.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // CcTextBox1
            // 
            CcTextBox1.Location = new Point(280, 308);
            CcTextBox1.Multiline = true;
            CcTextBox1.Name = "CcTextBox1";
            CcTextBox1.ScrollBars = ScrollBars.Vertical;
            CcTextBox1.Size = new Size(280, 324);
            CcTextBox1.TabIndex = 17;
            // 
            // CcLabel所定労働日数半年間
            // 
            CcLabel所定労働日数半年間.ForeColor = Color.Black;
            CcLabel所定労働日数半年間.Location = new Point(8, 48);
            CcLabel所定労働日数半年間.Name = "CcLabel所定労働日数半年間";
            CcLabel所定労働日数半年間.Size = new Size(548, 20);
            CcLabel所定労働日数半年間.TabIndex = 19;
            CcLabel所定労働日数半年間.Text = "所定労働日数（日曜日/祝祭日/会社指定日を除く）：";
            CcLabel所定労働日数半年間.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // CcLabel休日日数半年間
            // 
            CcLabel休日日数半年間.ForeColor = Color.Black;
            CcLabel休日日数半年間.Location = new Point(8, 72);
            CcLabel休日日数半年間.Name = "CcLabel休日日数半年間";
            CcLabel休日日数半年間.Size = new Size(548, 20);
            CcLabel休日日数半年間.TabIndex = 20;
            CcLabel休日日数半年間.Text = "休日・祭日・指定休日：";
            CcLabel休日日数半年間.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // CcLabel所定労働日数１年間
            // 
            CcLabel所定労働日数１年間.ForeColor = Color.Black;
            CcLabel所定労働日数１年間.Location = new Point(8, 48);
            CcLabel所定労働日数１年間.Name = "CcLabel所定労働日数１年間";
            CcLabel所定労働日数１年間.Size = new Size(548, 20);
            CcLabel所定労働日数１年間.TabIndex = 22;
            CcLabel所定労働日数１年間.Text = "所定労働日数（日曜日/祝祭日/会社指定日を除く）：";
            CcLabel所定労働日数１年間.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // CcLabel出勤率１年間
            // 
            CcLabel出勤率１年間.ForeColor = Color.Black;
            CcLabel出勤率１年間.Location = new Point(8, 96);
            CcLabel出勤率１年間.Name = "CcLabel出勤率１年間";
            CcLabel出勤率１年間.Size = new Size(548, 20);
            CcLabel出勤率１年間.TabIndex = 23;
            CcLabel出勤率１年間.Text = "出勤率：";
            CcLabel出勤率１年間.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // CcGroupBox半年間
            // 
            CcGroupBox半年間.Controls.Add(CcLabelNumberOfWorkingHalfDays);
            CcGroupBox半年間.Controls.Add(CcLabel出勤率半年間);
            CcGroupBox半年間.Controls.Add(CcLabel所定労働日数半年間);
            CcGroupBox半年間.Controls.Add(CcLabel休日日数半年間);
            CcGroupBox半年間.ForeColor = Color.Blue;
            CcGroupBox半年間.Location = new Point(4, 40);
            CcGroupBox半年間.Name = "CcGroupBox半年間";
            CcGroupBox半年間.Size = new Size(560, 124);
            CcGroupBox半年間.TabIndex = 24;
            CcGroupBox半年間.TabStop = false;
            CcGroupBox半年間.Text = "半年分の情報";
            // 
            // CcGroupBox１年間
            // 
            CcGroupBox１年間.Controls.Add(CcLabel出勤率１年間);
            CcGroupBox１年間.Controls.Add(CcLabelNumberOfWorkingFullDays);
            CcGroupBox１年間.Controls.Add(CcLabel休日日数１年間);
            CcGroupBox１年間.Controls.Add(CcLabel所定労働日数１年間);
            CcGroupBox１年間.ForeColor = Color.Blue;
            CcGroupBox１年間.Location = new Point(4, 172);
            CcGroupBox１年間.Name = "CcGroupBox１年間";
            CcGroupBox１年間.Size = new Size(560, 124);
            CcGroupBox１年間.TabIndex = 25;
            CcGroupBox１年間.TabStop = false;
            CcGroupBox１年間.Text = "１年分の情報";
            // 
            // PaidLeaveDetail
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(571, 697);
            Controls.Add(CcGroupBox１年間);
            Controls.Add(CcGroupBox半年間);
            Controls.Add(CcTextBox1);
            Controls.Add(CcNumericUpDownYearsOfService);
            Controls.Add(CcLabelYearsOfService);
            Controls.Add(ccLabel6);
            Controls.Add(CcButtonUpdate);
            Controls.Add(CcNumericUpDownGrantedDays);
            Controls.Add(CcLabelPaidLeaveCommencementDate);
            Controls.Add(CcLabelPaidLeaveReferenceDate);
            Controls.Add(ccLabel5);
            Controls.Add(ccLabel4);
            Controls.Add(ccLabel3);
            Controls.Add(CcLabelStaffName);
            Controls.Add(ccLabel2);
            Controls.Add(ccLabel1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "PaidLeaveDetail";
            Text = "PaidLeaveDetail";
            FormClosing += PaidLeaveDetail_FormClosing;
            Load += PaidLeaveDetail_Load;
            ((System.ComponentModel.ISupportInitialize)CcNumericUpDownGrantedDays).EndInit();
            ((System.ComponentModel.ISupportInitialize)CcNumericUpDownYearsOfService).EndInit();
            CcGroupBox半年間.ResumeLayout(false);
            CcGroupBox１年間.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private CcControl.CcLabel ccLabel1;
        private CcControl.CcLabel ccLabel2;
        private CcControl.CcLabel CcLabelStaffName;
        private CcControl.CcLabel ccLabel3;
        private CcControl.CcLabel ccLabel4;
        private CcControl.CcLabel ccLabel5;
        private CcControl.CcLabel CcLabelPaidLeaveReferenceDate;
        private CcControl.CcLabel CcLabelPaidLeaveCommencementDate;
        private CcControl.CcNumericUpDown CcNumericUpDownGrantedDays;
        private CcControl.CcLabel CcLabelNumberOfWorkingFullDays;
        private CcControl.CcButton CcButtonUpdate;
        private CcControl.CcLabel ccLabel6;
        private CcControl.CcLabel CcLabelYearsOfService;
        private CcControl.CcNumericUpDown CcNumericUpDownYearsOfService;
        private CcControl.CcLabel CcLabelNumberOfWorkingHalfDays;
        private CcControl.CcLabel CcLabel休日日数１年間;
        private CcControl.CcLabel CcLabel出勤率半年間;
        private CcControl.CcTextBox CcTextBox1;
        private CcControl.CcLabel CcLabel所定労働日数半年間;
        private CcControl.CcLabel CcLabel休日日数半年間;
        private CcControl.CcLabel CcLabel所定労働日数１年間;
        private CcControl.CcLabel CcLabel出勤率１年間;
        private CcControl.CcGroupBox CcGroupBox半年間;
        private CcControl.CcGroupBox CcGroupBox１年間;
    }
}