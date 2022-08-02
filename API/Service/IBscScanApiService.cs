using Common.Models;

namespace Service
{
    public interface IBscScanApiService
    {
        Task<BscScanResult<string>> GetBalanceForAddressWithContract(string address, string contractAddress);

        Task<BscScanResult<List<BscScanTokenTransfer>>> GetTokenTransferEventsForAddress(string address, int offset, int page, string? contractAddress = null);
    }
}
