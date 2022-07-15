using System;
using System.Linq;
using AutoMapper;
using BookStoreAuthor.DBOperations;
using Microsoft.EntityFrameworkCore;

namespace BookStoreAuthor.Application.BookOperations.Queries.GetBook
{
    public class GetBookQuery
    {
        private readonly IBookStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetBookQuery(IBookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public int BookId { get; set; }

        public BookViewModel Handle()
        {
            var book = _dbContext.Books.Include(x => x.Genre).SingleOrDefault(x => x.Id == BookId);
            if (book is null)
                throw new InvalidOperationException("Boyle bir kitap yok");

            var vm = _mapper.Map<BookViewModel>(book);

            return vm;
        }

        public class BookViewModel
        {
            public string Title { get; set; }
            public int PageCount { get; set; }
            public string PublishDate { get; set; }
            public string Genre { get; set; }
        }
    }
}