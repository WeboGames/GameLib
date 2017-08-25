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
        /// Retrieve an element from a ItemSerializable list based on a given Serializable's Id.
        /// </summary>
        /// <param name="item">Base Item to find.</param>
        /// <param name="itemSerializableList">List to find the item in.</param>
        /// <typeparam name="T">ItemSerializable type.</typeparam>
        /// <returns>Matching ItemSerializable</returns>
        public static T FindItem<T>(Serializable item, IList<T> itemSerializableList)
            where T : ItemSerializable
        {
            var getItemById = from relatedItem in itemSerializableList
                where relatedItem.ItemId == item.Id
                select relatedItem;
            return getItemById.FirstOrDefault();
        }

        /// <summary>
        /// Get instance from database list.
        /// </summary>
        /// <param name="list">Database list.</param>
        /// <param name="id">Id of element to extract.</param>
        /// <typeparam name="T">Type.</typeparam>
        /// <returns>Element.</returns>
        public static T GetFromList<T>(IList<T> list, int id)
        {
            return list[id];
        }

        /// <summary>
        /// Loads descriptions from resources as long as they can be wrapped by a GenericListWrapper.
        /// </summary>
        /// <param name="jsonString">Location of the descriptions.</param>
        /// <returns>List with descriptions.</returns>
        public static List<T> LoadDescriptions<T>(string jsonString)
        {
            return jsonString.Length == 0 
                ? new List<T>()
                : JsonConvert.DeserializeObject<List<T>>(jsonString);
        }
    }
}
