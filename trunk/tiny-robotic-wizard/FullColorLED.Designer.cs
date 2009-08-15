namespace tiny_robotic_wizard
{
    partial class FullColorLED
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FullColorLED));
            this.FullColorLEDList = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // FullColorLEDList
            // 
            this.FullColorLEDList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("FullColorLEDList.ImageStream")));
            this.FullColorLEDList.TransparentColor = System.Drawing.Color.Transparent;
            this.FullColorLEDList.Images.SetKeyName(0, "LED-Black.png");
            this.FullColorLEDList.Images.SetKeyName(1, "LED-Blue.png");
            this.FullColorLEDList.Images.SetKeyName(2, "LED-Green.png");
            this.FullColorLEDList.Images.SetKeyName(3, "LED-Cyan.png");
            this.FullColorLEDList.Images.SetKeyName(4, "LED-Red.png");
            this.FullColorLEDList.Images.SetKeyName(5, "LED-Magenta.png");
            this.FullColorLEDList.Images.SetKeyName(6, "LED-Yellow.png");
            this.FullColorLEDList.Images.SetKeyName(7, "LED-White.png");
            // 
            // FullColorLED
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Name = "FullColorLED";
            this.Size = new System.Drawing.Size(180, 100);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList FullColorLEDList;
    }
}
