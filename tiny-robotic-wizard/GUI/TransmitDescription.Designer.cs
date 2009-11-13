namespace tiny_robotic_wizard.GUI
{
    partial class TransmitDescription
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TransmitDescription));
            this.ready = new System.Windows.Forms.PictureBox();
            this.successed = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.ready)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.successed)).BeginInit();
            this.SuspendLayout();
            // 
            // ready
            // 
            this.ready.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ready.Image = ((System.Drawing.Image)(resources.GetObject("ready.Image")));
            this.ready.Location = new System.Drawing.Point(0, 0);
            this.ready.Name = "ready";
            this.ready.Size = new System.Drawing.Size(432, 301);
            this.ready.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.ready.TabIndex = 0;
            this.ready.TabStop = false;
            this.ready.Visible = false;
            // 
            // successed
            // 
            this.successed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.successed.Image = ((System.Drawing.Image)(resources.GetObject("successed.Image")));
            this.successed.Location = new System.Drawing.Point(0, 0);
            this.successed.Name = "successed";
            this.successed.Size = new System.Drawing.Size(432, 301);
            this.successed.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.successed.TabIndex = 1;
            this.successed.TabStop = false;
            this.successed.Visible = false;
            // 
            // TransmitDescription
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.ready);
            this.Controls.Add(this.successed);
            this.Name = "TransmitDescription";
            this.Size = new System.Drawing.Size(432, 301);
            ((System.ComponentModel.ISupportInitialize)(this.ready)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.successed)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox ready;
        private System.Windows.Forms.PictureBox successed;
    }
}
