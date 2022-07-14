using System.Linq;
using System;
using WebApi.DBOperations;
using AutoMapper;

namespace WebApi.BookOperations
{
    public class DeleteBookCommand{
        public int DeleteId { get; set; }
        private readonly BookStoreDbContext _dbContext;

        public DeleteBookCommand(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle(){
            var result =_dbContext.Books.SingleOrDefault(book=>book.Id==DeleteId);
            if(result is null)            
                throw new InvalidOperationException("Bu isimde kitap yok");
            _dbContext.Books.Remove(result);
            _dbContext.SaveChanges();
        }
    
    }
}