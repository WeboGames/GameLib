using System.IO;
using GameLib.Inventory;
using GameLib.Resources;
using GameLib.test.unit.Resources;
using NUnit.Framework;

namespace GameLib.Tests.unit.Resources {
    [TestFixture]
    public class SerializableTest {
        [Test]
        public void BaseItemDeserializationTest()
        {
            var jsonText = File.ReadAllText("./src/GameLib.Tests/unit/Resources/item_description.json");
            var descriptionList =
                Serializable.LoadDescriptions<Item>(jsonText);
            Assert.That(3, Is.EqualTo(descriptionList.Count));
            Assert.That(1, Is.EqualTo(descriptionList[1].Id));
            Assert.That("description_02", Is.EqualTo(descriptionList[2].Description));
        }
        
        [Test]
        public void ExtendedItemDeserializationTest()
        {
            var jsonText = File.ReadAllText("./src/GameLib.Tests/unit/Resources/extended_item_description.json");
            var descriptionList =
                Serializable.LoadDescriptions<ItemExtended>(jsonText);
            Assert.That(2, Is.EqualTo(descriptionList.Count));
            Assert.That(5, Is.EqualTo(descriptionList[1].Slot));
            Assert.IsNull(descriptionList[0].SecretString);
        }
    }
}
