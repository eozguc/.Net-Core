using FluentValidation;

namespace WebApi.Application.MovieActorOperations.Commands.DeleteMovieActor
{
    public class DeleteMovieActorCommandValidator : AbstractValidator<DeleteMovieActorCommand>
    {
        public DeleteMovieActorCommandValidator()
        {
            RuleFor(command => command.ActorId).GreaterThan(0);
            RuleFor(command => command.MovieId).GreaterThan(0);
        }
    }

}