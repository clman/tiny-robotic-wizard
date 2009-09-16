using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace tiny_robotic_wizard
{
    class ProgramManager
    {
        // プログラムファイルを保存するするディレクトリ
        public string Directory { get; set; }
        public ProgramManager(string directory)
        {
            this.Directory = directory;

            // ディレクトリが存在しないなら作る．
            if (!System.IO.Directory.Exists(this.Directory))
            {
                System.IO.Directory.CreateDirectory(this.Directory);
            }
        }

        /// <summary>
        /// 指定されたディレクトリの中のプログラムファイルを探す
        /// </summary>
        /// <returns>プログラムファイルのパスの配列</returns>
        public string[] getFileList()
        {
            string extension = Properties.Resources.Extension;
            string searchPattern = Path.ChangeExtension("*", extension);
            return System.IO.Directory.GetFiles(Directory, searchPattern, SearchOption.TopDirectoryOnly);
        }

        /// <summary>
        /// Directory内の<paramref name="fileName"/>を開いてProgramDataを復元する
        /// </summary>
        /// <param name="fileName">復元するファイルの名前</param>
        /// <returns>復元されたProgramData</returns>
        public ProgramData Load(string fileName)
        {
            FileStream fs = new FileStream(Path.Combine(Directory, fileName), FileMode.Open, FileAccess.Read);

            BinaryFormatter bf = new BinaryFormatter();

            // ファイルのデシリアライズ化
            ProgramData temp = (ProgramData)(bf.Deserialize(fs));

            fs.Close();

            return temp;
        }

        /// <summary>
        /// 指定されたProgramDataをDirectoryで指定されたディレクトリの中に<paramref name="fileName"/>保存する
        /// </summary>
        /// <param name="programData">保存するProgramData</param>
        /// <param name="fileName">保存するファイル名</param>
        public void Save(ProgramData programData, string fileName)
        {
            // 保存するためのストリームを生成
            FileStream fs = new FileStream(Path.Combine(Directory, fileName), FileMode.OpenOrCreate, FileAccess.Write);

            // ファイルをシリアライズ
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(fs, programData);

            // ストリームを閉じる
            fs.Close();
        }
    }
}
