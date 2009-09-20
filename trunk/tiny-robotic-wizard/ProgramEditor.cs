using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace tiny_robotic_wizard
{
    delegate void ModifiedChanged(bool modified);

    class ProgramEditor:UserControl
    {
        // 定義画像をとってくる
        private Image definitionImage = Image.FromFile(Application.StartupPath + @"\definition.png");
        // タイル画像のサイズ
        private Size imageSize = new Size(180, 100);
        // 画像のPadding
        private Size padding = new Size(20, 20);
        // 画像のMargin
        private Size margin = new Size(10, 10);

        // クリックされたcontextとそのaction番号
        private List<int> currentContext;
        private int currentAction;

        // Action選択コンテキストメニュー
        private ContextMenuStrip[] actionSelectMenu;

        /// <summary>
        /// ProgramDataが編集されたときのイベント
        /// </summary>
        public event ModifiedChanged ModifiedChanged;

        private bool modified = false;
        /// <summary>
        /// ProgramDataが編集されたか
        /// </summary>
        public bool Modified
        {
            get
            {
                return this.modified;
            }
            set
            {
                this.modified = value;
                if (this.ModifiedChanged != null)
                    this.ModifiedChanged(this.modified);
            }
        }

        // ProgramData
        private ProgramData programData = null;
        public ProgramData ProgramData
        {
            get
            {
                return programData;
            }
            set
            {
                this.programData = value;

                // 編集フラグを落とす
                this.modified = false;

                // コントロールのサイズを決定
                {
                    int columnCount = this.programData.ProgramTemplate.Context.Status.Length + this.programData.ProgramTemplate.Actions.Action.Length;
                    int rowCount = this.programData.Length;

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
                    this.actionSelectMenu = new ContextMenuStrip[this.programData.ProgramTemplate.Actions.Action.Length];
                    for (int i = 0; i <= this.programData.ProgramTemplate.Actions.Action.Length - 1; i++)
                    {
                        this.actionSelectMenu[i] = new ContextMenuStrip();
                        for (int j = 0; j <= this.programData.ProgramTemplate.Actions.Action[i].Procedure.Length - 1; j++)
                        {
                            string text = this.programData.ProgramTemplate.Actions.Action[i].Procedure[j].Caption;
                            Image image = new Bitmap(this.programData.ProgramTemplate.Actions.Action[i].Image[j]);
                            image = image.GetThumbnailImage(90, 50, delegate() { return false; }, IntPtr.Zero);
                            this.actionSelectMenu[i].Items.Add(new ToolStripMenuItem(text, image, new EventHandler(onActionSelectMenuItemClick)));
                            this.actionSelectMenu[i].Items[j].DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
                            this.actionSelectMenu[i].Items[j].ImageScaling = ToolStripItemImageScaling.None;
                            this.actionSelectMenu[i].Items[j].MergeIndex = j;
                        }
                    }
                }

                // タイル描画関数をPaintイベントに追加
                this.Paint += new PaintEventHandler(drawContextAndActions);

                // マウス移動イベント
                this.MouseMove += delegate(object sender, MouseEventArgs e)
                {
                    if (onAction(new Point(e.X, e.Y)))
                    {
                        Cursor.Current = Cursors.Hand;
                    }
                    else
                    {
                        Cursor.Current = Cursors.Arrow;
                    }
                };

                // マウスクリックイベント
                this.MouseClick += delegate(object sender, MouseEventArgs e)
                {
                    Point point = new Point(e.X, e.Y);
                    List<int> context;
                    int action;
                    bool onAction_ = onAction(point, out context, out action);

                    // 左クリック かつ アクションタイル上なら
                    if (e.Button == MouseButtons.Left && onAction_)
                    {
                        currentContext = context;
                        currentAction = action;
                        actionSelectMenu[action].Show((Control)sender, point);
                    }
                };
            }
        }

        public ProgramEditor()
            : base()
        {
        }

        public ProgramEditor(ProgramData programData)
        {
            this.ProgramData = programData;
        }

        // actionSelectMenuのクリックイベント
        private void onActionSelectMenuItemClick(object sender, EventArgs e)
        {
            // 編集フラグをたてる
            this.Modified = true;

            List<int> temporaryActions = new List<int>(this.ProgramData[currentContext]);
            temporaryActions[currentAction] = ((ToolStripMenuItem)sender).MergeIndex;
            this.ProgramData[currentContext] = temporaryActions;
            this.Refresh();
        }

        /// <summary>
        /// タイル描画関数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void drawContextAndActions(object sender, PaintEventArgs e)
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

        /// <summary>
        /// pointにactionタイルが存在するかを判別し，
        /// 存在するなら，そのcontextとactionの番号を参照に収める．
        /// </summary>
        /// <param name="point">任意のPoint</param>
        /// <param name="context">actionタイルが存在する場合はそのcontextが収められる</param>
        /// <param name="action">actionsタイルが存在する場合はそのaction番号が収められる</param>
        /// <returns>actionタイルが存在するならtrue．しないならfalse．</returns>
        private bool onAction(Point point, out List<int> context, out int action)
        {
            if (onAction(point))
            {
                // Marginを消してPaddingを足す(先頭に足したことにする)(計算の簡単化のため)
                point -= this.margin;
                point += this.padding;

                // contextの判別
                {
                    context = new List<int>();

                    // 行番号
                    int rowIndex = point.Y / (this.imageSize.Height + this.padding.Height);

                    context = this.ProgramData.GetContextByRowIndex(rowIndex);
                }

                // actionの判別
                {
                    int pointXOfFirstActionTile = (this.imageSize.Width + this.padding.Width) * this.ProgramData.ProgramTemplate.Context.Status.Length + this.padding.Width + this.definitionImage.Width;
                    // 最初のactionタイルの位置を左端までシフト
                    point.X -= pointXOfFirstActionTile;
                    // action番号を収める
                    action = point.X / (this.imageSize.Width + this.padding.Width);
                }

                return true;
            }
            else
            {
                context = null;
                action = 0;
                return false;
            }
        }

        /// <summary>
        /// pointにactionタイルが存在するかを判別
        /// </summary>
        /// <param name="point">任意のPoint</param>
        /// <returns>actionタイルが存在するならtrue．しないならfalse．</returns>
        private bool onAction(Point point)
        {
            // Marginを消してPaddingを足す(先頭に足したことにする)(計算の簡単化のため)
            point -= this.margin;
            point += this.padding;

            bool column;
            {
                // pointが定義画像より右側だったら
                if ((this.imageSize.Width + this.padding.Width) * this.ProgramData.ProgramTemplate.Context.Status.Length + this.padding.Width + this.definitionImage.Width <= point.X)
                {
                    // 定義画像とそのPaddingの分だけ左にシフト(計算の簡単化のため)
                    point.X += -this.definitionImage.Width - this.padding.Width;

                    int x = point.X % (this.imageSize.Width + this.padding.Width);

                    column = !(x <= this.padding.Width);
                }
                else
                {
                    column = false;
                }
            }

            bool row;
            {
                int y = point.Y % (this.imageSize.Height + this.padding.Height);
                row = !(y <= this.padding.Height);
            }
            return (column && row);
        }
    }
}
