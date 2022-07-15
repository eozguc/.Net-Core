using System;
using FluentValidation;

namespace BookStore.Application.BookOperations.Commands.CreateBook
{
    public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
    {
        public CreateBookCommandValidator()
        {
            RuleFor(command => command.Model.GenreId).GreaterThan(-1);
            RuleFor(command => command.Model.PageCount).GreaterThan(0);
            RuleFor(c => c.Model.PublishDate).NotEmpty().LessThan(DateTime.Now.Date);
            RuleFor(c => c.Model.Title).NotEmpty().MinimumLength(1);
        }
    }
}