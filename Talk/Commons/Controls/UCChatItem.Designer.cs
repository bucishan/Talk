namespace Talk.Commons.Controls
{
    partial class UCChatItem
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
            this.txtMsg = new System.Windows.Forms.RichTextBox();
            this.picHead = new System.Windows.Forms.PictureBox();
            this.panelConfirm = new System.Windows.Forms.Panel();
            this.btnAgree = new System.Windows.Forms.Button();
            this.btnDisAgree = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picHead)).BeginInit();
            this.panelConfirm.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtMsg
            // 
            this.txtMsg.BackColor = System.Drawing.SystemColors.Control;
            this.txtMsg.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMsg.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtMsg.Font = new System.Drawing.Font("宋体", 10F);
            this.txtMsg.Location = new System.Drawing.Point(65, 19);
            this.txtMsg.MinimumSize = new System.Drawing.Size(40, 25);
            this.txtMsg.Name = "txtMsg";
            this.txtMsg.ReadOnly = true;
            this.txtMsg.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.txtMsg.ShortcutsEnabled = false;
            this.txtMsg.Size = new System.Drawing.Size(147, 25);
            this.txtMsg.TabIndex = 1;
            this.txtMsg.TabStop = false;
            this.txtMsg.Text = "";
            this.txtMsg.ContentsResized += new System.Windows.Forms.ContentsResizedEventHandler(this.txtMsg_ContentsResized);
            // 
            // picHead
            // 
            this.picHead.Location = new System.Drawing.Point(15, 14);
            this.picHead.Name = "picHead";
            this.picHead.Size = new System.Drawing.Size(35, 35);
            this.picHead.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picHead.TabIndex = 0;
            this.picHead.TabStop = false;
            // 
            // panelConfirm
            // 
            this.panelConfirm.Controls.Add(this.btnDisAgree);
            this.panelConfirm.Controls.Add(this.btnAgree);
            this.panelConfirm.Location = new System.Drawing.Point(218, 19);
            this.panelConfirm.Margin = new System.Windows.Forms.Padding(0);
            this.panelConfirm.Name = "panelConfirm";
            this.panelConfirm.Size = new System.Drawing.Size(50, 25);
            this.panelConfirm.TabIndex = 2;
            this.panelConfirm.Visible = false;
            // 
            // btnAgree
            // 
            this.btnAgree.FlatAppearance.BorderSize = 0;
            this.btnAgree.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAgree.Location = new System.Drawing.Point(3, 1);
            this.btnAgree.Margin = new System.Windows.Forms.Padding(0);
            this.btnAgree.Name = "btnAgree";
            this.btnAgree.Size = new System.Drawing.Size(23, 23);
            this.btnAgree.TabIndex = 0;
            this.btnAgree.Tag = "yes";
            this.btnAgree.Text = "是";
            this.btnAgree.UseVisualStyleBackColor = true;
            this.btnAgree.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // btnDisAgree
            // 
            this.btnDisAgree.FlatAppearance.BorderSize = 0;
            this.btnDisAgree.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDisAgree.Location = new System.Drawing.Point(26, 1);
            this.btnDisAgree.Margin = new System.Windows.Forms.Padding(0);
            this.btnDisAgree.Name = "btnDisAgree";
            this.btnDisAgree.Size = new System.Drawing.Size(23, 23);
            this.btnDisAgree.TabIndex = 0;
            this.btnDisAgree.Tag = "no";
            this.btnDisAgree.Text = "否";
            this.btnDisAgree.UseVisualStyleBackColor = true;
            this.btnDisAgree.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // UCChatItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelConfirm);
            this.Controls.Add(this.txtMsg);
            this.Controls.Add(this.picHead);
            this.Name = "UCChatItem";
            this.Size = new System.Drawing.Size(292, 63);
            ((System.ComponentModel.ISupportInitialize)(this.picHead)).EndInit();
            this.panelConfirm.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picHead;
        private System.Windows.Forms.RichTextBox txtMsg;
        private System.Windows.Forms.Panel panelConfirm;
        private System.Windows.Forms.Button btnDisAgree;
        private System.Windows.Forms.Button btnAgree;
    }
}
