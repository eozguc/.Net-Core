using System.Collections.Generic;
using System.Linq;
using WebApi.Common;
using WebApi.DBOperations;

namespace WebApi.BookOperations.GetBooks
{
    public class GetBooksQuery
    {
        private readonly BookStoreDbContext _dbContext; 
        public GetBooksQuery(BookStoreDbContext dbContext) 
        {
            _dbContext = dbContext;
        }
        public List<BookViewModel> Handle() 
        {
            var bookList = _dbContext.Books.OrderBy(x => x.Id).ToList(); 
            List<BookViewModel> vm = new List<BookViewModel>(); 
            foreach (var book in bookList)
            {
                vm.Add(new BookViewModel
                {
                    Title = book.Title,
                    Genre = ((GenreEnum)book.GenreId).ToString(),
                    PublishDate = book.PublishDate.Date.ToString("dd/MM/yyyy"),
                    PageCount = book.PageCount
                });
            }
            return vm;
        }
    }
    public class BookViewModel 
    {
        public string Title { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
        public string Genre { get; set; }
    }
}
