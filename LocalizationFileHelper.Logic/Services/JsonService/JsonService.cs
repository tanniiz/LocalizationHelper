using LocalizationFileHelper.Logic.Info.JsonInfo;
using LocalizationFileHelper.Logic.Interfaces.IJsonService;
using LocalizationFileHelper.Logic.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalizationFileHelper.Logic.Services.JsonService
{
    public class JsonService : IJsonService
    {
        private Dictionary<string, string> ParseJsonToDictionary(string path)
        {
            var fileContent = FileUtils.ReadJsonFile(path);
            JObject jsonContent = JObject.Parse(fileContent);

            return jsonContent.ToObject<Dictionary<string, string>>();
        }

        public void GetAllKeys(JsonInfo jsonInfo)
        {
            
        }

        public void Rearrange(JsonInfo jsonInfo)
        {
            foreach(var filePath in jsonInfo.LocalizationFilePaths)
            {
                var json = ParseJsonToDictionary(filePath).OrderBy(x => x.Key).ToDictionary(x => x.Key, y => y.Value);

                var contentToFile = JsonConvert.SerializeObject(json, Formatting.Indented);
                FileUtils.WriteToFile(filePath, contentToFile);
            }
        }
    }
}
