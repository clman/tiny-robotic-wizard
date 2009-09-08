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

        private Size imageSize = new Size(180, 100);

        public ProgramEditor(ProgramData programData)
        {
            this.ProgramData = programData;

            // コントロールの初期化
            {
                int rowCount = this.ProgramData.Length;
                int columnCount = this.ProgramData.ProgramTemplate.Context.Status.Length + this.ProgramData.ProgramTemplate.Actions.Action.Length;
               
                int height = rowCount * imageSize.Height;
                int width = columnCount * imageSize.Width;
               
                this.Size = new Size(width, height);
            }
            // タイル描画関数をPaintイベントに追加
            this.Paint += new PaintEventHandler(drawItems);
        }

        /// <summary>
        /// タイル描画関数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void drawItems(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            int rowCount = 0;

            foreach (KeyValuePair<List<int>, int[]> contextAndActions in this.ProgramData)
            {

                int columnCount = 0;

                int[] context = contextAndActions.Key.ToArray();
                for (int status = 0; status <= context.Length - 1; status++)
                {
                    int matter = context[status];
                    Image image = this.ProgramData.ProgramTemplate.Context.Status[status].Image[matter];
                    Point point = new Point(columnCount * imageSize.Width, rowCount * imageSize.Height);
                    e.Graphics.DrawImage(image, new Rectangle(point, imageSize));
                    columnCount++;
                }

                int[] actions = contextAndActions.Value;
                for (int action = 0; action <= actions.Length - 1; action++)
                {
                    int procedure = actions[action];
                    Image image = this.ProgramData.ProgramTemplate.Actions.Action[action].Image[procedure];
                    Point point = new Point(columnCount * imageSize.Width, rowCount * imageSize.Height);
                    e.Graphics.DrawImage(image, new Rectangle(point, imageSize));
                    columnCount++;
                }
                rowCount++;
            }
        }
    }
}
