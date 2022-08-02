namespace Common.Models
{
    public class BEP20TokenTransactionResponse
    {
        private readonly string _address;
        private readonly List<BscScanTokenTransfer> _transactions;
        private readonly List<BEP20TokenTransactions> _subAddressTransactions;

        public BEP20TokenTransactionResponse(string address, List<BscScanTokenTransfer> transactions, List<BEP20TokenTransactions> subAddressTransactions)
        {
            _address = address;
            _transactions = transactions;
            _subAddressTransactions = subAddressTransactions;
        }

        public string Address { get { return _address; } }
        public List<BscScanTokenTransfer> Transactions { get { return _transactions; } }
        public List<BEP20TokenTransactions> SubAddressTransactions { get { return _subAddressTransactions; } }
    }
}
