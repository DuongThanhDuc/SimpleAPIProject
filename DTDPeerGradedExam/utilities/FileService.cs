using System.Text.Json;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace utilities
{
    public class FileService
    {
        private readonly string dir;

        public FileService()
        {
            dir = Path.Combine(Directory.GetCurrentDirectory(), "Data");
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
        }

        protected string GetFilePath(string fileName)
        {
            return Path.Combine(dir, fileName);
        }

        protected List<T> LoadFile<T>(string fileName)
        {
            var path = GetFilePath(fileName);
            if (!File.Exists(path))
            {
                return new List<T>();
            }

            var jsonData = File.ReadAllText(path);
            return JsonSerializer.Deserialize<List<T>>(jsonData) ?? new List<T>();
        }

        protected void SaveFile<T>(string fileName, List<T> data)
        {
            var path = GetFilePath(fileName);
            var opt = new JsonSerializerOptions { WriteIndented = true };
            File.WriteAllText(path, JsonSerializer.Serialize(data, opt));
        }
    
    }
}