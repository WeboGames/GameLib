using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using Newtonsoft.Json;

namespace GameLib.Resources {
    [Serializable]
    public class Serializable {
        /// <summary>
        /// Object Id.
        /// </summary>
        public int Id;

        /// <summary>
        /// Object name.
        /// </summary>
        public string Name;

        /// <summary>
        /// Object description.
        /// </summary>
        public string Description;

        /// <summary>
        /// Returns a copy of the given object.
        /// </summary>
        /// <param name="source">Object to copy.</param>
        /// <typeparam name="T">Type.</typeparam>
        /// <returns>Copy.</returns>
        public static T Clone<T>(T source)
        {
            using (var memoryStream = new MemoryStream()) {
                var binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(memoryStream, source);
                memoryStream.Position = 0;
                return (T) binaryFormatter.Deserialize(memoryStream);
            }
        }

        /// <summary>
        /// Loads descriptions from resources as long as they can be serialized.
        /// </summary>
        /// <param name="jsonString">Json String to parse.</param>
        /// <returns>List with descriptions.</returns>
        public static List<T> LoadDescriptions<T>(string jsonString) where T : new()
        {
            return jsonString.Length == 0
                ? new List<T>()
                : JsonConvert.DeserializeObject<List<T>>(jsonString);
        }

        /// <summary>
        /// Loads descriptions from resources as long as they can be serialized.
        /// </summary>
        /// <param name="jsonString">Json String to parse.</param>
        /// <returns>Dictionary with descriptions.</returns>
        public static Dictionary<uint, T> LoadDescriptionMap<T>(string jsonString) where T : new()
        {
            return jsonString.Length == 0
                ? new Dictionary<uint, T>()
                : JsonConvert.DeserializeObject<Dictionary<uint, T>>(jsonString);
        }

        /// <summary>
        /// Loads a single object description from resources.
        /// </summary>
        /// <param name="jsonString">Json String to parse.</param>
        /// <typeparam name="T"></typeparam>
        /// <returns>Description.</returns>
        public static T LoadDescription<T>(string jsonString) where T : new()
        {
            return jsonString.Length == 0
                ? new T()
                : JsonConvert.DeserializeObject<T>(jsonString);
        }
    }
}
