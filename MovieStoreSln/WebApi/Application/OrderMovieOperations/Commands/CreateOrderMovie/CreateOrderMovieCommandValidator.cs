using FluentValidation;

namespace WebApi.Application.OrderMovieOperations.Commands.CreateOrderMovie
{
    public class CreateOrderMovieCommandValidator : AbstractValidator<CreateOrderMovieCommand>
    {
        public CreateOrderMovieCommandValidator()
        {
            RuleFor(command => command.Model.CustomerId).GreaterThan(0);
            RuleFor(command => command.Model.MovieId).GreaterThan(0);
            RuleFor(command => command.Model.PurchasedPrice).GreaterThanOrEqualTo(0);
        }
    }
}