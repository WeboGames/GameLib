using System.Diagnostics;
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
            var jsonText =
                File.ReadAllText("./src/GameLib.Tests/unit/Resources/item.json");
            var item =
                Serializable.LoadDescription<Item>(jsonText);
            Assert.That(item.Price, Is.EqualTo(100));
            Assert.That(item.Id, Is.EqualTo(0));
            Assert.That(item.Description, Is.EqualTo("description_00"));
        }

        [Test]
        public void BaseItemListDeserializationTest()
        {
            var jsonText =
                File.ReadAllText("./src/GameLib.Tests/unit/Resources/item_description.json");
            var descriptionList =
                Serializable.LoadDescriptions<Item>(jsonText);
            Assert.That(3, Is.EqualTo(descriptionList.Count));
            Assert.That(1, Is.EqualTo(descriptionList[1].Id));
            Assert.That("description_02", Is.EqualTo(descriptionList[2].Description));
        }

        [Test]
        public void ExtendedItemDeserializationTest()
        {
            var jsonText =
                File.ReadAllText("./src/GameLib.Tests/unit/Resources/extended_item.json");
            var extendedItem =
                Serializable.LoadDescription<ItemExtended>(jsonText);
            Assert.That(extendedItem.Description, Is.EqualTo("description_00"));
            Assert.That(extendedItem.Slot, Is.EqualTo(3));
            Assert.IsNull(extendedItem.SecretString);
        }

        [Test]
        public void ExtendedItemListDeserializationTest()
        {
            var jsonText =
                File.ReadAllText(
                    "./src/GameLib.Tests/unit/Resources/extended_item_description.json");
            var descriptionList =
                Serializable.LoadDescriptions<ItemExtended>(jsonText);
            Assert.That(2, Is.EqualTo(descriptionList.Count));
            Assert.That(5, Is.EqualTo(descriptionList[1].Slot));
            Assert.IsNull(descriptionList[0].SecretString);
        }

        [Test]
        public void BlueprintDeserializationTest()
        {
            var jsonText =
                File.ReadAllText(
                    "./src/GameLib.Tests/unit/Resources/blueprint_description.json");
            var blueprintList = Serializable.LoadDescriptions<Blueprint>(jsonText);
            Assert.AreEqual(1, blueprintList.Count);
            Assert.AreEqual("blueprint_00", blueprintList[0].Name);
            Assert.AreEqual("description_blueprint_00", blueprintList[0].Description);
            Assert.AreEqual(3, blueprintList[0].Ingredients.Count);
            Assert.AreEqual(3, blueprintList[0].Ingredients[1].Item2);
            Assert.AreEqual(2, blueprintList[0].Product.Item1);
        }
    }
}
