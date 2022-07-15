using FluentValidation;

namespace WebApi.Application.ActorOperations.Commands.UpdateActor
{
    public class UpdateActorCommandValidator : AbstractValidator<UpdateActorCommand>
    {
        public UpdateActorCommandValidator()
        {
            RuleFor(command => command.Model.FirstName).MinimumLength(2).When(x=> x.Model.FirstName.Trim() != string.Empty);
            RuleFor(command => command.Model.LastName).MinimumLength(2).When(x=> x.Model.LastName.Trim() != string.Empty);
            RuleFor(command => command.ActorId).GreaterThan(0);
        }
    }
}