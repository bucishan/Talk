namespace Talk.Commons.Controls
{
    partial class UCMenuItem
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
            this.lblNickName = new System.Windows.Forms.Label();
            this.lblIPAddress = new System.Windows.Forms.Label();
            this.picHead = new System.Windows.Forms.PictureBox();
            this.lblLatestMsg = new System.Windows.Forms.Label();
            this.lblLatestMsgTime = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picHead)).BeginInit();
            this.SuspendLayout();
            // 
            // lblNickName
            // 
            this.lblNickName.AutoEllipsis = true;
            this.lblNickName.BackColor = System.Drawing.Color.Transparent;
            this.lblNickName.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(134)));
            this.lblNickName.Location = new System.Drawing.Point(58, 13);
            this.lblNickName.Name = "lblNickName";
            this.lblNickName.Size = new System.Drawing.Size(124, 20);
            this.lblNickName.TabIndex = 1;
            this.lblNickName.Text = "ShanLiRen";
            this.lblNickName.Click += new System.EventHandler(this.Item_Click);
            this.lblNickName.MouseEnter += new System.EventHandler(this.UCMenuItem_MouseEnter);
            this.lblNickName.MouseLeave += new System.EventHandler(this.UCMenuItem_MouseLeave);
            // 
            // lblIPAddress
            // 
            this.lblIPAddress.AutoSize = true;
            this.lblIPAddress.BackColor = System.Drawing.Color.Transparent;
            this.lblIPAddress.Location = new System.Drawing.Point(116, 1);
            this.lblIPAddress.Name = "lblIPAddress";
            this.lblIPAddress.Size = new System.Drawing.Size(59, 12);
            this.lblIPAddress.TabIndex = 1;
            this.lblIPAddress.Text = "127.0.0.1";
            this.lblIPAddress.Visible = false;
            this.lblIPAddress.Click += new System.EventHandler(this.Item_Click);
            this.lblIPAddress.MouseEnter += new System.EventHandler(this.UCMenuItem_MouseEnter);
            this.lblIPAddress.MouseLeave += new System.EventHandler(this.UCMenuItem_MouseLeave);
            // 
            // picHead
            // 
            this.picHead.BackColor = System.Drawing.Color.Transparent;
            this.picHead.Location = new System.Drawing.Point(12, 13);
            this.picHead.Name = "picHead";
            this.picHead.Size = new System.Drawing.Size(40, 40);
            this.picHead.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picHead.TabIndex = 0;
            this.picHead.TabStop = false;
            this.picHead.Click += new System.EventHandler(this.Item_Click);
            this.picHead.MouseEnter += new System.EventHandler(this.UCMenuItem_MouseEnter);
            this.picHead.MouseLeave += new System.EventHandler(this.UCMenuItem_MouseLeave);
            // 
            // lblLatestMsg
            // 
            this.lblLatestMsg.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblLatestMsg.AutoEllipsis = true;
            this.lblLatestMsg.BackColor = System.Drawing.Color.Transparent;
            this.lblLatestMsg.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(134)));
            this.lblLatestMsg.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(110)))), ((int)(((byte)(110)))), ((int)(((byte)(110)))));
            this.lblLatestMsg.Location = new System.Drawing.Point(59, 41);
            this.lblLatestMsg.Name = "lblLatestMsg";
            this.lblLatestMsg.Size = new System.Drawing.Size(123, 12);
            this.lblLatestMsg.TabIndex = 1;
            this.lblLatestMsg.Click += new System.EventHandler(this.Item_Click);
            this.lblLatestMsg.MouseEnter += new System.EventHandler(this.UCMenuItem_MouseEnter);
            this.lblLatestMsg.MouseLeave += new System.EventHandler(this.UCMenuItem_MouseLeave);
            // 
            // lblLatestMsgTime
            // 
            this.lblLatestMsgTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblLatestMsgTime.AutoEllipsis = true;
            this.lblLatestMsgTime.BackColor = System.Drawing.Color.Transparent;
            this.lblLatestMsgTime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(110)))), ((int)(((byte)(110)))), ((int)(((byte)(110)))));
            this.lblLatestMsgTime.Location = new System.Drawing.Point(188, 13);
            this.lblLatestMsgTime.Name = "lblLatestMsgTime";
            this.lblLatestMsgTime.Size = new System.Drawing.Size(35, 12);
            this.lblLatestMsgTime.TabIndex = 1;
            this.lblLatestMsgTime.Click += new System.EventHandler(this.Item_Click);
            this.lblLatestMsgTime.MouseEnter += new System.EventHandler(this.UCMenuItem_MouseEnter);
            this.lblLatestMsgTime.MouseLeave += new System.EventHandler(this.UCMenuItem_MouseLeave);
            // 
            // UCMenuItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblLatestMsg);
            this.Controls.Add(this.lblLatestMsgTime);
            this.Controls.Add(this.lblIPAddress);
            this.Controls.Add(this.lblNickName);
            this.Controls.Add(this.picHead);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.MinimumSize = new System.Drawing.Size(200, 65);
            this.Name = "UCMenuItem";
            this.Size = new System.Drawing.Size(232, 65);
            this.Click += new System.EventHandler(this.Item_Click);
            this.MouseEnter += new System.EventHandler(this.UCMenuItem_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.UCMenuItem_MouseLeave);
            ((System.ComponentModel.ISupportInitialize)(this.picHead)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picHead;
        private System.Windows.Forms.Label lblNickName;
        private System.Windows.Forms.Label lblIPAddress;
        private System.Windows.Forms.Label lblLatestMsg;
        private System.Windows.Forms.Label lblLatestMsgTime;
    }
}
