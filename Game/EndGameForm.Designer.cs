namespace Game
{
    partial class EndGameForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EndGameForm));
            this.pbSave = new System.Windows.Forms.PictureBox();
            this.pbCancel = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tlpStats = new System.Windows.Forms.TableLayoutPanel();
            this.lblCoin = new System.Windows.Forms.Label();
            this.pbCoin = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbSave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCancel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCoin)).BeginInit();
            this.SuspendLayout();
            // 
            // pbSave
            // 
            this.pbSave.BackColor = System.Drawing.Color.Transparent;
            this.pbSave.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pbSave.BackgroundImage")));
            this.pbSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbSave.Location = new System.Drawing.Point(30, 410);
            this.pbSave.Name = "pbSave";
            this.pbSave.Size = new System.Drawing.Size(172, 48);
            this.pbSave.TabIndex = 0;
            this.pbSave.TabStop = false;
            this.pbSave.Click += new System.EventHandler(this.pbSave_Click);
            // 
            // pbCancel
            // 
            this.pbCancel.BackColor = System.Drawing.Color.Transparent;
            this.pbCancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pbCancel.BackgroundImage")));
            this.pbCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbCancel.Location = new System.Drawing.Point(234, 410);
            this.pbCancel.Name = "pbCancel";
            this.pbCancel.Size = new System.Drawing.Size(172, 48);
            this.pbCancel.TabIndex = 1;
            this.pbCancel.TabStop = false;
            this.pbCancel.Click += new System.EventHandler(this.pbCancel_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(121, 141);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(198, 31);
            this.label1.TabIndex = 2;
            this.label1.Text = "GAME STATS";
            // 
            // tlpStats
            // 
            this.tlpStats.BackColor = System.Drawing.Color.Transparent;
            this.tlpStats.ColumnCount = 2;
            this.tlpStats.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpStats.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpStats.Location = new System.Drawing.Point(39, 215);
            this.tlpStats.Name = "tlpStats";
            this.tlpStats.RowCount = 1;
            this.tlpStats.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 191F));
            this.tlpStats.Size = new System.Drawing.Size(349, 151);
            this.tlpStats.TabIndex = 3;
            // 
            // lblCoin
            // 
            this.lblCoin.AutoSize = true;
            this.lblCoin.BackColor = System.Drawing.Color.Transparent;
            this.lblCoin.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCoin.ForeColor = System.Drawing.Color.White;
            this.lblCoin.Location = new System.Drawing.Point(60, 189);
            this.lblCoin.Name = "lblCoin";
            this.lblCoin.Size = new System.Drawing.Size(88, 20);
            this.lblCoin.TabIndex = 0;
            this.lblCoin.Text = "Loading...";
            // 
            // pbCoin
            // 
            this.pbCoin.BackColor = System.Drawing.Color.Transparent;
            this.pbCoin.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pbCoin.BackgroundImage")));
            this.pbCoin.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbCoin.Location = new System.Drawing.Point(39, 189);
            this.pbCoin.Name = "pbCoin";
            this.pbCoin.Size = new System.Drawing.Size(20, 20);
            this.pbCoin.TabIndex = 4;
            this.pbCoin.TabStop = false;
            // 
            // EndGameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(132)))), ((int)(((byte)(73)))));
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(435, 485);
            this.Controls.Add(this.pbCoin);
            this.Controls.Add(this.lblCoin);
            this.Controls.Add(this.tlpStats);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pbCancel);
            this.Controls.Add(this.pbSave);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "EndGameForm";
            this.Text = "EndGameForm";
            this.TransparencyKey = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(132)))), ((int)(((byte)(73)))));
            ((System.ComponentModel.ISupportInitialize)(this.pbSave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCancel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCoin)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbSave;
        private System.Windows.Forms.PictureBox pbCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tlpStats;
        private System.Windows.Forms.Label lblCoin;
        private System.Windows.Forms.PictureBox pbCoin;
    }
}