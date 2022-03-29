namespace Talk
{
    partial class MChat
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.p_TabBlock = new System.Windows.Forms.Panel();
            this.btnOnlineMenu = new Talk.Commons.Controls.UCTabBlock();
            this.btnMusic = new Talk.Commons.Controls.UCTabBlock();
            this.btnSelf = new Talk.Commons.Controls.UCTabBlock();
            this.p_TabPage = new System.Windows.Forms.Panel();
            this.TabPageMusic = new Talk.Commons.Controls.UCTabPageMusic();
            this.TabPagePersonalInfo = new Talk.Commons.Controls.UCTabPagePersonalInfo();
            this.TabPageOnlineMenu = new Talk.Commons.Controls.UCTabPageOnlineMenu();
            this.p_ChatContainer = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1.SuspendLayout();
            this.p_TabBlock.SuspendLayout();
            this.p_TabPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 270F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.p_TabBlock, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.p_TabPage, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.p_ChatContainer, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(700, 500);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // p_TabBlock
            // 
            this.p_TabBlock.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(43)))), ((int)(((byte)(46)))));
            this.p_TabBlock.Controls.Add(this.btnOnlineMenu);
            this.p_TabBlock.Controls.Add(this.btnMusic);
            this.p_TabBlock.Controls.Add(this.btnSelf);
            this.p_TabBlock.Dock = System.Windows.Forms.DockStyle.Fill;
            this.p_TabBlock.Location = new System.Drawing.Point(0, 440);
            this.p_TabBlock.Margin = new System.Windows.Forms.Padding(0);
            this.p_TabBlock.Name = "p_TabBlock";
            this.p_TabBlock.Size = new System.Drawing.Size(270, 60);
            this.p_TabBlock.TabIndex = 0;
            // 
            // btnOnlineMenu
            // 
            this.btnOnlineMenu.BackColor = System.Drawing.Color.Transparent;
            this.btnOnlineMenu.Location = new System.Drawing.Point(3, 7);
            this.btnOnlineMenu.Name = "btnOnlineMenu";
            this.btnOnlineMenu.Size = new System.Drawing.Size(74, 50);
            this.btnOnlineMenu.TabColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.btnOnlineMenu.TabFontSize = 22;
            this.btnOnlineMenu.TabIcon = 61669;
            this.btnOnlineMenu.TabIndex = 1;
            this.btnOnlineMenu.TabSelected = false;
            this.btnOnlineMenu.TabSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(193)))), ((int)(((byte)(96)))));
            this.btnOnlineMenu.TabSelectedIcon = 61557;
            this.btnOnlineMenu.Click += new System.EventHandler(this.btnTabBlock_Click);
            // 
            // btnMusic
            // 
            this.btnMusic.BackColor = System.Drawing.Color.Transparent;
            this.btnMusic.Location = new System.Drawing.Point(94, 7);
            this.btnMusic.Name = "btnMusic";
            this.btnMusic.Size = new System.Drawing.Size(75, 50);
            this.btnMusic.TabColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.btnMusic.TabFontSize = 22;
            this.btnMusic.TabIcon = 61441;
            this.btnMusic.TabIndex = 0;
            this.btnMusic.TabSelected = false;
            this.btnMusic.TabSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(193)))), ((int)(((byte)(96)))));
            this.btnMusic.TabSelectedIcon = 61441;
            this.btnMusic.Click += new System.EventHandler(this.btnTabBlock_Click);
            // 
            // btnSelf
            // 
            this.btnSelf.BackColor = System.Drawing.Color.Transparent;
            this.btnSelf.Location = new System.Drawing.Point(192, 7);
            this.btnSelf.Name = "btnSelf";
            this.btnSelf.Size = new System.Drawing.Size(75, 50);
            this.btnSelf.TabColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.btnSelf.TabFontSize = 22;
            this.btnSelf.TabIcon = 62141;
            this.btnSelf.TabIndex = 0;
            this.btnSelf.TabSelected = false;
            this.btnSelf.TabSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(193)))), ((int)(((byte)(96)))));
            this.btnSelf.TabSelectedIcon = 62142;
            this.btnSelf.Click += new System.EventHandler(this.btnTabBlock_Click);
            // 
            // p_TabPage
            // 
            this.p_TabPage.Controls.Add(this.TabPageMusic);
            this.p_TabPage.Controls.Add(this.TabPagePersonalInfo);
            this.p_TabPage.Controls.Add(this.TabPageOnlineMenu);
            this.p_TabPage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.p_TabPage.Location = new System.Drawing.Point(0, 0);
            this.p_TabPage.Margin = new System.Windows.Forms.Padding(0);
            this.p_TabPage.Name = "p_TabPage";
            this.p_TabPage.Size = new System.Drawing.Size(270, 440);
            this.p_TabPage.TabIndex = 1;
            // 
            // TabPageMusic
            // 
            this.TabPageMusic.BackColor = System.Drawing.Color.Transparent;
            this.TabPageMusic.Location = new System.Drawing.Point(12, 271);
            this.TabPageMusic.Name = "TabPageMusic";
            this.TabPageMusic.Size = new System.Drawing.Size(245, 119);
            this.TabPageMusic.TabIndex = 2;
            // 
            // TabPagePersonalInfo
            // 
            this.TabPagePersonalInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.TabPagePersonalInfo.Location = new System.Drawing.Point(12, 141);
            this.TabPagePersonalInfo.Name = "TabPagePersonalInfo";
            this.TabPagePersonalInfo.Size = new System.Drawing.Size(245, 104);
            this.TabPagePersonalInfo.TabIndex = 1;
            // 
            // TabPageOnlineMenu
            // 
            this.TabPageOnlineMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.TabPageOnlineMenu.Location = new System.Drawing.Point(12, 18);
            this.TabPageOnlineMenu.Name = "TabPageOnlineMenu";
            this.TabPageOnlineMenu.Size = new System.Drawing.Size(245, 102);
            this.TabPageOnlineMenu.TabIndex = 0;
            // 
            // p_ChatContainer
            // 
            this.p_ChatContainer.BackColor = System.Drawing.Color.WhiteSmoke;
            this.p_ChatContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.p_ChatContainer.Location = new System.Drawing.Point(270, 0);
            this.p_ChatContainer.Margin = new System.Windows.Forms.Padding(0);
            this.p_ChatContainer.Name = "p_ChatContainer";
            this.tableLayoutPanel1.SetRowSpan(this.p_ChatContainer, 2);
            this.p_ChatContainer.Size = new System.Drawing.Size(430, 500);
            this.p_ChatContainer.TabIndex = 2;
            this.p_ChatContainer.Paint += new System.Windows.Forms.PaintEventHandler(this.p_ChatContainer_Paint);
            this.p_ChatContainer.Resize += new System.EventHandler(this.p_ChatContainer_Resize);
            // 
            // MChat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.ClientSize = new System.Drawing.Size(700, 500);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MinimumSize = new System.Drawing.Size(700, 500);
            this.Name = "MChat";
            this.Text = "MChat";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MChat_FormClosing);
            this.Load += new System.EventHandler(this.Chat_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.MChat_Paint);
            this.Controls.SetChildIndex(this.tableLayoutPanel1, 0);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.p_TabBlock.ResumeLayout(false);
            this.p_TabPage.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel p_TabBlock;
        private System.Windows.Forms.Panel p_TabPage;
        private System.Windows.Forms.Panel p_ChatContainer;
        private Commons.Controls.UCTabBlock btnOnlineMenu;
        private Commons.Controls.UCTabBlock btnSelf;
        private Commons.Controls.UCTabPageOnlineMenu TabPageOnlineMenu;
        private Commons.Controls.UCTabPagePersonalInfo TabPagePersonalInfo;
        private Commons.Controls.UCTabBlock btnMusic;
        private Commons.Controls.UCTabPageMusic TabPageMusic;
    }
}