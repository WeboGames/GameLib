using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;

namespace GameLib.Persistence {
    public class Persistence : IPersistence {
        private readonly object _lock = new object();
        private readonly Regex _saveFileRegex;

        public Persistence()
        {
            _saveFileRegex = new Regex("\\." + SaveFile.GetSavefileExtension() + "$");
        }

        public void Save<T>(List<T> dataToSave, string fileName)
        {
            lock (_lock) {
                if (!Directory.Exists(SaveFile.GetSavefileLocation())) {
                    Directory.CreateDirectory(SaveFile.GetSavefileLocation());
                }
                var binaryFormatter = new BinaryFormatter();
                var saveFile = File.Open(fileName, FileMode.Create);
                binaryFormatter.Serialize(saveFile, dataToSave);
                saveFile.Close();
            }
        }

        public List<T> Load<T>(string fileName)
        {
            lock (_lock) {
                var binaryFormatter = new BinaryFormatter();
                var saveFile = File.OpenRead(fileName);
                var list = (List<T>) binaryFormatter.Deserialize(saveFile);
                saveFile.Close();
                return list;
            }
        }

        public List<SaveFile> GetSaveFiles()
        {
            var saveFiles = Directory.GetFiles(SaveFile.GetSavefileLocation());
            saveFiles.OrderBy(s => s).ToList();
            if (saveFiles.Length == 0) return null;
            var saveFileNames = new List<SaveFile>();
            foreach (var fileName in saveFiles) {
                if (!_saveFileRegex.IsMatch(fileName)) continue;
                var saveFileName = _saveFileRegex.Split(fileName);
                if (string.IsNullOrEmpty(saveFileName[0])) continue;
                var saveFile =
                    new SaveFile(saveFileName[0].Substring(SaveFile.GetSavefileLocation().Length));
                if (!saveFileNames.Contains(saveFile)) {
                    saveFileNames.Add(saveFile);
                }
            }
            return saveFileNames;
        }

        public bool SaveFilesAvailable()
        {
            var saveFiles = Directory.GetFiles(SaveFile.GetSavefileLocation());
            // TODO: verify if the files found really conform to format.
            return saveFiles.Length != 0;
        }
    }
}
