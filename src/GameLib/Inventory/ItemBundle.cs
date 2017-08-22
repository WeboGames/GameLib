using System.Collections.Generic;

namespace GameLib.Inventory {
    public class ItemBundle : IItemBundle {
        public int Id { get; private set; }
        public int MaxBundleSize { get; private set; }

        public int Count
        {
            get { return _bundle.Count; }
        }

        public const int NotSet = -1;

        private readonly List<Item> _bundle;

        public ItemBundle()
        {
            Id = NotSet;
            MaxBundleSize = NotSet;
            _bundle = new List<Item>();
        }

        public ItemBundle(Item preset)
        {
            Id = preset.Id;
            MaxBundleSize = preset.MaxBundleSize;
            _bundle = new List<Item>();
        }

        public bool AddItemToBundle(Item item)
        {
            if (!CanBeAddedToBundle(item)) return false;
            if (Id == NotSet) {
                SetBundle(item);
            }
            _bundle.Add(item);
            return true;
        }

        public Item RemoveItemFromBundle()
        {
            if (_bundle.Count <= 0) return null;
            var tmp = _bundle[0];
            _bundle.RemoveAt(0);
            if (_bundle.Count == 0) {
                Reset();
            }
            return tmp;
        }

        public int RemoveItemsFromBundle(int itemAmount)
        {
            var result = 0;
            for (var i = 0; i < itemAmount; i++) {
                if (_bundle.Count <= 0) break;
                _bundle.RemoveAt(0);
                result++;
            }
            return result;
        }

        public int AddBundleToBundle(ItemBundle bundle)
        {
            var itemsToAdd = 0;
            if (!CanBeAddedToBundle(bundle)) return itemsToAdd;
            if (Id == NotSet) {
                SetBundle(bundle);
            }
            itemsToAdd = MaxBundleSize - _bundle.Count < bundle.Count
                ? MaxBundleSize - _bundle.Count
                : bundle.Count;
            for (var i = 0; i < itemsToAdd; i++) {
                _bundle.Add(bundle.RemoveItemFromBundle());
            }
            return itemsToAdd;
        }

        public bool CanBeAddedToBundle(Item item)
        {
            return Id == item.Id && _bundle.Count < MaxBundleSize || Id == NotSet;
        }

        public bool CanBeAddedToBundle(ItemBundle itemBundle)
        {
            return Id == itemBundle.Id && _bundle.Count <= MaxBundleSize || Id == NotSet;
        }

        public bool Reset()
        {
            if (_bundle.Count > 0) {
                return false;
            }
            Id = NotSet;
            MaxBundleSize = NotSet;
            _bundle.Clear();
            return true;
        }

        private void SetBundle(ItemBundle itemBundle)
        {
            Id = itemBundle.Id;
            MaxBundleSize = itemBundle.MaxBundleSize;
        }

        private void SetBundle(Item item)
        {
            Id = item.Id;
            MaxBundleSize = item.MaxBundleSize;
        }
    }
}
