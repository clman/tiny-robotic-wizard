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
            this.mainPanel = new System.Windows.Forms.Panel();
            this.guideText = new System.Windows.Forms.TextBox();
            this.edit = new tiny_robotic_wizard.Wizard.AquaIcon();
            this.delete = new tiny_robotic_wizard.Wizard.AquaIcon();
            this.saveAs = new tiny_robotic_wizard.Wizard.AquaIcon();
            this.transfer = new tiny_robotic_wizard.Wizard.AquaIcon();
            this.save = new tiny_robotic_wizard.Wizard.AquaIcon();
            this.open = new tiny_robotic_wizard.Wizard.AquaIcon();
            this.new_ = new tiny_robotic_wizard.Wizard.AquaIcon();
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
            this.container.Size = new System.Drawing.Size(857, 462);
            this.container.TabIndex = 7;
            // 
            // mainPanel
            // 
            this.mainPanel.AutoScroll = true;
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(0, 19);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(855, 441);
            this.mainPanel.TabIndex = 1;
            // 
            // guideText
            // 
            this.guideText.BackColor = System.Drawing.SystemColors.Info;
            this.guideText.Dock = System.Windows.Forms.DockStyle.Top;
            this.guideText.Location = new System.Drawing.Point(0, 0);
            this.guideText.Name = "guideText";
            this.guideText.ReadOnly = true;
            this.guideText.Size = new System.Drawing.Size(855, 19);
            this.guideText.TabIndex = 0;
            // 
            // edit
            // 
            this.edit.BackColor = System.Drawing.Color.Transparent;
            this.edit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.edit.Hint = "編集";
            this.edit.Image = ((System.Drawing.Image)(resources.GetObject("edit.Image")));
            this.edit.Location = new System.Drawing.Point(542, 12);
            this.edit.Name = "edit";
            this.edit.Size = new System.Drawing.Size(100, 100);
            this.edit.TabIndex = 10;
            this.edit.MouseClick += new System.Windows.Forms.MouseEventHandler(this.edit_MouseClick);
            // 
            // delete
            // 
            this.delete.BackColor = System.Drawing.Color.Transparent;
            this.delete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.delete.Hint = "削除";
            this.delete.Image = ((System.Drawing.Image)(resources.GetObject("delete.Image")));
            this.delete.Location = new System.Drawing.Point(224, 12);
            this.delete.Name = "delete";
            this.delete.Size = new System.Drawing.Size(100, 100);
            this.delete.TabIndex = 9;
            this.delete.MouseClick += new System.Windows.Forms.MouseEventHandler(this.delete_MouseClick);
            // 
            // saveAs
            // 
            this.saveAs.BackColor = System.Drawing.Color.Transparent;
            this.saveAs.Cursor = System.Windows.Forms.Cursors.Hand;
            this.saveAs.Hint = "名前付けて保存";
            this.saveAs.Image = ((System.Drawing.Image)(resources.GetObject("saveAs.Image")));
            this.saveAs.Location = new System.Drawing.Point(330, 12);
            this.saveAs.Name = "saveAs";
            this.saveAs.Size = new System.Drawing.Size(100, 100);
            this.saveAs.TabIndex = 8;
            this.saveAs.MouseClick += new System.Windows.Forms.MouseEventHandler(this.saveAs_MouseClick);
            // 
            // transfer
            // 
            this.transfer.BackColor = System.Drawing.Color.Transparent;
            this.transfer.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("transfer.BackgroundImage")));
            this.transfer.Cursor = System.Windows.Forms.Cursors.Hand;
            this.transfer.Hint = "ロボットに転送";
            this.transfer.Image = ((System.Drawing.Image)(resources.GetObject("transfer.Image")));
            this.transfer.Location = new System.Drawing.Point(648, 12);
            this.transfer.Name = "transfer";
            this.transfer.Size = new System.Drawing.Size(100, 100);
            this.transfer.TabIndex = 6;
            // 
            // save
            // 
            this.save.BackColor = System.Drawing.Color.Transparent;
            this.save.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("save.BackgroundImage")));
            this.save.Cursor = System.Windows.Forms.Cursors.Hand;
            this.save.Hint = "保存";
            this.save.Image = ((System.Drawing.Image)(resources.GetObject("save.Image")));
            this.save.Location = new System.Drawing.Point(436, 13);
            this.save.Name = "save";
            this.save.Size = new System.Drawing.Size(100, 100);
            this.save.TabIndex = 5;
            this.save.MouseClick += new System.Windows.Forms.MouseEventHandler(this.save_MouseClick);
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
            this.open.MouseClick += new System.Windows.Forms.MouseEventHandler(this.open_MouseClick);
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
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(881, 592);
            this.Controls.Add(this.edit);
            this.Controls.Add(this.delete);
            this.Controls.Add(this.saveAs);
            this.Controls.Add(this.container);
            this.Controls.Add(this.transfer);
            this.Controls.Add(this.save);
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
        private tiny_robotic_wizard.Wizard.AquaIcon save;
        private tiny_robotic_wizard.Wizard.AquaIcon transfer;
        private System.Windows.Forms.Panel container;
        private System.Windows.Forms.Panel mainPanel;
        private tiny_robotic_wizard.Wizard.AquaIcon saveAs;
        private System.Windows.Forms.TextBox guideText;
        private tiny_robotic_wizard.Wizard.AquaIcon delete;
        private tiny_robotic_wizard.Wizard.AquaIcon edit;





    }
}

