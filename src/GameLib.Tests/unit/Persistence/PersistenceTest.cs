using System.Collections.Generic;
using System.IO;
using GameLib.Inventory;
using GameLib.Persistence;
using NUnit.Framework;

namespace GameLib.Tests.unit.Persistence {
    [TestFixture]
    public class PersistenceTest {
        
        [Test]
        public void TestSaveFile()
        {
            var persistence = PersistenceFactory.CreatePersistance();
            var dataToSave = new List<Item>();
            for (var i = 0; i < 10; i++) {
                dataToSave.Add(new Item(i, "name_" + i, i * 10, i, (Item.ItemRarity) (i % 4),
                    i * 5.0f,
                    "description_" + i, (Item.OwnershipType) (i % 3), i % 2 == 0));
            }
            var saveFile = new SaveFile("test");
            persistence.Save(dataToSave, saveFile.GetWorldName());
            Assert.True(File.Exists(saveFile.GetWorldName()));
            var loadedData = persistence.Load<Item>(saveFile.GetWorldName());
            Assert.That(10, Is.EqualTo(loadedData.Count));
            Assert.True(persistence.SaveFilesAvailable());
            File.Delete(saveFile.GetWorldName());
        }
    }
}
