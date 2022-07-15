using FluentValidation;

namespace WebApi.Application.ActorOperations.Commands.DeleteActor
{
    public class DeleteActorCommandValidator : AbstractValidator<DeleteActorCommand>
    {
        public DeleteActorCommandValidator()
        {
            RuleFor(command => command.ActorId).GreaterThan(0);
        }
    }
}