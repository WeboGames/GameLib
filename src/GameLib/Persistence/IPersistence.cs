using System.Collections.Generic;

namespace GameLib.Persistence {
    /// <summary>
    /// This interface defines the behavior necessary to keep track of all the environment variables
    /// that need to be persisted. Because it is a centralized repository it will be a singleton.
    /// </summary>
    public interface IPersistence {

        /// <summary>
        /// Saves the given list of data to disk. T must be serializable.
        /// </summary>
        /// <param name="dataToSave">List of serializable objects to save.</param>
        /// <param name="fileName">File name to save to.</param>
        void Save<T>(List<T> dataToSave, string fileName);

        /// <summary>
        /// Loads specific save file from disk.
        /// </summary>
        /// <param name="fileName">File name to load from.</param>
        /// <returns>List of deserialized data.</returns>
        List<T> Load<T>(string fileName);

        /// <summary>
        /// Get list of save files from disk.
        /// </summary>
        List<SaveFile> GetSaveFiles();

        /// <summary>
        /// Check whether there are savefiles present.
        /// </summary>
        /// <returns>True if present. False if not.</returns>
        bool SaveFilesAvailable();
    }
}
