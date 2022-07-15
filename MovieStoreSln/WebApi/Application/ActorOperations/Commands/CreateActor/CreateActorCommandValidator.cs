using FluentValidation;

namespace WebApi.Application.ActorOperations.Commands.CreateActor
{
    public class CreateActorCommandValidator : AbstractValidator<CreateActorCommand>
    {
        public CreateActorCommandValidator()
        {
            RuleFor(command => command.Model.FirstName).NotEmpty().MinimumLength(2);
            RuleFor(command => command.Model.LastName).NotEmpty().MinimumLength(2);
        }
    }
}