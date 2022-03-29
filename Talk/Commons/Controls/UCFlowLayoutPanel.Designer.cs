namespace Talk.Commons.Controls
{
    partial class UCFlowLayoutPanel
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
            this.cPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.cScrollBar = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // cPanel
            // 
            this.cPanel.AutoScroll = true;
            this.cPanel.BackColor = System.Drawing.Color.Transparent;
            this.cPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.cPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.cPanel.Location = new System.Drawing.Point(0, 0);
            this.cPanel.Margin = new System.Windows.Forms.Padding(0);
            this.cPanel.Name = "cPanel";
            this.cPanel.Size = new System.Drawing.Size(118, 200);
            this.cPanel.TabIndex = 0;
            this.cPanel.WrapContents = false;
            this.cPanel.ControlAdded += new System.Windows.Forms.ControlEventHandler(this.cPanel_ControlAdded);
            this.cPanel.ControlRemoved += new System.Windows.Forms.ControlEventHandler(this.cPanel_ControlRemoved);
            // 
            // cScrollBar
            // 
            this.cScrollBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cScrollBar.BackColor = System.Drawing.Color.DarkOrange;
            this.cScrollBar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.cScrollBar.Location = new System.Drawing.Point(136, 0);
            this.cScrollBar.Margin = new System.Windows.Forms.Padding(0);
            this.cScrollBar.Name = "cScrollBar";
            this.cScrollBar.Size = new System.Drawing.Size(16, 122);
            this.cScrollBar.TabIndex = 1;
            this.cScrollBar.Visible = false;
            this.cScrollBar.Paint += new System.Windows.Forms.PaintEventHandler(this.cScrollBar_Paint);
            this.cScrollBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.cScrollBar_MouseDown);
            this.cScrollBar.MouseMove += new System.Windows.Forms.MouseEventHandler(this.cScrollBar_MouseMove);
            this.cScrollBar.MouseUp += new System.Windows.Forms.MouseEventHandler(this.cScrollBar_MouseUp);
            // 
            // UCFlowLayoutPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.cScrollBar);
            this.Controls.Add(this.cPanel);
            this.Name = "UCFlowLayoutPanel";
            this.Size = new System.Drawing.Size(152, 200);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel cPanel;
        private System.Windows.Forms.Panel cScrollBar;
    }
}
