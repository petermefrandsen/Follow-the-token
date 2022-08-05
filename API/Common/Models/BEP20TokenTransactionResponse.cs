namespace Common.Models
{
    public class Bep20TokenTransactionResponse
    {
        private readonly string _address;
        private readonly List<BscScanTokenTransfer> _transactions;
        private readonly List<Bep20TokenTransactions> _subAddressTransactions;

        public Bep20TokenTransactionResponse(string address, List<BscScanTokenTransfer> transactions, List<Bep20TokenTransactions> subAddressTransactions)
        {
            _address = address;
            _transactions = transactions;
            _subAddressTransactions = subAddressTransactions;
        }

        public string Address { get { return _address; } }
        public List<BscScanTokenTransfer> Transactions { get { return _transactions; } }
        public List<Bep20TokenTransactions> SubAddressTransactions { get { return _subAddressTransactions; } }
    }
}
