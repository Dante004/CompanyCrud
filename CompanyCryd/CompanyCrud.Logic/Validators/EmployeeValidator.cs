using CompanyCrud.Models;
using FluentValidation;

namespace CompanyCrud.Logic.Validators
{
    public class EmployeeValidator : AbstractValidator<Employee>
    {
        public EmployeeValidator()
        {
            RuleFor(x => x.ID)
                .NotNull();
            RuleFor(x => x.DateOfBirth)
                .NotNull();
        }
    }
}
