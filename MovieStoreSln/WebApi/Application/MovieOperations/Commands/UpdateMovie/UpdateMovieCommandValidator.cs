using System;
using FluentValidation;

namespace WebApi.Application.MovieOperations.Commands.UpdateMovie
{
    public class UpdateMovieCommandValidator : AbstractValidator<UpdateMovieCommand>
    {
        public UpdateMovieCommandValidator()
        {
            RuleFor(command => command.Model.Title).MinimumLength(2).When(x=> x.Model.Title.Trim() != string.Empty);
            RuleFor(command => command.Model.Price).GreaterThanOrEqualTo(0);
            RuleFor(command => command.Model.GenreId).GreaterThan(0);
            RuleFor(command => command.Model.DirectorId).GreaterThan(0);
            RuleFor(command => command.Model.ReleaseDate).GreaterThan(1500).LessThanOrEqualTo(DateTime.Now.Year);
            RuleFor(command => command.MovieId).GreaterThan(0);
        }
    }
}