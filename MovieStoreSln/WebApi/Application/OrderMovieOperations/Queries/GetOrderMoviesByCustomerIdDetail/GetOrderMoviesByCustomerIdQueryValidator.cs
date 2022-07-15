using FluentValidation;

namespace WebApi.Application.OrderMovieOperations.Queries.GetOrderMoviesByCustomerIdDetail
{
    public class GetOrderMoviesByCustomerIdQueryValidator : AbstractValidator<GetOrderMoviesByCutomerIdQuery>
    {
        public GetOrderMoviesByCustomerIdQueryValidator()
        {
            RuleFor(query => query.CustomerId).GreaterThan(0);
            
        }
    }
}