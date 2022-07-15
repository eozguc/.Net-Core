using FluentValidation;

namespace WebApi.Application.OrderMovieOperations.Commands.UpdateOrderMovie
{
    public class UpdateOrderMovieCommandValidator : AbstractValidator<UpdateOrderMovieCommand>
    {
        public UpdateOrderMovieCommandValidator()
        {
            RuleFor(command => command.OrderId).GreaterThan(0);

        }
    }
}