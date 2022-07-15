using System;
using System.Linq;
using BookStore.DBOperations;

namespace BookStore.Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommand
    {
        private readonly BookStoreDbContext _context;

        public UpdateAuthorCommand(BookStoreDbContext context)
        {
            _context = context;
        }

        public int AuthorId { get; set; }
        public UpdateAuthorModel Model { get; set; }

        public void Handle()
        {
            var author = _context.Authors.SingleOrDefault(x => x.Id == AuthorId);
            if (author is null)
                throw new InvalidOperationException("Boyle bir yazar yok.");

            if (_context.Authors.Any(x =>
                (x.FirstName + x.LastName).ToLower() == (Model.FirstName + Model.LastName).ToLower())) ;

            author.FirstName = Model.FirstName;
            author.LastName = Model.LastName;
            author.DateOfBirth = Model.DateOfBirth;
            _context.SaveChanges();
        }
    }

    public class UpdateAuthorModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}