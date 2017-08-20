using System.IO;
using GameLib.Resources;
using NUnit.Framework;

namespace GameLib.test.unit.Resources {
    [TestFixture]
    public class SerializableTest {
        [Test]
        public void DeserializationTest()
        {
            var jsonText = File.ReadAllText("./src/GameLib.Tests/unit/Resources/test_description.json");
            var descriptionList =
                Serializable.LoadDescriptions<Description>(jsonText);
            Assert.That(3, Is.EqualTo(descriptionList.Count));
            Assert.That(1, Is.EqualTo(descriptionList[1].Id));
            Assert.That("string_02", Is.EqualTo(descriptionList[2].SomeString));
        }
    }
}
