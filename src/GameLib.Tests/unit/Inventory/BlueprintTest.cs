using NUnit.Framework;
using GameLib.Inventory;
using System.Collections.Generic;

namespace GameLib.Tests.unit.Inventory
{
    class BlueprintTest
    {
        private IBlueprint _blueprint;
        private IItemBundle _itemBundle00;
        private IItemBundle _itemBundle01;
        private IItemBundle _itemBundle02;

        [SetUp]
        public void SetUp()
        {
            var ingredients = new List<IItemBundle>();
            var item00 = new Item(0, "Iron Ore", 100, 8, Item.ItemRarity.Common,
                                  5, "Basic crafting material. Available in asteroids.",
                                  Item.OwnershipType.Public, true);
            var item01 = new Item(1, "Beacon", 250, 2, Item.ItemRarity.Uncommon, 15,
                                  "Use to generate a beacon on the map.",
                                  Item.OwnershipType.Public, true);
            var item02 = new Item(2, "Non Stackable", 250, 2,
                                  Item.ItemRarity.Uncommon, 5, "",
                                  Item.OwnershipType.Public, false);
            var item03 = new Item(3, "Non Stackable", 250, 2,
                                  Item.ItemRarity.Uncommon, 5, "",
                                  Item.OwnershipType.Public, false);
            _itemBundle00 = new ItemBundle(item00);
            _itemBundle01 = new ItemBundle(item01);
            _itemBundle02 = new ItemBundle(item02);
            var itemBundle03 = new ItemBundle(item03);
            for (var i = 0; i < 5; i++) {
                _itemBundle00.AddItemToBundle(item00);
            }
            for (var i = 0; i < 3; i++) {
                _itemBundle01.AddItemToBundle(item01);
            }
            for (var i = 0; i < 8; i++) {
                _itemBundle02.AddItemToBundle(item02);
            }
            for (var i = 0; i < 2; i++) {
                itemBundle03.AddItemToBundle(item03);
            }
            ingredients.Add(_itemBundle00);
            ingredients.Add(_itemBundle01);
            ingredients.Add(_itemBundle02);
            _blueprint = new Blueprint(ingredients, itemBundle03);
        }

        [Test]
        public void T00_MatchIngredientList()
        {
            var ingredients = new List<IItemBundle>();
            ingredients.Add(_itemBundle00);
            Assert.IsFalse(_blueprint.Match(ingredients));
            ingredients.Add(_itemBundle01);
            ingredients.Add(_itemBundle02);
            Assert.IsTrue(_blueprint.Match(ingredients));
        }

        [Test]
        public void T01_CraftItem()
        {
            var ingredients = new List<IItemBundle>();
            ingredients.Add(_itemBundle00);
            Assert.IsNull(_blueprint.Craft(ingredients));
            ingredients.Add(_itemBundle01);
            ingredients.Add(_itemBundle02);
            Assert.IsNotNull(_blueprint.Craft(ingredients));
        }
    }
}
