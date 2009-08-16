namespace tiny_robotic_wizard
{
    partial class Move
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Move));
            this.moveFunctionImageList = new System.Windows.Forms.ImageList(this.components);
            this.changeMoveFunctionMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.changeMoveFunctionToThrough = new System.Windows.Forms.ToolStripMenuItem();
            this.changeMoveFunctionToForward = new System.Windows.Forms.ToolStripMenuItem();
            this.changeMoveFunctionToBack = new System.Windows.Forms.ToolStripMenuItem();
            this.changeMoveFunctionToCW = new System.Windows.Forms.ToolStripMenuItem();
            this.changeMoveFunctionToCCW = new System.Windows.Forms.ToolStripMenuItem();
            this.changeMoveFunctionToStop = new System.Windows.Forms.ToolStripMenuItem();
            this.changeMoveFunctionMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // moveFunctionImageList
            // 
            this.moveFunctionImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("moveFunctionImageList.ImageStream")));
            this.moveFunctionImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.moveFunctionImageList.Images.SetKeyName(0, "移動-何もしない.png");
            this.moveFunctionImageList.Images.SetKeyName(1, "移動-前進.png");
            this.moveFunctionImageList.Images.SetKeyName(2, "移動-後進.png");
            this.moveFunctionImageList.Images.SetKeyName(3, "移動-右回り.png");
            this.moveFunctionImageList.Images.SetKeyName(4, "移動-左回り.png");
            this.moveFunctionImageList.Images.SetKeyName(5, "移動-停止.png");
            // 
            // changeMoveFunctionMenu
            // 
            this.changeMoveFunctionMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.changeMoveFunctionToThrough,
            this.changeMoveFunctionToForward,
            this.changeMoveFunctionToBack,
            this.changeMoveFunctionToCW,
            this.changeMoveFunctionToCCW,
            this.changeMoveFunctionToStop});
            this.changeMoveFunctionMenu.Name = "contextMenuStrip1";
            this.changeMoveFunctionMenu.Size = new System.Drawing.Size(137, 136);
            // 
            // changeMoveFunctionToThrough
            // 
            this.changeMoveFunctionToThrough.Name = "changeMoveFunctionToThrough";
            this.changeMoveFunctionToThrough.Size = new System.Drawing.Size(136, 22);
            this.changeMoveFunctionToThrough.Text = "何もしない";
            this.changeMoveFunctionToThrough.Click += new System.EventHandler(this.changeMoveFunctionToThrough_Click);
            // 
            // changeMoveFunctionToForward
            // 
            this.changeMoveFunctionToForward.Name = "changeMoveFunctionToForward";
            this.changeMoveFunctionToForward.Size = new System.Drawing.Size(136, 22);
            this.changeMoveFunctionToForward.Text = "前進";
            this.changeMoveFunctionToForward.Click += new System.EventHandler(this.changeMoveFunctionToForward_Click);
            // 
            // changeMoveFunctionToBack
            // 
            this.changeMoveFunctionToBack.Name = "changeMoveFunctionToBack";
            this.changeMoveFunctionToBack.Size = new System.Drawing.Size(136, 22);
            this.changeMoveFunctionToBack.Text = "後進";
            this.changeMoveFunctionToBack.Click += new System.EventHandler(this.changeMoveFunctionToBack_Click);
            // 
            // changeMoveFunctionToCW
            // 
            this.changeMoveFunctionToCW.Name = "changeMoveFunctionToCW";
            this.changeMoveFunctionToCW.Size = new System.Drawing.Size(136, 22);
            this.changeMoveFunctionToCW.Text = "右回り";
            this.changeMoveFunctionToCW.Click += new System.EventHandler(this.changeMoveFunctionToCW_Click);
            // 
            // changeMoveFunctionToCCW
            // 
            this.changeMoveFunctionToCCW.Name = "changeMoveFunctionToCCW";
            this.changeMoveFunctionToCCW.Size = new System.Drawing.Size(136, 22);
            this.changeMoveFunctionToCCW.Text = "左回り";
            this.changeMoveFunctionToCCW.Click += new System.EventHandler(this.changeMoveFunctionToCCW_Click);
            // 
            // changeMoveFunctionToStop
            // 
            this.changeMoveFunctionToStop.Name = "changeMoveFunctionToStop";
            this.changeMoveFunctionToStop.Size = new System.Drawing.Size(136, 22);
            this.changeMoveFunctionToStop.Text = "停止";
            this.changeMoveFunctionToStop.Click += new System.EventHandler(this.changeMoveFunctionToStop_Click);
            // 
            // Move
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Name = "Move";
            this.Size = new System.Drawing.Size(180, 100);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Move_MouseClick);
            this.changeMoveFunctionMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList moveFunctionImageList;
        private System.Windows.Forms.ContextMenuStrip changeMoveFunctionMenu;
        private System.Windows.Forms.ToolStripMenuItem changeMoveFunctionToThrough;
        private System.Windows.Forms.ToolStripMenuItem changeMoveFunctionToForward;
        private System.Windows.Forms.ToolStripMenuItem changeMoveFunctionToBack;
        private System.Windows.Forms.ToolStripMenuItem changeMoveFunctionToCW;
        private System.Windows.Forms.ToolStripMenuItem changeMoveFunctionToCCW;
        private System.Windows.Forms.ToolStripMenuItem changeMoveFunctionToStop;
    }
}
