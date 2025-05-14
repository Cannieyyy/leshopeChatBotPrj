using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace chatBotPrj
{
    public class MemoryManager
    {
        private readonly string memoryFilePath;

        public MemoryManager()
        {
            string basePath = AppDomain.CurrentDomain.BaseDirectory.Replace("bin\\Debug\\", "");
            memoryFilePath = Path.Combine(basePath, "memory.txt");
        }



        public void SaveData(string key, string value)
        {
            var data = new Dictionary<string, string>();

            if (File.Exists(memoryFilePath))
            {
                foreach (var line in File.ReadAllLines(memoryFilePath))
                {
                    var parts = line.Split('=');
                    if (parts.Length == 2)
                        data[parts[0]] = parts[1];
                }
            }
            else
            {
                Console.WriteLine("file is not found");
            }

            data[key] = value;

            using (StreamWriter writer = new StreamWriter(memoryFilePath))
            {
                foreach (var pair in data)
                {
                    writer.WriteLine($"{pair.Key}={pair.Value}");
                }
            }
        }

        public string LoadData(string key)
        {
            if (!File.Exists(memoryFilePath))
                return null;

            foreach (var line in File.ReadAllLines(memoryFilePath))
            {
                if (line.StartsWith(key + "="))
                {
                    return line.Substring(key.Length + 1);
                }
            }
            return null;
        }

        public void AppendToList(string key, string item)
        {
            var current = LoadData(key);
            var items = new HashSet<string>((current ?? "").Split(','));
            if (!string.IsNullOrWhiteSpace(item))
                items.Add(item);

            SaveData(key, string.Join(",", items));
        }

        public List<string> LoadList(string key)
        {
            var data = LoadData(key);
            if (data == null) return new List<string>();
            return new List<string>(data.Split(','));
        }

    }
}