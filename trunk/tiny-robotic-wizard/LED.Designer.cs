namespace tiny_robotic_wizard
{
    partial class LED
    {
        /// <summary> 
        /// 必要なデザイナ変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region コンポーネント デザイナで生成されたコード

        /// <summary> 
        /// デザイナ サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディタで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LED));
            this.LEDColorList = new System.Windows.Forms.ImageList(this.components);
            this.changeColorMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.changeToBlack = new System.Windows.Forms.ToolStripMenuItem();
            this.changeToBlue = new System.Windows.Forms.ToolStripMenuItem();
            this.changeToGreen = new System.Windows.Forms.ToolStripMenuItem();
            this.changeToCyan = new System.Windows.Forms.ToolStripMenuItem();
            this.changeToRed = new System.Windows.Forms.ToolStripMenuItem();
            this.changeToMagenta = new System.Windows.Forms.ToolStripMenuItem();
            this.changeToYellow = new System.Windows.Forms.ToolStripMenuItem();
            this.changeToWhite = new System.Windows.Forms.ToolStripMenuItem();
            this.changeColorMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // LEDColorList
            // 
            this.LEDColorList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("LEDColorList.ImageStream")));
            this.LEDColorList.TransparentColor = System.Drawing.Color.Transparent;
            this.LEDColorList.Images.SetKeyName(0, "LED-Black.png");
            this.LEDColorList.Images.SetKeyName(1, "LED-Blue.png");
            this.LEDColorList.Images.SetKeyName(2, "LED-Green.png");
            this.LEDColorList.Images.SetKeyName(3, "LED-Cyan.png");
            this.LEDColorList.Images.SetKeyName(4, "LED-Red.png");
            this.LEDColorList.Images.SetKeyName(5, "LED-Magenta.png");
            this.LEDColorList.Images.SetKeyName(6, "LED-Yellow.png");
            this.LEDColorList.Images.SetKeyName(7, "LED-White.png");
            // 
            // changeColorMenu
            // 
            this.changeColorMenu.ImageScalingSize = new System.Drawing.Size(180, 100);
            this.changeColorMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.changeToBlack,
            this.changeToBlue,
            this.changeToGreen,
            this.changeToCyan,
            this.changeToRed,
            this.changeToMagenta,
            this.changeToYellow,
            this.changeToWhite});
            this.changeColorMenu.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Table;
            this.changeColorMenu.Name = "changeColorMenu";
            this.changeColorMenu.Size = new System.Drawing.Size(125, 180);
            // 
            // changeToBlack
            // 
            this.changeToBlack.Name = "changeToBlack";
            this.changeToBlack.Size = new System.Drawing.Size(124, 22);
            this.changeToBlack.Text = "非点灯";
            this.changeToBlack.Click += new System.EventHandler(this.changeToBlack_Click);
            // 
            // changeToBlue
            // 
            this.changeToBlue.Name = "changeToBlue";
            this.changeToBlue.Size = new System.Drawing.Size(124, 22);
            this.changeToBlue.Text = "青";
            this.changeToBlue.Click += new System.EventHandler(this.changeToBlue_Click);
            // 
            // changeToGreen
            // 
            this.changeToGreen.Name = "changeToGreen";
            this.changeToGreen.Size = new System.Drawing.Size(124, 22);
            this.changeToGreen.Text = "緑";
            this.changeToGreen.Click += new System.EventHandler(this.changeToGreen_Click);
            // 
            // changeToCyan
            // 
            this.changeToCyan.Name = "changeToCyan";
            this.changeToCyan.Size = new System.Drawing.Size(124, 22);
            this.changeToCyan.Text = "シアン";
            this.changeToCyan.Click += new System.EventHandler(this.changeToCyan_Click);
            // 
            // changeToRed
            // 
            this.changeToRed.Name = "changeToRed";
            this.changeToRed.Size = new System.Drawing.Size(124, 22);
            this.changeToRed.Text = "赤";
            this.changeToRed.Click += new System.EventHandler(this.changeToRed_Click);
            // 
            // changeToMagenta
            // 
            this.changeToMagenta.Name = "changeToMagenta";
            this.changeToMagenta.Size = new System.Drawing.Size(124, 22);
            this.changeToMagenta.Text = "マゼンタ";
            this.changeToMagenta.Click += new System.EventHandler(this.changeToMagenta_Click);
            // 
            // changeToYellow
            // 
            this.changeToYellow.Name = "changeToYellow";
            this.changeToYellow.Size = new System.Drawing.Size(124, 22);
            this.changeToYellow.Text = "黄";
            this.changeToYellow.Click += new System.EventHandler(this.changeToYellow_Click);
            // 
            // changeToWhite
            // 
            this.changeToWhite.Name = "changeToWhite";
            this.changeToWhite.Size = new System.Drawing.Size(124, 22);
            this.changeToWhite.Text = "白";
            this.changeToWhite.Click += new System.EventHandler(this.changeToWhite_Click);
            // 
            // LED
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Name = "LED";
            this.Size = new System.Drawing.Size(180, 100);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.LED_MouseClick);
            this.changeColorMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList LEDColorList;
        private System.Windows.Forms.ContextMenuStrip changeColorMenu;
        private System.Windows.Forms.ToolStripMenuItem changeToBlack;
        private System.Windows.Forms.ToolStripMenuItem changeToBlue;
        private System.Windows.Forms.ToolStripMenuItem changeToGreen;
        private System.Windows.Forms.ToolStripMenuItem changeToCyan;
        private System.Windows.Forms.ToolStripMenuItem changeToRed;
        private System.Windows.Forms.ToolStripMenuItem changeToMagenta;
        private System.Windows.Forms.ToolStripMenuItem changeToYellow;
        private System.Windows.Forms.ToolStripMenuItem changeToWhite;
    }
}
