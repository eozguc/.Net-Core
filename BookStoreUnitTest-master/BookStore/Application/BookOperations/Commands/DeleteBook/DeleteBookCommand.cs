using System;
using System.Linq;
using BookStore.DBOperations;

namespace BookStore.Application.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommand
    {
        private readonly IBookStoreDbContext _dbContext;

        public DeleteBookCommand(IBookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int bookId { get; set; }

        public void Handle()
        {
            var book = _dbContext.Books.SingleOrDefault(x => x.Id == bookId);
            if (book is null)
                throw new InvalidOperationException("Bu isimde kitap yok");

            _dbContext.Books.Remove(book);
            _dbContext.SaveChanges();
        }
    }
}