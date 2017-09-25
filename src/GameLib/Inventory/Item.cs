using System;
using GameLib.Resources;

namespace GameLib.Inventory {
    [Serializable]
    public class Item : Serializable {
        public int Price;
        public int MaxBundleSize;
        public float Weight;
        public bool Stackable;
        public ItemRarity Rarity;
        public OwnershipType Ownership;

        public Item() { }

        public Item(int id, string name, int price, int maxBundleSize,
            ItemRarity rarity, float weight, string description,
            OwnershipType ownership, bool stackable)
        {
            Id = id;
            Name = name;
            Price = price;
            MaxBundleSize = maxBundleSize;
            Rarity = rarity;
            Weight = weight;
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
    }
}
