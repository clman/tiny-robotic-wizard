using System;
using System.Collections.Generic;
using System.Text;

namespace tiny_robotic_wizard
{
    class ProgramData
    {
        /// <summary>
        /// ProgramTemplate
        /// </summary>
        public ProgramTemplate ProgramTemplate{get; private set;}
        /// <summary>
        /// プログラムデータを格納しておくクラス
        /// </summary>
        private Dictionary<List<int>, int[]>contextAndActions = new Dictionary<List<int>, int[]>();

        /// <summary>
        /// matter[statusの番号]に対応するprocedure[actionの番号]を返す
        /// </summary>
        /// <param name="context">matter値の配列</param>
        /// <returns>procedure値の配列</returns>
        public int[] this[int[] context]
        {
            get
            {
                List<int> temp = new List<int>();
                foreach (int status in context)
                {
                    temp.Add(status);
                }
                return contextAndActions[temp];
            }
            private set
            {
                List<int> temp = new List<int>();
                foreach (int status in context)
                {
                    temp.Add(status);
                }
                contextAndActions.Add(temp, value);
            }
        }

        /// <summary>
        /// ProgramTemplateを引数にProgramDataのインスタンスを生成
        /// </summary>
        /// <param name="programTemplate">ProgramTemplate(設定後は変更不可)</param>
        public ProgramData(ProgramTemplate programTemplate)
        {
            this.ProgramTemplate = programTemplate;

            // デフォルト値のActionsを作る
            int[] actions = new int[this.ProgramTemplate.Actions.Action.Length];
            for (int i = 0; i <= actions.Length - 1; i++)
            {
                actions[i] = 0;
            }

            // 初期化用の各Statusの最大値を納めた配列を作る
            int[] max = new int[this.ProgramTemplate.Context.Status.Length];
            for (int i = 0; i <= max.Length - 1; i++)
            {
                max[i] = programTemplate.Context.Status[i].Matter.Length - 1;
            }

            // 初期化用のContextを作る
            int[] context = new int[this.ProgramTemplate.Context.Status.Length];
            for (int i = 0; i <= context.Length - 1; i++)
            {
                context[i] = 0;
            }

            // すべてのContextについて初期化されたActionsを設定
            for (bool flag = true; flag; )
            {
                List<int> temp = new List<int>();
                foreach (int status in context)
                {
                    temp.Add(status);
                }

                contextAndActions.Add(temp, actions);

                int index = 0;
                while(true)
                {
                    if (context[index] != max[index])
                    {
                        context[index]++;
                        break;
                    }
                    else
                    {
                        if (index != context.Length - 1)
                        {
                            context[index] = 0;
                            index++;
                        }
                        else
                        {
                            flag = false;
                            break;
                        }
                    }
                }
            }
        }
    }
}
