using FluentValidation;
using MinimalApi.BookBasicExample.Entities;

namespace MinimalApi.BookBasicExample.Validators
{
    public class BookValidator:AbstractValidator<Book>
    {
        public BookValidator()
        {
            RuleFor(b => b.Title).NotEmpty().WithMessage("Title is required");
            RuleFor(b => b.Author).NotEmpty().WithMessage("Author is required");
            RuleFor(b => b.Isbn).NotEmpty().WithMessage("Isbn is required");
            RuleFor(b => b.Isbn).GreaterThan(11) .WithMessage("Isbn min length 12");
            RuleFor(b => b.PageSize).GreaterThan(20).WithMessage("PageSize should be greater than 0");
            RuleFor(b => b.PublishDate).NotEmpty().WithMessage("PublishDate is required");
        }
    }
}
