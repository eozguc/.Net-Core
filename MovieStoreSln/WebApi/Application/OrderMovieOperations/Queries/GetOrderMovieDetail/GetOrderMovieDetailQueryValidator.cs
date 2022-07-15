using FluentValidation;

namespace WebApi.Application.OrderMovieOperations.Queries.GetOrderMovieDetail
{
    public class GetOrderMovieDetailQueryValidator : AbstractValidator<GetOrderMovieDetailQuery>
    {
        public GetOrderMovieDetailQueryValidator()
        {
            RuleFor(query => query.OrderId).GreaterThan(0);
            
        }
    }
}