namespace GameLib.Persistence {
    public class SaveFile : ISaveFile{
        private readonly string _name;
        private readonly string _worldName;
        private readonly string _playerName;

        private static string _savefileLocation = "Saves/";
        private const string SavefileExtension = ".dat";

        /// <summary>
        /// Instantiate a savefile object.
        /// </summary>
        /// <param name="name">Name for associated files.</param>
        public SaveFile(string name)
        {
            _name = name;
            _worldName = _savefileLocation + name + "_world" +  SavefileExtension;
            _playerName = _savefileLocation + name + "_player" + SavefileExtension;
        }

        /// <summary>
        /// Change the savefile location from the defaults Saves/ directory.
        /// </summary>
        /// <param name="savefileLocation">New save file location.</param>
        public static void SetSavefileLocation(string savefileLocation)
        {
            _savefileLocation = savefileLocation;
        }

        /// <summary>
        /// Get currently configured save file location. Default is "Saves/".
        /// </summary>
        /// <returns>Save file location.</returns>
        public static string GetSavefileLocation()
        {
            return _savefileLocation;
        }

        public string GetName()
        {
            return _name;
        }

        public string GetWorldName()
        {
            return _worldName;
        }

        public string GetPlayerName()
        {
            return _playerName;
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
