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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.container = new System.Windows.Forms.Panel();
            this.aquaIcon4 = new tiny_robotic_wizard.Wizard.AquaIcon();
            this.aquaIcon3 = new tiny_robotic_wizard.Wizard.AquaIcon();
            this.open = new tiny_robotic_wizard.Wizard.AquaIcon();
            this.new_ = new tiny_robotic_wizard.Wizard.AquaIcon();
            this.guideText = new System.Windows.Forms.TextBox();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.container.SuspendLayout();
            this.SuspendLayout();
            // 
            // container
            // 
            this.container.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.container.AutoScroll = true;
            this.container.BackColor = System.Drawing.SystemColors.Window;
            this.container.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.container.Controls.Add(this.mainPanel);
            this.container.Controls.Add(this.guideText);
            this.container.Location = new System.Drawing.Point(12, 118);
            this.container.Name = "container";
            this.container.Size = new System.Drawing.Size(727, 462);
            this.container.TabIndex = 7;
            // 
            // aquaIcon4
            // 
            this.aquaIcon4.BackColor = System.Drawing.Color.Transparent;
            this.aquaIcon4.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("aquaIcon4.BackgroundImage")));
            this.aquaIcon4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.aquaIcon4.Hint = "ロボットに転送";
            this.aquaIcon4.Image = ((System.Drawing.Image)(resources.GetObject("aquaIcon4.Image")));
            this.aquaIcon4.Location = new System.Drawing.Point(330, 12);
            this.aquaIcon4.Name = "aquaIcon4";
            this.aquaIcon4.Size = new System.Drawing.Size(100, 100);
            this.aquaIcon4.TabIndex = 6;
            // 
            // aquaIcon3
            // 
            this.aquaIcon3.BackColor = System.Drawing.Color.Transparent;
            this.aquaIcon3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("aquaIcon3.BackgroundImage")));
            this.aquaIcon3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.aquaIcon3.Hint = "保存";
            this.aquaIcon3.Image = ((System.Drawing.Image)(resources.GetObject("aquaIcon3.Image")));
            this.aquaIcon3.Location = new System.Drawing.Point(224, 12);
            this.aquaIcon3.Name = "aquaIcon3";
            this.aquaIcon3.Size = new System.Drawing.Size(100, 100);
            this.aquaIcon3.TabIndex = 5;
            // 
            // open
            // 
            this.open.BackColor = System.Drawing.Color.Transparent;
            this.open.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("open.BackgroundImage")));
            this.open.Cursor = System.Windows.Forms.Cursors.Hand;
            this.open.Hint = "開く";
            this.open.Image = ((System.Drawing.Image)(resources.GetObject("open.Image")));
            this.open.Location = new System.Drawing.Point(118, 12);
            this.open.Name = "open";
            this.open.Size = new System.Drawing.Size(100, 100);
            this.open.TabIndex = 4;
            // 
            // new_
            // 
            this.new_.BackColor = System.Drawing.Color.Transparent;
            this.new_.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("new_.BackgroundImage")));
            this.new_.Cursor = System.Windows.Forms.Cursors.Hand;
            this.new_.Hint = "新規作成";
            this.new_.Image = ((System.Drawing.Image)(resources.GetObject("new_.Image")));
            this.new_.Location = new System.Drawing.Point(12, 12);
            this.new_.Name = "new_";
            this.new_.Size = new System.Drawing.Size(100, 100);
            this.new_.TabIndex = 3;
            this.new_.MouseClick += new System.Windows.Forms.MouseEventHandler(this.new__MouseClick);
            // 
            // guideText
            // 
            this.guideText.BackColor = System.Drawing.SystemColors.Info;
            this.guideText.Dock = System.Windows.Forms.DockStyle.Top;
            this.guideText.Location = new System.Drawing.Point(0, 0);
            this.guideText.Name = "guideText";
            this.guideText.ReadOnly = true;
            this.guideText.Size = new System.Drawing.Size(725, 19);
            this.guideText.TabIndex = 0;
            // 
            // mainPanel
            // 
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(0, 19);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(725, 441);
            this.mainPanel.TabIndex = 1;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(751, 592);
            this.Controls.Add(this.container);
            this.Controls.Add(this.aquaIcon4);
            this.Controls.Add(this.aquaIcon3);
            this.Controls.Add(this.open);
            this.Controls.Add(this.new_);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.container.ResumeLayout(false);
            this.container.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private tiny_robotic_wizard.Wizard.AquaIcon new_;
        private tiny_robotic_wizard.Wizard.AquaIcon open;
        private tiny_robotic_wizard.Wizard.AquaIcon aquaIcon3;
        private tiny_robotic_wizard.Wizard.AquaIcon aquaIcon4;
        private System.Windows.Forms.Panel container;
        private System.Windows.Forms.TextBox guideText;
        private System.Windows.Forms.Panel mainPanel;





    }
}

