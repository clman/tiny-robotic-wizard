using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace tiny_robotic_wizard
{
    class FileNameValidator
    {
        public static string ValidFileName(string s)
        {

            string valid = s;
            char[] invalidch = Path.GetInvalidFileNameChars();

            foreach (char c in invalidch)
            {
                valid = valid.Replace(c, '_');
            }
            return valid;
        }
    }
}
