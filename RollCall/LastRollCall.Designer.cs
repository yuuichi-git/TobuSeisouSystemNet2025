namespace RollCall {
    partial class LastRollCall {
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
            LabelExSetName = new CcControl.CcLabel();
            labelEx2 = new CcControl.CcLabel();
            DateTimePickerExOperationDate = new CcControl.CcDateTime();
            labelEx3 = new CcControl.CcLabel();
            labelEx4 = new CcControl.CcLabel();
            labelEx5 = new CcControl.CcLabel();
            labelEx6 = new CcControl.CcLabel();
            labelEx7 = new CcControl.CcLabel();
            labelEx8 = new CcControl.CcLabel();
            labelEx9 = new CcControl.CcLabel();
            labelEx10 = new CcControl.CcLabel();
            CheckBoxExDelete = new CcControl.CcCheckBox();
            ButtonExUpdate = new CcControl.CcButton();
            NumericUpDownExLastPlantCount = new CcControl.CcNumericUpDown();
            ComboBoxExLastPlantName = new CcControl.CcComboBox();
            NumericUpDownExFirstOdoMeter = new CcControl.CcNumericUpDown();
            NumericUpDownExLastOdoMeter = new CcControl.CcNumericUpDown();
            NumericUpDownExOilAmount = new CcControl.CcNumericUpDown();
            CcTimeFirstRollCallTime = new CcControl.CcTime();
            CcTimeLastPlantTime = new CcControl.CcTime();
            CcTimeLastRollCallTime = new CcControl.CcTime();
            CcTimeContinuousDrivingTime = new CcControl.CcTime();
            ccLabel1 = new CcControl.CcLabel();
            ((System.ComponentModel.ISupportInitialize)NumericUpDownExLastPlantCount).BeginInit();
            ((System.ComponentModel.ISupportInitialize)NumericUpDownExFirstOdoMeter).BeginInit();
            ((System.ComponentModel.ISupportInitialize)NumericUpDownExLastOdoMeter).BeginInit();
            ((System.ComponentModel.ISupportInitialize)NumericUpDownExOilAmount).BeginInit();
            SuspendLayout();
            // 
            // LabelExSetName
            // 
            LabelExSetName.BorderStyle = BorderStyle.Fixed3D;
            LabelExSetName.Font = new Font("Yu Gothic UI", 18F);
            LabelExSetName.Location = new Point(4, 4);
            LabelExSetName.Name = "LabelExSetName";
            LabelExSetName.Size = new Size(424, 40);
            LabelExSetName.TabIndex = 0;
            LabelExSetName.Text = "歌舞伎2-52";
            LabelExSetName.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // labelEx2
            // 
            labelEx2.AutoSize = true;
            labelEx2.Location = new Point(88, 60);
            labelEx2.Name = "labelEx2";
            labelEx2.Size = new Size(55, 15);
            labelEx2.TabIndex = 1;
            labelEx2.Text = "点呼日付";
            // 
            // DateTimePickerExOperationDate
            // 
            DateTimePickerExOperationDate.CultureFlag = false;
            DateTimePickerExOperationDate.CustomFormat = " 明治33年01月01日(月曜日)";
            DateTimePickerExOperationDate.Font = new Font("Yu Gothic UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 128);
            DateTimePickerExOperationDate.Format = DateTimePickerFormat.Custom;
            DateTimePickerExOperationDate.ImeMode = ImeMode.Off;
            DateTimePickerExOperationDate.Location = new Point(148, 56);
            DateTimePickerExOperationDate.Name = "DateTimePickerExOperationDate";
            DateTimePickerExOperationDate.Size = new Size(196, 25);
            DateTimePickerExOperationDate.TabIndex = 0;
            DateTimePickerExOperationDate.Value = new DateTime(1900, 1, 1, 0, 0, 0, 0);
            // 
            // labelEx3
            // 
            labelEx3.AutoSize = true;
            labelEx3.Location = new Point(88, 92);
            labelEx3.Name = "labelEx3";
            labelEx3.Size = new Size(55, 15);
            labelEx3.TabIndex = 4;
            labelEx3.Text = "出庫時刻";
            // 
            // labelEx4
            // 
            labelEx4.AutoSize = true;
            labelEx4.Location = new Point(64, 156);
            labelEx4.Name = "labelEx4";
            labelEx4.Size = new Size(79, 15);
            labelEx4.TabIndex = 5;
            labelEx4.Text = "最終搬入場所";
            // 
            // labelEx5
            // 
            labelEx5.AutoSize = true;
            labelEx5.Location = new Point(88, 124);
            labelEx5.Name = "labelEx5";
            labelEx5.Size = new Size(55, 15);
            labelEx5.TabIndex = 6;
            labelEx5.Text = "搬入回数";
            // 
            // labelEx6
            // 
            labelEx6.AutoSize = true;
            labelEx6.Location = new Point(64, 188);
            labelEx6.Name = "labelEx6";
            labelEx6.Size = new Size(79, 15);
            labelEx6.TabIndex = 7;
            labelEx6.Text = "最終搬入時刻";
            // 
            // labelEx7
            // 
            labelEx7.AutoSize = true;
            labelEx7.Location = new Point(88, 220);
            labelEx7.Name = "labelEx7";
            labelEx7.Size = new Size(55, 15);
            labelEx7.TabIndex = 8;
            labelEx7.Text = "帰庫時刻";
            // 
            // labelEx8
            // 
            labelEx8.AutoSize = true;
            labelEx8.Location = new Point(64, 280);
            labelEx8.Name = "labelEx8";
            labelEx8.Size = new Size(76, 15);
            labelEx8.TabIndex = 9;
            labelEx8.Text = "出庫時メーター";
            // 
            // labelEx9
            // 
            labelEx9.AutoSize = true;
            labelEx9.Location = new Point(64, 312);
            labelEx9.Name = "labelEx9";
            labelEx9.Size = new Size(76, 15);
            labelEx9.TabIndex = 10;
            labelEx9.Text = "帰庫時メーター";
            // 
            // labelEx10
            // 
            labelEx10.AutoSize = true;
            labelEx10.Location = new Point(96, 344);
            labelEx10.Name = "labelEx10";
            labelEx10.Size = new Size(43, 15);
            labelEx10.TabIndex = 11;
            labelEx10.Text = "給油量";
            // 
            // CheckBoxExDelete
            // 
            CheckBoxExDelete.AutoSize = true;
            CheckBoxExDelete.Location = new Point(148, 384);
            CheckBoxExDelete.Name = "CheckBoxExDelete";
            CheckBoxExDelete.Size = new Size(107, 19);
            CheckBoxExDelete.TabIndex = 10;
            CheckBoxExDelete.TabStop = false;
            CheckBoxExDelete.Text = "帰庫点呼を削除";
            CheckBoxExDelete.UseVisualStyleBackColor = true;
            // 
            // ButtonExUpdate
            // 
            ButtonExUpdate.ForeColor = SystemColors.ControlText;
            ButtonExUpdate.Location = new Point(280, 380);
            ButtonExUpdate.Name = "ButtonExUpdate";
            ButtonExUpdate.SetTextDirectionVertical = "";
            ButtonExUpdate.Size = new Size(136, 28);
            ButtonExUpdate.TabIndex = 11;
            ButtonExUpdate.Text = "UPDATE";
            ButtonExUpdate.UseVisualStyleBackColor = true;
            ButtonExUpdate.Click += ButtonExUpdate_Click;
            // 
            // NumericUpDownExLastPlantCount
            // 
            NumericUpDownExLastPlantCount.Font = new Font("Yu Gothic UI", 9.75F);
            NumericUpDownExLastPlantCount.ImeMode = ImeMode.Off;
            NumericUpDownExLastPlantCount.Location = new Point(148, 120);
            NumericUpDownExLastPlantCount.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            NumericUpDownExLastPlantCount.Name = "NumericUpDownExLastPlantCount";
            NumericUpDownExLastPlantCount.Size = new Size(104, 25);
            NumericUpDownExLastPlantCount.TabIndex = 2;
            NumericUpDownExLastPlantCount.TextAlign = HorizontalAlignment.Right;
            // 
            // ComboBoxExLastPlantName
            // 
            ComboBoxExLastPlantName.Font = new Font("Yu Gothic UI", 9.75F);
            ComboBoxExLastPlantName.FormattingEnabled = true;
            ComboBoxExLastPlantName.ImeMode = ImeMode.Hiragana;
            ComboBoxExLastPlantName.Location = new Point(148, 152);
            ComboBoxExLastPlantName.Name = "ComboBoxExLastPlantName";
            ComboBoxExLastPlantName.Size = new Size(196, 25);
            ComboBoxExLastPlantName.TabIndex = 3;
            // 
            // NumericUpDownExFirstOdoMeter
            // 
            NumericUpDownExFirstOdoMeter.Font = new Font("Yu Gothic UI", 9.75F);
            NumericUpDownExFirstOdoMeter.ImeMode = ImeMode.Off;
            NumericUpDownExFirstOdoMeter.Location = new Point(148, 276);
            NumericUpDownExFirstOdoMeter.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            NumericUpDownExFirstOdoMeter.Name = "NumericUpDownExFirstOdoMeter";
            NumericUpDownExFirstOdoMeter.Size = new Size(104, 25);
            NumericUpDownExFirstOdoMeter.TabIndex = 7;
            NumericUpDownExFirstOdoMeter.TabStop = false;
            NumericUpDownExFirstOdoMeter.TextAlign = HorizontalAlignment.Right;
            // 
            // NumericUpDownExLastOdoMeter
            // 
            NumericUpDownExLastOdoMeter.Font = new Font("Yu Gothic UI", 9.75F);
            NumericUpDownExLastOdoMeter.ImeMode = ImeMode.Off;
            NumericUpDownExLastOdoMeter.Location = new Point(148, 308);
            NumericUpDownExLastOdoMeter.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            NumericUpDownExLastOdoMeter.Name = "NumericUpDownExLastOdoMeter";
            NumericUpDownExLastOdoMeter.Size = new Size(104, 25);
            NumericUpDownExLastOdoMeter.TabIndex = 8;
            NumericUpDownExLastOdoMeter.TabStop = false;
            NumericUpDownExLastOdoMeter.TextAlign = HorizontalAlignment.Right;
            // 
            // NumericUpDownExOilAmount
            // 
            NumericUpDownExOilAmount.Font = new Font("Yu Gothic UI", 9.75F);
            NumericUpDownExOilAmount.ImeMode = ImeMode.Off;
            NumericUpDownExOilAmount.Location = new Point(148, 340);
            NumericUpDownExOilAmount.Name = "NumericUpDownExOilAmount";
            NumericUpDownExOilAmount.Size = new Size(104, 25);
            NumericUpDownExOilAmount.TabIndex = 9;
            NumericUpDownExOilAmount.TextAlign = HorizontalAlignment.Right;
            // 
            // CcTimeFirstRollCallTime
            // 
            CcTimeFirstRollCallTime.BorderStyle = BorderStyle.FixedSingle;
            CcTimeFirstRollCallTime.Font = new Font("Yu Gothic UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 128);
            CcTimeFirstRollCallTime.ImeMode = ImeMode.Off;
            CcTimeFirstRollCallTime.Location = new Point(148, 88);
            CcTimeFirstRollCallTime.Mask = "00:00";
            CcTimeFirstRollCallTime.Name = "CcTimeFirstRollCallTime";
            CcTimeFirstRollCallTime.RejectInputOnFirstFailure = true;
            CcTimeFirstRollCallTime.Size = new Size(104, 25);
            CcTimeFirstRollCallTime.TabIndex = 1;
            CcTimeFirstRollCallTime.TextAlign = HorizontalAlignment.Right;
            CcTimeFirstRollCallTime.ValidatingType = typeof(DateTime);
            // 
            // CcTimeLastPlantTime
            // 
            CcTimeLastPlantTime.BorderStyle = BorderStyle.FixedSingle;
            CcTimeLastPlantTime.Font = new Font("Yu Gothic UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 128);
            CcTimeLastPlantTime.ImeMode = ImeMode.Off;
            CcTimeLastPlantTime.Location = new Point(148, 184);
            CcTimeLastPlantTime.Mask = "00:00";
            CcTimeLastPlantTime.Name = "CcTimeLastPlantTime";
            CcTimeLastPlantTime.RejectInputOnFirstFailure = true;
            CcTimeLastPlantTime.Size = new Size(104, 25);
            CcTimeLastPlantTime.TabIndex = 4;
            CcTimeLastPlantTime.TextAlign = HorizontalAlignment.Right;
            CcTimeLastPlantTime.ValidatingType = typeof(DateTime);
            // 
            // CcTimeLastRollCallTime
            // 
            CcTimeLastRollCallTime.BorderStyle = BorderStyle.FixedSingle;
            CcTimeLastRollCallTime.Font = new Font("Yu Gothic UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 128);
            CcTimeLastRollCallTime.ImeMode = ImeMode.Off;
            CcTimeLastRollCallTime.Location = new Point(148, 216);
            CcTimeLastRollCallTime.Mask = "00:00";
            CcTimeLastRollCallTime.Name = "CcTimeLastRollCallTime";
            CcTimeLastRollCallTime.RejectInputOnFirstFailure = true;
            CcTimeLastRollCallTime.Size = new Size(104, 25);
            CcTimeLastRollCallTime.TabIndex = 5;
            CcTimeLastRollCallTime.TextAlign = HorizontalAlignment.Right;
            CcTimeLastRollCallTime.ValidatingType = typeof(DateTime);
            // 
            // CcTimeContinuousDrivingTime
            // 
            CcTimeContinuousDrivingTime.BorderStyle = BorderStyle.FixedSingle;
            CcTimeContinuousDrivingTime.Font = new Font("Yu Gothic UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 128);
            CcTimeContinuousDrivingTime.ImeMode = ImeMode.Off;
            CcTimeContinuousDrivingTime.Location = new Point(148, 247);
            CcTimeContinuousDrivingTime.Mask = "00:00";
            CcTimeContinuousDrivingTime.Name = "CcTimeContinuousDrivingTime";
            CcTimeContinuousDrivingTime.RejectInputOnFirstFailure = true;
            CcTimeContinuousDrivingTime.Size = new Size(104, 25);
            CcTimeContinuousDrivingTime.TabIndex = 6;
            CcTimeContinuousDrivingTime.TextAlign = HorizontalAlignment.Right;
            CcTimeContinuousDrivingTime.ValidatingType = typeof(DateTime);
            // 
            // ccLabel1
            // 
            ccLabel1.AutoSize = true;
            ccLabel1.Font = new Font("Yu Gothic UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 128);
            ccLabel1.ForeColor = Color.Red;
            ccLabel1.Location = new Point(64, 252);
            ccLabel1.Name = "ccLabel1";
            ccLabel1.Size = new Size(79, 15);
            ccLabel1.TabIndex = 13;
            ccLabel1.Text = "連続運転時間";
            // 
            // LastRollCall
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(433, 421);
            Controls.Add(ccLabel1);
            Controls.Add(CcTimeContinuousDrivingTime);
            Controls.Add(CcTimeLastRollCallTime);
            Controls.Add(CcTimeLastPlantTime);
            Controls.Add(CcTimeFirstRollCallTime);
            Controls.Add(NumericUpDownExOilAmount);
            Controls.Add(NumericUpDownExLastOdoMeter);
            Controls.Add(NumericUpDownExFirstOdoMeter);
            Controls.Add(ComboBoxExLastPlantName);
            Controls.Add(NumericUpDownExLastPlantCount);
            Controls.Add(ButtonExUpdate);
            Controls.Add(CheckBoxExDelete);
            Controls.Add(labelEx10);
            Controls.Add(labelEx9);
            Controls.Add(labelEx8);
            Controls.Add(labelEx7);
            Controls.Add(labelEx6);
            Controls.Add(labelEx5);
            Controls.Add(labelEx4);
            Controls.Add(labelEx3);
            Controls.Add(DateTimePickerExOperationDate);
            Controls.Add(labelEx2);
            Controls.Add(LabelExSetName);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "LastRollCall";
            Text = "LastRollCall";
            FormClosing += LastRollCall_FormClosing;
            KeyDown += LastRollCall_KeyDown;
            ((System.ComponentModel.ISupportInitialize)NumericUpDownExLastPlantCount).EndInit();
            ((System.ComponentModel.ISupportInitialize)NumericUpDownExFirstOdoMeter).EndInit();
            ((System.ComponentModel.ISupportInitialize)NumericUpDownExLastOdoMeter).EndInit();
            ((System.ComponentModel.ISupportInitialize)NumericUpDownExOilAmount).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private CcControl.CcLabel LabelExSetName;
        private CcControl.CcLabel labelEx2;
        private CcControl.CcDateTime DateTimePickerExOperationDate;
        private CcControl.CcLabel labelEx3;
        private CcControl.CcLabel labelEx4;
        private CcControl.CcLabel labelEx5;
        private CcControl.CcLabel labelEx6;
        private CcControl.CcLabel labelEx7;
        private CcControl.CcLabel labelEx8;
        private CcControl.CcLabel labelEx9;
        private CcControl.CcLabel labelEx10;
        private CcControl.CcCheckBox CheckBoxExDelete;
        private CcControl.CcButton ButtonExUpdate;
        private CcControl.CcNumericUpDown NumericUpDownExLastPlantCount;
        private CcControl.CcComboBox ComboBoxExLastPlantName;
        private CcControl.CcNumericUpDown NumericUpDownExFirstOdoMeter;
        private CcControl.CcNumericUpDown NumericUpDownExLastOdoMeter;
        private CcControl.CcNumericUpDown NumericUpDownExOilAmount;
        private CcControl.CcTime CcTimeFirstRollCallTime;
        private CcControl.CcTime CcTimeLastPlantTime;
        private CcControl.CcTime CcTimeLastRollCallTime;
        private CcControl.CcTime CcTimeContinuousDrivingTime;
        private CcControl.CcLabel ccLabel1;
    }
}