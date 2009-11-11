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
            this.connectButton = new System.Windows.Forms.Button();
            this.leftWheelGroup = new System.Windows.Forms.GroupBox();
            this.brakeLButton = new System.Windows.Forms.Button();
            this.backwardLButton = new System.Windows.Forms.Button();
            this.forwardLButton = new System.Windows.Forms.Button();
            this.stopLButton = new System.Windows.Forms.Button();
            this.rightWheelGroup = new System.Windows.Forms.GroupBox();
            this.brakeRButton = new System.Windows.Forms.Button();
            this.backwardRButton = new System.Windows.Forms.Button();
            this.forwardRButton = new System.Windows.Forms.Button();
            this.stopRButton = new System.Windows.Forms.Button();
            this.ledGroup = new System.Windows.Forms.GroupBox();
            this.redCheck = new System.Windows.Forms.CheckBox();
            this.greenCheck = new System.Windows.Forms.CheckBox();
            this.blueCheck = new System.Windows.Forms.CheckBox();
            this.sensorGroup = new System.Windows.Forms.GroupBox();
            this.distanceLabel = new System.Windows.Forms.Label();
            this.distanceText = new System.Windows.Forms.TextBox();
            this.lineRCheck = new System.Windows.Forms.CheckBox();
            this.lineLCheck = new System.Windows.Forms.CheckBox();
            this.statusTimer = new System.Windows.Forms.Timer(this.components);
            this.writeButton = new System.Windows.Forms.Button();
            this.leftWheelGroup.SuspendLayout();
            this.rightWheelGroup.SuspendLayout();
            this.ledGroup.SuspendLayout();
            this.sensorGroup.SuspendLayout();
            this.SuspendLayout();
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
            // leftWheelGroup
            // 
            this.leftWheelGroup.Controls.Add(this.brakeLButton);
            this.leftWheelGroup.Controls.Add(this.backwardLButton);
            this.leftWheelGroup.Controls.Add(this.forwardLButton);
            this.leftWheelGroup.Controls.Add(this.stopLButton);
            this.leftWheelGroup.Location = new System.Drawing.Point(12, 110);
            this.leftWheelGroup.Name = "leftWheelGroup";
            this.leftWheelGroup.Size = new System.Drawing.Size(288, 57);
            this.leftWheelGroup.TabIndex = 8;
            this.leftWheelGroup.TabStop = false;
            this.leftWheelGroup.Text = "左車輪";
            // 
            // brakeLButton
            // 
            this.brakeLButton.Location = new System.Drawing.Point(216, 18);
            this.brakeLButton.Name = "brakeLButton";
            this.brakeLButton.Size = new System.Drawing.Size(64, 31);
            this.brakeLButton.TabIndex = 7;
            this.brakeLButton.Text = "ブレーキ";
            this.brakeLButton.UseVisualStyleBackColor = true;
            this.brakeLButton.Click += new System.EventHandler(this.ChangeMotorModeButton_Click);
            // 
            // backwardLButton
            // 
            this.backwardLButton.Location = new System.Drawing.Point(146, 18);
            this.backwardLButton.Name = "backwardLButton";
            this.backwardLButton.Size = new System.Drawing.Size(64, 31);
            this.backwardLButton.TabIndex = 6;
            this.backwardLButton.Text = "逆方向";
            this.backwardLButton.UseVisualStyleBackColor = true;
            this.backwardLButton.Click += new System.EventHandler(this.ChangeMotorModeButton_Click);
            // 
            // forwardLButton
            // 
            this.forwardLButton.Location = new System.Drawing.Point(76, 18);
            this.forwardLButton.Name = "forwardLButton";
            this.forwardLButton.Size = new System.Drawing.Size(64, 31);
            this.forwardLButton.TabIndex = 5;
            this.forwardLButton.Text = "順方向";
            this.forwardLButton.UseVisualStyleBackColor = true;
            this.forwardLButton.Click += new System.EventHandler(this.ChangeMotorModeButton_Click);
            // 
            // stopLButton
            // 
            this.stopLButton.Location = new System.Drawing.Point(6, 18);
            this.stopLButton.Name = "stopLButton";
            this.stopLButton.Size = new System.Drawing.Size(64, 31);
            this.stopLButton.TabIndex = 4;
            this.stopLButton.Text = "停止";
            this.stopLButton.UseVisualStyleBackColor = true;
            this.stopLButton.Click += new System.EventHandler(this.ChangeMotorModeButton_Click);
            // 
            // rightWheelGroup
            // 
            this.rightWheelGroup.Controls.Add(this.brakeRButton);
            this.rightWheelGroup.Controls.Add(this.backwardRButton);
            this.rightWheelGroup.Controls.Add(this.forwardRButton);
            this.rightWheelGroup.Controls.Add(this.stopRButton);
            this.rightWheelGroup.Location = new System.Drawing.Point(12, 173);
            this.rightWheelGroup.Name = "rightWheelGroup";
            this.rightWheelGroup.Size = new System.Drawing.Size(288, 57);
            this.rightWheelGroup.TabIndex = 9;
            this.rightWheelGroup.TabStop = false;
            this.rightWheelGroup.Text = "右車輪";
            // 
            // brakeRButton
            // 
            this.brakeRButton.Location = new System.Drawing.Point(216, 18);
            this.brakeRButton.Name = "brakeRButton";
            this.brakeRButton.Size = new System.Drawing.Size(64, 31);
            this.brakeRButton.TabIndex = 7;
            this.brakeRButton.Text = "ブレーキ";
            this.brakeRButton.UseVisualStyleBackColor = true;
            this.brakeRButton.Click += new System.EventHandler(this.ChangeMotorModeButton_Click);
            // 
            // backwardRButton
            // 
            this.backwardRButton.Location = new System.Drawing.Point(146, 18);
            this.backwardRButton.Name = "backwardRButton";
            this.backwardRButton.Size = new System.Drawing.Size(64, 31);
            this.backwardRButton.TabIndex = 6;
            this.backwardRButton.Text = "逆方向";
            this.backwardRButton.UseVisualStyleBackColor = true;
            this.backwardRButton.Click += new System.EventHandler(this.ChangeMotorModeButton_Click);
            // 
            // forwardRButton
            // 
            this.forwardRButton.Location = new System.Drawing.Point(76, 18);
            this.forwardRButton.Name = "forwardRButton";
            this.forwardRButton.Size = new System.Drawing.Size(64, 31);
            this.forwardRButton.TabIndex = 5;
            this.forwardRButton.Text = "順方向";
            this.forwardRButton.UseVisualStyleBackColor = true;
            this.forwardRButton.Click += new System.EventHandler(this.ChangeMotorModeButton_Click);
            // 
            // stopRButton
            // 
            this.stopRButton.Location = new System.Drawing.Point(6, 18);
            this.stopRButton.Name = "stopRButton";
            this.stopRButton.Size = new System.Drawing.Size(64, 31);
            this.stopRButton.TabIndex = 4;
            this.stopRButton.Text = "停止";
            this.stopRButton.UseVisualStyleBackColor = true;
            this.stopRButton.Click += new System.EventHandler(this.ChangeMotorModeButton_Click);
            // 
            // ledGroup
            // 
            this.ledGroup.Controls.Add(this.blueCheck);
            this.ledGroup.Controls.Add(this.greenCheck);
            this.ledGroup.Controls.Add(this.redCheck);
            this.ledGroup.Location = new System.Drawing.Point(12, 56);
            this.ledGroup.Name = "ledGroup";
            this.ledGroup.Size = new System.Drawing.Size(130, 48);
            this.ledGroup.TabIndex = 10;
            this.ledGroup.TabStop = false;
            this.ledGroup.Text = "LED";
            // 
            // redCheck
            // 
            this.redCheck.AutoSize = true;
            this.redCheck.Location = new System.Drawing.Point(6, 18);
            this.redCheck.Name = "redCheck";
            this.redCheck.Size = new System.Drawing.Size(36, 16);
            this.redCheck.TabIndex = 12;
            this.redCheck.Text = "赤";
            this.redCheck.UseVisualStyleBackColor = true;
            this.redCheck.CheckedChanged += new System.EventHandler(this.redCheck_CheckedChanged);
            // 
            // greenCheck
            // 
            this.greenCheck.AutoSize = true;
            this.greenCheck.Location = new System.Drawing.Point(48, 18);
            this.greenCheck.Name = "greenCheck";
            this.greenCheck.Size = new System.Drawing.Size(36, 16);
            this.greenCheck.TabIndex = 13;
            this.greenCheck.Text = "緑";
            this.greenCheck.UseVisualStyleBackColor = true;
            this.greenCheck.CheckedChanged += new System.EventHandler(this.greenCheck_CheckedChanged);
            // 
            // blueCheck
            // 
            this.blueCheck.AutoSize = true;
            this.blueCheck.Location = new System.Drawing.Point(90, 18);
            this.blueCheck.Name = "blueCheck";
            this.blueCheck.Size = new System.Drawing.Size(36, 16);
            this.blueCheck.TabIndex = 14;
            this.blueCheck.Text = "青";
            this.blueCheck.UseVisualStyleBackColor = true;
            this.blueCheck.CheckedChanged += new System.EventHandler(this.blueCheck_CheckedChanged);
            // 
            // sensorGroup
            // 
            this.sensorGroup.Controls.Add(this.lineRCheck);
            this.sensorGroup.Controls.Add(this.lineLCheck);
            this.sensorGroup.Controls.Add(this.distanceText);
            this.sensorGroup.Controls.Add(this.distanceLabel);
            this.sensorGroup.Location = new System.Drawing.Point(148, 25);
            this.sensorGroup.Name = "sensorGroup";
            this.sensorGroup.Size = new System.Drawing.Size(152, 79);
            this.sensorGroup.TabIndex = 11;
            this.sensorGroup.TabStop = false;
            this.sensorGroup.Text = "センサ";
            // 
            // distanceLabel
            // 
            this.distanceLabel.AutoSize = true;
            this.distanceLabel.Location = new System.Drawing.Point(6, 50);
            this.distanceLabel.Name = "distanceLabel";
            this.distanceLabel.Size = new System.Drawing.Size(29, 12);
            this.distanceLabel.TabIndex = 0;
            this.distanceLabel.Text = "距離";
            // 
            // distanceText
            // 
            this.distanceText.Location = new System.Drawing.Point(41, 47);
            this.distanceText.Name = "distanceText";
            this.distanceText.ReadOnly = true;
            this.distanceText.Size = new System.Drawing.Size(64, 19);
            this.distanceText.TabIndex = 1;
            // 
            // lineRCheck
            // 
            this.lineRCheck.AutoSize = true;
            this.lineRCheck.Enabled = false;
            this.lineRCheck.Location = new System.Drawing.Point(84, 18);
            this.lineRCheck.Name = "lineRCheck";
            this.lineRCheck.Size = new System.Drawing.Size(62, 16);
            this.lineRCheck.TabIndex = 15;
            this.lineRCheck.Text = "ライン右";
            this.lineRCheck.UseVisualStyleBackColor = true;
            // 
            // lineLCheck
            // 
            this.lineLCheck.AutoSize = true;
            this.lineLCheck.Enabled = false;
            this.lineLCheck.Location = new System.Drawing.Point(8, 18);
            this.lineLCheck.Name = "lineLCheck";
            this.lineLCheck.Size = new System.Drawing.Size(62, 16);
            this.lineLCheck.TabIndex = 14;
            this.lineLCheck.Text = "ライン左";
            this.lineLCheck.UseVisualStyleBackColor = true;
            // 
            // statusTimer
            // 
            this.statusTimer.Interval = 200;
            this.statusTimer.Tick += new System.EventHandler(this.statusTimer_Tick);
            // 
            // writeButton
            // 
            this.writeButton.Location = new System.Drawing.Point(78, 12);
            this.writeButton.Name = "writeButton";
            this.writeButton.Size = new System.Drawing.Size(64, 23);
            this.writeButton.TabIndex = 12;
            this.writeButton.Text = "書き込み";
            this.writeButton.UseVisualStyleBackColor = true;
            this.writeButton.Click += new System.EventHandler(this.writeButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(304, 233);
            this.Controls.Add(this.writeButton);
            this.Controls.Add(this.sensorGroup);
            this.Controls.Add(this.ledGroup);
            this.Controls.Add(this.rightWheelGroup);
            this.Controls.Add(this.leftWheelGroup);
            this.Controls.Add(this.connectButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "ライントレーサコントロール";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.leftWheelGroup.ResumeLayout(false);
            this.rightWheelGroup.ResumeLayout(false);
            this.ledGroup.ResumeLayout(false);
            this.ledGroup.PerformLayout();
            this.sensorGroup.ResumeLayout(false);
            this.sensorGroup.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button connectButton;
        private System.Windows.Forms.GroupBox leftWheelGroup;
        private System.Windows.Forms.Button brakeLButton;
        private System.Windows.Forms.Button backwardLButton;
        private System.Windows.Forms.Button forwardLButton;
        private System.Windows.Forms.Button stopLButton;
        private System.Windows.Forms.GroupBox rightWheelGroup;
        private System.Windows.Forms.Button brakeRButton;
        private System.Windows.Forms.Button backwardRButton;
        private System.Windows.Forms.Button forwardRButton;
        private System.Windows.Forms.Button stopRButton;
        private System.Windows.Forms.GroupBox ledGroup;
        private System.Windows.Forms.CheckBox blueCheck;
        private System.Windows.Forms.CheckBox greenCheck;
        private System.Windows.Forms.CheckBox redCheck;
        private System.Windows.Forms.GroupBox sensorGroup;
        private System.Windows.Forms.CheckBox lineRCheck;
        private System.Windows.Forms.CheckBox lineLCheck;
        private System.Windows.Forms.TextBox distanceText;
        private System.Windows.Forms.Label distanceLabel;
        private System.Windows.Forms.Timer statusTimer;
        private System.Windows.Forms.Button writeButton;
    }
}

