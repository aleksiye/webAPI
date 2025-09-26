using DataAccess;
using FluentValidation;
using webAPI.Dto;

namespace webAPI.Core.Validators
{
    public class CreateProductValidator : AbstractValidator<ProductDto>
    {
        private readonly TerminContext _context;
        public CreateProductValidator(TerminContext context) {
            _context = context;
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name is a required parameter")
                .Must(name => !_context.Products.Any(p => p.Name == name))
                .WithMessage(p => $"Product with the name of {p.Name} already exists in the database.");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than 0.");
        }
    }
}
