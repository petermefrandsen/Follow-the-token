using Common.Models;
using Common.Validators;
using FluentValidation;
using FluentValidation.TestHelper;
using Xunit;

namespace Common.Tests
{
    public class Bep20TokenTransactionRequestValidatorTests
    {
        private readonly Bep20TokenTransactionRequestValidator _validator;

        public Bep20TokenTransactionRequestValidatorTests()
        {
            _validator = new Bep20TokenTransactionRequestValidator();
        }

        [Fact]
        public void Validator_ReturnsError_WhenAddressIsNull()
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
            var result = _validator.TestValidate(request);

            // ASSERT
            result.ShouldHaveValidationErrorFor(request => request.Address)
              .WithErrorMessage("'Address' must not be empty.")
              .WithSeverity(Severity.Error)
              .WithErrorCode("NotNullValidator");
        }

        [Fact]
        public void Validator_ReturnsError_WhenAddressIsEmpty()
        {
            // ARRANGE
            var request = new Bep20TokenTransactionRequest()
            {
                Address = "",
                Bep20TokenContract = "1234",
                ExplorationDepth = 0,
                IgnoreAddresses = null
            };

            // ACT
            var result = _validator.TestValidate(request);

            // ASSERT
            result.ShouldHaveValidationErrorFor(request => request.Address)
              .WithErrorMessage("'Address' must not be empty.")
              .WithSeverity(Severity.Error)
              .WithErrorCode("NotEmptyValidator");
        }

        [Fact]
        public void Validator_ReturnsError_WhenBEP20TokenContractIsNull()
        {
            // ARRANGE
            var request = new Bep20TokenTransactionRequest()
            {
                Address = "1234",
                Bep20TokenContract = null,
                ExplorationDepth = 0,
                IgnoreAddresses = null
            };

            // ACT
            var result = _validator.TestValidate(request);

            // ASSERT
            result.ShouldHaveValidationErrorFor(request => request.Bep20TokenContract)
              .WithErrorMessage("'Bep20 Token Contract' must not be empty.")
              .WithSeverity(Severity.Error)
              .WithErrorCode("NotNullValidator");
        }

        [Fact]
        public void Validator_ReturnsError_WhenBEP20TokenContractIsEmpty()
        {
            // ARRANGE
            var request = new Bep20TokenTransactionRequest()
            {
                Address = "1234",
                Bep20TokenContract = "",
                ExplorationDepth = 0,
                IgnoreAddresses = null
            };

            // ACT
            var result = _validator.TestValidate(request);

            // ASSERT
            result.ShouldHaveValidationErrorFor(request => request.Bep20TokenContract)
              .WithErrorMessage("'Bep20 Token Contract' must not be empty.")
              .WithSeverity(Severity.Error)
              .WithErrorCode("NotEmptyValidator");
        }
    }
}