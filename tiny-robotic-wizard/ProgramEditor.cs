using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace tiny_robotic_wizard
{
    class ProgramEditor:UserControl
    {
        public ProgramData ProgramData{ get; private set; }

        // 定義画像をとってくる
        private Image definitionImage = Image.FromFile(Application.StartupPath + @"\definition.png");
        // タイル画像のサイズ
        private Size imageSize = new Size(180, 100);
        // 画像のPadding
        private Size padding = new Size(20, 20);
        // 画像のMargin
        private Size margin = new Size(5, 20);

        // Action選択コンテキストメニュー
        private ContextMenu[] actionSelectMenu;

        public ProgramEditor(ProgramData programData)
        {
            this.ProgramData = programData;

            // コントロールのサイズを決定
            {
                int columnCount = this.ProgramData.ProgramTemplate.Context.Status.Length + this.ProgramData.ProgramTemplate.Actions.Action.Length;
                int rowCount = this.ProgramData.Length;

                Size size = new Size(0, 0);

                // 画像の分を計算
                size.Width += columnCount * imageSize.Width;
                size.Height += rowCount * imageSize.Height;

                // 定義画像とそのPadding分を計算
                size.Width += definitionImage.Width;
                size.Width += padding.Width;

                // Padding分を計算
                size.Width += (columnCount - 1) * padding.Width;
                size.Height += (rowCount - 1) * padding.Height;

                // Margin分を計算
                size.Width += margin.Width;
                size.Height += margin.Height;

                this.Size = size;
            }

            // Action選択コンテキストメニューを生成
            {
                this.actionSelectMenu = new ContextMenu[this.ProgramData.ProgramTemplate.Actions.Action.Length];
                for (int i = 0; i <= this.ProgramData.ProgramTemplate.Actions.Action.Length - 1; i++)
                {
                    this.actionSelectMenu[i] = new ContextMenu();
                    foreach (Procedure procedure in this.ProgramData.ProgramTemplate.Actions.Action[i].Procedure)
                    {
                        this.actionSelectMenu[i].MenuItems.Add(new MenuItem(procedure.Caption, new EventHandler(onClick)));
                    }
                }
            }

            // タイル描画関数をPaintイベントに追加
            this.Paint += new PaintEventHandler(drawItems);

            // マウス移動イベント
            this.MouseMove += delegate(object sender, MouseEventArgs e)
            {
                if (onAction(new Point(e.X, e.Y)))
                {
                    ((ProgramEditor)sender).Parent.Text = "○";
                    Cursor.Current = Cursors.Hand;
                }
                else
                {
                    ((ProgramEditor)sender).Parent.Text = "×";
                    Cursor.Current = Cursors.Arrow;
                }
                ((ProgramEditor)sender).Parent.Text += " " + Convert.ToString(e.X) + ", " + Convert.ToString(e.Y);

            };

            // マウスクリックイベント
            this.MouseClick += delegate(object sender, MouseEventArgs e)
            {
                // 左クリック かつ アクションタイル上なら
                if (e.Button == MouseButtons.Left && onAction(new Point(e.X, e.Y)))
                {
                    actionSelectMenu[1].Show((Control)sender, new Point(e.X, e.Y));
                }
                else
                {

                }
            };
        }

        // actionSelectMenuのクリックイベント
        private void onClick(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// タイル描画関数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void drawItems(object sender, PaintEventArgs e)
        {
            // 1行ずつ描画
            Point point = new Point(0, 0) + margin;
            foreach (KeyValuePair<List<int>, List<int>> contextAndActions in this.ProgramData)
            {

                // contextの画像を描画
                int[] context = contextAndActions.Key.ToArray();
                for (int status = 0; status <= context.Length - 1; status++)
                {
                    // 画像のインデックス
                    int matter = context[status];
                    
                    // 画像をとってくる
                    Image image = this.ProgramData.ProgramTemplate.Context.Status[status].Image[matter];
                    
                    // 画像を描画
                    e.Graphics.DrawImage(image, new Rectangle(point, imageSize));

                    // 画像の描画位置を更新
                    point.X += imageSize.Width + padding.Width;
                }

                // 定義画像を描画
                {
                    // 画像を描画
                    e.Graphics.DrawImage(definitionImage, new Rectangle(point, definitionImage.Size));

                    // 画像の描画位置を更新
                    point.X += definitionImage.Width + padding.Width;
                }

                // actionsの画像を描画
                List<int> actions = contextAndActions.Value;
                for (int action = 0; action <= actions.ToArray().Length - 1; action++)
                {
                    // 画像のインデックス
                    int procedure = actions[action];

                    // 画像をとってくる
                    Image image = this.ProgramData.ProgramTemplate.Actions.Action[action].Image[procedure];

                    // 画像を描画
                    e.Graphics.DrawImage(image, new Rectangle(point, imageSize));

                    // 画像の描画位置を更新
                    point.X += imageSize.Width + padding.Width;
                }

                // 1行描き終ったのでX方向の描画位置をリセット・Y方向を更新
                point.X = margin.Width;
                point.Y += imageSize.Height + padding.Height;
            }
        }

        private bool onAction(Point point)
        {
            // Marginを消してPaddingを足す(先頭に足したことにする)(計算の簡単化のため)
            point -= this.margin;
            point += this.padding;

            bool column;
            {
                // pointが定義画像より右側だったら
                if ((this.imageSize.Width + this.padding.Width) * this.ProgramData.ProgramTemplate.Context.Status.Length + this.definitionImage.Width <= point.X)
                {
                    // 定義画像とそのPaddingの分だけ左にシフト(計算の簡単化のため)
                    point.X += -this.definitionImage.Width - this.padding.Width;

                    int x = point.X % (this.imageSize.Width + this.padding.Width);
                    if (x <= this.padding.Width)
                    {
                        column = false;
                    }
                    else
                    {
                        column = true;
                    }
                }
                else
                {
                    column = false;
                }
            }

            bool row;
            {
                int y = point.Y % (this.imageSize.Height + this.padding.Height);
                if (y <= this.padding.Height)
                {
                    row = false;
                }
                else
                {
                    row = true;
                }
            }
            return (column && row);
        }
    }
}
