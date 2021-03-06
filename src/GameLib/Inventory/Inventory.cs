﻿using System.Collections.Generic;
using System.Linq;

namespace GameLib.Inventory
{
    public class Inventory : IInventory
    {
        public int Capacity
        {
            get; set;
        }

        public List<IItemBundle> ItemBundles;

        public Inventory(int capacity)
        {
            Capacity = capacity;
            ItemBundles = new List<IItemBundle>(capacity);
            for (var i = 0; i < Capacity; i++) {
                ItemBundles.Add(new ItemBundle());
            }
        }

        public int GetUsage()
        {
            return ItemBundles.Count(x => x.Preset != null);
        }

        public bool CanAdd(Item item)
        {
            var bundle = HasAvailableBundle(item);
            return bundle != null || GetUsage() < Capacity;
        }

        public IItemBundle AddItem(Item item)
        {
            var bundle = HasAvailableBundle(item);
            if (bundle != null) {
                bundle.AddItemToBundle(item);
            }
            return bundle;
        }

        public IItemBundle AddItem(int bundlePosition, Item item, int count)
        {
            var bundle = ItemBundles[bundlePosition];
            var wasAdded = true;
            while (wasAdded && count != 0) {
                wasAdded = bundle.AddItemToBundle(item);
                count--;
            }
            return bundle;
        }

        public int AddBundleToBundle(IItemBundle itemBundle, int bundlePosition)
        {
            return ItemBundles[bundlePosition].AddBundleToBundle(itemBundle);
        }

        public IItemBundle GetItemBundle(int bundlePosition)
        {
            return ItemBundles[bundlePosition];
        }

        public Item RemoveItem(Item item)
        {
            var bundleToRemoveFrom = ItemBundles.FirstOrDefault(t => t.Preset != null
                && t.Preset.Id == item.Id);
            Item removedItem = null;
            if (bundleToRemoveFrom != null) {
                removedItem = bundleToRemoveFrom.RemoveItemFromBundle();
            }
            return removedItem;
        }

        public IItemBundle GetItemBundle(Item item)
        {
            return ItemBundles.FirstOrDefault(itemBundle => itemBundle.Preset != null
                && itemBundle.Preset.Id == item.Id);
        }

        public List<IItemBundle> GetItemBundles(Item item)
        {
            return ItemBundles.Where(ib => ib.Preset != null && ib.Preset.Id == item.Id).ToList();
        }

        public Item RemoveItem(IItemBundle targetBundle)
        {
            var toRemoveFrom = ItemBundles.FirstOrDefault(t => t == targetBundle);
            Item removed = null;
            if (toRemoveFrom != null) {
                removed = toRemoveFrom.RemoveItemFromBundle();
            }
            return removed;
        }

        public int GetBundlePosition(IItemBundle targetBundle)
        {
            var result = -1;
            for (var i = 0; i < ItemBundles.Count; i++) {
                if (ItemBundles[i] != targetBundle)
                    continue;
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

        private IItemBundle HasAvailableBundle(Item item)
        {
            return ItemBundles.FirstOrDefault(t => (t.Preset != null && t.Preset.Id == item.Id
                                              && t.CanBeAddedToBundle(item)) || t.Preset == null);
        }
    }
}