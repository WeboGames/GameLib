using GameLib.Resources;

namespace GameLib.Inventory
{
    public class ItemBundle : IItemBundle
    {
        private Item _preset = null;

        public Item Preset
        {
            get {
                return _preset;
            }
        }

        public int Count
        {
            get {
                return _count;
            }
        }

        public int MaxBundleSize
        {
            get; private set;
        }

        public const int c_NOT_SET = -1;
        private int _count = 0;

        public ItemBundle()
        {
            _preset = null;
            MaxBundleSize = c_NOT_SET;
        }

        public ItemBundle(Item preset)
        {
            _preset = preset;
            MaxBundleSize = preset.MaxBundleSize;
        }

        public bool AddItemToBundle(Item item)
        {
            if (!CanBeAddedToBundle(item))
                return false;
            if (_preset == null) {
                SetBundle(item);
            }
            _count++;
            return true;
        }

        public Item RemoveItemFromBundle()
        {
            if (_count <= 0)
                return null;
            _count--;
            var clone = Serializable.Clone(_preset);
            if (_count == 0) {
                Reset();
            }
            return clone;
        }

        public int RemoveItemsFromBundle(int itemAmount)
        {
            var result = 0;
            for (var i = 0; i < itemAmount; i++) {
                if (_count <= 0)
                    break;
                _count--;
                result++;
            }
            return result;
        }

        public int AddBundleToBundle(IItemBundle bundle)
        {
            var itemsToAdd = 0;
            if (!CanBeAddedToBundle(bundle))
                return itemsToAdd;
            if (_preset == null) {
                SetBundle(bundle);
            }
            itemsToAdd = MaxBundleSize - _count < bundle.Count
                ? MaxBundleSize - _count
                : bundle.Count;
            for (var i = 0; i < itemsToAdd; i++) {
                bundle.RemoveItemFromBundle();
                _count++;
            }
            return itemsToAdd;
        }

        public bool CanBeAddedToBundle(Item item)
        {
            return (_preset != null && _preset.Id == item.Id && _count < MaxBundleSize) || _preset == null;
        }

        public bool CanBeAddedToBundle(IItemBundle itemBundle)
        {
            return (_preset != null && _preset.Id == itemBundle.Preset.Id && _count <= MaxBundleSize) || _preset == null;
        }

        public bool Reset()
        {
            if (_count > 0) {
                return false;
            }
            _preset = null;
            MaxBundleSize = c_NOT_SET;
            _count = 0;
            return true;
        }

        private void SetBundle(IItemBundle itemBundle)
        {
            _preset = Serializable.Clone(itemBundle.Preset);
            MaxBundleSize = itemBundle.MaxBundleSize;
        }

        private void SetBundle(Item item)
        {
            _preset = Serializable.Clone(item);
            MaxBundleSize = item.MaxBundleSize;
        }
    }
}