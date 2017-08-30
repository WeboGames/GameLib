using System.Collections.Generic;
using System.IO;
using GameLib.Persistence;
using NUnit.Framework;

namespace GameLib.Tests.unit.Persistence {
    [TestFixture]
    public class PersistenceTest {
        private IPersistence _persistence;
        private List<TestData> _dataToSave;

        [SetUp]
        public void Setup()
        {
            _dataToSave = new List<TestData>();
            for (var i = 0; i < 10; i++) {
                _dataToSave.Add(new TestData(i, i + 0.5f, i.ToString()));
            }
            SaveFile.SetSavefileExtension("dat");
        }

        [TearDown]
        public void TearDown()
        {
            Directory.Delete(SaveFile.GetSavefileLocation(), true);
        }

        [Test]
        public void TestSaving()
        {
            _persistence = PersistenceFactory.CreatePersistance();
            var saveFile = new SaveFile("test");
            _persistence.Save(_dataToSave, saveFile.GetFullPath());
            Assert.True(File.Exists(saveFile.GetFullPath()));
            var loadedData = _persistence.Load<TestData>(saveFile.GetFullPath());
            Assert.That(loadedData.Count, Is.EqualTo(10));
            Assert.True(_persistence.SaveFilesAvailable());
        }

        [Test]
        public void TestCustomExtension()
        {
            SaveFile.SetSavefileExtension("test");
            _persistence = PersistenceFactory.CreatePersistance();
            var saveFiles = new List<SaveFile>();
            for (var i = 0; i < 10; i++) {
                saveFiles.Add(new SaveFile("save_" + i));
            }
            foreach (var saveFile in saveFiles) {
                _persistence.Save(_dataToSave, saveFile.GetFullPath());
            }
            var loadedFiles = _persistence.GetSaveFiles();
            Assert.That(loadedFiles.Count, Is.EqualTo(10));
        }

        [Test]
        public void TestExclusion()
        {
            var saveFiles = new List<SaveFile>();
            for (var i = 0; i < 10; i++) {
                saveFiles.Add(new SaveFile("save_" + i));
            }
            foreach (var saveFile in saveFiles) {
                _persistence.Save(_dataToSave, saveFile.GetFullPath());
            }
            saveFiles.Clear();
            SaveFile.SetSavefileExtension("test");
            _persistence = PersistenceFactory.CreatePersistance();
            for (var i = 0; i < 10; i++) {
                saveFiles.Add(new SaveFile("save_" + i));
            }
            foreach (var saveFile in saveFiles) {
                _persistence.Save(_dataToSave, saveFile.GetFullPath());
            }
            var loadedFiles = _persistence.GetSaveFiles();
            Assert.That(loadedFiles.Count, Is.EqualTo(10));
        }
    }
}
