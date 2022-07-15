using FluentValidation;

namespace BookStoreAuthor.Application.BookOperations.Queries.GetBook
{
    public class GetBookQueryValidator : AbstractValidator<GetBookQuery>
    {
        public GetBookQueryValidator()
        {
            RuleFor(command => command.BookId).GreaterThan(0);
        }
    }
}