using System;
using GameLib.Resources;

namespace GameLib.Inventory {
    [Serializable]
    public class Item : Serializable {
        public int Price;
        public int MaxBundleSize;
        public bool Stackable;
        public ItemRarity Rarity;
        public OwnershipType Ownership;

        public Item() { }

        public Item(int id, string name, int price, int maxBundleSize,
            ItemRarity rarity, string description,
            OwnershipType ownership, bool stackable)
        {
            Id = id;
            Name = name;
            Price = price;
            MaxBundleSize = maxBundleSize;
            Rarity = rarity;
            Description = description;
            Ownership = ownership;
            Stackable = stackable;
        }

        public enum OwnershipType {
            Public = 0,
            Shared = 1,
            Private = 2
        }

        public enum ItemRarity {
            Common = 0,
            Uncommon = 1,
            Rare = 2,
            Legendary = 3
        }

        public override bool Equals(object other)
        {
            var result = true;
            var item = (Item) other;
            if (Price != item.Price) {
                result = false;
            }
            if (MaxBundleSize != item.MaxBundleSize) {
                result = false;
            }
            if (Stackable != item.Stackable) {
                result = false;
            }
            if (Rarity != item.Rarity) {
                result = false;
            }
            if (Ownership != item.Ownership) {
                result = false;
            }
            return result;
        }
    }
}
