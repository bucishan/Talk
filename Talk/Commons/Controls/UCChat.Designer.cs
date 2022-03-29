namespace Talk.Commons.Controls
{
    partial class UCChat
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.p_Footer = new System.Windows.Forms.Panel();
            this.btnMore = new System.Windows.Forms.Panel();
            this.btnFile = new System.Windows.Forms.Panel();
            this.btnEmoj = new System.Windows.Forms.Panel();
            this.txtMsgInput = new System.Windows.Forms.RichTextBox();
            this.p_Header = new System.Windows.Forms.Panel();
            this.btnSetting = new System.Windows.Forms.Panel();
            this.lblUserName = new System.Windows.Forms.Label();
            this.txtMsgOutput = new Talk.Commons.Controls.UCFlowLayoutPanel();
            this.tableLayoutPanel1.SuspendLayout();
            this.p_Footer.SuspendLayout();
            this.p_Header.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.p_Footer, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.p_Header, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtMsgOutput, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 67F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(404, 405);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // p_Footer
            // 
            this.p_Footer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.p_Footer.Controls.Add(this.btnMore);
            this.p_Footer.Controls.Add(this.btnFile);
            this.p_Footer.Controls.Add(this.btnEmoj);
            this.p_Footer.Controls.Add(this.txtMsgInput);
            this.p_Footer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.p_Footer.Location = new System.Drawing.Point(0, 345);
            this.p_Footer.Margin = new System.Windows.Forms.Padding(0);
            this.p_Footer.Name = "p_Footer";
            this.p_Footer.Size = new System.Drawing.Size(404, 60);
            this.p_Footer.TabIndex = 0;
            // 
            // btnMore
            // 
            this.btnMore.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMore.BackColor = System.Drawing.Color.Transparent;
            this.btnMore.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMore.Location = new System.Drawing.Point(370, 18);
            this.btnMore.Name = "btnMore";
            this.btnMore.Size = new System.Drawing.Size(25, 25);
            this.btnMore.TabIndex = 2;
            this.btnMore.Click += new System.EventHandler(this.btnMore_Click);
            this.btnMore.Paint += new System.Windows.Forms.PaintEventHandler(this.Btn_Paint);
            this.btnMore.MouseEnter += new System.EventHandler(this.Btn_MouseEnter);
            this.btnMore.MouseLeave += new System.EventHandler(this.Btn_MouseLeave);
            // 
            // btnFile
            // 
            this.btnFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFile.BackColor = System.Drawing.Color.Transparent;
            this.btnFile.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFile.Location = new System.Drawing.Point(339, 18);
            this.btnFile.Name = "btnFile";
            this.btnFile.Size = new System.Drawing.Size(25, 25);
            this.btnFile.TabIndex = 2;
            this.btnFile.Paint += new System.Windows.Forms.PaintEventHandler(this.Btn_Paint);
            this.btnFile.MouseEnter += new System.EventHandler(this.Btn_MouseEnter);
            this.btnFile.MouseLeave += new System.EventHandler(this.Btn_MouseLeave);
            // 
            // btnEmoj
            // 
            this.btnEmoj.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEmoj.BackColor = System.Drawing.Color.Transparent;
            this.btnEmoj.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEmoj.Location = new System.Drawing.Point(308, 18);
            this.btnEmoj.Name = "btnEmoj";
            this.btnEmoj.Size = new System.Drawing.Size(25, 25);
            this.btnEmoj.TabIndex = 2;
            this.btnEmoj.Click += new System.EventHandler(this.btnEmoj_Click);
            this.btnEmoj.Paint += new System.Windows.Forms.PaintEventHandler(this.Btn_Paint);
            this.btnEmoj.MouseEnter += new System.EventHandler(this.Btn_MouseEnter);
            this.btnEmoj.MouseLeave += new System.EventHandler(this.Btn_MouseLeave);
            // 
            // txtMsgInput
            // 
            this.txtMsgInput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMsgInput.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.txtMsgInput.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMsgInput.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtMsgInput.Location = new System.Drawing.Point(10, 10);
            this.txtMsgInput.MaxLength = 300;
            this.txtMsgInput.Name = "txtMsgInput";
            this.txtMsgInput.Size = new System.Drawing.Size(292, 40);
            this.txtMsgInput.TabIndex = 0;
            this.txtMsgInput.Text = "";
            this.txtMsgInput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMsgInput_KeyDown);
            this.txtMsgInput.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtMsgInput_KeyUp);
            // 
            // p_Header
            // 
            this.p_Header.Controls.Add(this.btnSetting);
            this.p_Header.Controls.Add(this.lblUserName);
            this.p_Header.Dock = System.Windows.Forms.DockStyle.Fill;
            this.p_Header.Location = new System.Drawing.Point(0, 0);
            this.p_Header.Margin = new System.Windows.Forms.Padding(0);
            this.p_Header.Name = "p_Header";
            this.p_Header.Size = new System.Drawing.Size(404, 67);
            this.p_Header.TabIndex = 1;
            // 
            // btnSetting
            // 
            this.btnSetting.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSetting.BackColor = System.Drawing.Color.Transparent;
            this.btnSetting.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSetting.Location = new System.Drawing.Point(368, 25);
            this.btnSetting.Name = "btnSetting";
            this.btnSetting.Size = new System.Drawing.Size(20, 20);
            this.btnSetting.TabIndex = 1;
            this.btnSetting.Paint += new System.Windows.Forms.PaintEventHandler(this.Btn_Paint);
            this.btnSetting.MouseEnter += new System.EventHandler(this.Btn_MouseEnter);
            this.btnSetting.MouseLeave += new System.EventHandler(this.Btn_MouseLeave);
            // 
            // lblUserName
            // 
            this.lblUserName.AutoSize = true;
            this.lblUserName.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(134)));
            this.lblUserName.Location = new System.Drawing.Point(17, 25);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(59, 16);
            this.lblUserName.TabIndex = 0;
            this.lblUserName.Text = "山里人";
            // 
            // txtMsgOutput
            // 
            this.txtMsgOutput.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.txtMsgOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMsgOutput.Location = new System.Drawing.Point(0, 67);
            this.txtMsgOutput.Margin = new System.Windows.Forms.Padding(0);
            this.txtMsgOutput.Name = "txtMsgOutput";
            this.txtMsgOutput.ScrollBarColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(200)))), ((int)(((byte)(198)))));
            this.txtMsgOutput.ScrollBarEnterColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(177)))), ((int)(((byte)(176)))));
            this.txtMsgOutput.ScrollBarHoverShow = true;
            this.txtMsgOutput.Size = new System.Drawing.Size(404, 278);
            this.txtMsgOutput.TabIndex = 2;
            // 
            // UCChat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "UCChat";
            this.Size = new System.Drawing.Size(404, 405);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.p_Footer.ResumeLayout(false);
            this.p_Header.ResumeLayout(false);
            this.p_Header.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel p_Footer;
        private System.Windows.Forms.Panel p_Header;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.RichTextBox txtMsgInput;
        private System.Windows.Forms.Panel btnSetting;
        private System.Windows.Forms.Panel btnMore;
        private System.Windows.Forms.Panel btnFile;
        private System.Windows.Forms.Panel btnEmoj;
        private UCFlowLayoutPanel txtMsgOutput;
    }
}
