using Common.Models;
using Common.Validators;
using Moq;
using Service;

namespace Logic.Tests
{
    public class Bep20LogicTests
    {
        private readonly Bep20Logic _bep20Logic;
        private readonly Mock<IBscScanApiService> _bscScanApiServiceMock;
        private readonly Bep20TokenTransactionRequestValidator _requestValidator;

        public Bep20LogicTests()
        {
            _bscScanApiServiceMock = new Mock<IBscScanApiService>();
            _requestValidator = new Bep20TokenTransactionRequestValidator();
            _bep20Logic = new Bep20Logic(_bscScanApiServiceMock.Object, _requestValidator);
        }

        [Fact]
        public async Task GetTransactionsForAddress_ThrowsException_WhenBscScanApiThrowsException()
        {
            // ARRANGE
            var request = new Bep20TokenTransactionRequest()
            {
                Address = "1234",
                Bep20TokenContract = "1234",
                ExplorationDepth = 0,
                IgnoreAddresses = null
            };
            _bscScanApiServiceMock.Setup(mock => mock.GetTokenTransferEventsForAddress(request.Address, 1000, 1, request.Bep20TokenContract, 10, 200))
                .ThrowsAsync(new Exception());

            // ACT
            Func<Task> act = () => _bep20Logic.GetTransactionsForAddress(request.Address, request.Bep20TokenContract);

            // ASSERT
            await act.Should().ThrowAsync<Exception>();
        }

        [Fact]
        public async Task GetTransactionsForAddress_ReturnsList_WhenOnePage()
        {
            // ARRANGE
            var request = new Bep20TokenTransactionRequest()
            {
                Address = "1234",
                Bep20TokenContract = "1234",
                ExplorationDepth = 0,
                IgnoreAddresses = null
            };
            _bscScanApiServiceMock.SetupSequence(mock => mock.GetTokenTransferEventsForAddress(request.Address, 1000, It.IsAny<int>(), request.Bep20TokenContract, 10, 200))
                .ReturnsAsync(GetBscScanResult(1))
                .ReturnsAsync(new BscScanResult<List<BscScanTokenTransfer>>
                {
                    Message = "",
                    Status = "1",
                    Result = new List<BscScanTokenTransfer>()
                });

            // ACT
            var result = await _bep20Logic.GetTransactionsForAddress(request.Address, request.Bep20TokenContract);

            // ASSERT
            result.Should().HaveCount(1);
        }

        [Fact]
        public async Task GetTransactionsForAddress_ReturnsList_WhenTwoPages()
        {
            // ARRANGE
            var request = new Bep20TokenTransactionRequest()
            {
                Address = "1234",
                Bep20TokenContract = "1234",
                ExplorationDepth = 0,
                IgnoreAddresses = null
            };
            _bscScanApiServiceMock.SetupSequence(mock => mock.GetTokenTransferEventsForAddress(request.Address, 1000, It.IsAny<int>(), request.Bep20TokenContract, 10, 200))
                .ReturnsAsync(GetBscScanResult(1))
                .ReturnsAsync(GetBscScanResult(2))
                .ReturnsAsync(new BscScanResult<List<BscScanTokenTransfer>>
                {
                    Message = "",
                    Status = "1",
                    Result = new List<BscScanTokenTransfer>()
                });

            // ACT
            var result = await _bep20Logic.GetTransactionsForAddress(request.Address, request.Bep20TokenContract);

            // ASSERT
            result.Should().HaveCount(2);
        }

        [Fact]
        public async Task GetTransactionsForAddress_ReturnsEmptyList_WhenFirstResultIsNull()
        {
            // ARRANGE
            var request = new Bep20TokenTransactionRequest()
            {
                Address = "1234",
                Bep20TokenContract = "1234",
                ExplorationDepth = 0,
                IgnoreAddresses = null
            };

            // ACT
            var result = await _bep20Logic.GetTransactionsForAddress(request.Address, request.Bep20TokenContract);

            // ASSERT
            result.Should().HaveCount(0);
        }

        [Fact]
        public async Task GetTransactionsForAddress_ReturnsOneItemList_WhenSecondResultStatusIsNull()
        {
            // ARRANGE
            var request = new Bep20TokenTransactionRequest()
            {
                Address = "1234",
                Bep20TokenContract = "1234",
                ExplorationDepth = 0,
                IgnoreAddresses = null
            };
            _bscScanApiServiceMock.SetupSequence(mock => mock.GetTokenTransferEventsForAddress(request.Address, 1000, It.IsAny<int>(), request.Bep20TokenContract, 10, 200))
                .ReturnsAsync(GetBscScanResult(1))
                .ReturnsAsync(new BscScanResult<List<BscScanTokenTransfer>>
                {
                    Message = "",
                    Status = null
                });

            // ACT
            var result = await _bep20Logic.GetTransactionsForAddress(request.Address, request.Bep20TokenContract);

            // ASSERT
            result.Should().HaveCount(1);
        }


        [Fact]
        public async Task GetBep20TokenTransactions_ThrowsException_WhenInvalidRequest()
        {
            // ARRANGE
            var request = new Bep20TokenTransactionRequest()
            {
                Address = null,
                Bep20TokenContract = "1234",
                ExplorationDepth = 0,
                IgnoreAddresses = null
            };

            // ACT
            Func<Task> act = () => _bep20Logic.GetBep20TokenTransactions(request);

            // ASSERT
            await act.Should().ThrowAsync<FluentValidation.ValidationException>();
        }

        [Fact]
        public async Task GetBep20TokenTransactions_ReturnsReponseWithEmptyLists_WhenAddressHasNoTransactions()
        {
            // ARRANGE
            var request = new Bep20TokenTransactionRequest()
            {
                Address = "1234",
                Bep20TokenContract = "1234",
                ExplorationDepth = 0,
                IgnoreAddresses = null
            };
            _bscScanApiServiceMock.SetupSequence(mock => mock.GetTokenTransferEventsForAddress(request.Address, 1000, It.IsAny<int>(), request.Bep20TokenContract, 10, 200))
                .ReturnsAsync(new BscScanResult<List<BscScanTokenTransfer>>
                {
                    Message = "",
                    Status = "1",
                    Result = new List<BscScanTokenTransfer>()
                });

            // ACT
            var result = await _bep20Logic.GetBep20TokenTransactions(request);

            // ASSERT
            result.Address.Should().Be(request.Address);
            result.Transactions.Should().HaveCount(0);
            result.SubAddressTransactions.Should().HaveCount(0);
        }

        private static BscScanResult<List<BscScanTokenTransfer>> GetBscScanResult(int id)
        {
            return new BscScanResult<List<BscScanTokenTransfer>>
            {
                Message = "",
                Status = "1",
                Result = new List<BscScanTokenTransfer>()
                    {
                        new BscScanTokenTransfer {
                            BlockNumber= "" + id,
                            TimeStamp= "" + id,
                            Hash= "" + id,
                            Nonce= "" + id,
                            BlockHash= "" + id,
                            From= "" + id,
                            ContractAddress= "" + id,
                            To= "" + id,
                            Value= "" + id,
                            TokenName= "" + id,
                            TokenSymbol= "" + id,
                            TokenDecimal= "" + id,
                            TransactionIndex= "" + id,
                            Gas= "" + id,
                            GasPrice= "" + id,
                            GasUsed= "" + id,
                            CumulativeGasUsed= "" + id,
                            Input= "" + id,
                            Confirmations= "" + id
                        }
                    }
            };
        }
    }
}