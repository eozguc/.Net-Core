using System;
using System.Linq;
using WebApi.DBOperations;

namespace WebApi.BookOperations{
    public class UpdateBookCommand{
        public UpdateBookModel Model { get; set; }     
        public int UpdateBookId { get; set; }
        private readonly BookStoreDbContext _dbContext;

        public UpdateBookCommand(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
       public void Handle(){
         var book = _dbContext.Books.SingleOrDefault(x=>x.Id ==UpdateBookId);
          if(book is null)
           throw new InvalidOperationException("Boyle bir kitap yok");
 

        book.GenreId =Model.GenreId !=default ? Model.GenreId : book.GenreId;
        book.Title =Model.Title !=default ? Model.Title : book.Title;
        _dbContext.SaveChanges();
        }

    }
    public class UpdateBookModel{
        public int GenreId { get; set; }
        public string Title { get; set; }
        
    }
}