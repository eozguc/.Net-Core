using FluentValidation;

namespace WebApi.Application.MovieActorOperations.Commands.CreateMovieActor
{
    public class CreateMovieActorCommandValidator : AbstractValidator<CreateMovieActorCommand>
    {
        public CreateMovieActorCommandValidator()
        {
            RuleFor(command => command.Model.ActorId).GreaterThan(0);
            RuleFor(command => command.Model.MovieId).GreaterThan(0);
        }
    }
}