using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace assignment_3
{
    public class SaveManager
    {
        private static readonly JsonSerializerOptions _options =
            new()
            {
                PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
                WriteIndented = true,
                PropertyNameCaseInsensitive = true
            };

        public static void SaveToJson<T>(T obj, string path)
        {
            if (obj is null)
                throw new NullReferenceException($"Object can't be saved! It is null.");
            try
            {
                string jsonString = JsonSerializer.Serialize(obj, _options);
                using StreamWriter writer = new(obj.GetType() + path + ".json");
                writer.Write(jsonString);
            }
            catch (Exception e)
            {
                throw new JsonException($"An error occurred while saving to JSON: {e.Message}");
            }
        }

        public static T? LoadFromJson<T>(string filePath)
        {
            try
            {
                using StreamReader reader = new(filePath);
                string jsonString = reader.ReadToEnd();
                T? obj = JsonSerializer.Deserialize<T>(jsonString);

                Console.WriteLine($"Object loaded from {filePath}");
                return obj;
            }
            catch (Exception e)
            {
                throw new JsonException($"An error occurred while loading from JSON: {e.Message}");
            }
        }
    }
}
