namespace LTControl
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
            this.components = new System.ComponentModel.Container();
            this.sensorLabel0 = new System.Windows.Forms.Label();
            this.sensorValue0 = new System.Windows.Forms.TextBox();
            this.sensorValue1 = new System.Windows.Forms.TextBox();
            this.sensorLabel1 = new System.Windows.Forms.Label();
            this.sensorValue2 = new System.Windows.Forms.TextBox();
            this.sensorLabel2 = new System.Windows.Forms.Label();
            this.connectButton = new System.Windows.Forms.Button();
            this.sensorTimer = new System.Windows.Forms.Timer(this.components);
            this.motorOutputLTrackbar = new System.Windows.Forms.TrackBar();
            this.motorLGroup = new System.Windows.Forms.GroupBox();
            this.brakeLButton = new System.Windows.Forms.Button();
            this.motorRGroup = new System.Windows.Forms.GroupBox();
            this.brakeRButton = new System.Windows.Forms.Button();
            this.motorOutputRTrackbar = new System.Windows.Forms.TrackBar();
            this.positionText = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.positionTrackbar = new System.Windows.Forms.TrackBar();
            this.setLowerButton = new System.Windows.Forms.Button();
            this.setUpperButton = new System.Windows.Forms.Button();
            this.sensorMin0 = new System.Windows.Forms.TextBox();
            this.sensorMax0 = new System.Windows.Forms.TextBox();
            this.sensorNormalized0 = new System.Windows.Forms.TextBox();
            this.sensorMin1 = new System.Windows.Forms.TextBox();
            this.sensorMin2 = new System.Windows.Forms.TextBox();
            this.sensorMax1 = new System.Windows.Forms.TextBox();
            this.sensorMax2 = new System.Windows.Forms.TextBox();
            this.sensorNormalized1 = new System.Windows.Forms.TextBox();
            this.sensorNormalized2 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.motorOutputLTrackbar)).BeginInit();
            this.motorLGroup.SuspendLayout();
            this.motorRGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.motorOutputRTrackbar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.positionTrackbar)).BeginInit();
            this.SuspendLayout();
            // 
            // sensorLabel0
            // 
            this.sensorLabel0.AutoSize = true;
            this.sensorLabel0.Location = new System.Drawing.Point(12, 49);
            this.sensorLabel0.Name = "sensorLabel0";
            this.sensorLabel0.Size = new System.Drawing.Size(17, 12);
            this.sensorLabel0.TabIndex = 0;
            this.sensorLabel0.Text = "左";
            // 
            // sensorValue0
            // 
            this.sensorValue0.Location = new System.Drawing.Point(97, 46);
            this.sensorValue0.Name = "sensorValue0";
            this.sensorValue0.ReadOnly = true;
            this.sensorValue0.Size = new System.Drawing.Size(44, 19);
            this.sensorValue0.TabIndex = 1;
            this.sensorValue0.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // sensorValue1
            // 
            this.sensorValue1.Location = new System.Drawing.Point(97, 71);
            this.sensorValue1.Name = "sensorValue1";
            this.sensorValue1.ReadOnly = true;
            this.sensorValue1.Size = new System.Drawing.Size(44, 19);
            this.sensorValue1.TabIndex = 3;
            this.sensorValue1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // sensorLabel1
            // 
            this.sensorLabel1.AutoSize = true;
            this.sensorLabel1.Location = new System.Drawing.Point(12, 74);
            this.sensorLabel1.Name = "sensorLabel1";
            this.sensorLabel1.Size = new System.Drawing.Size(29, 12);
            this.sensorLabel1.TabIndex = 2;
            this.sensorLabel1.Text = "中央";
            // 
            // sensorValue2
            // 
            this.sensorValue2.Location = new System.Drawing.Point(97, 97);
            this.sensorValue2.Name = "sensorValue2";
            this.sensorValue2.ReadOnly = true;
            this.sensorValue2.Size = new System.Drawing.Size(44, 19);
            this.sensorValue2.TabIndex = 5;
            this.sensorValue2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // sensorLabel2
            // 
            this.sensorLabel2.AutoSize = true;
            this.sensorLabel2.Location = new System.Drawing.Point(12, 100);
            this.sensorLabel2.Name = "sensorLabel2";
            this.sensorLabel2.Size = new System.Drawing.Size(17, 12);
            this.sensorLabel2.TabIndex = 4;
            this.sensorLabel2.Text = "右";
            // 
            // connectButton
            // 
            this.connectButton.Location = new System.Drawing.Point(12, 12);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(64, 23);
            this.connectButton.TabIndex = 6;
            this.connectButton.Text = "接続 (&C)";
            this.connectButton.UseVisualStyleBackColor = true;
            this.connectButton.Click += new System.EventHandler(this.connectButton_Click);
            // 
            // sensorTimer
            // 
            this.sensorTimer.Interval = 10;
            this.sensorTimer.Tick += new System.EventHandler(this.sensorTimer_Tick);
            // 
            // motorOutputLTrackbar
            // 
            this.motorOutputLTrackbar.LargeChange = 8;
            this.motorOutputLTrackbar.Location = new System.Drawing.Point(6, 18);
            this.motorOutputLTrackbar.Maximum = 255;
            this.motorOutputLTrackbar.Minimum = -255;
            this.motorOutputLTrackbar.Name = "motorOutputLTrackbar";
            this.motorOutputLTrackbar.Size = new System.Drawing.Size(276, 45);
            this.motorOutputLTrackbar.TabIndex = 7;
            this.motorOutputLTrackbar.Scroll += new System.EventHandler(this.motorOutputLTrackbar_Scroll);
            // 
            // motorLGroup
            // 
            this.motorLGroup.Controls.Add(this.brakeLButton);
            this.motorLGroup.Controls.Add(this.motorOutputLTrackbar);
            this.motorLGroup.Location = new System.Drawing.Point(14, 196);
            this.motorLGroup.Name = "motorLGroup";
            this.motorLGroup.Size = new System.Drawing.Size(355, 70);
            this.motorLGroup.TabIndex = 8;
            this.motorLGroup.TabStop = false;
            this.motorLGroup.Text = "モーター左";
            // 
            // brakeLButton
            // 
            this.brakeLButton.Location = new System.Drawing.Point(288, 18);
            this.brakeLButton.Name = "brakeLButton";
            this.brakeLButton.Size = new System.Drawing.Size(52, 45);
            this.brakeLButton.TabIndex = 8;
            this.brakeLButton.Text = "ブレーキ";
            this.brakeLButton.UseVisualStyleBackColor = true;
            this.brakeLButton.Click += new System.EventHandler(this.brakeLButton_Click);
            // 
            // motorRGroup
            // 
            this.motorRGroup.Controls.Add(this.brakeRButton);
            this.motorRGroup.Controls.Add(this.motorOutputRTrackbar);
            this.motorRGroup.Location = new System.Drawing.Point(14, 272);
            this.motorRGroup.Name = "motorRGroup";
            this.motorRGroup.Size = new System.Drawing.Size(355, 70);
            this.motorRGroup.TabIndex = 9;
            this.motorRGroup.TabStop = false;
            this.motorRGroup.Text = "モーター右";
            // 
            // brakeRButton
            // 
            this.brakeRButton.Location = new System.Drawing.Point(288, 18);
            this.brakeRButton.Name = "brakeRButton";
            this.brakeRButton.Size = new System.Drawing.Size(52, 45);
            this.brakeRButton.TabIndex = 8;
            this.brakeRButton.Text = "ブレーキ";
            this.brakeRButton.UseVisualStyleBackColor = true;
            this.brakeRButton.Click += new System.EventHandler(this.brakeRButton_Click);
            // 
            // motorOutputRTrackbar
            // 
            this.motorOutputRTrackbar.LargeChange = 8;
            this.motorOutputRTrackbar.Location = new System.Drawing.Point(6, 18);
            this.motorOutputRTrackbar.Maximum = 255;
            this.motorOutputRTrackbar.Minimum = -255;
            this.motorOutputRTrackbar.Name = "motorOutputRTrackbar";
            this.motorOutputRTrackbar.Size = new System.Drawing.Size(276, 45);
            this.motorOutputRTrackbar.TabIndex = 7;
            this.motorOutputRTrackbar.Scroll += new System.EventHandler(this.motorOutputRTrackbar_Scroll);
            // 
            // positionText
            // 
            this.positionText.Location = new System.Drawing.Point(47, 122);
            this.positionText.Name = "positionText";
            this.positionText.ReadOnly = true;
            this.positionText.Size = new System.Drawing.Size(44, 19);
            this.positionText.TabIndex = 11;
            this.positionText.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 125);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 10;
            this.label1.Text = "位置";
            // 
            // positionTrackbar
            // 
            this.positionTrackbar.Enabled = false;
            this.positionTrackbar.LargeChange = 64;
            this.positionTrackbar.Location = new System.Drawing.Point(12, 145);
            this.positionTrackbar.Maximum = 1023;
            this.positionTrackbar.Name = "positionTrackbar";
            this.positionTrackbar.Size = new System.Drawing.Size(358, 45);
            this.positionTrackbar.TabIndex = 12;
            this.positionTrackbar.TickFrequency = 64;
            // 
            // setLowerButton
            // 
            this.setLowerButton.Location = new System.Drawing.Point(168, 12);
            this.setLowerButton.Name = "setLowerButton";
            this.setLowerButton.Size = new System.Drawing.Size(98, 23);
            this.setLowerButton.TabIndex = 13;
            this.setLowerButton.Text = "最小値設定 (&M)";
            this.setLowerButton.UseVisualStyleBackColor = true;
            this.setLowerButton.Click += new System.EventHandler(this.setLowerButton_Click);
            // 
            // setUpperButton
            // 
            this.setUpperButton.Location = new System.Drawing.Point(272, 12);
            this.setUpperButton.Name = "setUpperButton";
            this.setUpperButton.Size = new System.Drawing.Size(98, 23);
            this.setUpperButton.TabIndex = 14;
            this.setUpperButton.Text = "最大値設定 (&A)";
            this.setUpperButton.UseVisualStyleBackColor = true;
            this.setUpperButton.Click += new System.EventHandler(this.setUpperButton_Click);
            // 
            // sensorMin0
            // 
            this.sensorMin0.Location = new System.Drawing.Point(47, 46);
            this.sensorMin0.Name = "sensorMin0";
            this.sensorMin0.ReadOnly = true;
            this.sensorMin0.Size = new System.Drawing.Size(44, 19);
            this.sensorMin0.TabIndex = 15;
            this.sensorMin0.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // sensorMax0
            // 
            this.sensorMax0.Location = new System.Drawing.Point(147, 46);
            this.sensorMax0.Name = "sensorMax0";
            this.sensorMax0.ReadOnly = true;
            this.sensorMax0.Size = new System.Drawing.Size(44, 19);
            this.sensorMax0.TabIndex = 16;
            this.sensorMax0.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // sensorNormalized0
            // 
            this.sensorNormalized0.Location = new System.Drawing.Point(210, 46);
            this.sensorNormalized0.Name = "sensorNormalized0";
            this.sensorNormalized0.ReadOnly = true;
            this.sensorNormalized0.Size = new System.Drawing.Size(44, 19);
            this.sensorNormalized0.TabIndex = 17;
            this.sensorNormalized0.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // sensorMin1
            // 
            this.sensorMin1.Location = new System.Drawing.Point(47, 71);
            this.sensorMin1.Name = "sensorMin1";
            this.sensorMin1.ReadOnly = true;
            this.sensorMin1.Size = new System.Drawing.Size(44, 19);
            this.sensorMin1.TabIndex = 18;
            this.sensorMin1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // sensorMin2
            // 
            this.sensorMin2.Location = new System.Drawing.Point(47, 97);
            this.sensorMin2.Name = "sensorMin2";
            this.sensorMin2.ReadOnly = true;
            this.sensorMin2.Size = new System.Drawing.Size(44, 19);
            this.sensorMin2.TabIndex = 19;
            this.sensorMin2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // sensorMax1
            // 
            this.sensorMax1.Location = new System.Drawing.Point(147, 71);
            this.sensorMax1.Name = "sensorMax1";
            this.sensorMax1.ReadOnly = true;
            this.sensorMax1.Size = new System.Drawing.Size(44, 19);
            this.sensorMax1.TabIndex = 20;
            this.sensorMax1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // sensorMax2
            // 
            this.sensorMax2.Location = new System.Drawing.Point(147, 97);
            this.sensorMax2.Name = "sensorMax2";
            this.sensorMax2.ReadOnly = true;
            this.sensorMax2.Size = new System.Drawing.Size(44, 19);
            this.sensorMax2.TabIndex = 21;
            this.sensorMax2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // sensorNormalized1
            // 
            this.sensorNormalized1.Location = new System.Drawing.Point(210, 71);
            this.sensorNormalized1.Name = "sensorNormalized1";
            this.sensorNormalized1.ReadOnly = true;
            this.sensorNormalized1.Size = new System.Drawing.Size(44, 19);
            this.sensorNormalized1.TabIndex = 22;
            this.sensorNormalized1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // sensorNormalized2
            // 
            this.sensorNormalized2.Location = new System.Drawing.Point(210, 97);
            this.sensorNormalized2.Name = "sensorNormalized2";
            this.sensorNormalized2.ReadOnly = true;
            this.sensorNormalized2.Size = new System.Drawing.Size(44, 19);
            this.sensorNormalized2.TabIndex = 23;
            this.sensorNormalized2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(82, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(71, 23);
            this.button1.TabIndex = 24;
            this.button1.Text = "発進!? (&R)";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(382, 329);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.sensorNormalized2);
            this.Controls.Add(this.sensorNormalized1);
            this.Controls.Add(this.sensorMax2);
            this.Controls.Add(this.sensorMax1);
            this.Controls.Add(this.sensorMin2);
            this.Controls.Add(this.sensorMin1);
            this.Controls.Add(this.sensorNormalized0);
            this.Controls.Add(this.sensorMax0);
            this.Controls.Add(this.sensorMin0);
            this.Controls.Add(this.setUpperButton);
            this.Controls.Add(this.setLowerButton);
            this.Controls.Add(this.positionTrackbar);
            this.Controls.Add(this.positionText);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.motorRGroup);
            this.Controls.Add(this.motorLGroup);
            this.Controls.Add(this.connectButton);
            this.Controls.Add(this.sensorValue2);
            this.Controls.Add(this.sensorLabel2);
            this.Controls.Add(this.sensorValue1);
            this.Controls.Add(this.sensorLabel1);
            this.Controls.Add(this.sensorValue0);
            this.Controls.Add(this.sensorLabel0);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "ライントレーサコントロール";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.motorOutputLTrackbar)).EndInit();
            this.motorLGroup.ResumeLayout(false);
            this.motorLGroup.PerformLayout();
            this.motorRGroup.ResumeLayout(false);
            this.motorRGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.motorOutputRTrackbar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.positionTrackbar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label sensorLabel0;
        private System.Windows.Forms.TextBox sensorValue0;
        private System.Windows.Forms.TextBox sensorValue1;
        private System.Windows.Forms.Label sensorLabel1;
        private System.Windows.Forms.TextBox sensorValue2;
        private System.Windows.Forms.Label sensorLabel2;
        private System.Windows.Forms.Button connectButton;
        private System.Windows.Forms.Timer sensorTimer;
        private System.Windows.Forms.TrackBar motorOutputLTrackbar;
        private System.Windows.Forms.GroupBox motorLGroup;
        private System.Windows.Forms.Button brakeLButton;
        private System.Windows.Forms.GroupBox motorRGroup;
        private System.Windows.Forms.Button brakeRButton;
        private System.Windows.Forms.TrackBar motorOutputRTrackbar;
        private System.Windows.Forms.TextBox positionText;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar positionTrackbar;
        private System.Windows.Forms.Button setLowerButton;
        private System.Windows.Forms.Button setUpperButton;
        private System.Windows.Forms.TextBox sensorMin0;
        private System.Windows.Forms.TextBox sensorMax0;
        private System.Windows.Forms.TextBox sensorNormalized0;
        private System.Windows.Forms.TextBox sensorMin1;
        private System.Windows.Forms.TextBox sensorMin2;
        private System.Windows.Forms.TextBox sensorMax1;
        private System.Windows.Forms.TextBox sensorMax2;
        private System.Windows.Forms.TextBox sensorNormalized1;
        private System.Windows.Forms.TextBox sensorNormalized2;
        private System.Windows.Forms.Button button1;
    }
}

