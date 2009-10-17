using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace tiny_robotic_wizard
{
    static class Program
    {
        /// <summary>
        /// 基本となるフォント
        /// </summary>
        public static readonly System.Drawing.Font BaseFont = new System.Drawing.Font(System.Drawing.FontFamily.GenericSansSerif, 18);

        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            // 2重起動を監視
            {
                bool createdNew;
                //Mutexクラスの作成
                //"MyName"の部分を適当な文字列に変える
                System.Threading.Mutex mutex =
                    new System.Threading.Mutex(true, "MyName", out createdNew);
                if (createdNew == false)
                {
                    //ミューテックスの初期所有権が付与されなかったときは
                    //すでに起動していると判断して終了
                    MessageBox.Show("多重起動はできません。");
                    return;
                }
                //ミューテックスを解放する
                mutex.ReleaseMutex();
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
