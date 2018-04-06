using GameLib.Inventory;
using GameLib.Resources;
using NUnit.Framework;

public class InventoryTest
{
    private Inventory _inventory;
    private Item _item0;
    private Item _item1;
    private Item _item2;

    [SetUp]
    public void SetUp()
    {
        _inventory = new Inventory(20);
        _item0 = new Item(0, "Iron Ore", 100, 8, Item.ItemRarity.Common,
            "Basic crafting material. Available in asteroids.",
            Item.OwnershipType.Public);
        _item1 = new Item(1, "Beacon", 250, 2, Item.ItemRarity.Uncommon,
            "Use to generate a beacon on the map.",
            Item.OwnershipType.Public);
        _item2 = new Item(2, "Non Stackable", 250, 1,
            Item.ItemRarity.Uncommon, "Use to generate a beacon on the map.",
            Item.OwnershipType.Public);
    }

    [Test]
    public void Inventory_T00_AddItemSuccessful()
    {
        var ironOre = Serializable.Clone(_item0);
        _inventory.AddItem(ironOre);
        Assert.AreEqual(1, _inventory.GetUsage());
    }

    [Test]
    public void Inventory_T01_AddItemToSecondBundleSuccessful()
    {
        var ironOre = Serializable.Clone(_item0);
        _inventory.AddItem(ironOre);
        for (var i = 0; i < ironOre.MaxBundleSize; i++) {
            _inventory.AddItem(Serializable.Clone(_item0));
        }
        Assert.AreEqual(2, _inventory.GetUsage());
    }

    [Test]
    public void Inventory_T02_AddItemToThirdBundleFailure()
    {
        _inventory = new Inventory(2);
        var ironOre = Serializable.Clone(_item0);
        _inventory.AddItem(ironOre);
        for (var i = 0; i < ironOre.MaxBundleSize; i++) {
            _inventory.AddItem(Serializable.Clone(_item0));
        }
        var nullBundle = _inventory.AddItem(Serializable.Clone(_item1));
        Assert.AreEqual(2, _inventory.GetUsage());
        Assert.IsNull(nullBundle);
    }

    [Test]
    public void Inventory_T03_AddItemToFullFailure()
    {
        _inventory = new Inventory(3);
        var ironOre = Serializable.Clone(_item0);
        _inventory.AddItem(ironOre);
        for (var i = 0; i < ironOre.MaxBundleSize; i++) {
            _inventory.AddItem(Serializable.Clone(ironOre));
        }
        _inventory.AddItem(Serializable.Clone(ironOre));
        Assert.AreEqual(8, _inventory.GetItemBundle(ironOre).Count);
        for (var i = 0; i < ironOre.MaxBundleSize; i++) {
            _inventory.AddItem(Serializable.Clone(ironOre));
        }
        Assert.AreEqual(3, _inventory.GetUsage());
    }

    [Test]
    public void Inventory_T04_EmptyInventory()
    {
        _inventory.AddItem(Serializable.Clone(_item0));
        _inventory.AddItem(Serializable.Clone(_item0));
        _inventory.RemoveItem(Serializable.Clone(_item0));
        _inventory.RemoveItem(Serializable.Clone(_item0));
        Assert.AreEqual(0, _inventory.GetUsage());
    }

    [Test]
    public void Inventory_T05_BundleCreated()
    {
        var ironOre = Serializable.Clone(_item0);
        var beacon = Serializable.Clone(_item1);
        _inventory.AddItem(ironOre);
        _inventory.AddItem(beacon);
        Assert.AreEqual(ironOre.Id, _inventory.GetItemBundle(0).Preset.Id);
        Assert.AreEqual(beacon.Id, _inventory.GetItemBundle(1).Preset.Id);
    }

    [Test]
    public void Inventory_T06_GetItemFromBundle()
    {
        var ironOre = Serializable.Clone(_item0);
        var beacon = Serializable.Clone(_item1);
        _inventory.AddItem(ironOre);
        _inventory.AddItem(beacon);
        Assert.AreEqual(beacon, _inventory.RemoveItem(_inventory.GetItemBundle(1)));
        Assert.AreEqual(ironOre, _inventory.RemoveItem(_inventory.GetItemBundle(0)));
    }

    [Test]
    public void Inventory_T07_AddNonStackableItems()
    {
        var item = Serializable.Clone(_item2);
        var item1 = Serializable.Clone(_item2);
        _inventory.AddItem(item);
        _inventory.AddItem(item1);
        Assert.AreEqual(2, _inventory.GetUsage());
    }

    [Test]
    public void Inventory_T08_GetCorrectBundlePosition()
    {
        var item = Serializable.Clone(_item0);
        var item1 = Serializable.Clone(_item1);
        _inventory.AddItem(item);
        var bundle = _inventory.AddItem(item1);
        var bundlePosition = _inventory.GetBundlePosition(bundle);
        Assert.AreEqual(1, bundlePosition);
    }

    [Test]
    public void Inventory_T09_NoItemFound()
    {
        var item = Serializable.Clone(_item0);
        var item1 = Serializable.Clone(_item1);
        _inventory.AddItem(item1);
        var foundItem = _inventory.RemoveItem(item);
        Assert.IsNull(foundItem);
    }

    [Test]
    public void Inventory_T10_GetAllMatchingBundles()
    {
        for (var i = 0; i < _item0.MaxBundleSize + 2; i++) {
            _inventory.AddItem(Serializable.Clone(_item0));
        }
        var oldUsage = _inventory.GetUsage();
        Assert.AreEqual(2, _inventory.GetUsage());
        var foundLists = _inventory.GetItemBundles(_item0);
        Assert.AreEqual(2, foundLists.Count);
        for (var i = 0; i < _item1.MaxBundleSize * 2 + 1; i++) {
            _inventory.AddItem(Serializable.Clone(_item1));
        }
        var foundLists1 = _inventory.GetItemBundles(_item1);
        Assert.AreEqual(5, _inventory.GetUsage());
        Assert.AreEqual(3, foundLists1.Count);
    }

    [Test]
    public void Inventory_T11_CanBeAdded()
    {
        _inventory = new Inventory(3);
        Assert.IsTrue(_inventory.CanAdd(_item0));
        _inventory.AddItem(0, _item0, _item0.MaxBundleSize);
        _inventory.AddItem(1, _item1, _item1.MaxBundleSize);
        _inventory.AddItem(2, _item2, _item2.MaxBundleSize);
        Assert.IsFalse(_inventory.CanAdd(_item0));
    }
}