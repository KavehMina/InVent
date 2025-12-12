using InVent.Data.Models;
using System;
using System.IO;
using System.Text.Json;

namespace InVent.Extensions
{
    public static class JsonFileReader
    {
        public static async Task<T> ReadAsync1<T>(string filePath)
        {
            using FileStream stream = File.OpenRead(filePath);
            var res = await JsonSerializer.DeserializeAsync<T>(stream);
            return res;
        }

        public static async Task<T> ReadAsync2<T>(string filePath)
        {
            var json = await File.ReadAllTextAsync(filePath);
            var res = JsonSerializer.Deserialize<T>(json);
            return res;
        }

        public static async Task<T> ReadAsync<T>(string filePath)
        {
            using StreamReader r = new(filePath);
            string json =await r.ReadToEndAsync();
            var res = JsonSerializer.Deserialize<T>(json);
            return res;
        }
        
    }
    
}
