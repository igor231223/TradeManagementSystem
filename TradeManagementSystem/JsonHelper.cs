using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace TradeManagementSystem
{
    public static class JsonHelper
    {
        public static List<T> LoadFromFile<T>(string filePath)
        {
            if (!File.Exists(filePath))
                return new List<T>();

            var json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<T>>(json) ?? new List<T>();
        }

        public static void SaveToFile<T>(string filePath, List<T> items)
        {
            var json = JsonSerializer.Serialize(items, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
        }
    }
}
