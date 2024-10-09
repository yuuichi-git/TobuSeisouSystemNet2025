namespace TobuSeisouSystemNet2025 {
    partial class StartProject {
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
            this.TableLayoutPanelExBase = new ControlEx.TableLayoutPanelEx();
            this.MenuStripEx1 = new ControlEx.MenuStripEx();
            this.StatusStripEx1 = new ControlEx.StatusStripEx();
            this.PanelExLeft = new ControlEx.PanelEx();
            this.LabelExLocation = new ControlEx.LabelEx();
            this.LabelExIpAddress = new ControlEx.LabelEx();
            this.LabelExPcName = new ControlEx.LabelEx();
            this.TreeView1 = new TreeView();
            this.PanelExRight = new ControlEx.PanelEx();
            this.ComboBoxExMonitor = new ControlEx.ComboBoxEx();
            this.labelEx3 = new ControlEx.LabelEx();
            this.ButtonExDisConnect = new ControlEx.ButtonEx();
            this.ButtonExConnect = new ControlEx.ButtonEx();
            this.LabelExStatus = new ControlEx.LabelEx();
            this.LabelExDataBaseName = new ControlEx.LabelEx();
            this.LabelExServerName = new ControlEx.LabelEx();
            this.labelEx2 = new ControlEx.LabelEx();
            this.labelEx1 = new ControlEx.LabelEx();
            this.TabControlExConnect = new ControlEx.TabControlEx();
            this.TabPageSystem = new TabPage();
            this.TabPageAdachi = new TabPage();
            this.labelEx5 = new ControlEx.LabelEx();
            this.labelEx4 = new ControlEx.LabelEx();
            this.TabPageMisato = new TabPage();
            this.TableLayoutPanelExBase.SuspendLayout();
            this.PanelExLeft.SuspendLayout();
            this.PanelExRight.SuspendLayout();
            this.TabControlExConnect.SuspendLayout();
            this.TabPageAdachi.SuspendLayout();
            this.SuspendLayout();
            // 
            // TableLayoutPanelExBase
            // 
            this.TableLayoutPanelExBase.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            this.TableLayoutPanelExBase.ColumnCount = 2;
            this.TableLayoutPanelExBase.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            this.TableLayoutPanelExBase.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70F));
            this.TableLayoutPanelExBase.Controls.Add(this.MenuStripEx1, 0, 0);
            this.TableLayoutPanelExBase.Controls.Add(this.StatusStripEx1, 0, 2);
            this.TableLayoutPanelExBase.Controls.Add(this.PanelExLeft, 0, 1);
            this.TableLayoutPanelExBase.Controls.Add(this.PanelExRight, 1, 1);
            this.TableLayoutPanelExBase.Dock = DockStyle.Fill;
            this.TableLayoutPanelExBase.Location = new Point(0, 0);
            this.TableLayoutPanelExBase.Name = "TableLayoutPanelExBase";
            this.TableLayoutPanelExBase.RowCount = 3;
            this.TableLayoutPanelExBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            this.TableLayoutPanelExBase.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            this.TableLayoutPanelExBase.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            this.TableLayoutPanelExBase.Size = new Size(1119, 721);
            this.TableLayoutPanelExBase.TabIndex = 0;
            // 
            // MenuStripEx1
            // 
            this.TableLayoutPanelExBase.SetColumnSpan(this.MenuStripEx1, 2);
            this.MenuStripEx1.Location = new Point(1, 1);
            this.MenuStripEx1.Name = "MenuStripEx1";
            this.MenuStripEx1.Size = new Size(1117, 24);
            this.MenuStripEx1.TabIndex = 0;
            this.MenuStripEx1.Text = "menuStripEx1";
            this.MenuStripEx1.ToolStripMenuItemDataBaseLocalFlag = false;
            // 
            // StatusStripEx1
            // 
            this.TableLayoutPanelExBase.SetColumnSpan(this.StatusStripEx1, 2);
            this.StatusStripEx1.Location = new Point(1, 698);
            this.StatusStripEx1.Name = "StatusStripEx1";
            this.StatusStripEx1.Size = new Size(1117, 22);
            this.StatusStripEx1.SizingGrip = false;
            this.StatusStripEx1.TabIndex = 1;
            this.StatusStripEx1.Text = "statusStripEx1";
            // 
            // PanelExLeft
            // 
            this.PanelExLeft.Controls.Add(this.LabelExLocation);
            this.PanelExLeft.Controls.Add(this.LabelExIpAddress);
            this.PanelExLeft.Controls.Add(this.LabelExPcName);
            this.PanelExLeft.Controls.Add(this.TreeView1);
            this.PanelExLeft.Dock = DockStyle.Fill;
            this.PanelExLeft.Location = new Point(4, 29);
            this.PanelExLeft.Name = "PanelExLeft";
            this.PanelExLeft.Size = new Size(328, 663);
            this.PanelExLeft.TabIndex = 2;
            // 
            // LabelExLocation
            // 
            this.LabelExLocation.Location = new Point(12, 68);
            this.LabelExLocation.Name = "LabelExLocation";
            this.LabelExLocation.Size = new Size(304, 20);
            this.LabelExLocation.TabIndex = 3;
            this.LabelExLocation.Text = "○ NW-Location : 本社より接続";
            this.LabelExLocation.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // LabelExIpAddress
            // 
            this.LabelExIpAddress.Location = new Point(12, 40);
            this.LabelExIpAddress.Name = "LabelExIpAddress";
            this.LabelExIpAddress.Size = new Size(304, 20);
            this.LabelExIpAddress.TabIndex = 2;
            this.LabelExIpAddress.Text = "○ IPAddress : 192.168.000.000";
            this.LabelExIpAddress.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // LabelExPcName
            // 
            this.LabelExPcName.Location = new Point(12, 12);
            this.LabelExPcName.Name = "LabelExPcName";
            this.LabelExPcName.Size = new Size(304, 20);
            this.LabelExPcName.TabIndex = 1;
            this.LabelExPcName.Text = "○ PC-Name : YuuichiPC";
            this.LabelExPcName.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // TreeView1
            // 
            this.TreeView1.BackColor = SystemColors.Control;
            this.TreeView1.Location = new Point(4, 96);
            this.TreeView1.Name = "TreeView1";
            this.TreeView1.Size = new Size(320, 564);
            this.TreeView1.TabIndex = 0;
            // 
            // PanelExRight
            // 
            this.PanelExRight.Controls.Add(this.ComboBoxExMonitor);
            this.PanelExRight.Controls.Add(this.labelEx3);
            this.PanelExRight.Controls.Add(this.ButtonExDisConnect);
            this.PanelExRight.Controls.Add(this.ButtonExConnect);
            this.PanelExRight.Controls.Add(this.LabelExStatus);
            this.PanelExRight.Controls.Add(this.LabelExDataBaseName);
            this.PanelExRight.Controls.Add(this.LabelExServerName);
            this.PanelExRight.Controls.Add(this.labelEx2);
            this.PanelExRight.Controls.Add(this.labelEx1);
            this.PanelExRight.Controls.Add(this.TabControlExConnect);
            this.PanelExRight.Dock = DockStyle.Fill;
            this.PanelExRight.Location = new Point(339, 29);
            this.PanelExRight.Name = "PanelExRight";
            this.PanelExRight.Size = new Size(776, 663);
            this.PanelExRight.TabIndex = 3;
            // 
            // ComboBoxExMonitor
            // 
            this.ComboBoxExMonitor.DropDownStyle = ComboBoxStyle.DropDownList;
            this.ComboBoxExMonitor.FormattingEnabled = true;
            this.ComboBoxExMonitor.Location = new Point(92, 134);
            this.ComboBoxExMonitor.Name = "ComboBoxExMonitor";
            this.ComboBoxExMonitor.Size = new Size(276, 23);
            this.ComboBoxExMonitor.TabIndex = 11;
            // 
            // labelEx3
            // 
            this.labelEx3.ForeColor = Color.Black;
            this.labelEx3.Location = new Point(36, 136);
            this.labelEx3.Name = "labelEx3";
            this.labelEx3.Size = new Size(56, 20);
            this.labelEx3.TabIndex = 10;
            this.labelEx3.Text = "モニター：";
            this.labelEx3.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // ButtonExDisConnect
            // 
            this.ButtonExDisConnect.Enabled = false;
            this.ButtonExDisConnect.Location = new Point(568, 84);
            this.ButtonExDisConnect.Name = "ButtonExDisConnect";
            this.ButtonExDisConnect.Size = new Size(144, 24);
            this.ButtonExDisConnect.TabIndex = 9;
            this.ButtonExDisConnect.Text = "DisConnect";
            this.ButtonExDisConnect.UseVisualStyleBackColor = true;
            this.ButtonExDisConnect.Click += this.ButtonEx_Click;
            // 
            // ButtonExConnect
            // 
            this.ButtonExConnect.Font = new Font("Yu Gothic UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 128);
            this.ButtonExConnect.Location = new Point(568, 44);
            this.ButtonExConnect.Name = "ButtonExConnect";
            this.ButtonExConnect.Size = new Size(144, 36);
            this.ButtonExConnect.TabIndex = 8;
            this.ButtonExConnect.Text = "Connect";
            this.ButtonExConnect.UseVisualStyleBackColor = true;
            this.ButtonExConnect.Click += this.ButtonEx_Click;
            // 
            // LabelExStatus
            // 
            this.LabelExStatus.ForeColor = Color.DimGray;
            this.LabelExStatus.Location = new Point(36, 108);
            this.LabelExStatus.Name = "LabelExStatus";
            this.LabelExStatus.Size = new Size(332, 20);
            this.LabelExStatus.TabIndex = 7;
            this.LabelExStatus.Text = "状態：";
            this.LabelExStatus.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // LabelExDataBaseName
            // 
            this.LabelExDataBaseName.ForeColor = Color.DimGray;
            this.LabelExDataBaseName.Location = new Point(36, 84);
            this.LabelExDataBaseName.Name = "LabelExDataBaseName";
            this.LabelExDataBaseName.Size = new Size(332, 20);
            this.LabelExDataBaseName.TabIndex = 6;
            this.LabelExDataBaseName.Text = "接続先データベース：";
            this.LabelExDataBaseName.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // LabelExServerName
            // 
            this.LabelExServerName.ForeColor = Color.DimGray;
            this.LabelExServerName.Location = new Point(36, 60);
            this.LabelExServerName.Name = "LabelExServerName";
            this.LabelExServerName.Size = new Size(332, 20);
            this.LabelExServerName.TabIndex = 5;
            this.LabelExServerName.Text = "接続先サーバー：";
            this.LabelExServerName.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // labelEx2
            // 
            this.labelEx2.ForeColor = Color.Black;
            this.labelEx2.Location = new Point(36, 36);
            this.labelEx2.Name = "labelEx2";
            this.labelEx2.Size = new Size(332, 20);
            this.labelEx2.TabIndex = 4;
            this.labelEx2.Text = "データベースへの接続と切断及びステータスを管理";
            this.labelEx2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // labelEx1
            // 
            this.labelEx1.ForeColor = Color.Blue;
            this.labelEx1.Location = new Point(20, 12);
            this.labelEx1.Name = "labelEx1";
            this.labelEx1.Size = new Size(92, 20);
            this.labelEx1.TabIndex = 3;
            this.labelEx1.Text = "データベース接続";
            this.labelEx1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // TabControlExConnect
            // 
            this.TabControlExConnect.Controls.Add(this.TabPageSystem);
            this.TabControlExConnect.Controls.Add(this.TabPageAdachi);
            this.TabControlExConnect.Controls.Add(this.TabPageMisato);
            this.TabControlExConnect.Location = new Point(8, 164);
            this.TabControlExConnect.Name = "TabControlExConnect";
            this.TabControlExConnect.SelectedIndex = 0;
            this.TabControlExConnect.Size = new Size(760, 492);
            this.TabControlExConnect.SizeMode = TabSizeMode.Fixed;
            this.TabControlExConnect.TabIndex = 0;
            // 
            // TabPageSystem
            // 
            this.TabPageSystem.Location = new Point(4, 24);
            this.TabPageSystem.Name = "TabPageSystem";
            this.TabPageSystem.Padding = new Padding(3);
            this.TabPageSystem.Size = new Size(752, 464);
            this.TabPageSystem.TabIndex = 0;
            this.TabPageSystem.Text = "システム管理";
            this.TabPageSystem.UseVisualStyleBackColor = true;
            // 
            // TabPageAdachi
            // 
            this.TabPageAdachi.Controls.Add(this.labelEx5);
            this.TabPageAdachi.Controls.Add(this.labelEx4);
            this.TabPageAdachi.Location = new Point(4, 24);
            this.TabPageAdachi.Name = "TabPageAdachi";
            this.TabPageAdachi.Padding = new Padding(3);
            this.TabPageAdachi.Size = new Size(752, 464);
            this.TabPageAdachi.TabIndex = 1;
            this.TabPageAdachi.Text = "本社";
            this.TabPageAdachi.UseVisualStyleBackColor = true;
            // 
            // labelEx5
            // 
            this.labelEx5.ForeColor = Color.DimGray;
            this.labelEx5.Location = new Point(12, 32);
            this.labelEx5.Name = "labelEx5";
            this.labelEx5.Size = new Size(344, 20);
            this.labelEx5.TabIndex = 13;
            this.labelEx5.Text = "　ドラッグ＆ドロップによるUI配車システム";
            this.labelEx5.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // labelEx4
            // 
            this.labelEx4.ForeColor = Color.Black;
            this.labelEx4.Location = new Point(12, 12);
            this.labelEx4.Name = "labelEx4";
            this.labelEx4.Size = new Size(344, 20);
            this.labelEx4.TabIndex = 12;
            this.labelEx4.Tag = "VehicleDispatchBoard";
            this.labelEx4.Text = "配車システム";
            this.labelEx4.TextAlign = ContentAlignment.MiddleLeft;
            this.labelEx4.Click += this.Label_Click;
            this.labelEx4.MouseEnter += this.Label_MouseEnter;
            this.labelEx4.MouseLeave += this.Label_MouseLeave;
            // 
            // TabPageMisato
            // 
            this.TabPageMisato.Location = new Point(4, 24);
            this.TabPageMisato.Name = "TabPageMisato";
            this.TabPageMisato.Size = new Size(752, 464);
            this.TabPageMisato.TabIndex = 2;
            this.TabPageMisato.Text = "三郷";
            this.TabPageMisato.UseVisualStyleBackColor = true;
            // 
            // StartProject
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(1119, 721);
            this.Controls.Add(this.TableLayoutPanelExBase);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.MenuStripEx1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "StartProject";
            this.Text = "StartForm";
            this.FormClosing += this.StartForm_FormClosing;
            this.TableLayoutPanelExBase.ResumeLayout(false);
            this.TableLayoutPanelExBase.PerformLayout();
            this.PanelExLeft.ResumeLayout(false);
            this.PanelExRight.ResumeLayout(false);
            this.TabControlExConnect.ResumeLayout(false);
            this.TabPageAdachi.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private ControlEx.TableLayoutPanelEx TableLayoutPanelExBase;
        private ControlEx.MenuStripEx MenuStripEx1;
        private ControlEx.StatusStripEx StatusStripEx1;
        private ControlEx.PanelEx PanelExLeft;
        private ControlEx.PanelEx PanelExRight;
        private ControlEx.TabControlEx TabControlExConnect;
        private TabPage TabPageSystem;
        private TabPage TabPageAdachi;
        private TabPage TabPageMisato;
        private TreeView TreeView1;
        private ControlEx.LabelEx LabelExPcName;
        private ControlEx.LabelEx LabelExIpAddress;
        private ControlEx.LabelEx labelEx1;
        private ControlEx.LabelEx LabelExStatus;
        private ControlEx.LabelEx LabelExDataBaseName;
        private ControlEx.LabelEx LabelExServerName;
        private ControlEx.LabelEx labelEx2;
        private ControlEx.ButtonEx ButtonExConnect;
        private ControlEx.ButtonEx ButtonExDisConnect;
        private ControlEx.LabelEx labelEx3;
        private ControlEx.ComboBoxEx ComboBoxExMonitor;
        private ControlEx.LabelEx LabelExLocation;
        private ControlEx.LabelEx labelEx4;
        private ControlEx.LabelEx labelEx5;
    }
}