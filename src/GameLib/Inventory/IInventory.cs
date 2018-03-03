using System.Collections.Generic;

namespace GameLib.Inventory {
    /// <summary>
    /// The inventory interface handles the behavior of the inventory implementation related to
    /// adding items, grouping into bundles, removing items, etc.
    /// </summary>
    public interface IInventory {
        /// <summary>
        /// Maximum total weight allowed in inventory.
        /// </summary>
        float WeightCapacity { get; set; }

        /// <summary>
        /// Maximum number of bundles allowed in inventory.
        /// </summary>
        int BundleCapacity { get; set; }

        /// <summary>
        /// Actual weight of all items in inventory.
        /// </summary>
        float Usage { get; set; }

        /// <summary>
        /// Add item to inventory
        /// </summary>
        /// <param name="item">desired item object to add</param>
        /// <returns>1 if added, 0 if not</returns>
        IItemBundle AddItem(Item item);

        /// <summary>
        /// Add a number of a given item to a specific bundle in an inventory.
        /// </summary>
        /// <param name="bundlePosition">Bundle position to add to.</param>
        /// <param name="item">Item to add.</param>
        /// <param name="count">Number of items to add.</param>
        /// <returns>Modified bundle.</returns>
        IItemBundle AddItem(int bundlePosition, Item item, int count);

        /// <summary>
        ///  Add an item to a specific bundle
        /// </summary>
        /// <param name="itemBundle">Item bundle to add</param>
        /// <param name="bundlePosition">Position of the bundle to add to</param>
        /// <returns>Number of items added</returns>
        int AddBundleToBundle(IItemBundle itemBundle, int bundlePosition);

        /// <summary>
        /// Get number of bundles in inventory
        /// </summary>
        /// <returns>Number of bundles in inventory</returns>
        int GetBundleNumber();

        /// <summary>
        /// Get a bundle based on a passed index
        /// </summary>
        /// <param name="bundlePosition">Index of bundle to retrieve</param>
        /// <returns>Bundle</returns>
        IItemBundle GetItemBundle(int bundlePosition);

        /// <summary>
        /// Looks for an Item within the inventory.
        /// </summary>
        /// <param name="item">Item to remove</param>
        /// <returns>Item</returns>
        Item RemoveItem(Item item);

        /// <summary>
        /// Gets a bundle for a given item, if possible.
        /// </summary>
        /// <param name="item">Item for which to get a bundle</param>
        /// <returns>Item bundle for given item</returns>
        IItemBundle GetItemBundle(Item item);

        /// <summary>
        /// Finds a list of bundles containing the desired item.
        /// </summary>
        /// <param name="item">Item to search for.</param>
        /// <returns>List of found bundles.</returns>
        List<IItemBundle> GetItemBundles(Item item);

        /// <summary>
        /// Retrieves an Item from a bundle.
        /// </summary>
        /// <param name="targetBundle">Bundle from which to retrieve an item.</param>
        /// <returns>Item from bundle</returns>
        Item RemoveItem(IItemBundle targetBundle);

        /// <summary>
        /// Finds the position of a given bundle in the inventory.
        /// </summary>
        /// <param name="targetBundle">Bundle to look for</param>
        /// <returns>Position if found, -1 if not</returns>
        int GetBundlePosition(IItemBundle targetBundle);

        /// <summary>
        /// Removes items from a given bundle
        /// </summary>
        /// <param name="numberOfItems">Number of items to remove</param>
        /// <param name="bundlePosition">Bundle to remove from</param>
        /// <returns>Number of removed items</returns>
        int RemoveItemsFromBundle(int bundlePosition, int numberOfItems = 1);
    }
}
