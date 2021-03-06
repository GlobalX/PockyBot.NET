using System.Collections.Generic;
using System.Threading.Tasks;
using PockyBot.NET.Persistence.Models;

namespace PockyBot.NET.Persistence.Repositories
{
    internal interface IConfigRepository
    {
        IList<GeneralConfig> GetAllGeneralConfig();
        int? GetGeneralConfig(string name);
        Task SetGeneralConfig(string name, int value);
        Task DeleteGeneralConfig(GeneralConfig config);
        Task DeleteGeneralConfig(string name);
        IList<StringConfig> GetAllStringConfig();
        IList<string> GetStringConfig(string name);
        Task AddStringConfig(string name, string value);
        Task DeleteStringConfig(string name, string value);
    }
}
