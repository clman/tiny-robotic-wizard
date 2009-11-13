namespace tiny_robotic_wizard.Wizard
{
    partial class AquaIcon
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AquaIcon));
            this.baseImages = new System.Windows.Forms.ImageList(this.components);
            this.mainImage = new System.Windows.Forms.PictureBox();
            this._3dImage = new System.Windows.Forms.PictureBox();
            this.baseImage = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.mainImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._3dImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.baseImage)).BeginInit();
            this.SuspendLayout();
            // 
            // baseImages
            // 
            this.baseImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("baseImages.ImageStream")));
            this.baseImages.TransparentColor = System.Drawing.Color.Transparent;
            this.baseImages.Images.SetKeyName(0, "base2.png");
            this.baseImages.Images.SetKeyName(1, "currentBase2.png");
            this.baseImages.Images.SetKeyName(2, "disabledBase2.png");
            // 
            // mainImage
            // 
            this.mainImage.Location = new System.Drawing.Point(0, 0);
            this.mainImage.Margin = new System.Windows.Forms.Padding(0);
            this.mainImage.Name = "mainImage";
            this.mainImage.Size = new System.Drawing.Size(100, 100);
            this.mainImage.TabIndex = 0;
            this.mainImage.TabStop = false;
            // 
            // _3dImage
            // 
            this._3dImage.Image = ((System.Drawing.Image)(resources.GetObject("_3dImage.Image")));
            this._3dImage.Location = new System.Drawing.Point(0, 0);
            this._3dImage.Margin = new System.Windows.Forms.Padding(0);
            this._3dImage.Name = "_3dImage";
            this._3dImage.Size = new System.Drawing.Size(100, 100);
            this._3dImage.TabIndex = 1;
            this._3dImage.TabStop = false;
            // 
            // baseImage
            // 
            this.baseImage.Location = new System.Drawing.Point(0, 0);
            this.baseImage.Margin = new System.Windows.Forms.Padding(0);
            this.baseImage.Name = "baseImage";
            this.baseImage.Size = new System.Drawing.Size(100, 100);
            this.baseImage.TabIndex = 2;
            this.baseImage.TabStop = false;
            // 
            // AquaIcon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this._3dImage);
            this.Controls.Add(this.mainImage);
            this.Controls.Add(this.baseImage);
            this.Name = "AquaIcon";
            this.Size = new System.Drawing.Size(100, 100);
            ((System.ComponentModel.ISupportInitialize)(this.mainImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._3dImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.baseImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList baseImages;
        private System.Windows.Forms.PictureBox mainImage;
        private System.Windows.Forms.PictureBox _3dImage;
        private System.Windows.Forms.PictureBox baseImage;
    }
}
