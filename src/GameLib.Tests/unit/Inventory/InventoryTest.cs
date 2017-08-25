using GameLib.Inventory;
using GameLib.Resources;
using NUnit.Framework;

public class InventoryTest {
    private Inventory _inventory;
    private Item _item0;
    private Item _item1;
    private Item _item2;

    [SetUp]
    public void SetUp()
    {
        _inventory = new Inventory(20f, 2);
        _item0 = new Item(0, "Iron Ore", 100, 8, Item.ItemRarity.Common, 5,
            "Basic crafting material. Available in asteroids.",
            Item.OwnershipType.Public, true);
        _item1 = new Item(1, "Beacon", 250, 2, Item.ItemRarity.Uncommon, 15,
            "Use to generate a beacon on the map.",
            Item.OwnershipType.Public, true);
        _item2 = new Item(2, "Non Stackable", 250, 2,
            Item.ItemRarity.Uncommon, 5, "Use to generate a beacon on the map.",
            Item.OwnershipType.Public, false);
    }

    [Test]
    public void T00_AddItemSuccessful()
    {
        var ironOre = Serializable.Clone(_item0);
        _inventory.AddItem(ironOre);
        Assert.AreEqual(ironOre.Weight, _inventory.Usage);
        Assert.AreEqual(2, _inventory.GetBundleNumber());
    }

    [Test]
    public void T01_AddItemToSecondBundleSuccessful()
    {
        _inventory.WeightCapacity = 200f;
        var ironOre = Serializable.Clone(_item0);
        _inventory.AddItem(ironOre);
        for (var i = 0; i < ironOre.MaxBundleSize; i++) {
            _inventory.AddItem(Serializable.Clone(_item0));
        }
        Assert.AreEqual(2, _inventory.GetBundleNumber());
    }

    [Test]
    public void T02_AddItemToThirdBundleFailure()
    {
        _inventory.WeightCapacity = 200f;
        var ironOre = Serializable.Clone(_item0);
        _inventory.AddItem(ironOre);
        for (var i = 0; i < ironOre.MaxBundleSize; i++) {
            _inventory.AddItem(Serializable.Clone(_item0));
        }
        _inventory.AddItem(Serializable.Clone(_item1));
        Assert.AreEqual(2, _inventory.GetBundleNumber());
    }

    [Test]
    public void T03_AddItemToFullFailure()
    {
        var ironOre = Serializable.Clone(_item0);
        _inventory.AddItem(ironOre);
        for (var i = 0; i < 3; i++) {
            _inventory.AddItem(Serializable.Clone(_item0));
        }
        _inventory.AddItem(Serializable.Clone(_item0));
        Assert.AreEqual(20f, _inventory.Usage);
        Assert.AreEqual(4, _inventory.GetItemBundle(ironOre).Count);
    }

    [Test]
    public void T04_EmptyInventory()
    {
        _inventory.AddItem(Serializable.Clone(_item0));
        _inventory.AddItem(Serializable.Clone(_item0));
        _inventory.RemoveItem(Serializable.Clone(_item0));
        _inventory.RemoveItem(Serializable.Clone(_item0));
        Assert.AreEqual(0, _inventory.Usage);
    }

    [Test]
    public void T05_BundleCreated()
    {
        var ironOre = Serializable.Clone(_item0);
        var beacon = Serializable.Clone(_item1);
        _inventory.AddItem(ironOre);
        _inventory.AddItem(beacon);
        Assert.AreEqual(ironOre.Id, _inventory.GetItemBundle(0).Id);
        Assert.AreEqual(beacon.Id, _inventory.GetItemBundle(1).Id);
    }

    [Test]
    public void T06_GetItemFromBundle()
    {
        var ironOre = Serializable.Clone(_item0);
        var beacon = Serializable.Clone(_item1);
        _inventory.AddItem(ironOre);
        _inventory.AddItem(beacon);
        Assert.AreEqual(beacon, _inventory.RemoveItem(_inventory.GetItemBundle(1)));
        Assert.AreEqual(ironOre, _inventory.RemoveItem(_inventory.GetItemBundle(0)));
    }

    [Test]
    public void T07_AddNonStackableItems()
    {
        var item = Serializable.Clone(_item2);
        var item1 = Serializable.Clone(_item2);
        _inventory.AddItem(item);
        _inventory.AddItem(item1);
        Assert.AreEqual(2, _inventory.GetBundleNumber());
    }

    [Test]
    public void T08_GetCorrectBundlePosition()
    {
        var item = Serializable.Clone(_item0);
        var item1 = Serializable.Clone(_item1);
        _inventory.AddItem(item);
        var bundle = _inventory.AddItem(item1);
        var bundlePosition = _inventory.GetBundlePosition(bundle);
        Assert.AreEqual(1, bundlePosition);
    }

    [Test]
    public void T09_NoItemFound()
    {
        var item = Serializable.Clone(_item0);
        var item1 = Serializable.Clone(_item1);
        _inventory.AddItem(item1);
        var foundItem = _inventory.RemoveItem(item);
        Assert.IsNull(foundItem);
    }
}
