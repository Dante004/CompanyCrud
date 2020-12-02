using CompanyCrud.Models;
using FluentValidation;

namespace CompanyCrud.Logic.Validators
{
    public class CompanyValidator : AbstractValidator<Company>
    {
        public CompanyValidator()
        {
            RuleFor(x => x.ID)
                .NotNull();
            RuleFor(x => x.Name)
                .NotEmpty();
            RuleFor(x => x.Establishment)
                .InclusiveBetween(0, 2000);
        }
    }
}
