using System.Collections.Generic;
using System.Linq;
using WebApi.DBOperations;
using WebApi.Common;
using AutoMapper;

namespace WebApi.BookOperations.GetBooks{
    public class GetBooksQuery{

        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetBooksQuery(BookStoreDbContext dbContext , IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

         public List<BooksViewModel> Handle(){
            var bookList = _dbContext.Books.OrderBy(x=>x.Id).ToList<Book>();
            List<BooksViewModel> booksViewModel = _mapper.Map<List<BooksViewModel>>(bookList);
           // booksViewModel = new List<BooksViewModel>();
           //foreach (var book in bookList){
           //    booksViewModel.Add(new BooksViewModel{
           //        Title =book.Title,
           //        Genre =((GenreEnum)book.GenreId).ToString(),
           //        PublishDate =book.PublishDate.Date.ToString("dd/mm/yyyy"),
           //        PageCount =book.PageCount
           //    });
           //}
            return booksViewModel;
        
        }
    }

    public class BooksViewModel{
        public string Title  { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
        public string Genre { get; set; }
    }
}