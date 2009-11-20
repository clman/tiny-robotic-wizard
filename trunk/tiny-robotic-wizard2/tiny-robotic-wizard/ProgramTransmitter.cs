using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;
using AvrLib.Image;
using HidBootLib;

namespace tiny_robotic_wizard
{
    class ProgramTransmitter
    {
        public static void Transmit(string programCode)
        {
            WinAvrTranslator translator = new WinAvrTranslator();

            // HEXファイルを生成
            MemoryStream hexStream = new MemoryStream();
            translator.Translate(programCode, hexStream);

            // ブートローダを検索
            String[] targetDevices = HidBoot.Enumerate();
/*          
            if (targetDevices.Length == 0)
            {
                MessageBox.Show("HIDBootデバイスが見つかりませんでした．接続を確認してください．\nユーザーアプリケーションを実行中の場合はリセットボタンを押してください．");
                return;
            }
 */

            using (HidBoot hidBoot = new HidBoot(targetDevices[0]))
            {
                // 転送開始
                SparseImage spm = HexLoader.LoadIntel(hexStream);
                hexStream.Close();
                byte[] prog = spm.ToBlockImage();
                hidBoot.WriteApplication(prog, (int)spm.MinimumAddress, (int)spm.MaximumAddress);
            }
        }
    }
}
