﻿using Common.Models;
using Flurl;
using Flurl.Http;
using Microsoft.Extensions.Configuration;

namespace Service
{
    public class BscScanApiService : IBscScanApiService
    {
        private readonly string _bscScanApiKey;
        private readonly string _bscScanBaseUrl;
        private const int apiLimitDelay = 200;

        public BscScanApiService(IConfiguration configuration)
        {
            _bscScanApiKey = configuration["BscScanApiKey"];
            _bscScanBaseUrl = configuration.GetConnectionString("BscScanBaseUrl");
        }

        public async Task<BscScanResult<string>> GetBalanceForAddressWithContract(string address, string contractAddress)
        {
            var delayApiCall = false;

            BscScanResult<string>? result;
            do
            {
                if (delayApiCall) Thread.Sleep(apiLimitDelay);

                try
                {
                    result = await _bscScanBaseUrl
                        .SetQueryParams(new
                        {
                            module = "account",
                            action = "tokenbalance",
                            contractaddress = contractAddress,
                            address,
                            tag = "latest",
                            apikey = _bscScanApiKey
                        }).GetJsonAsync<BscScanResult<string>>();

                    delayApiCall = true;
                }
                catch (Exception e)
                {

                    if (e.InnerException != null && !e.InnerException.Message.Contains("Max rate limit reached"))
                    {
                        throw;
                    }

                    result = new BscScanResult<string>
                    {
                        Status = "0"
                    };
                }
            } while (result.Status != "1" && result.Result != null && result.Result.Contains("Max rate limit reached"));

            return result;
        }

        public async Task<BscScanResult<List<BscScanTokenTransfer>>> GetTokenTransferEventsForAddress(string address, int offset, int page, string? contractAddress = null)
        {
            var delayApiCall = false;

            BscScanResult<List<BscScanTokenTransfer>>? result;
            do
            {
                if (delayApiCall) Thread.Sleep(apiLimitDelay);

                try
                {
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
                    delayApiCall = true;

                    if (e.InnerException != null && !e.InnerException.Message.Contains("Max rate limit reached"))
                    {
                        throw;
                    }

                    result = new BscScanResult<List<BscScanTokenTransfer>>
                    {
                        Status = "0"
                    };
                }
            } while (result.Status != "1");

            return result;
        }
    }
}
