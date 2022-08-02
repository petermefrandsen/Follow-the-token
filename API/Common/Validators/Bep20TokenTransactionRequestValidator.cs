using Common.Models;
using FluentValidation;

namespace Common.Validators
{
    public class Bep20TokenTransactionRequestValidator : AbstractValidator<Bep20TokenTransactionRequest>
    {
        public Bep20TokenTransactionRequestValidator()
        {
            RuleFor(request => request.Address).NotNull().NotEmpty();
            RuleFor(request => request.Bep20TokenContract).NotNull().NotEmpty();
        }
    }
}
