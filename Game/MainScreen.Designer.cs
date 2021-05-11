namespace Game
{
    partial class MainScreen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainScreen));
            this.pbPlay = new System.Windows.Forms.PictureBox();
            this.pbProfile = new System.Windows.Forms.PictureBox();
            this.pbScoreboard = new System.Windows.Forms.PictureBox();
            this.pbExit = new System.Windows.Forms.PictureBox();
            this.pnlProfile = new System.Windows.Forms.Panel();
            this.pnlScoreboard = new System.Windows.Forms.Panel();
            this.tlpScoreboard = new System.Windows.Forms.TableLayoutPanel();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblScoreboard = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pnlInventory = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pbPlay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbProfile)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbScoreboard)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbExit)).BeginInit();
            this.pnlScoreboard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pbPlay
            // 
            this.pbPlay.BackColor = System.Drawing.Color.Transparent;
            this.pbPlay.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pbPlay.BackgroundImage")));
            this.pbPlay.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbPlay.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbPlay.Location = new System.Drawing.Point(27, 85);
            this.pbPlay.Name = "pbPlay";
            this.pbPlay.Size = new System.Drawing.Size(182, 48);
            this.pbPlay.TabIndex = 0;
            this.pbPlay.TabStop = false;
            this.pbPlay.Click += new System.EventHandler(this.pbPlay_Click);
            // 
            // pbProfile
            // 
            this.pbProfile.BackColor = System.Drawing.Color.Transparent;
            this.pbProfile.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pbProfile.BackgroundImage")));
            this.pbProfile.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbProfile.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbProfile.Location = new System.Drawing.Point(27, 160);
            this.pbProfile.Name = "pbProfile";
            this.pbProfile.Size = new System.Drawing.Size(182, 48);
            this.pbProfile.TabIndex = 2;
            this.pbProfile.TabStop = false;
            this.pbProfile.Click += new System.EventHandler(this.pbProfile_Click);
            // 
            // pbScoreboard
            // 
            this.pbScoreboard.BackColor = System.Drawing.Color.Transparent;
            this.pbScoreboard.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pbScoreboard.BackgroundImage")));
            this.pbScoreboard.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbScoreboard.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbScoreboard.Location = new System.Drawing.Point(27, 235);
            this.pbScoreboard.Name = "pbScoreboard";
            this.pbScoreboard.Size = new System.Drawing.Size(182, 48);
            this.pbScoreboard.TabIndex = 3;
            this.pbScoreboard.TabStop = false;
            this.pbScoreboard.Click += new System.EventHandler(this.pbScoreboard_Click);
            // 
            // pbExit
            // 
            this.pbExit.BackColor = System.Drawing.Color.Transparent;
            this.pbExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pbExit.BackgroundImage")));
            this.pbExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbExit.Location = new System.Drawing.Point(27, 385);
            this.pbExit.Name = "pbExit";
            this.pbExit.Size = new System.Drawing.Size(182, 48);
            this.pbExit.TabIndex = 4;
            this.pbExit.TabStop = false;
            this.pbExit.Click += new System.EventHandler(this.pbExit_Click);
            // 
            // pnlProfile
            // 
            this.pnlProfile.BackColor = System.Drawing.Color.Transparent;
            this.pnlProfile.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlProfile.BackgroundImage")));
            this.pnlProfile.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlProfile.Location = new System.Drawing.Point(258, 3);
            this.pnlProfile.Name = "pnlProfile";
            this.pnlProfile.Size = new System.Drawing.Size(493, 474);
            this.pnlProfile.TabIndex = 6;
            // 
            // pnlScoreboard
            // 
            this.pnlScoreboard.BackColor = System.Drawing.Color.Transparent;
            this.pnlScoreboard.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlScoreboard.BackgroundImage")));
            this.pnlScoreboard.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlScoreboard.Controls.Add(this.tlpScoreboard);
            this.pnlScoreboard.Controls.Add(this.label5);
            this.pnlScoreboard.Controls.Add(this.label4);
            this.pnlScoreboard.Controls.Add(this.label3);
            this.pnlScoreboard.Controls.Add(this.label2);
            this.pnlScoreboard.Controls.Add(this.label1);
            this.pnlScoreboard.Controls.Add(this.lblScoreboard);
            this.pnlScoreboard.Location = new System.Drawing.Point(258, 3);
            this.pnlScoreboard.Name = "pnlScoreboard";
            this.pnlScoreboard.Size = new System.Drawing.Size(493, 474);
            this.pnlScoreboard.TabIndex = 7;
            // 
            // tlpScoreboard
            // 
            this.tlpScoreboard.AutoScroll = true;
            this.tlpScoreboard.ColumnCount = 5;
            this.tlpScoreboard.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpScoreboard.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 175F));
            this.tlpScoreboard.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 85F));
            this.tlpScoreboard.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tlpScoreboard.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 71F));
            this.tlpScoreboard.Location = new System.Drawing.Point(32, 196);
            this.tlpScoreboard.Name = "tlpScoreboard";
            this.tlpScoreboard.RowCount = 1;
            this.tlpScoreboard.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 243F));
            this.tlpScoreboard.Size = new System.Drawing.Size(431, 243);
            this.tlpScoreboard.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(37, 169);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(21, 24);
            this.label5.TabIndex = 6;
            this.label5.Text = "#";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(397, 169);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 24);
            this.label4.TabIndex = 5;
            this.label4.Text = "Wave";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(333, 169);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 24);
            this.label3.TabIndex = 4;
            this.label3.Text = "Kills";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(247, 169);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 24);
            this.label2.TabIndex = 3;
            this.label2.Text = "Score";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(92, 169);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 24);
            this.label1.TabIndex = 2;
            this.label1.Text = "Username";
            // 
            // lblScoreboard
            // 
            this.lblScoreboard.AutoSize = true;
            this.lblScoreboard.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblScoreboard.ForeColor = System.Drawing.Color.White;
            this.lblScoreboard.Location = new System.Drawing.Point(157, 130);
            this.lblScoreboard.Name = "lblScoreboard";
            this.lblScoreboard.Size = new System.Drawing.Size(178, 31);
            this.lblScoreboard.TabIndex = 0;
            this.lblScoreboard.Text = "Leaderboard";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox1.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.InitialImage")));
            this.pictureBox1.Location = new System.Drawing.Point(27, 310);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(182, 48);
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pbInventory_Click);
            // 
            // pnlInventory
            // 
            this.pnlInventory.BackColor = System.Drawing.Color.Transparent;
            this.pnlInventory.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlInventory.BackgroundImage")));
            this.pnlInventory.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlInventory.Location = new System.Drawing.Point(258, 3);
            this.pnlInventory.Name = "pnlInventory";
            this.pnlInventory.Size = new System.Drawing.Size(493, 474);
            this.pnlInventory.TabIndex = 9;
            // 
            // MainScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(132)))), ((int)(((byte)(73)))));
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(800, 500);
            this.Controls.Add(this.pnlInventory);
            this.Controls.Add(this.pnlProfile);
            this.Controls.Add(this.pbExit);
            this.Controls.Add(this.pbScoreboard);
            this.Controls.Add(this.pbProfile);
            this.Controls.Add(this.pbPlay);
            this.Controls.Add(this.pnlScoreboard);
            this.Controls.Add(this.pictureBox1);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MainScreen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MainScreen";
            this.TransparencyKey = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(132)))), ((int)(((byte)(73)))));
            ((System.ComponentModel.ISupportInitialize)(this.pbPlay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbProfile)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbScoreboard)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbExit)).EndInit();
            this.pnlScoreboard.ResumeLayout(false);
            this.pnlScoreboard.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbPlay;
        private System.Windows.Forms.PictureBox pbProfile;
        private System.Windows.Forms.PictureBox pbScoreboard;
        private System.Windows.Forms.PictureBox pbExit;
        private System.Windows.Forms.Panel pnlProfile;
        private System.Windows.Forms.Panel pnlScoreboard;
        private System.Windows.Forms.Label lblScoreboard;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TableLayoutPanel tlpScoreboard;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel pnlInventory;
    }
}