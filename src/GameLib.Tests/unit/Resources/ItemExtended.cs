using System;
using GameLib.Inventory;

namespace GameLib.test.unit.Resources {
    public class ItemExtended : Item {
        public int Slot;
        
        // Non serialiazable property, similar to Unity3D classes.
        [NonSerialized]
        public string SecretString;
        
        // Base constructor copy
        public ItemExtended(int id, string name, int price, int maxBundleSize, ItemRarity rarity,
            float weight, string description, OwnershipType ownership, bool stackable) : base(id,
            name, price, maxBundleSize, rarity, weight, description, ownership, stackable) { }
    }
}
