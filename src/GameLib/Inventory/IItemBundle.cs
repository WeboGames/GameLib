namespace GameLib.Inventory {
    public interface IItemBundle {
        /// <summary>
        /// Id of the item to be contained in this bundle.
        /// </summary>
        int Id { get; }

        /// <summary>
        /// Maximum number of items to be contained in this bundle.
        /// </summary>
        int MaxBundleSize { get; }

        /// <summary>
        /// Current number of items in bundle.
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Adds item to bundle
        /// </summary>
        /// <param name="item">Item to add</param>
        /// <returns>true if added, false otherwise</returns>
        bool AddItemToBundle(Item item);

        /// <summary>
        /// Retrieve item at pos 0 from bundle.
        /// </summary>
        /// <returns>Retrieved item.</returns>
        Item RemoveItemFromBundle();

        /// <summary>
        /// Remove a number of items from a bundle
        /// </summary>
        /// <param name="itemAmount">Number of items to remove</param>
        /// <returns>Number of items removed</returns>
        int RemoveItemsFromBundle(int itemAmount);

        /// <summary>
        /// Try to move items from one bundle to another.
        /// </summary>
        /// <param name="bundle">Bundle from which to move the items.</param>
        /// <returns>Number of items actually moved.</returns>
        int AddBundleToBundle(ItemBundle bundle);

        /// <summary>
        /// Checks whether an item can be added to the bundle.
        /// </summary>
        /// <param name="item">Item to add.</param>
        /// <returns>Whether it can be added.</returns>
        bool CanBeAddedToBundle(Item item);

        /// <summary>
        /// Checks wheter the contents of a bundle can be moved into another.
        /// </summary>
        /// <param name="itemBundle">Bundle from which to move the items.</param>
        /// <returns>Whether they can be added.</returns>
        bool CanBeAddedToBundle(ItemBundle itemBundle);

        /// <summary>
        /// Resets bundle object
        /// </summary>
        /// <returns>true if was reset, false if not</returns>
        bool Reset();
    }
}
