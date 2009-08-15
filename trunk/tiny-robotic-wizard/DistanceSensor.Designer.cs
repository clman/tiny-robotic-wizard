namespace tiny_robotic_wizard
{
    partial class DistanceSensor
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
            this.DistanceLevel = new System.Windows.Forms.TrackBar();
            this.near = new System.Windows.Forms.Label();
            this.far = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.DistanceLevel)).BeginInit();
            this.SuspendLayout();
            // 
            // DistanceLevel
            // 
            this.DistanceLevel.Enabled = false;
            this.DistanceLevel.LargeChange = 1;
            this.DistanceLevel.Location = new System.Drawing.Point(-1, 45);
            this.DistanceLevel.Maximum = 2;
            this.DistanceLevel.Name = "DistanceLevel";
            this.DistanceLevel.Size = new System.Drawing.Size(181, 45);
            this.DistanceLevel.TabIndex = 0;
            this.DistanceLevel.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            // 
            // near
            // 
            this.near.AutoSize = true;
            this.near.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.near.Location = new System.Drawing.Point(-4, 18);
            this.near.Name = "near";
            this.near.Size = new System.Drawing.Size(34, 24);
            this.near.TabIndex = 1;
            this.near.Text = "近";
            // 
            // far
            // 
            this.far.AutoSize = true;
            this.far.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.far.Location = new System.Drawing.Point(151, 18);
            this.far.Name = "far";
            this.far.Size = new System.Drawing.Size(34, 24);
            this.far.TabIndex = 2;
            this.far.Text = "遠";
            // 
            // DistanceSensor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.far);
            this.Controls.Add(this.near);
            this.Controls.Add(this.DistanceLevel);
            this.Name = "DistanceSensor";
            this.Size = new System.Drawing.Size(180, 100);
            ((System.ComponentModel.ISupportInitialize)(this.DistanceLevel)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar DistanceLevel;
        private System.Windows.Forms.Label near;
        private System.Windows.Forms.Label far;
    }
}
