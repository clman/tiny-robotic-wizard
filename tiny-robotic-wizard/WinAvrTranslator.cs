using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.Text;
using System.Xml;
using System.Xml.Xsl;
using System.Windows.Forms;

namespace tiny_robotic_wizard
{
    /// <summary>
    /// WinAVRを用いて，対象のプログラムをAVRで実行可能な形式に変換する．
    /// </summary>
    public class WinAvrTranslator
    {
        private Dictionary<string, string> config;

        public WinAvrTranslator()
        {
            this.config = new Dictionary<string, string>();
            loadDefaultConfigurations();
        }

        /// <summary>
        /// 標準の設定を読み込む
        /// </summary>
        private void loadDefaultConfigurations()
        {
            this.config.Clear();
            this.config["WinAvrPath"] = @"WinAVR\bin";
            this.config["Frequency"] = "8000000UL";
            this.config["MCU"] = "atmega88";
            this.config["CFlags"] = "-Wall -gdwarf-2 -std=gnu99 -Os -funsigned-char -funsigned-bitfields -fpack-struct -fshort-enums -MD -MP ";
            this.config["FlashFlags"] = "-O ihex -R .eeprom";
            this.config["EepFlags"] = "-O ihex -j .eeprom --set-section-flags=.eeprom=\"alloc,load\" --change-section-lma .eeprom=0 --no-change-warnings";
            this.config["IncludeDirs"] = "-I\".\"";
            this.config["ListOpts"] = "-h -S";
            this.config["CCompiler"] = "avr-gcc.exe";
            this.config["Make"] = "make.exe";
            this.config["ObjCopy"] = "avr-objcopy.exe";
            this.config["ObjDump"] = "avr-objdump.exe";
        }

        public void Translate(string input, Stream output)
        {
            // WinAVR\binのフルパス
            string winAvrPath = Path.Combine(Application.StartupPath, this.config["WinAvrPath"]);

            // 作業用のディレクトリを作って，そこにCファイルを作る
            DirectoryInfo tempDirectory = Directory.CreateDirectory(Path.Combine(Application.StartupPath, "temp"));
            string tempPath = Path.Combine(tempDirectory.FullName, "program.c");
            StreamWriter program = new StreamWriter(tempPath);
            program.Write(input);
            program.Close();

            // 外部の実行ファイルのエラーメッセージ
            string stdErrorContent;

            // 出力した結果をGCCを用いてコンパイルする．
            string elfFileName = Path.ChangeExtension(tempPath, "elf");
            {
                // 引数の設定
                StringBuilder args = new StringBuilder();
                args.AppendFormat("-o \"{0}\" ", Path.GetFileName(elfFileName));
                args.AppendFormat("-mmcu={0} ", this.config["MCU"]);
                args.AppendFormat("-DF_CPU={0} ", this.config["Frequency"]);
                args.AppendFormat("{0} ", this.config["CFlags"]);
                args.AppendFormat("\"{0}\"", Path.GetFileName(tempPath));

                // コンパイルする
                string ccPath = Path.Combine(winAvrPath, this.config["CCompiler"]);
                if (this.ExecuteExternal(ccPath, tempDirectory.FullName, args.ToString(), out stdErrorContent) != 0)
                    throw new Exception("コンパイル時にエラーが発生しました．" + Environment.NewLine + stdErrorContent);
            }

            // ELFからIntel HEXに変換する．
            string hexFileName = Path.ChangeExtension(tempPath, "hex");
            {
                // 引数の設定
                StringBuilder args = new StringBuilder();
                args.AppendFormat("{0} \"{1}\" \"{2}\"", this.config["FlashFlags"], elfFileName, hexFileName);

                // HEXファイル生成
                string objcopyPath = Path.Combine(winAvrPath, this.config["ObjCopy"]);
                if (this.ExecuteExternal(objcopyPath, tempDirectory.FullName, args.ToString(), out stdErrorContent) != 0)
                    throw new Exception("コンパイル時にエラーが発生しました．" + Environment.NewLine + stdErrorContent);
            }
#if DEBUG
            // リストファイルを生成する．
            string lstFileName = Path.ChangeExtension(tempPath, "lst");
            {
                // 引数の設定
                StringBuilder args = new StringBuilder();
                args.AppendFormat("{0} \"{1}\"", this.config["ListOpts"], elfFileName);

                // リストファイル生成
                string objdumpPath = Path.Combine(winAvrPath, this.config["ObjDump"]);
                string debugList;
                if (this.ExecuteExternal(objdumpPath, tempDirectory.FullName, args.ToString(), out debugList, out stdErrorContent) != 0)
                    throw new Exception("コンパイル時にエラーが発生しました．" + Environment.NewLine + stdErrorContent);

                // ファイルの書き出し
                using (StreamWriter lstWriter = new StreamWriter(lstFileName))
                    lstWriter.Write(debugList);
            }
#endif
            // 結果を出力のストリームにコピーし，元のファイルを削除する．
            {
                // 10k確保すれば足りるだろう
                byte[] buffer = new byte[10480];
                using (FileStream resultFile = File.OpenRead(hexFileName))
                {
                    while (resultFile.CanRead)
                    {
                        int bytesRead = resultFile.Read(buffer, 0, buffer.Length);
                        if (bytesRead == 0) break;
                        output.Write(buffer, 0, bytesRead);
                    }
                }
            }
            // ストリームのポインタを先頭に戻す．
            output.Seek(0, SeekOrigin.Begin);
        }

        private int ExecuteExternal(string path, string workDir, string args, out string error)
        {
            string output;
            return this.ExecuteExternal(path, workDir, args, out output, out error);
        }
        private int ExecuteExternal(string path, string workDir, string args, out string output, out string error)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo(path);
            startInfo.WorkingDirectory = workDir;
            startInfo.Arguments = args;
            startInfo.CreateNoWindow = true;
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;
            Process process = Process.Start(startInfo);

            StringBuilder outputString = new StringBuilder();
            while (!process.HasExited)
            {
                outputString.Append(process.StandardOutput.ReadToEnd());
                Thread.Sleep(1);
            }
            outputString.Append(process.StandardOutput.ReadToEnd());

            error = process.StandardError.ReadToEnd();
            output = outputString.ToString();
            return process.ExitCode;
        }
    }
}
