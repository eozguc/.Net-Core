using System;
using FluentValidation;

namespace BookStore.Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandValidator : AbstractValidator<UpdateAuthorCommand>
    {
        public UpdateAuthorCommandValidator()
        {
            RuleFor(command => command.Model.FirstName).MinimumLength(2).When(x => x.Model.FirstName != string.Empty);
            RuleFor(command => command.Model.LastName).MinimumLength(2).When(x => x.Model.LastName != string.Empty);
            RuleFor(command => command.Model.DateOfBirth).NotEmpty().LessThan(DateTime.Now.Date);
        }
    }
}