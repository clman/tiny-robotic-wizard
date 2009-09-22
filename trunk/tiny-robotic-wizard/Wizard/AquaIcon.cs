using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace tiny_robotic_wizard.Wizard
{
    public partial class AquaIcon : UserControl
    {
        private Size initialSize = new Size(100, 100);

        ToolTip toolTip = new ToolTip { IsBalloon = true };
        private string hint = "";
        public string Hint
        {
            get
            {
                return this.hint;
            }
            set
            {
                this.hint = value;
                this.toolTip.SetToolTip(this._3dImage, hint);
            }
        }

        public Image Image
        {
            get
            {
                return this.mainImage.Image;
            }
            set
            {
                this.mainImage.Image = value;
            }
        }

        public AquaIcon()
        {
            InitializeComponent();

            // 固定のサイズ
            this.Size = initialSize;

            // 標準のカーソル
            this.Cursor = Cursors.Hand;

            // 親子関係を清算して透過対象を適切にする
            this.Controls.Remove(mainImage);
            this.baseImage.Controls.Add(mainImage);
            this.Controls.Remove(_3dImage);
            this.mainImage.Controls.Add(_3dImage);

            // 初期背景を設定する
            this.baseImage.Image = this.baseImages.Images[0];

            // 丸のリージョンを設定
            {
                var path = new System.Drawing.Drawing2D.GraphicsPath();
                path.StartFigure();
                path.AddEllipse(-1, -1, this.Height + 2, this.Height + 2);
                path.CloseFigure();
                this.Region = new Region(path);
            }

            // マウスポインタが来たら背景色を変える
            this._3dImage.MouseEnter += delegate(object sender, EventArgs e)
            {
                this.baseImage.Image = this.baseImages.Images[1];
            };

            // マウスポインタが離れたら背景色を戻す
            this._3dImage.MouseLeave += delegate(object sender, EventArgs e)
            {
                if (this.Enabled) // ※このifは必要．このコントロールのクリックでEnabledを変更すると，EnableChangedの直後にこのイベントが呼び出されてしまう．
                    this.baseImage.Image = this.baseImages.Images[0];
            };

            // _3dImageが一番上に来ているので，MouseClickイベントを渡してやる
            this._3dImage.MouseClick += delegate(object sender, MouseEventArgs e)
            {
                if(e.Button == MouseButtons.Left)
                    this.OnMouseClick(e);
            };

            // Enableに応じて背景を設定
            this.EnabledChanged += delegate(object sender, EventArgs e)
            {
                this.baseImage.Image = this.baseImages.Images[this.Enabled ? 0 : 2];
            };

            // 固定サイズにする
            this.SizeChanged += delegate(object sender, EventArgs e)
            {
                this.Size = initialSize;
            };
        }
    }
}
