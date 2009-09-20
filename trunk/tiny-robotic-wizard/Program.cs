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
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
