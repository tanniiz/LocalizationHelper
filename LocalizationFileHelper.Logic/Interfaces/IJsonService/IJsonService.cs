using LocalizationFileHelper.Logic.Info.JsonInfo;
using LocalizationFileHelper.Logic.Info.LocalizationFileInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalizationFileHelper.Logic.Interfaces.IJsonService
{
    public interface IJsonService
    {
        void Rearrange(JsonInfo jsonInfo);

        List<LocalizationFileInfo> GetFileInfoes(JsonInfo jsonInfo);
    }
}
