using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalizationFileHelper.Logic.Utils
{
    public static class FileUtils
    {
        public static string ReadJsonFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                return File.ReadAllText(filePath);
            }
            else
            {
                throw new ApplicationException("File does not exist.");
            }
        }

        public static void WriteToFile(string fullPath, string content)
        {
            System.IO.File.WriteAllText(fullPath, content);
        }
    }
}
