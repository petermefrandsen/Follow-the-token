using Common.Models;
using Common.Validators;
using Moq;
using Service;

namespace Logic.Tests
{
    public class Bep20LogicTests
    {
        private readonly Bep20Logic _bep20Logic;
        private readonly Mock<IBscScanApiService> _bscScanApiService;
        private readonly Bep20TokenTransactionRequestValidator _requestValidator;

        public Bep20LogicTests()
        {
            _bscScanApiService = new Mock<IBscScanApiService>();
            _requestValidator = new Bep20TokenTransactionRequestValidator();
            _bep20Logic = new Bep20Logic(_bscScanApiService.Object, _requestValidator);
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
    }
}