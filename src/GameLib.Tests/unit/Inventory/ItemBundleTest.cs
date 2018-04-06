using GameLib.Inventory;
using GameLib.Resources;
using NUnit.Framework;

public class ItemBundleTest
{
    private ItemBundle _itemBundle;
    private Item _item0;
    private Item _item1;

    [SetUp]
    public void SetUp()
    {
        _item0 = new Item(0, "Beacon", 250, 8, Item.ItemRarity.Uncommon,
            "Use to generate a beacon on the map.",
            Item.OwnershipType.Public);
        _item1 = new Item(1, "Non Stackable", 250, 2,
            Item.ItemRarity.Uncommon, "Use to generate a beacon on the map.",
            Item.OwnershipType.Public);
        _itemBundle = new ItemBundle(_item0);
    }

    [Test]
    public void ItemBundle_T00_AddItemSuccess()
    {
        _itemBundle.AddItemToBundle(Serializable.Clone(_item0));
        Assert.AreEqual(1, _itemBundle.Count);
    }

    [Test]
    public void ItemBundle_T01_AddTooManyItemsFailure()
    {
        for (var i = 0; i < _itemBundle.MaxBundleSize; i++) {
            _itemBundle.AddItemToBundle(Serializable.Clone(_item0));
        }
        var itemAdded = _itemBundle.AddItemToBundle(Serializable.Clone(_item0));
        Assert.AreEqual(false, itemAdded);
    }

    [Test]
    public void ItemBundle_T02_RemoveItemSuccess()
    {
        _itemBundle.AddItemToBundle(Serializable.Clone(_item0));
        var item = _itemBundle.RemoveItemFromBundle();
        Assert.IsNotNull(item);
        Assert.AreEqual(0, _itemBundle.Count);
    }

    [Test]
    public void ItemBundle_T03_RemoveItemFromEmptyFailure()
    {
        var item = _itemBundle.RemoveItemFromBundle();
        Assert.IsNull(item);
        Assert.AreEqual(0, _itemBundle.Count);
    }

    [Test]
    public void ItemBundle_T04_AddBundleToBundleSuccess()
    {
        for (var i = 0; i < 4; i++) {
            _itemBundle.AddItemToBundle(Serializable.Clone(_item0));
        }
        var itemBundle = new ItemBundle(Serializable.Clone(_item0));
        for (var i = 0; i < 2; i++) {
            itemBundle.AddItemToBundle(Serializable.Clone(_item0));
        }
        var itemsAdded = _itemBundle.AddBundleToBundle(itemBundle);
        Assert.AreEqual(2, itemsAdded);
        Assert.AreEqual(6, _itemBundle.Count);
        Assert.AreEqual(0, itemBundle.Count);
    }

    [Test]
    public void ItemBundle_T05_AddBundleToBundleFailure()
    {
        for (var i = 0; i < _itemBundle.MaxBundleSize; i++) {
            _itemBundle.AddItemToBundle(Serializable.Clone(_item0));
        }
        var itemBundle = new ItemBundle(Serializable.Clone(_item0));
        for (var i = 0; i < 2; i++) {
            itemBundle.AddItemToBundle(Serializable.Clone(_item0));
        }
        var itemsAdded = _itemBundle.AddBundleToBundle(itemBundle);
        Assert.AreEqual(0, itemsAdded);
        Assert.AreEqual(8, _itemBundle.Count);
        Assert.AreEqual(2, itemBundle.Count);
    }

    [Test]
    public void ItemBundle_T06_AddNonMatchingItemFailure()
    {
        _itemBundle.AddItemToBundle(Serializable.Clone(_item1));
        Assert.AreEqual(0, _itemBundle.Count);
    }

    [Test]
    public void ItemBundle_T07_AddUnmatchingBundleToBundleFailure()
    {
        var mismatchingBundle = new ItemBundle(Serializable.Clone(_item1));
        for (var i = 0; i < mismatchingBundle.MaxBundleSize; i++) {
            mismatchingBundle.AddItemToBundle(Serializable.Clone(_item1));
        }
        _itemBundle.AddBundleToBundle(mismatchingBundle);
        Assert.AreEqual(0, _itemBundle.Count);
    }

    [Test]
    public void ItemBundle_T08_AddBigBundleToBundleSuccess()
    {
        for (var i = 0; i < 4; i++) {
            _itemBundle.AddItemToBundle(Serializable.Clone(_item0));
        }
        var itemBundle = new ItemBundle(Serializable.Clone(_item0));
        for (var i = 0; i < 8; i++) {
            itemBundle.AddItemToBundle(Serializable.Clone(_item0));
        }
        var itemsAdded = _itemBundle.AddBundleToBundle(itemBundle);
        Assert.AreEqual(4, itemsAdded);
        Assert.AreEqual(8, _itemBundle.Count);
        Assert.AreEqual(4, itemBundle.Count);
    }
}