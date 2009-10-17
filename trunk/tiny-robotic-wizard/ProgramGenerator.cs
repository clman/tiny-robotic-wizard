using System;
using System.Collections.Generic;
using System.Text;

namespace tiny_robotic_wizard
{
    class ProgramGenerator
    {
        public string ProgramCode { get; private set; }
        public ProgramData ProgramData { get; private set; }
        public ProgramGenerator(ProgramData programData)
        {
            this.ProgramData = programData;

            // コードの生成
            {
                this.ProgramCode = "";

                // 基本的なヘッダファイルをinclude
                this.ProgramCode += "#include <avr/io.h>\r\n";

                // statusのコードをすべて挿入
                foreach (Status status in this.ProgramData.ProgramTemplate.Context.Status)
                {
                    this.ProgramCode += "/*" + status.Caption + "*/\r\n";
                    this.ProgramCode += status.Code;
                    this.ProgramCode += "\r\n";
                }

                // actionのコードをすべて挿入
                foreach (Action action in this.ProgramData.ProgramTemplate.Actions.Action)
                {
                    this.ProgramCode += "/*" + action.Caption + "*/\r\n";
                    this.ProgramCode += action.Code;
                this.ProgramCode += "\r\n";
                }


                // main関数始まり
                this.ProgramCode += "int main(void){\r\n";

                // 作業用変数wの宣言・初期化
                ProgramCode += "int w = 0;\r\n";

                // actionの関数ポインタを収める変数の宣言
                this.ProgramCode += "void (*action";
                foreach (Status status in this.ProgramData.ProgramTemplate.Context.Status)
                {
                    this.ProgramCode += "[" + Convert.ToString(status.Matter.Length) + "]";
                }
                this.ProgramCode += "[" + Convert.ToString(this.ProgramData.ProgramTemplate.Actions.Action.Length) + "]";
                this.ProgramCode += ")(void);\r\n";

                // status初期化関数を挿入
                this.ProgramCode += "/* statusの初期化 */\r\n";
                foreach (Status status in this.ProgramData.ProgramTemplate.Context.Status)
                {
                    this.ProgramCode += "initialize_" + status.Name + "();\r\n";
                }

                // actions初期化関数を挿入
                this.ProgramCode += "/* actionの初期化 */\r\n";
                foreach (Action action in this.ProgramData.ProgramTemplate.Actions.Action)
                {
                    this.ProgramCode += "initialize_" + action.Name + "();\r\n";
                }

                // actionの関数ポインタの初期化
                this.ProgramCode += "/* actionの関数ポインタの初期化 */\r\n";
                foreach (KeyValuePair<List<int>, List<int>> contextAndActions in this.ProgramData)
                {
                    int actionIndex = 0;
                    foreach (int action in contextAndActions.Value)
                    {
                        ProgramCode += "action";
                        foreach (int status in contextAndActions.Key)
                        {
                            ProgramCode += "[" + Convert.ToString(status) + "]";
                        }
                        ProgramCode += "[" + Convert.ToString(actionIndex) + "] = ";
                        
                        ProgramCode += this.ProgramData.ProgramTemplate.Actions.Action[actionIndex].Name + "[" + Convert.ToString(action) + "];\r\n";

                        actionIndex++;
                    }
                }

                // while始まり
                this.ProgramCode += "while(1){\r\n";

                // status更新関数を挿入
                this.ProgramCode += "/* statusの初期化 */\r\n";
                foreach (Status status in this.ProgramData.ProgramTemplate.Context.Status)
                {
                    this.ProgramCode += "update_" + status.Name + "();\r\n";
                }

                // for始まり 関数の実行
                this.ProgramCode += "for(w = 0; w <= " + Convert.ToString(this.ProgramData.ProgramTemplate.Actions.Action.Length - 1) + "; w++){\r\n";

                // actionの関数ポインタから処理を実行
                this.ProgramCode += "(*action";
                foreach (Status status in programData.ProgramTemplate.Context.Status)
                {
                    this.ProgramCode += "[" + status.Name + "]";
                }
                this.ProgramCode += "[w])();\r\n";

                // for終わり
                this.ProgramCode += "}\r\n";

                // while終わり
                this.ProgramCode += "}\r\n";

                // main関数のreturn
                this.ProgramCode += "return 0;\r\n";

                // main関数の終わり
                this.ProgramCode += "}\r\n";
            }
        }
    }
}
