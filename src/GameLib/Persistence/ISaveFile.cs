namespace GameLib.Persistence {
    /// <summary>
    /// SaveFile stores the information related to a game state for persistence.
    /// </summary>
    public interface ISaveFile {
        /// <summary>
        /// Get associated players savefile name.
        /// </summary>
        /// <returns>Name.</returns>
        string GetFullPath();
        
        /// <summary>
        /// Get save file name.
        /// </summary>
        /// <returns>Name.</returns>
        string GetName();
    }
}
