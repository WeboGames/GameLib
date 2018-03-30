using System;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using GameLib.Inventory;

namespace GameLib.test.unit.Resources {
    public class ItemExtended : Item {
        public int Slot;

        // Non serialiazable property, similar to Unity3D classes.
        [NonSerialized] public string SecretString;

        public ItemExtended() { }

        // Base constructor copy
        public ItemExtended(int id, string name, int price, int maxBundleSize, ItemRarity rarity,
            string description, OwnershipType ownership, bool stackable,
            int slot) : base(id,
            name, price, maxBundleSize, rarity, description, ownership, stackable)
        {
            Slot = slot;
        }
    }
}
