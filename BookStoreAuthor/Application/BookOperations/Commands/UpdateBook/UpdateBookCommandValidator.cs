using FluentValidation;

namespace BookStoreAuthor.Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand>
    {
        public UpdateBookCommandValidator()
        {
            RuleFor(command => command.BookId).GreaterThan(0);
            RuleFor(command => command.Model.GenreId).GreaterThan(-1);
            RuleFor(c => c.Model.Title).NotEmpty().MinimumLength(1);
        }
    }
}