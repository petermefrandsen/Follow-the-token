namespace Common.Models
{
    public class BEP20TokenTransactions
    {
        private readonly string _address;
        private readonly int _explorationDepth;
        private readonly List<BscScanTokenTransfer> _bscScanTokenTransfers;

        public BEP20TokenTransactions(string initialAddress, int explorationDepth, List<BscScanTokenTransfer> bscScanTokenTransfers)
        {
            _address = initialAddress;
            _explorationDepth = explorationDepth;
            _bscScanTokenTransfers = bscScanTokenTransfers;
        }

        public string Address { get { return _address; } }
        public int ExplorationDepth { get { return _explorationDepth; } }
        public List<BscScanTokenTransfer> BscScanTokenTransfers { get { return _bscScanTokenTransfers; } }
    }
}
