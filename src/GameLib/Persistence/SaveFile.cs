namespace GameLib.Persistence {
    public class SaveFile : ISaveFile {
        private readonly string _name;
        private readonly string _fullPath;

        private static string _savefileLocation = "Saves/";
        private static string _savefileExtension = "dat";

        /// <summary>
        /// Instantiate a savefile object.
        /// </summary>
        /// <param name="name">Name for associated files.</param>
        public SaveFile(string name)
        {
            _name = name;
            _fullPath = _savefileLocation + _name + "." + _savefileExtension;
        }

        public string GetFullPath()
        {
            return _fullPath;
        }

        public string GetName()
        {
            return _name;
        }

        /// <summary>
        /// Get the currently configured savefile extension.
        /// </summary>
        /// <returns>Extension without . (dot).</returns>
        public static string GetSavefileExtension()
        {
            return _savefileExtension;
        }

        /// <summary>
        /// Get the currently configured savefile location.
        /// </summary>
        /// <returns>Location with / (slash).</returns>
        public static string GetSavefileLocation()
        {
            return _savefileLocation;
        }

        /// <summary>
        /// Set a new savefile extension.
        /// </summary>
        /// <param name="savefileExtension">New extension.</param>
        public static void SetSavefileExtension(string savefileExtension)
        {
            _savefileExtension = savefileExtension.StartsWith(".")
                ? savefileExtension.Substring(1)
                : savefileExtension;
        }

        /// <summary>
        /// Set a new savefile location.
        /// </summary>
        /// <param name="savefileLocation">New location.</param>
        public static void SetSavefileLocation(string savefileLocation)
        {
            _savefileLocation = savefileLocation;
            if (!_savefileLocation.EndsWith("/")) {
                _savefileLocation += "/";
            }
        }

        public override bool Equals(object obj)
        {
            var item = obj as SaveFile;
            return item != null && _name.Equals(item.GetName());
        }

        public override int GetHashCode()
        {
            return _name.GetHashCode();
        }
    }
}
