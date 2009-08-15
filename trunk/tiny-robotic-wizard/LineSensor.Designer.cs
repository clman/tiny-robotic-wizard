namespace tiny_robotic_wizard
{
    partial class LineSensor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LineSensor));
            this.lineSensorStatusList = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // lineSensorStatusList
            // 
            this.lineSensorStatusList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("lineSensorStatusList.ImageStream")));
            this.lineSensorStatusList.TransparentColor = System.Drawing.Color.Transparent;
            this.lineSensorStatusList.Images.SetKeyName(0, "ラインセンサ0.png");
            this.lineSensorStatusList.Images.SetKeyName(1, "ラインセンサ1.png");
            this.lineSensorStatusList.Images.SetKeyName(2, "ラインセンサ2.png");
            this.lineSensorStatusList.Images.SetKeyName(3, "ラインセンサ3.png");
            // 
            // LineSensor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Name = "LineSensor";
            this.Size = new System.Drawing.Size(180, 100);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList lineSensorStatusList;

    }
}
