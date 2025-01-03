namespace VehicleDispatch {
    partial class Memo {
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
            this.TextBoxExMemo = new ControlEx.TextBoxEx();
            this.ButtonExUpdate = new ControlEx.ButtonEx();
            this.ButtonExTimeStamp = new ControlEx.ButtonEx();
            this.SuspendLayout();
            // 
            // TextBoxExMemo
            // 
            this.TextBoxExMemo.Font = new Font("Yu Gothic UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            this.TextBoxExMemo.ImeMode = ImeMode.Hiragana;
            this.TextBoxExMemo.Location = new Point(4, 4);
            this.TextBoxExMemo.Multiline = true;
            this.TextBoxExMemo.Name = "TextBoxExMemo";
            this.TextBoxExMemo.Size = new Size(524, 100);
            this.TextBoxExMemo.TabIndex = 0;
            // 
            // ButtonExUpdate
            // 
            this.ButtonExUpdate.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.ButtonExUpdate.Location = new Point(356, 112);
            this.ButtonExUpdate.Name = "ButtonExUpdate";
            this.ButtonExUpdate.SetTextDirectionVertical = "";
            this.ButtonExUpdate.Size = new Size(156, 36);
            this.ButtonExUpdate.TabIndex = 1;
            this.ButtonExUpdate.Text = "UPDATE";
            this.ButtonExUpdate.UseVisualStyleBackColor = true;
            this.ButtonExUpdate.Click += this.ButtonExUpdate_Click;
            // 
            // ButtonExTimeStamp
            // 
            this.ButtonExTimeStamp.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.ButtonExTimeStamp.Location = new Point(40, 116);
            this.ButtonExTimeStamp.Name = "ButtonExTimeStamp";
            this.ButtonExTimeStamp.SetTextDirectionVertical = "";
            this.ButtonExTimeStamp.Size = new Size(116, 28);
            this.ButtonExTimeStamp.TabIndex = 2;
            this.ButtonExTimeStamp.Text = "タイムスタンプ";
            this.ButtonExTimeStamp.UseVisualStyleBackColor = true;
            this.ButtonExTimeStamp.Click += this.ButtonExTimeStamp_Click;
            // 
            // Memo
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(534, 158);
            this.Controls.Add(this.ButtonExTimeStamp);
            this.Controls.Add(this.ButtonExUpdate);
            this.Controls.Add(this.TextBoxExMemo);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Memo";
            this.Text = "Memo";
            this.FormClosing += this.Memo_FormClosing;
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private ControlEx.TextBoxEx TextBoxExMemo;
        private ControlEx.ButtonEx ButtonExUpdate;
        private ControlEx.ButtonEx ButtonExTimeStamp;
    }
}