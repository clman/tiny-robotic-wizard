using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace tiny_robotic_wizard.Wizard
{
    public partial class AquaButton : UserControl
    {
        private Size initialSize;

        // http://msdn.microsoft.com/ja-jp/library/y7k5hkke.aspx
        [BrowsableAttribute(true)]
        [BindableAttribute(true)]
        public string Caption
        {
            get { return this.label.Text; }
            set { this.label.Text = value; }
        }

        [BrowsableAttribute(true)]
        [BindableAttribute(true)]
        public override Font Font
        {
            get { return this.label.Font; }
            set { this.label.Font = value; }
        }

        public AquaButton()
        {
            InitializeComponent();

            // 固定のサイズ
            this.initialSize = new Size(240, 100);
            this.Size = initialSize;

            // 標準のカーソル
            this.Cursor = Cursors.Hand;

            // 初期背景を設定
            this.label.Image = baseImages.Images[0];

            // _3dImageの親をlabelにしてTransparentを有効にする
            this.Controls.Remove(this._3dImage);
            this.label.Controls.Add(this._3dImage);

            // 固定サイズにする
            this.SizeChanged += delegate(object sender, EventArgs e)
            {
                this.Size = initialSize;
            };

            // マウスポインタが来たら背景色を変える
            this._3dImage.MouseEnter += delegate(object sender, EventArgs e)
            {
                this.label.Image = baseImages.Images[1];
            };

            // マウスポインタが離れたら背景色を戻す
            this._3dImage.MouseLeave += delegate(object sender, EventArgs e)
            {
                this.label.Image = baseImages.Images[0];
            };

            // 角丸のリージョンを設定
            // thanks : http://programming-nikki.seesaa.net/article/119656486.html
            {
                Rectangle rect = new Rectangle(-1, -1, this.Width + 2, this.Height + 2);
                int radius = this.Height / 2;

                var path = new System.Drawing.Drawing2D.GraphicsPath();

                path.StartFigure();

                // 左上の角丸
                path.AddArc(rect.Left, rect.Top,
                    radius * 2, radius * 2,
                    180, 90);
                // 上の線
                path.AddLine(rect.Left + radius, rect.Top,
                    rect.Right - radius, rect.Top);
                // 右上の角丸
                path.AddArc(rect.Right - radius * 2, rect.Top,
                    radius * 2, radius * 2,
                    270, 90);
                // 右の線
                path.AddLine(rect.Right, rect.Top + radius,
                    rect.Right, rect.Bottom - radius);
                // 右下の角丸
                path.AddArc(rect.Right - radius * 2, rect.Bottom - radius * 2,
                    radius * 2, radius * 2,
                    0, 90);
                // 下の線
                path.AddLine(rect.Right - radius, rect.Bottom,
                    rect.Left + radius, rect.Bottom);
                // 左下の角丸
                path.AddArc(rect.Left, rect.Bottom - radius * 2,
                    radius * 2, radius * 2,
                    90, 90);
                // 左の線
                path.AddLine(rect.Left, rect.Bottom - radius,
                    rect.Left, rect.Top + radius);

                path.CloseFigure();

                this.Region = new Region(path);
            }
        }
    }
}
