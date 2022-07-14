using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.BookOperations.DeleteBookValidator
{
    public class DeleteBookValidator :AbstractValidator<DeleteBookCommand>
    {
        public DeleteBookValidator()
        {
            RuleFor(book => book.DeleteId).GreaterThan(0);
        }
    }
}
