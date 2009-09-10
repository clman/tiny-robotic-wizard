using System;
using System.Collections.Generic;
using System.Text;

namespace tiny_robotic_wizard
{
    class ProgramData : IEnumerable<KeyValuePair<List<int>, List<int>>>
    {
        /// <summary>
        /// ProgramTemplate
        /// </summary>
        public ProgramTemplate ProgramTemplate{get; private set;}
        /// <summary>
        /// プログラムデータを格納しておくクラス
        /// </summary>
        private Dictionary<List<int>, List<int>>contextAndActions = new Dictionary<List<int>, List<int>>();

        /// <summary>
        /// matter[statusの番号]に対応するprocedure[actionの番号]を返す
        /// </summary>
        /// <param name="context">matter値の配列</param>
        /// <returns>procedure値の配列</returns>
        public List<int> this[List<int> context]
        {
            get
            {
                return contextAndActions[context];
            }
            private set
            {
                contextAndActions[context] = value;
            }
        }

        /// <summary>
        /// contextとactionsのペアの数
        /// </summary>
        public int Length {
            get
            {
                return contextAndActions.Count;
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
            List<int> actions = new List<int>();
            for (int i = 0; i <= this.ProgramTemplate.Actions.Action.Length - 1; i++)
            {
                actions.Add(0);
            }

            // 初期化用の各Statusの最大値を納めた配列を作る
            List<int> max = new List<int>();
            for (int i = 0; i <= this.ProgramTemplate.Context.Status.Length - 1; i++)
            {
                max.Add(programTemplate.Context.Status[i].Matter.Length - 1);
            }

            // 初期化用のContextを作る
            List<int> context = new List<int>();
            for (int i = 0; i <= this.ProgramTemplate.Context.Status.Length - 1; i++)
            {
                context.Add(0);
            }

            // すべてのContextについて初期化されたActionsを設定
            for (bool flag = false; !flag; )
            {
                {
                    // contextは操作し続けるので，Dictionaryのキーに参照渡しするのはだめ．
                    // クローンをつくる．
                    List<int> contextClone = new List<int>(context);
                    // Dictionaryに追加する
                    contextAndActions.Add(contextClone, actions);
                }

                int index = 0;
                while (true)
                {
                    if (context[index] != max[index])
                    {
                        context[index]++;
                        break;
                    }
                    else
                    {
                        if (index != context.ToArray().Length - 1)
                        {
                            context[index] = 0;
                            index++;
                        }
                        else
                        {
                            flag = true;
                            break;
                        }
                    }
                }
            }
        }

        #region IEnumerable<KeyValuePair<List<int>,int[]>> メンバ

        IEnumerator<KeyValuePair<List<int>, List<int>>> IEnumerable<KeyValuePair<List<int>, List<int>>>.GetEnumerator()
        {
            return contextAndActions.GetEnumerator();
        }

        #endregion

        #region IEnumerable メンバ

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return contextAndActions.GetEnumerator();
        }

        #endregion
    }
}
