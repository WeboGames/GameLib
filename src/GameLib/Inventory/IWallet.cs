namespace GameLib.Inventory
{
    public interface IWallet
    {
        /// <summary>
        /// Returns current amount held in wallet.
        /// </summary>
        /// <returns>Current balance.</returns>
        int Balance();

        /// <summary>
        /// Checks whether the wallet has the necessary funds to buy.
        /// </summary>
        /// <param name="item">Item to buy.</param>
        /// <returns>Whether or not it can be bought.</returns>
        bool CanBuy(Item item);

        /// <summary>
        /// Checks whether the wallet has the necessary funds to buy.
        /// </summary>
        /// <param name="item">Item to buy.</param>
        /// <returns>Whether or not it can be bought.</returns>
        bool CanBuy(IItemBundle item);

        /// <summary>
        /// Substracts the item price from the current balance.
        /// </summary>
        /// <param name="item">Item to buy.</param>
        /// <returns>New balance.</returns>
        int Buy(Item item);

        /// <summary>
        /// Substracts the total item bundle price from the current balance.
        /// </summary>
        /// <param name="itemBundle">Item bundle to buy.</param>
        /// <returns>New balance.</returns>
        int Buy(IItemBundle itemBundle);

        /// <summary>
        /// Adds the item price to the current balance.
        /// </summary>
        /// <param name="item">Item to sell.</param>
        /// <returns>New balance.</returns>
        int Sell(Item item);

        /// <summary>
        /// Adds the total item bundle price to the current balance.
        /// </summary>
        /// <param name="itemBundle">Item bundle to sell.</param>
        /// <returns>New balance.</returns>
        int Sell(IItemBundle itemBundle);

        /// <summary>
        /// Adds funds to wallet.
        /// </summary>
        /// <param name="amount">Amount to add.</param>
        /// <returns>Balance after transaction.</returns>
        int AddFunds(int amount);

        /// <summary>
        /// Withdraw funds from wallet.
        /// </summary>
        /// <param name="amount">Amount to withdraw.</param>
        /// <returns>Balance after transaction.</returns>
        int WithdrawFunds(int amount);

        /// <summary>
        /// Checks if balance allows.
        /// </summary>
        /// <param name="amount">Amount to check.</param>
        /// <returns>Whether possible to withdraw.</returns>
        bool CanWithdrawFunds(int amount);
    }
}