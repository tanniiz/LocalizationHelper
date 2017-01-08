using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalizationFileHelper.Logic.Info.LocalizationFileInfo
{
    public class LocalizationFileInfo
    {
        public bool IsOriginal { get; set; }
        public int TotalKeyValue { get; set; }
        public int TotalMissingKeys { get; set; }
        public string FilePath { get; set; }
        public Dictionary<string, string> KeyValues { get; set; }
        public List<string> MissingKey { get; set; }
    }
}
