namespace tiny_robotic_wizard.Wizard
{
    partial class CreateOrOpen
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
            this.message = new System.Windows.Forms.Label();
            this.aquaButton2 = new tiny_robotic_wizard.Wizard.AquaButton();
            this.aquaButton1 = new tiny_robotic_wizard.Wizard.AquaButton();
            this.SuspendLayout();
            // 
            // message
            // 
            this.message.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.message.AutoSize = true;
            this.message.Font = new System.Drawing.Font("HG丸ｺﾞｼｯｸM-PRO", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.message.Location = new System.Drawing.Point(14, 22);
            this.message.Name = "message";
            this.message.Size = new System.Drawing.Size(470, 35);
            this.message.TabIndex = 2;
            this.message.Text = "プログラミングを開始します";
            // 
            // aquaButton2
            // 
            this.aquaButton2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.aquaButton2.BackColor = System.Drawing.Color.Transparent;
            this.aquaButton2.Caption = "開く";
            this.aquaButton2.Location = new System.Drawing.Point(260, 101);
            this.aquaButton2.Margin = new System.Windows.Forms.Padding(0);
            this.aquaButton2.Name = "aquaButton2";
            this.aquaButton2.Size = new System.Drawing.Size(240, 100);
            this.aquaButton2.TabIndex = 1;
            // 
            // aquaButton1
            // 
            this.aquaButton1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.aquaButton1.BackColor = System.Drawing.Color.Transparent;
            this.aquaButton1.Caption = "新規作成";
            this.aquaButton1.Location = new System.Drawing.Point(0, 101);
            this.aquaButton1.Margin = new System.Windows.Forms.Padding(0);
            this.aquaButton1.Name = "aquaButton1";
            this.aquaButton1.Size = new System.Drawing.Size(240, 100);
            this.aquaButton1.TabIndex = 0;
            // 
            // CreateOrOpen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.message);
            this.Controls.Add(this.aquaButton2);
            this.Controls.Add(this.aquaButton1);
            this.Name = "CreateOrOpen";
            this.Size = new System.Drawing.Size(500, 201);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private AquaButton aquaButton1;
        private AquaButton aquaButton2;
        private System.Windows.Forms.Label message;

    }
}
