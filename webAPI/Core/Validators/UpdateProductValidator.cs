using DataAccess;
using FluentValidation;
using webAPI.Dto;

namespace webAPI.Core.Validators
{
    public class UpdateProductValidator : AbstractValidator<ProductDto>
    {
        private readonly TerminContext _context;
        public UpdateProductValidator(TerminContext context)
        {
            RuleFor(x => x.Name)
                 .NotEmpty()
                 .WithMessage("Name is a required parameter")
                 .Must((dto, name) => !context.Products.Any(p => p.Name == name && p.Id == dto.Id ))
                 .WithMessage(p => $"Product with the name of {p.Name} already exists in the database.");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than 0.");

        }
    }
}
