namespace tiny_robotic_wizard.Wizard
{
    partial class AquaButton
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AquaButton));
            this.label = new System.Windows.Forms.Label();
            this._3dImage = new System.Windows.Forms.PictureBox();
            this.baseImages = new System.Windows.Forms.ImageList(this.components);
            ((System.ComponentModel.ISupportInitialize)(this._3dImage)).BeginInit();
            this.SuspendLayout();
            // 
            // label
            // 
            this.label.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label.Font = new System.Drawing.Font("HG丸ｺﾞｼｯｸM-PRO", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label.ForeColor = System.Drawing.Color.Black;
            this.label.Location = new System.Drawing.Point(0, 0);
            this.label.Margin = new System.Windows.Forms.Padding(0);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(240, 100);
            this.label.TabIndex = 1;
            this.label.Text = "label";
            this.label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _3dImage
            // 
            this._3dImage.Image = ((System.Drawing.Image)(resources.GetObject("_3dImage.Image")));
            this._3dImage.Location = new System.Drawing.Point(0, 0);
            this._3dImage.Margin = new System.Windows.Forms.Padding(0);
            this._3dImage.Name = "_3dImage";
            this._3dImage.Size = new System.Drawing.Size(240, 100);
            this._3dImage.TabIndex = 2;
            this._3dImage.TabStop = false;
            // 
            // baseImages
            // 
            this.baseImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("baseImages.ImageStream")));
            this.baseImages.TransparentColor = System.Drawing.Color.Transparent;
            this.baseImages.Images.SetKeyName(0, "base.png");
            this.baseImages.Images.SetKeyName(1, "selectedBase.png");
            // 
            // AquaButton
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this._3dImage);
            this.Controls.Add(this.label);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "AquaButton";
            this.Size = new System.Drawing.Size(240, 100);
            ((System.ComponentModel.ISupportInitialize)(this._3dImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label;
        private System.Windows.Forms.PictureBox _3dImage;
        private System.Windows.Forms.ImageList baseImages;
    }
}
