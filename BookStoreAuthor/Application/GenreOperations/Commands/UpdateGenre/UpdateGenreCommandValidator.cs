﻿using FluentValidation;

namespace BookStoreAuthor.Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommandValidator : AbstractValidator<UpdateGenreCommand>
    {
        public UpdateGenreCommandValidator()
        {
            RuleFor(command => command.Model.Name).MinimumLength(2).When(x => x.Model.Name != string.Empty);
        }
    }
}