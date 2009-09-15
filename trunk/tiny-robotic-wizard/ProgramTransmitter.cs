using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

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
        }
    }
}
