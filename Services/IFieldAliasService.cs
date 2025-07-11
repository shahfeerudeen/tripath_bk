using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace tripath.Services
{
    public interface IFieldAliasService
    {

        string GetField(string collectionName, string alias);
    }

    public class FieldAliasService : IFieldAliasService
    {
        private readonly Dictionary<string, Dictionary<string, string>> _fieldMap;

        public FieldAliasService(IWebHostEnvironment env)
        {
            var path = Path.Combine(env.ContentRootPath, "Config", "field-aliases.json");
            if (!File.Exists(path))
                throw new FileNotFoundException("Alias config file not found", path);

            var json = File.ReadAllText(path);
            _fieldMap = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, string>>>(json)
                        ?? new();
        }

        public string GetField(string collectionName, string alias)
        {
            if (_fieldMap.TryGetValue(collectionName, out var aliasMap) &&
                aliasMap.TryGetValue(alias, out var realField))
            {
                return realField;
            }

            return alias;
        }
    }
}