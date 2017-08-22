using System.Collections.Generic;
using System.Linq;

namespace GameLib.Inventory {
    public class Inventory : IInventory {
        public float WeightCapacity { get; set; }
        public int BundleCapacity { get; set; }
        public float Usage { get; set; }

        public List<ItemBundle> ItemBundles;

        public Inventory(float weightCapacity, int bundleCapacity)
        {
            WeightCapacity = weightCapacity;
            BundleCapacity = bundleCapacity;
            Usage = 0;
            ItemBundles = new List<ItemBundle>(bundleCapacity);
            for (var i = 0; i < BundleCapacity; i++) {
                ItemBundles.Add(new ItemBundle());
            }
        }

        public ItemBundle AddItem(Item item)
        {
            if (!(Usage + item.Weight <= WeightCapacity)) return null;
            var bundle = HasAvailableBundle(item);
            if (bundle != null) {
                bundle.AddItemToBundle(item);
            } else {
                if (!item.Stackable) {
                    bundle = ItemBundles.FirstOrDefault(t => t.Id == ItemBundle.NotSet);
                    if (bundle != null) {
                        bundle.AddItemToBundle(item);
                    }
                }
            }
            Usage += item.Weight;
            return bundle;
        }

        public ItemBundle AddItem(int bundlePosition, Item item, int count)
        {
            if (Usage + item.Weight > WeightCapacity) return null;
            var bundle = ItemBundles[bundlePosition];
            var wasAdded = true;
            while (wasAdded && count != 0) {
                wasAdded = bundle.AddItemToBundle(item);
                count--;
            }
            return bundle;
        }

        public int AddBundleToBundle(ItemBundle itemBundle, int bundlePosition)
        {
            return ItemBundles[bundlePosition].AddBundleToBundle(itemBundle);
        }

        public int GetBundleNumber()
        {
            return ItemBundles.Count;
        }

        public ItemBundle GetItemBundle(int bundlePosition)
        {
            return ItemBundles[bundlePosition];
        }

        public Item RemoveItem(Item item)
        {
            foreach (var itemBundle in ItemBundles) {
                if (itemBundle.Id != item.Id) continue;
                var item1 = itemBundle.RemoveItemFromBundle();
                Usage -= item1.Weight;
                return item1;
            }
            return null;
        }

        public ItemBundle GetItemBundle(Item item)
        {
            return ItemBundles.FirstOrDefault(itemBundle => itemBundle.Id == item.Id);
        }

        public Item RemoveItem(ItemBundle targetBundle)
        {
            foreach (var itemBundle in ItemBundles) {
                if (itemBundle != targetBundle) continue;
                var item = itemBundle.RemoveItemFromBundle();
                Usage -= item.Weight;
                return item;
            }
            return null;
        }

        public int GetBundlePosition(ItemBundle targetBundle)
        {
            var result = -1;
            for (var i = 0; i < ItemBundles.Count; i++) {
                if (ItemBundles[i] != targetBundle) continue;
                result = i;
            }
            return result;
        }

        public int RemoveItemsFromBundle(int bundlePosition, int numberOfItems = 1)
        {
            var removedItems = ItemBundles[bundlePosition].RemoveItemsFromBundle(numberOfItems);
            if (ItemBundles[bundlePosition].Count == 0) {
                ItemBundles[bundlePosition].Reset();
            }
            return removedItems;
        }

        private ItemBundle HasAvailableBundle(Item item)
        {
            return item.Stackable
                ? ItemBundles.FirstOrDefault(t => t.Id == item.Id && t.CanBeAddedToBundle(item) ||
                                                   t.Id == ItemBundle.NotSet)
                : null;
        }
    }
}
