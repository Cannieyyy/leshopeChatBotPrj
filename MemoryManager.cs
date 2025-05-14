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


        //method that saves data to a file
        public void SaveData(string key, string value)
        {
            var data = new Dictionary<string, string>();//data is stored using a dictionary

            if (File.Exists(memoryFilePath))
            {
                foreach (var line in File.ReadAllLines(memoryFilePath))
                {
                    var parts = line.Split('=');//lines are stored and separated by = sign
                    if (parts.Length == 2)
                        data[parts[0]] = parts[1];
                }
            }
            else
            {
              // Create the file and close it immediately
                File.Create(memoryFilePath).Close();           
            }

            data[key] = value;

            using (StreamWriter writer = new StreamWriter(memoryFilePath))//streamwriter object to write
            {
                foreach (var pair in data)
                {
                    writer.WriteLine($"{pair.Key}={pair.Value}");
                }
            }
        }

        //method that load data from a file
        public string LoadData(string key)
        {
            if (!File.Exists(memoryFilePath))
                return null;

            foreach (var line in File.ReadAllLines(memoryFilePath))
            {
                if (line.StartsWith(key + "="))//checks if a specific line consists of an equal sign
                {
                    return line.Substring(key.Length + 1);
                }
            }
            return null;
        }

        //a method to store topics in a list format
        public void AppendToList(string key, string item)
        {
            var current = LoadData(key);
            var items = new HashSet<string>((current ?? "").Split(','));//topics are split by a ,
            if (!string.IsNullOrWhiteSpace(item))
                items.Add(item);

            SaveData(key, string.Join(",", items));//saved as a joined topic
        }

        // a method that loads the list from a file
        public List<string> LoadList(string key)
        {
            var data = LoadData(key);
            if (data == null) return new List<string>();
            return new List<string>(data.Split(','));
        }

        //a method that clears the memory file
        public void ClearFile()
        {
            // Overwrite the file with an empty string
            File.WriteAllText(memoryFilePath, string.Empty);
        }

    }
}