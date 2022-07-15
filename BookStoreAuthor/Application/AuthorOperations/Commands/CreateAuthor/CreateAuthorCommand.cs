using System;
using System.Linq;
using BookStoreAuthor.DBOperations;
using BookStoreAuthor.Entities;

namespace BookStoreAuthor.Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommand
    {
        private readonly BookStoreDbContext _context;

        public CreateAuthorCommand(BookStoreDbContext context)
        {
            _context = context;
        }

        public CreateAuthorModel Model { get; set; }

        public void Handle()
        {
            var author =
                _context.Authors.SingleOrDefault(x => x.FirstName + x.LastName == Model.FirstName + Model.LastName);
            if (author is not null)
                throw new InvalidOperationException("Yazar zaten mevcut");

            author = new Author();
            author.FirstName = Model.FirstName;
            author.LastName = Model.LastName;
            author.DateOfBirth = Model.DateofBirth;
            _context.Authors.Add(author);
            _context.SaveChanges();
        }
    }

    public class CreateAuthorModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateofBirth { get; set; }
    }
}