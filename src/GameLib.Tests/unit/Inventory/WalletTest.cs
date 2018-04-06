using GameLib.Inventory;
using NUnit.Framework;

internal class WalletTest
{
    private IWallet _wallet;
    private Item _item0;
    private IItemBundle _itemBundle0;

    [SetUp]
    public void Setup()
    {
        _wallet = new Wallet(200);
        _item0 = new Item(0, "Iron Ore", 100, 8, Item.ItemRarity.Common,
            "Basic crafting material. Available in asteroids.",
            Item.OwnershipType.Public);
        _itemBundle0 = new ItemBundle();
        _itemBundle0.AddItemToBundle(_item0);
        _itemBundle0.AddItemToBundle(_item0);
    }

    [Test]
    public void Wallet_T00_BuyItemSucceeds()
    {
        Assert.IsTrue(_wallet.CanBuy(_item0));
        var newBalance = _wallet.Buy(_item0);
        Assert.AreEqual(100, newBalance);
    }

    [Test]
    public void Wallet_T01_BuyItemBundleSucceeds()
    {
        Assert.IsTrue(_wallet.CanBuy(_itemBundle0));
        var newBalance = _wallet.Buy(_itemBundle0);
        Assert.AreEqual(0, newBalance);
    }

    [Test]
    public void Wallet_T02_ItemRejectSucceeds()
    {
        _wallet.Buy(_itemBundle0);
        Assert.IsFalse(_wallet.CanBuy(_item0));
    }

    [Test]
    public void Wallet_T03_ItemBundleRejectSucceeds()
    {
        _wallet.Buy(_itemBundle0);
        Assert.IsFalse(_wallet.CanBuy(_itemBundle0));
    }
}