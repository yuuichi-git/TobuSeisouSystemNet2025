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
            CcLabelNumberOfWorkingDays = new CcControl.CcLabel();
            CcButtonUpdate = new CcControl.CcButton();
            ccLabel6 = new CcControl.CcLabel();
            CcLabelYearsOfService = new CcControl.CcLabel();
            CcNumericUpDownYearsOfService = new CcControl.CcNumericUpDown();
            ((System.ComponentModel.ISupportInitialize)CcNumericUpDownGrantedDays).BeginInit();
            ((System.ComponentModel.ISupportInitialize)CcNumericUpDownYearsOfService).BeginInit();
            SuspendLayout();
            // 
            // ccLabel1
            // 
            ccLabel1.Font = new Font("Yu Gothic UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 128);
            ccLabel1.Location = new Point(16, 8);
            ccLabel1.Name = "ccLabel1";
            ccLabel1.Size = new Size(344, 28);
            ccLabel1.TabIndex = 0;
            ccLabel1.Text = "起算日の登録と修正";
            ccLabel1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // ccLabel2
            // 
            ccLabel2.Location = new Point(16, 72);
            ccLabel2.Name = "ccLabel2";
            ccLabel2.Size = new Size(96, 20);
            ccLabel2.TabIndex = 1;
            ccLabel2.Text = "従事者名：";
            ccLabel2.TextAlign = ContentAlignment.MiddleRight;
            // 
            // CcLabelStaffName
            // 
            CcLabelStaffName.Location = new Point(116, 72);
            CcLabelStaffName.Name = "CcLabelStaffName";
            CcLabelStaffName.Size = new Size(244, 20);
            CcLabelStaffName.TabIndex = 2;
            CcLabelStaffName.Text = "辻　祐一";
            CcLabelStaffName.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // ccLabel3
            // 
            ccLabel3.Location = new Point(16, 128);
            ccLabel3.Name = "ccLabel3";
            ccLabel3.Size = new Size(96, 20);
            ccLabel3.TabIndex = 3;
            ccLabel3.Text = "有給基準日：";
            ccLabel3.TextAlign = ContentAlignment.MiddleRight;
            // 
            // ccLabel4
            // 
            ccLabel4.Location = new Point(16, 156);
            ccLabel4.Name = "ccLabel4";
            ccLabel4.Size = new Size(96, 20);
            ccLabel4.TabIndex = 4;
            ccLabel4.Text = "直近の起算日：";
            ccLabel4.TextAlign = ContentAlignment.MiddleRight;
            // 
            // ccLabel5
            // 
            ccLabel5.Location = new Point(16, 184);
            ccLabel5.Name = "ccLabel5";
            ccLabel5.Size = new Size(96, 20);
            ccLabel5.TabIndex = 5;
            ccLabel5.Text = "有給付与日数：";
            ccLabel5.TextAlign = ContentAlignment.MiddleRight;
            // 
            // CcLabelPaidLeaveReferenceDate
            // 
            CcLabelPaidLeaveReferenceDate.Location = new Point(116, 128);
            CcLabelPaidLeaveReferenceDate.Name = "CcLabelPaidLeaveReferenceDate";
            CcLabelPaidLeaveReferenceDate.Size = new Size(244, 20);
            CcLabelPaidLeaveReferenceDate.TabIndex = 6;
            CcLabelPaidLeaveReferenceDate.Text = "2026/10/10";
            CcLabelPaidLeaveReferenceDate.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // CcLabelPaidLeaveCommencementDate
            // 
            CcLabelPaidLeaveCommencementDate.Location = new Point(116, 156);
            CcLabelPaidLeaveCommencementDate.Name = "CcLabelPaidLeaveCommencementDate";
            CcLabelPaidLeaveCommencementDate.Size = new Size(244, 20);
            CcLabelPaidLeaveCommencementDate.TabIndex = 7;
            CcLabelPaidLeaveCommencementDate.Text = "2026/10/10";
            CcLabelPaidLeaveCommencementDate.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // CcNumericUpDownGrantedDays
            // 
            CcNumericUpDownGrantedDays.Font = new Font("Yu Gothic UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 128);
            CcNumericUpDownGrantedDays.Location = new Point(116, 180);
            CcNumericUpDownGrantedDays.Maximum = new decimal(new int[] { 20, 0, 0, 0 });
            CcNumericUpDownGrantedDays.Name = "CcNumericUpDownGrantedDays";
            CcNumericUpDownGrantedDays.Size = new Size(48, 25);
            CcNumericUpDownGrantedDays.TabIndex = 8;
            CcNumericUpDownGrantedDays.TextAlign = HorizontalAlignment.Right;
            // 
            // CcLabelNumberOfWorkingDays
            // 
            CcLabelNumberOfWorkingDays.ForeColor = Color.Red;
            CcLabelNumberOfWorkingDays.Location = new Point(16, 44);
            CcLabelNumberOfWorkingDays.Name = "CcLabelNumberOfWorkingDays";
            CcLabelNumberOfWorkingDays.Size = new Size(344, 20);
            CcLabelNumberOfWorkingDays.TabIndex = 9;
            CcLabelNumberOfWorkingDays.Text = "2026/12/12～2027/12/12までのの勤務日数：";
            CcLabelNumberOfWorkingDays.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // CcButtonUpdate
            // 
            CcButtonUpdate.ForeColor = SystemColors.ControlText;
            CcButtonUpdate.Location = new Point(108, 236);
            CcButtonUpdate.Name = "CcButtonUpdate";
            CcButtonUpdate.SetTextDirectionVertical = "";
            CcButtonUpdate.Size = new Size(160, 30);
            CcButtonUpdate.TabIndex = 10;
            CcButtonUpdate.Text = "UPDATE";
            CcButtonUpdate.UseVisualStyleBackColor = true;
            CcButtonUpdate.Click += CcButtonUpdate_Click;
            // 
            // ccLabel6
            // 
            ccLabel6.Location = new Point(16, 100);
            ccLabel6.Name = "ccLabel6";
            ccLabel6.Size = new Size(96, 20);
            ccLabel6.TabIndex = 11;
            ccLabel6.Text = "勤続年数：";
            ccLabel6.TextAlign = ContentAlignment.MiddleRight;
            // 
            // CcLabelYearsOfService
            // 
            CcLabelYearsOfService.Location = new Point(168, 100);
            CcLabelYearsOfService.Name = "CcLabelYearsOfService";
            CcLabelYearsOfService.Size = new Size(192, 20);
            CcLabelYearsOfService.TabIndex = 12;
            CcLabelYearsOfService.Text = "年6か月";
            CcLabelYearsOfService.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // CcNumericUpDownYearsOfService
            // 
            CcNumericUpDownYearsOfService.Font = new Font("Yu Gothic UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 128);
            CcNumericUpDownYearsOfService.Location = new Point(116, 96);
            CcNumericUpDownYearsOfService.Maximum = new decimal(new int[] { 20, 0, 0, 0 });
            CcNumericUpDownYearsOfService.Name = "CcNumericUpDownYearsOfService";
            CcNumericUpDownYearsOfService.Size = new Size(48, 25);
            CcNumericUpDownYearsOfService.TabIndex = 13;
            CcNumericUpDownYearsOfService.TextAlign = HorizontalAlignment.Right;
            // 
            // PaidLeaveDetail
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(374, 301);
            Controls.Add(CcNumericUpDownYearsOfService);
            Controls.Add(CcLabelYearsOfService);
            Controls.Add(ccLabel6);
            Controls.Add(CcButtonUpdate);
            Controls.Add(CcLabelNumberOfWorkingDays);
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
            ResumeLayout(false);
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
        private CcControl.CcLabel CcLabelNumberOfWorkingDays;
        private CcControl.CcButton CcButtonUpdate;
        private CcControl.CcLabel ccLabel6;
        private CcControl.CcLabel CcLabelYearsOfService;
        private CcControl.CcNumericUpDown CcNumericUpDownYearsOfService;
    }
}