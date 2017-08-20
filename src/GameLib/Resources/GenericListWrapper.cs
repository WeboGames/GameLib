using System.Collections.Generic;

namespace GameLib.Resources {
    /// <summary>
    /// This template provides a wrapper for the purposes of deserialization of json text resources 
    /// as a workaround to the Unity JSON parser lack of array support.
    /// </summary>
    /// <typeparam name="T">Serializable type.</typeparam>
    public class GenericListWrapper<T> {
        public List<T> Database;

        public GenericListWrapper()
        {
            Database = new List<T>();
        }
    }
}
