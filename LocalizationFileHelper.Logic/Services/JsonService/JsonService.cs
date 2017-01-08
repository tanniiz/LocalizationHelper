using LocalizationFileHelper.Logic.Info.JsonInfo;
using LocalizationFileHelper.Logic.Info.LocalizationFileInfo;
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

        public void Rearrange(JsonInfo jsonInfo)
        {
            foreach(var filePath in jsonInfo.LocalizationFilePaths)
            {
                var json = ParseJsonToDictionary(filePath).OrderBy(x => x.Key).ToDictionary(x => x.Key, y => y.Value);

                var contentToFile = JsonConvert.SerializeObject(json, Formatting.Indented);
                FileUtils.WriteToFile(filePath, contentToFile);
            }
        }

        public List<LocalizationFileInfo> GetFileInfoes(JsonInfo jsonInfo)
        {
            List<LocalizationFileInfo> infoes = new List<LocalizationFileInfo>();
            foreach (var json in jsonInfo.LocalizationFilePaths)
            {
                var keyValues = ParseJsonToDictionary(json);
                var missingKey = FindMissingKey(jsonInfo.OriginalFilePath, json);
                infoes.Add(new LocalizationFileInfo
                {
                    IsOriginal = json == jsonInfo.OriginalFilePath,
                    KeyValues = keyValues,
                    TotalKeyValue = keyValues.Count,
                    MissingKey = missingKey,
                    TotalMissingKeys = missingKey.Count,
                    FilePath = json
                });
            }

            return infoes;
        }

        private List<string> FindMissingKey(string originalPathFile, string filePathToCompare)
        {
            var original = ParseJsonToDictionary(originalPathFile);
            var toBeCompared = ParseJsonToDictionary(filePathToCompare);

            return original.Where(x => !toBeCompared.ContainsKey(x.Key)).Select(x => x.Key).ToList();
        }
    }
}
