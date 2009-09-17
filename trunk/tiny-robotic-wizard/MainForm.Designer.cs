namespace tiny_robotic_wizard
{
    partial class MainForm
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

        #region Windows フォーム デザイナで生成されたコード

        /// <summary>
        /// デザイナ サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディタで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.createOrOpen1 = new tiny_robotic_wizard.Wizard.CreateOrOpen();
            this.SuspendLayout();
            // 
            // createOrOpen1
            // 
            this.createOrOpen1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.createOrOpen1.Location = new System.Drawing.Point(0, 0);
            this.createOrOpen1.Name = "createOrOpen1";
            this.createOrOpen1.Size = new System.Drawing.Size(543, 388);
            this.createOrOpen1.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(543, 388);
            this.Controls.Add(this.createOrOpen1);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private tiny_robotic_wizard.Wizard.CreateOrOpen createOrOpen1;



    }
}

