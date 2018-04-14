namespace GameLib.Inventory
{
    public class Wallet : IWallet
    {
        private int _balance = 0;

        public Wallet()
        {
        }

        public Wallet(int startingBalance)
        {
            _balance = startingBalance;
        }

        public int Balance()
        {
            return _balance;
        }

        public bool CanBuy(Item item)
        {
            return _balance >= item.Price;
        }

        public bool CanBuy(IItemBundle itemBundle)
        {
            return _balance >= itemBundle.Count * itemBundle.Preset.Price;
        }

        public int Buy(Item item)
        {
            _balance -= item.Price;
            return _balance;
        }

        public int Buy(IItemBundle itemBundle)
        {
            _balance -= itemBundle.Count * itemBundle.Preset.Price;
            return _balance;
        }

        public int Sell(Item item)
        {
            _balance += item.Price;
            return _balance;
        }

        public int Sell(IItemBundle itemBundle)
        {
            _balance += itemBundle.Count * itemBundle.Preset.Price;
            return _balance;
        }

        public int AddFunds(int amount)
        {
            return _balance += amount;
        }

        public int WithdrawFunds(int amount)
        {
            return _balance -= amount;
        }

        public bool CanWithdrawFunds(int amount)
        {
            return _balance >= amount;
        }
    }
}