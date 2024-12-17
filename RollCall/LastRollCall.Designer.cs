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
            this.LabelExSetName = new ControlEx.LabelEx();
            this.labelEx2 = new ControlEx.LabelEx();
            this.DateTimePickerExOperationDate = new ControlEx.DateTimePickerEx();
            this.MaskedTextBoxExFirstRollCallTime = new ControlEx.MaskedTextBoxEx();
            this.labelEx3 = new ControlEx.LabelEx();
            this.labelEx4 = new ControlEx.LabelEx();
            this.labelEx5 = new ControlEx.LabelEx();
            this.labelEx6 = new ControlEx.LabelEx();
            this.labelEx7 = new ControlEx.LabelEx();
            this.labelEx8 = new ControlEx.LabelEx();
            this.labelEx9 = new ControlEx.LabelEx();
            this.labelEx10 = new ControlEx.LabelEx();
            this.CheckBoxExDelete = new ControlEx.CheckBoxEx();
            this.ButtonExUpdate = new ControlEx.ButtonEx();
            this.NumericUpDownExLastPlantCount = new ControlEx.NumericUpDownEx();
            this.MaskedTextBoxExLastPlantTime = new ControlEx.MaskedTextBoxEx();
            this.ComboBoxExLastPlantName = new ControlEx.ComboBoxEx();
            this.MaskedTextBoxExLastRollCallTime = new ControlEx.MaskedTextBoxEx();
            this.NumericUpDownExFirstOdoMeter = new ControlEx.NumericUpDownEx();
            this.NumericUpDownExLastOdoMeter = new ControlEx.NumericUpDownEx();
            this.NumericUpDownExOilAmount = new ControlEx.NumericUpDownEx();
            ((System.ComponentModel.ISupportInitialize)this.NumericUpDownExLastPlantCount).BeginInit();
            ((System.ComponentModel.ISupportInitialize)this.NumericUpDownExFirstOdoMeter).BeginInit();
            ((System.ComponentModel.ISupportInitialize)this.NumericUpDownExLastOdoMeter).BeginInit();
            ((System.ComponentModel.ISupportInitialize)this.NumericUpDownExOilAmount).BeginInit();
            this.SuspendLayout();
            // 
            // LabelExSetName
            // 
            this.LabelExSetName.BorderStyle = BorderStyle.Fixed3D;
            this.LabelExSetName.Font = new Font("Yu Gothic UI", 18F, FontStyle.Regular, GraphicsUnit.Point);
            this.LabelExSetName.Location = new Point(4, 4);
            this.LabelExSetName.Name = "LabelExSetName";
            this.LabelExSetName.Size = new Size(424, 40);
            this.LabelExSetName.TabIndex = 0;
            this.LabelExSetName.Text = "歌舞伎2-52";
            this.LabelExSetName.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // labelEx2
            // 
            this.labelEx2.AutoSize = true;
            this.labelEx2.Location = new Point(88, 60);
            this.labelEx2.Name = "labelEx2";
            this.labelEx2.Size = new Size(55, 15);
            this.labelEx2.TabIndex = 1;
            this.labelEx2.Text = "点呼日付";
            // 
            // DateTimePickerExOperationDate
            // 
            this.DateTimePickerExOperationDate.CultureFlag = false;
            this.DateTimePickerExOperationDate.CustomFormat = " 明治33年01月01日(月曜日)";
            this.DateTimePickerExOperationDate.Format = DateTimePickerFormat.Custom;
            this.DateTimePickerExOperationDate.ImeMode = ImeMode.Off;
            this.DateTimePickerExOperationDate.Location = new Point(148, 56);
            this.DateTimePickerExOperationDate.Name = "DateTimePickerExOperationDate";
            this.DateTimePickerExOperationDate.Size = new Size(196, 23);
            this.DateTimePickerExOperationDate.TabIndex = 0;
            this.DateTimePickerExOperationDate.Value = new DateTime(1900, 1, 1, 0, 0, 0, 0);
            // 
            // MaskedTextBoxExFirstRollCallTime
            // 
            this.MaskedTextBoxExFirstRollCallTime.ImeMode = ImeMode.Off;
            this.MaskedTextBoxExFirstRollCallTime.Location = new Point(148, 84);
            this.MaskedTextBoxExFirstRollCallTime.Name = "MaskedTextBoxExFirstRollCallTime";
            this.MaskedTextBoxExFirstRollCallTime.RejectInputOnFirstFailure = true;
            this.MaskedTextBoxExFirstRollCallTime.Size = new Size(104, 23);
            this.MaskedTextBoxExFirstRollCallTime.TabIndex = 1;
            this.MaskedTextBoxExFirstRollCallTime.TextAlign = HorizontalAlignment.Right;
            this.MaskedTextBoxExFirstRollCallTime.ValidatingType = typeof(DateTime);
            // 
            // labelEx3
            // 
            this.labelEx3.AutoSize = true;
            this.labelEx3.Location = new Point(88, 88);
            this.labelEx3.Name = "labelEx3";
            this.labelEx3.Size = new Size(55, 15);
            this.labelEx3.TabIndex = 4;
            this.labelEx3.Text = "出庫時刻";
            // 
            // labelEx4
            // 
            this.labelEx4.AutoSize = true;
            this.labelEx4.Location = new Point(64, 144);
            this.labelEx4.Name = "labelEx4";
            this.labelEx4.Size = new Size(79, 15);
            this.labelEx4.TabIndex = 5;
            this.labelEx4.Text = "最終搬入場所";
            // 
            // labelEx5
            // 
            this.labelEx5.AutoSize = true;
            this.labelEx5.Location = new Point(88, 116);
            this.labelEx5.Name = "labelEx5";
            this.labelEx5.Size = new Size(55, 15);
            this.labelEx5.TabIndex = 6;
            this.labelEx5.Text = "搬入回数";
            // 
            // labelEx6
            // 
            this.labelEx6.AutoSize = true;
            this.labelEx6.Location = new Point(64, 172);
            this.labelEx6.Name = "labelEx6";
            this.labelEx6.Size = new Size(79, 15);
            this.labelEx6.TabIndex = 7;
            this.labelEx6.Text = "最終搬入時刻";
            // 
            // labelEx7
            // 
            this.labelEx7.AutoSize = true;
            this.labelEx7.Location = new Point(88, 200);
            this.labelEx7.Name = "labelEx7";
            this.labelEx7.Size = new Size(55, 15);
            this.labelEx7.TabIndex = 8;
            this.labelEx7.Text = "帰庫時刻";
            // 
            // labelEx8
            // 
            this.labelEx8.AutoSize = true;
            this.labelEx8.Location = new Point(64, 228);
            this.labelEx8.Name = "labelEx8";
            this.labelEx8.Size = new Size(76, 15);
            this.labelEx8.TabIndex = 9;
            this.labelEx8.Text = "出庫時メーター";
            // 
            // labelEx9
            // 
            this.labelEx9.AutoSize = true;
            this.labelEx9.Location = new Point(64, 256);
            this.labelEx9.Name = "labelEx9";
            this.labelEx9.Size = new Size(76, 15);
            this.labelEx9.TabIndex = 10;
            this.labelEx9.Text = "帰庫時メーター";
            // 
            // labelEx10
            // 
            this.labelEx10.AutoSize = true;
            this.labelEx10.Location = new Point(96, 284);
            this.labelEx10.Name = "labelEx10";
            this.labelEx10.Size = new Size(43, 15);
            this.labelEx10.TabIndex = 11;
            this.labelEx10.Text = "給油量";
            // 
            // CheckBoxExDelete
            // 
            this.CheckBoxExDelete.AutoSize = true;
            this.CheckBoxExDelete.Location = new Point(148, 324);
            this.CheckBoxExDelete.Name = "CheckBoxExDelete";
            this.CheckBoxExDelete.Size = new Size(107, 19);
            this.CheckBoxExDelete.TabIndex = 9;
            this.CheckBoxExDelete.Text = "帰庫点呼を削除";
            this.CheckBoxExDelete.UseVisualStyleBackColor = true;
            // 
            // ButtonExUpdate
            // 
            this.ButtonExUpdate.Location = new Point(264, 316);
            this.ButtonExUpdate.Name = "ButtonExUpdate";
            this.ButtonExUpdate.SetTextDirectionVertical = "";
            this.ButtonExUpdate.Size = new Size(144, 36);
            this.ButtonExUpdate.TabIndex = 10;
            this.ButtonExUpdate.Text = "UPDATE";
            this.ButtonExUpdate.UseVisualStyleBackColor = true;
            this.ButtonExUpdate.Click += this.ButtonExUpdate_Click;
            // 
            // NumericUpDownExLastPlantCount
            // 
            this.NumericUpDownExLastPlantCount.ImeMode = ImeMode.Off;
            this.NumericUpDownExLastPlantCount.Location = new Point(148, 112);
            this.NumericUpDownExLastPlantCount.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            this.NumericUpDownExLastPlantCount.Name = "NumericUpDownExLastPlantCount";
            this.NumericUpDownExLastPlantCount.Size = new Size(104, 23);
            this.NumericUpDownExLastPlantCount.TabIndex = 2;
            this.NumericUpDownExLastPlantCount.TextAlign = HorizontalAlignment.Right;
            // 
            // MaskedTextBoxExLastPlantTime
            // 
            this.MaskedTextBoxExLastPlantTime.ImeMode = ImeMode.Off;
            this.MaskedTextBoxExLastPlantTime.Location = new Point(148, 168);
            this.MaskedTextBoxExLastPlantTime.Name = "MaskedTextBoxExLastPlantTime";
            this.MaskedTextBoxExLastPlantTime.Size = new Size(104, 23);
            this.MaskedTextBoxExLastPlantTime.TabIndex = 4;
            this.MaskedTextBoxExLastPlantTime.TextAlign = HorizontalAlignment.Right;
            this.MaskedTextBoxExLastPlantTime.ValidatingType = typeof(DateTime);
            // 
            // ComboBoxExLastPlantName
            // 
            this.ComboBoxExLastPlantName.FormattingEnabled = true;
            this.ComboBoxExLastPlantName.ImeMode = ImeMode.Hiragana;
            this.ComboBoxExLastPlantName.Location = new Point(148, 140);
            this.ComboBoxExLastPlantName.Name = "ComboBoxExLastPlantName";
            this.ComboBoxExLastPlantName.Size = new Size(196, 23);
            this.ComboBoxExLastPlantName.TabIndex = 3;
            // 
            // MaskedTextBoxExLastRollCallTime
            // 
            this.MaskedTextBoxExLastRollCallTime.ImeMode = ImeMode.Off;
            this.MaskedTextBoxExLastRollCallTime.Location = new Point(148, 196);
            this.MaskedTextBoxExLastRollCallTime.Name = "MaskedTextBoxExLastRollCallTime";
            this.MaskedTextBoxExLastRollCallTime.Size = new Size(104, 23);
            this.MaskedTextBoxExLastRollCallTime.TabIndex = 5;
            this.MaskedTextBoxExLastRollCallTime.TextAlign = HorizontalAlignment.Right;
            this.MaskedTextBoxExLastRollCallTime.ValidatingType = typeof(DateTime);
            // 
            // NumericUpDownExFirstOdoMeter
            // 
            this.NumericUpDownExFirstOdoMeter.ImeMode = ImeMode.Off;
            this.NumericUpDownExFirstOdoMeter.Location = new Point(148, 224);
            this.NumericUpDownExFirstOdoMeter.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            this.NumericUpDownExFirstOdoMeter.Name = "NumericUpDownExFirstOdoMeter";
            this.NumericUpDownExFirstOdoMeter.Size = new Size(104, 23);
            this.NumericUpDownExFirstOdoMeter.TabIndex = 6;
            this.NumericUpDownExFirstOdoMeter.TextAlign = HorizontalAlignment.Right;
            // 
            // NumericUpDownExLastOdoMeter
            // 
            this.NumericUpDownExLastOdoMeter.ImeMode = ImeMode.Off;
            this.NumericUpDownExLastOdoMeter.Location = new Point(148, 252);
            this.NumericUpDownExLastOdoMeter.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            this.NumericUpDownExLastOdoMeter.Name = "NumericUpDownExLastOdoMeter";
            this.NumericUpDownExLastOdoMeter.Size = new Size(104, 23);
            this.NumericUpDownExLastOdoMeter.TabIndex = 7;
            this.NumericUpDownExLastOdoMeter.TextAlign = HorizontalAlignment.Right;
            // 
            // NumericUpDownExOilAmount
            // 
            this.NumericUpDownExOilAmount.ImeMode = ImeMode.Off;
            this.NumericUpDownExOilAmount.Location = new Point(148, 280);
            this.NumericUpDownExOilAmount.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            this.NumericUpDownExOilAmount.Name = "NumericUpDownExOilAmount";
            this.NumericUpDownExOilAmount.Size = new Size(104, 23);
            this.NumericUpDownExOilAmount.TabIndex = 8;
            this.NumericUpDownExOilAmount.TextAlign = HorizontalAlignment.Right;
            // 
            // LastRollCall
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(432, 372);
            this.Controls.Add(this.NumericUpDownExOilAmount);
            this.Controls.Add(this.NumericUpDownExLastOdoMeter);
            this.Controls.Add(this.NumericUpDownExFirstOdoMeter);
            this.Controls.Add(this.MaskedTextBoxExLastRollCallTime);
            this.Controls.Add(this.ComboBoxExLastPlantName);
            this.Controls.Add(this.MaskedTextBoxExLastPlantTime);
            this.Controls.Add(this.NumericUpDownExLastPlantCount);
            this.Controls.Add(this.ButtonExUpdate);
            this.Controls.Add(this.CheckBoxExDelete);
            this.Controls.Add(this.labelEx10);
            this.Controls.Add(this.labelEx9);
            this.Controls.Add(this.labelEx8);
            this.Controls.Add(this.labelEx7);
            this.Controls.Add(this.labelEx6);
            this.Controls.Add(this.labelEx5);
            this.Controls.Add(this.labelEx4);
            this.Controls.Add(this.labelEx3);
            this.Controls.Add(this.MaskedTextBoxExFirstRollCallTime);
            this.Controls.Add(this.DateTimePickerExOperationDate);
            this.Controls.Add(this.labelEx2);
            this.Controls.Add(this.LabelExSetName);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LastRollCall";
            this.Text = "LastRollCall";
            this.FormClosing += this.LastRollCall_FormClosing;
            this.KeyDown += this.LastRollCall_KeyDown;
            ((System.ComponentModel.ISupportInitialize)this.NumericUpDownExLastPlantCount).EndInit();
            ((System.ComponentModel.ISupportInitialize)this.NumericUpDownExFirstOdoMeter).EndInit();
            ((System.ComponentModel.ISupportInitialize)this.NumericUpDownExLastOdoMeter).EndInit();
            ((System.ComponentModel.ISupportInitialize)this.NumericUpDownExOilAmount).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private ControlEx.LabelEx LabelExSetName;
        private ControlEx.LabelEx labelEx2;
        private ControlEx.DateTimePickerEx DateTimePickerExOperationDate;
        private ControlEx.MaskedTextBoxEx MaskedTextBoxExFirstRollCallTime;
        private ControlEx.LabelEx labelEx3;
        private ControlEx.LabelEx labelEx4;
        private ControlEx.LabelEx labelEx5;
        private ControlEx.LabelEx labelEx6;
        private ControlEx.LabelEx labelEx7;
        private ControlEx.LabelEx labelEx8;
        private ControlEx.LabelEx labelEx9;
        private ControlEx.LabelEx labelEx10;
        private ControlEx.CheckBoxEx CheckBoxExDelete;
        private ControlEx.ButtonEx ButtonExUpdate;
        private ControlEx.NumericUpDownEx NumericUpDownExLastPlantCount;
        private ControlEx.MaskedTextBoxEx MaskedTextBoxExLastPlantTime;
        private ControlEx.ComboBoxEx ComboBoxExLastPlantName;
        private ControlEx.MaskedTextBoxEx MaskedTextBoxExLastRollCallTime;
        private ControlEx.NumericUpDownEx NumericUpDownExFirstOdoMeter;
        private ControlEx.NumericUpDownEx NumericUpDownExLastOdoMeter;
        private ControlEx.NumericUpDownEx NumericUpDownExOilAmount;
    }
}