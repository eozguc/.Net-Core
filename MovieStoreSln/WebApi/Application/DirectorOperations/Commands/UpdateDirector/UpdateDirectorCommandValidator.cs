using FluentValidation;

namespace WebApi.Application.DirectorOperations.Commands.UpdateDirector
{
    public class UpdateDirectorCommandValidator : AbstractValidator<UpdateDirectorCommand>
    {
        public UpdateDirectorCommandValidator()
        {
            RuleFor(command => command.Model.FirstName).MinimumLength(2).When(x=> x.Model.FirstName.Trim() != string.Empty);
            RuleFor(command => command.Model.LastName).MinimumLength(2).When(x=> x.Model.LastName.Trim() != string.Empty);
            RuleFor(command => command.DirectorId).GreaterThan(0);
        }
    }
}