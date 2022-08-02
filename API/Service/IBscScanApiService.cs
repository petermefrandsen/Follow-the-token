using Common.Models;

namespace Service
{
    public interface IBscScanApiService
    {
        Task<BscScanResult<List<BscScanTokenTransfer>>> GetTokenTransferEventsForAddress(string address, int offset, int page, string? contractAddress = null, int retryLimit = 10, int apiLimitDelay = 200);
    }
}
