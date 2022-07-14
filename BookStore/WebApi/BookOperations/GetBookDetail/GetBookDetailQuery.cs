using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.Common;
using WebApi.DBOperations;

namespace WebApi.BookOperations.GetBookDetail
{
    public class GetBookDetailQuery
    {
        private readonly BookStoreDbContext _dbContext; 
        public int BookId { get; set; } 
        public GetBookDetailQuery(BookStoreDbContext dbContext) 
        {
            _dbContext = dbContext;
        }
        public BookDetailViewModel Handle()
        {
            var book = _dbContext.Books.Where(book => book.Id == BookId).SingleOrDefault(); 
            BookDetailViewModel vm = new BookDetailViewModel();
            if (book is null) 
            {
                throw new InvalidOperationException("Kitap bulunamadı.");
            }
            vm.Title = book.Title;
            vm.PageCount = book.PageCount;
            vm.PublishDate = book.PublishDate.Date.ToString("dd/MM/yy");
            vm.Genre = book.GenreId.ToString();

            return vm;
        }
        public class BookDetailViewModel 
        {
            public string Title { get; set; }
            public int PageCount { get; set; }
            public string PublishDate { get; set; }
            public string Genre { get; set; }
        }
    }
}
