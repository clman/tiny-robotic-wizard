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
        private Size imagePadding = new Size(10, 10);
        private int actionImageMargin = 50;

        public ProgramEditor(ProgramData programData)
        {
            this.ProgramData = programData;

            // コントロールの初期化
            {
                int rowCount = this.ProgramData.ProgramTemplate.Context.Status.Length;
                int columnCount = this.ProgramData.ProgramTemplate.Context.Status.Length + this.ProgramData.ProgramTemplate.Actions.Action.Length;
               
                int height = rowCount * 100;
                int width = columnCount * 180;
               
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
            Size offset = new Size(0, 0); ;
            int rowCount = 0;

            foreach (KeyValuePair<List<int>, int[]> contextAndActions in this.ProgramData)
            {

                int columnCount = 0;

                offset = new Size(0, 0);

                int[] context = contextAndActions.Key.ToArray();
                for (int status = 0; status <= context.Length - 1; status++)
                {
                    int matter = context[status];
                    Image image = this.ProgramData.ProgramTemplate.Context.Status[status].Image[matter];
                    Point point = new Point(columnCount * 180, rowCount * 100) + offset;
                    columnCount++;
                    e.Graphics.DrawImage(image, point);
                }

                offset = new Size(0, 0);

                int[] actions = contextAndActions.Value;
                for (int action = 0; action <= actions.Length - 1; action++)
                {
                    int procedure = actions[action];
                    Image image = this.ProgramData.ProgramTemplate.Actions.Action[action].Image[procedure];
                    Point point = new Point(columnCount * 180, rowCount * 100) + offset;
                    columnCount++;
                    e.Graphics.DrawImage(image, point);
                }
                rowCount++;
            }
        }
    }
}
