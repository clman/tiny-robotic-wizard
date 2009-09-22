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
        /// <summary>
        /// ファイル名がユニークかどうかを調べる
        /// </summary>
        /// <param name="fileName">調べたいファイル名</param>
        /// <returns>ユニークならtrue</returns>
        public bool IsUnique(string fileName)
        {
            foreach (string existing in this.GetFileList())
            {
                if (existing == fileName)
                {
                    return false;
                }
            }
            return true;
        }

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
        public string[] GetFileList()
        {
            string[] tempPath;
            string extension = Properties.Resources.Extension;
            string searchPattern = Path.ChangeExtension("*", extension);

            // ディレクトリ中のファイルの一覧を取得
            tempPath = System.IO.Directory.GetFiles(Directory, searchPattern, SearchOption.TopDirectoryOnly);
            // フルパスになっているので，ファイル名のみを抽出
            for (int i = 0; i <= tempPath.Length - 1; i++)
            {
                tempPath[i] = Path.GetFileName(tempPath[i]);
            }
            return tempPath;
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

        public void Delete(string fileName)
        {
            File.Delete(Path.Combine(Directory, fileName));
        }
    }
}
