namespace GUI
{
    partial class frmScanQR
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tableLayoutPanelMain = new System.Windows.Forms.TableLayoutPanel();
            this.panelTop = new System.Windows.Forms.Panel();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.cboCamera = new System.Windows.Forms.ComboBox();
            this.lblSelectCamera = new System.Windows.Forms.Label();
            this.picCamera = new System.Windows.Forms.PictureBox();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.txtResult = new System.Windows.Forms.TextBox();
            this.lblResult = new System.Windows.Forms.Label();
            this.timerScan = new System.Windows.Forms.Timer(this.components);
            this.tableLayoutPanelMain.SuspendLayout();
            this.panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picCamera)).BeginInit();
            this.panelBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanelMain
            // 
            this.tableLayoutPanelMain.ColumnCount = 1;
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelMain.Controls.Add(this.panelTop, 0, 0);
            this.tableLayoutPanelMain.Controls.Add(this.picCamera, 0, 1);
            this.tableLayoutPanelMain.Controls.Add(this.panelBottom, 0, 2);
            this.tableLayoutPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelMain.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            this.tableLayoutPanelMain.RowCount = 3;
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanelMain.Size = new System.Drawing.Size(800, 600);
            this.tableLayoutPanelMain.TabIndex = 0;
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.panelTop.Controls.Add(this.btnStop);
            this.panelTop.Controls.Add(this.btnStart);
            this.panelTop.Controls.Add(this.cboCamera);
            this.panelTop.Controls.Add(this.lblSelectCamera);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelTop.Location = new System.Drawing.Point(3, 3);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(794, 54);
            this.panelTop.TabIndex = 0;
            // 
            // btnStop
            // 
            this.btnStop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.btnStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStop.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnStop.ForeColor = System.Drawing.Color.White;
            this.btnStop.Location = new System.Drawing.Point(680, 12);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(100, 32);
            this.btnStop.TabIndex = 3;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = false;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnStart
            // 
            this.btnStart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStart.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnStart.ForeColor = System.Drawing.Color.White;
            this.btnStart.Location = new System.Drawing.Point(560, 12);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(100, 32);
            this.btnStart.TabIndex = 2;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = false;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // cboCamera
            // 
            this.cboCamera.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCamera.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboCamera.FormattingEnabled = true;
            this.cboCamera.Location = new System.Drawing.Point(130, 15);
            this.cboCamera.Name = "cboCamera";
            this.cboCamera.Size = new System.Drawing.Size(400, 25);
            this.cboCamera.TabIndex = 1;
            // 
            // lblSelectCamera
            // 
            this.lblSelectCamera.AutoSize = true;
            this.lblSelectCamera.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblSelectCamera.ForeColor = System.Drawing.Color.White;
            this.lblSelectCamera.Location = new System.Drawing.Point(15, 18);
            this.lblSelectCamera.Name = "lblSelectCamera";
            this.lblSelectCamera.Size = new System.Drawing.Size(109, 19);
            this.lblSelectCamera.TabIndex = 0;
            this.lblSelectCamera.Text = "Select Camera:";
            // 
            // picCamera
            // 
            this.picCamera.BackColor = System.Drawing.Color.Black;
            this.picCamera.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picCamera.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picCamera.Location = new System.Drawing.Point(3, 63);
            this.picCamera.Name = "picCamera";
            this.picCamera.Size = new System.Drawing.Size(794, 434);
            this.picCamera.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picCamera.TabIndex = 1;
            this.picCamera.TabStop = false;
            // 
            // panelBottom
            // 
            this.panelBottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(242)))), ((int)(((byte)(245)))));
            this.panelBottom.Controls.Add(this.txtResult);
            this.panelBottom.Controls.Add(this.lblResult);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBottom.Location = new System.Drawing.Point(3, 503);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(794, 94);
            this.panelBottom.TabIndex = 2;
            // 
            // txtResult
            // 
            this.txtResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtResult.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtResult.Location = new System.Drawing.Point(15, 35);
            this.txtResult.Multiline = true;
            this.txtResult.Name = "txtResult";
            this.txtResult.ReadOnly = true;
            this.txtResult.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtResult.Size = new System.Drawing.Size(765, 48);
            this.txtResult.TabIndex = 1;
            // 
            // lblResult
            // 
            this.lblResult.AutoSize = true;
            this.lblResult.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblResult.Location = new System.Drawing.Point(15, 10);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(94, 19);
            this.lblResult.TabIndex = 0;
            this.lblResult.Text = "Scan Result:";
            // 
            // timerScan
            // 
            this.timerScan.Interval = 500;
            this.timerScan.Tick += new System.EventHandler(this.timerScan_Tick);
            // 
            // frmScanQR
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.tableLayoutPanelMain);
            this.MinimumSize = new System.Drawing.Size(640, 480);
            this.Name = "frmScanQR";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "QR Code Scanner";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmScanQR_FormClosing);
            this.Load += new System.EventHandler(this.frmScanQR_Load);
            this.tableLayoutPanelMain.ResumeLayout(false);
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picCamera)).EndInit();
            this.panelBottom.ResumeLayout(false);
            this.panelBottom.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMain;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.PictureBox picCamera;
        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.Label lblSelectCamera;
        private System.Windows.Forms.ComboBox cboCamera;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.TextBox txtResult;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.Timer timerScan;
    }
}