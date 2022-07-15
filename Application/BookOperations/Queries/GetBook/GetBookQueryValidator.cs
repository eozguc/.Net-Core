using FluentValidation;

namespace BookStore.Application.BookOperations.Queries.GetBook
{
    public class GetBookQueryValidator : AbstractValidator<GetBookQuery>
    {
        public GetBookQueryValidator()
        {
            RuleFor(command => command.bookId).GreaterThan(0);
        }
    }
}