using System;
using FluentValidation;

namespace WebApi.Application.MovieOperations.Commands.CreateMovie
{
    public class CreateMovieCommandValidator : AbstractValidator<CreateMovieCommand>
    {
        public CreateMovieCommandValidator()
        {
            RuleFor(command => command.Model.Title).NotEmpty().MinimumLength(2);
            RuleFor(command => command.Model.Price).GreaterThanOrEqualTo(0);
            RuleFor(command => command.Model.GenreId).GreaterThan(0);
            RuleFor(command => command.Model.DirectorId).GreaterThan(0);
            RuleFor(command => command.Model.ReleaseDate).GreaterThan(1500).LessThanOrEqualTo(DateTime.Now.Year);
        }
    }
}