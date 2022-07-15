using AutoMapper;
using BookStore.Common;
using BookStore.DBOperations;
using Microsoft.EntityFrameworkCore;

namespace TestProject1.TestSetup
{
    public class CommonTestFixture
    {
        public CommonTestFixture()
        {
            var options = new DbContextOptionsBuilder<BookStoreDbContext>()
                .UseInMemoryDatabase("BookStoreTestDB").Options;
            Context = new BookStoreDbContext(options);

            Context.Database.EnsureCreated();
            Context.AddBooks();
            Context.AddGenres();
            Context.SaveChanges();

            Mapper = new MapperConfiguration(cfg => { cfg.AddProfile<MappingProfile>(); }).CreateMapper();
        }

        public BookStoreDbContext Context { get; set; }
        public IMapper Mapper { get; set; }
    }
}