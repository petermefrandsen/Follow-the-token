using Common.Models;

namespace Service.Tests
{
    public class BscScanApiServiceTests
    {
        private readonly HttpTest _httpTest;
        private readonly Mock<IConfiguration> _iconfigurationMock;
        private readonly string _bscScanBaseUrl = "http://www.example.com";
        private readonly string _bscScanApiKey = "APIKEY";

        public BscScanApiServiceTests()
        {
            _httpTest = new HttpTest();

            // Mocking the ASP.NET IConfiguration for getting the connection string from appsettings.json
            var mockConnectionStringSection = new Mock<IConfigurationSection>();
            mockConnectionStringSection.SetupGet(m => m[It.Is<string>(s => s == "BscScanBaseUrl")]).Returns(_bscScanBaseUrl);
            var mockConfSection = new Mock<IConfigurationSection>();
            mockConfSection.Setup(x => x.Value).Returns(_bscScanApiKey);

            _iconfigurationMock = new Mock<IConfiguration>();
            _iconfigurationMock.Setup(a => a.GetSection(It.Is<string>(s => s == "ConnectionStrings"))).Returns(mockConnectionStringSection.Object);
            _iconfigurationMock.SetupGet(x => x[It.Is<string>(s => s == "BscScanApiKey")]).Returns(_bscScanApiKey);
        }

        internal void Dispose()
        {
            _httpTest.Dispose();
        }

        [Fact]
        public void GetTokenTransferEventsForAddress_TestConfigurationGetsSet()
        {
            // ASSERT
            _iconfigurationMock.Object["BscScanApiKey"].Should().Be(_bscScanApiKey);
            _iconfigurationMock.Object.GetConnectionString("BscScanBaseUrl").Should().Be(_bscScanBaseUrl);
        }

        [Fact]
        public async Task GetTokenTransferEventsForAddress_ReturnsStatusMessageZero_WhenExceptionOtherThanApiLimit()
        {
            // ARANGE
            var bscScanApiService = new BscScanApiService(_iconfigurationMock.Object);
            _httpTest.RespondWith("server error", 500);
            var address = "testAddress";
            var offset = 0;
            var page = 0;
            var contract = "contract";
            var retryLimit = 5;

            // ACT
            var result = await bscScanApiService.GetTokenTransferEventsForAddress(address, offset, page, contract, retryLimit);

            // ASSERT
            result.Status.Should().Be("0");
            _httpTest.ShouldHaveCalled(_bscScanBaseUrl).Times(retryLimit);
        }

        [Fact]
        public async Task GetTokenTransferEventsForAddress_ReturnsCorrectObject_WhenUrlIsCalled()
        {
            var bscScanApiService = new BscScanApiService(_iconfigurationMock.Object);
            var expectedResult = new List<BscScanTokenTransfer>()
            {
                new BscScanTokenTransfer {
                    BlockNumber= "testValue",
                    TimeStamp= "testValue",
                    Hash= "testValue",
                    Nonce= "testValue",
                    BlockHash= "testValue",
                    From= "testValue",
                    ContractAddress= "testValue",
                    To= "testValue",
                    Value= "testValue",
                    TokenName= "testValue",
                    TokenSymbol= "testValue",
                    TokenDecimal= "testValue",
                    TransactionIndex= "testValue",
                    Gas= "testValue",
                    GasPrice= "testValue",
                    GasUsed= "testValue",
                    CumulativeGasUsed= "testValue",
                    Input= "testValue",
                    Confirmations= "testValue"
                }
            };
            
            _httpTest.RespondWithJson(new BscScanResult<List<BscScanTokenTransfer>>
            {
                Message = "",
                Status = "1",
                Result = expectedResult
            });

            var address = "testAddress";
            var offset = 0;
            var page = 0;
            var contractAddress = "contract";
            var retryLimit = 5;

            // ACT
            var result = await bscScanApiService.GetTokenTransferEventsForAddress(address, offset, page, contractAddress, retryLimit);

            // ASSERT
            _httpTest.ShouldHaveCalled(_bscScanBaseUrl)
                .WithQueryParams(new
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
                });
            result.Result.Should().NotBeNull();
            result.Result?.Count.Should().Be(1);
            result.Result?.Should().BeEquivalentTo(expectedResult);
        }

    }
}