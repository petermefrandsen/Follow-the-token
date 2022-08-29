using Common.Models;
using Flurl;
using Flurl.Http;
using Microsoft.Extensions.Configuration;

namespace Service
{
    public class BscScanApiService : IBscScanApiService
    {
        private readonly string _bscScanApiKey;
        private readonly string _bscScanBaseUrl;

        public BscScanApiService(IConfiguration configuration)
        {
            _bscScanApiKey = configuration["BSC_SCAN_API_KEY"];
            _bscScanBaseUrl = configuration.GetConnectionString("BSC_SCAN_API_URL");
        }

        public async Task<BscScanResult<List<BscScanTokenTransfer>>> GetTokenTransferEventsForAddress(string address, int offset, int page, string? contractAddress = null, int retryLimit = 10, int apiLimitDelay = 200)
        {
            var delayApiCall = false;
            var retries = 0;

            BscScanResult<List<BscScanTokenTransfer>>? result;
            do
            {
                if (delayApiCall) Thread.Sleep(apiLimitDelay);

                try
                {
                    delayApiCall = false;

                    result = await _bscScanBaseUrl
                    .SetQueryParams(new
                    {
                        module = "account",
                        sort = "desc",
                        action = "tokentx",
                        contractaddress = contractAddress,
                        startblock = 0,
                        endblock = 999999999,
                        offset,
                        page,
                        address,
                        apikey = _bscScanApiKey
                    }).GetJsonAsync<BscScanResult<List<BscScanTokenTransfer>>>();

                    return result;
                }
                catch (Exception e)
                {
                    retries++;
                    
                    if (e.InnerException != null && e.InnerException.Message.ToLower().Contains("max rate limit reached"))
                    {
                        delayApiCall = true;
                    }

                    result = new BscScanResult<List<BscScanTokenTransfer>>
                    {
                        Status = "0"
                    };
                }
            } while (result.Status != "1" && retries < retryLimit);

            return result;
        }
    }
}
