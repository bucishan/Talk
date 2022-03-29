namespace Talk.Commons.Controls
{
    partial class UCTabBlock
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
            this.icon = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // icon
            // 
            this.icon.BackColor = System.Drawing.Color.Transparent;
            this.icon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.icon.Location = new System.Drawing.Point(0, 0);
            this.icon.Name = "icon";
            this.icon.Size = new System.Drawing.Size(87, 50);
            this.icon.TabIndex = 0;
            this.icon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.icon.Click += new System.EventHandler(this.icon_Click);
            this.icon.MouseEnter += new System.EventHandler(this.UC_MouseEnter);
            this.icon.MouseLeave += new System.EventHandler(this.UC_MouseLeave);
            // 
            // UCTabBlock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.icon);
            this.Name = "UCTabBlock";
            this.Size = new System.Drawing.Size(87, 50);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label icon;
    }
}
