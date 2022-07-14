using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.BookOperations.GetBookDetails
{
    public class GetBookDetailsValidator : AbstractValidator<GetBookDetailQuery>
    {
        public GetBookDetailsValidator()
        {
            RuleFor(query => query.GetById).GreaterThan(0);
        }
    }
}
