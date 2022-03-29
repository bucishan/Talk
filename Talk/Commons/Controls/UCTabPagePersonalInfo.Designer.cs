namespace Talk.Commons.Controls
{
    partial class UCTabPagePersonalInfo
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
            this.components = new System.ComponentModel.Container();
            this.lblUserName = new System.Windows.Forms.Label();
            this.picHeadPic = new System.Windows.Forms.PictureBox();
            this.lblUserGroup = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.txtUserName = new Talk.Commons.Controls.UCTextBox();
            this.txtUserGroup = new Talk.Commons.Controls.UCTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnCheck = new Talk.Commons.Controls.UCPointButton();
            this.btnCancel = new Talk.Commons.Controls.UCPointButton();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.picHeadPic)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblUserName
            // 
            this.lblUserName.AutoSize = true;
            this.lblUserName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblUserName.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(134)));
            this.lblUserName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.lblUserName.Location = new System.Drawing.Point(3, 98);
            this.lblUserName.Margin = new System.Windows.Forms.Padding(3);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(74, 29);
            this.lblUserName.TabIndex = 0;
            this.lblUserName.Text = "用户名：";
            this.lblUserName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // picHeadPic
            // 
            this.picHeadPic.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picHeadPic.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picHeadPic.Location = new System.Drawing.Point(91, 1);
            this.picHeadPic.Name = "picHeadPic";
            this.picHeadPic.Size = new System.Drawing.Size(50, 50);
            this.picHeadPic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picHeadPic.TabIndex = 1;
            this.picHeadPic.TabStop = false;
            this.picHeadPic.Click += new System.EventHandler(this.picHeadPic_Click);
            // 
            // lblUserGroup
            // 
            this.lblUserGroup.AutoSize = true;
            this.lblUserGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblUserGroup.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(134)));
            this.lblUserGroup.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.lblUserGroup.Location = new System.Drawing.Point(3, 133);
            this.lblUserGroup.Margin = new System.Windows.Forms.Padding(3);
            this.lblUserGroup.Name = "lblUserGroup";
            this.lblUserGroup.Size = new System.Drawing.Size(74, 29);
            this.lblUserGroup.TabIndex = 0;
            this.lblUserGroup.Text = "用户组：";
            this.lblUserGroup.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.txtUserName, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.lblUserName, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.lblUserGroup, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.txtUserGroup, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 5);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 7;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(244, 294);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // txtUserName
            // 
            this.txtUserName.BoxBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.txtUserName.BoxBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(217)))), ((int)(((byte)(215)))));
            this.txtUserName.BoxFocus = false;
            this.txtUserName.BoxFocusBackColor = System.Drawing.Color.White;
            this.txtUserName.BoxIconColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtUserName.BoxShowSearchIcon = false;
            this.txtUserName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtUserName.Location = new System.Drawing.Point(83, 98);
            this.txtUserName.Margin = new System.Windows.Forms.Padding(3, 3, 25, 3);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(136, 29);
            this.txtUserName.TabIndex = 2;
            // 
            // txtUserGroup
            // 
            this.txtUserGroup.BoxBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.txtUserGroup.BoxBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(217)))), ((int)(((byte)(215)))));
            this.txtUserGroup.BoxFocus = false;
            this.txtUserGroup.BoxFocusBackColor = System.Drawing.Color.White;
            this.txtUserGroup.BoxIconColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.txtUserGroup.BoxShowSearchIcon = false;
            this.txtUserGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtUserGroup.Location = new System.Drawing.Point(83, 133);
            this.txtUserGroup.Margin = new System.Windows.Forms.Padding(3, 3, 25, 3);
            this.txtUserGroup.Name = "txtUserGroup";
            this.txtUserGroup.Size = new System.Drawing.Size(136, 29);
            this.txtUserGroup.TabIndex = 2;
            // 
            // panel1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.panel1, 2);
            this.panel1.Controls.Add(this.picHeadPic);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 38);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(238, 54);
            this.panel1.TabIndex = 3;
            // 
            // panel2
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.panel2, 2);
            this.panel2.Controls.Add(this.btnCheck);
            this.panel2.Controls.Add(this.btnCancel);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 203);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(238, 44);
            this.panel2.TabIndex = 5;
            // 
            // btnCheck
            // 
            this.btnCheck.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(174)))), ((int)(((byte)(79)))), ((int)(((byte)(79)))));
            this.btnCheck.BorderWidth = 1;
            this.btnCheck.Enabled = false;
            this.btnCheck.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(195)))), ((int)(((byte)(74)))));
            this.btnCheck.FillDisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(227)))), ((int)(((byte)(227)))));
            this.btnCheck.FillDownColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(156)))), ((int)(((byte)(22)))));
            this.btnCheck.FillEnterColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(187)))), ((int)(((byte)(51)))));
            this.btnCheck.IconFlagAlways = true;
            this.btnCheck.IconFlagColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnCheck.Location = new System.Drawing.Point(55, 2);
            this.btnCheck.Name = "btnCheck";
            this.btnCheck.Size = new System.Drawing.Size(40, 40);
            this.btnCheck.TabIndex = 6;
            this.btnCheck.Text = "ucPointButton1";
            this.btnCheck.UseVisualStyleBackColor = true;
            this.btnCheck.Click += new System.EventHandler(this.btnCheck_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(174)))), ((int)(((byte)(79)))), ((int)(((byte)(79)))));
            this.btnCancel.BorderWidth = 1;
            this.btnCancel.Enabled = false;
            this.btnCancel.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(79)))), ((int)(((byte)(74)))));
            this.btnCancel.FillDisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(227)))), ((int)(((byte)(227)))));
            this.btnCancel.FillDownColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(54)))), ((int)(((byte)(52)))));
            this.btnCancel.FillEnterColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(54)))), ((int)(((byte)(52)))));
            this.btnCancel.IconFlagAlways = true;
            this.btnCancel.IconFlagCode = 61453;
            this.btnCancel.IconFlagColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnCancel.Location = new System.Drawing.Point(150, 2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(40, 40);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "ucPointButton1";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errorProvider1.ContainerControl = this;
            // 
            // UCTabPagePersonalInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "UCTabPagePersonalInfo";
            this.Size = new System.Drawing.Size(244, 294);
            ((System.ComponentModel.ISupportInitialize)(this.picHeadPic)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.PictureBox picHeadPic;
        private System.Windows.Forms.Label lblUserGroup;
        private UCTextBox txtUserName;
        private UCTextBox txtUserGroup;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Panel panel2;
        private UCPointButton btnCancel;
        private UCPointButton btnCheck;
    }
}
