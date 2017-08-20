namespace GameLib.Persistence {
    public class PersistenceFactory {
        public static IPersistence CreatePersistance()
        {
            return new Persistence();
        } 
    }
}
