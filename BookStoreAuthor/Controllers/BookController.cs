using AutoMapper;
using BookStoreAuthor.Application.BookOperations.Commands.CreateBook;
using BookStoreAuthor.Application.BookOperations.Commands.DeleteBook;
using BookStoreAuthor.Application.BookOperations.Commands.UpdateBook;
using BookStoreAuthor.Application.BookOperations.Queries.GetBook;
using BookStoreAuthor.Application.BookOperations.Queries.GetBooks;
using BookStoreAuthor.DBOperations;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAuthor.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class BookController : ControllerBase
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public BookController(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetBooks()
        {
            var query = new GetBooksQuery(_context, _mapper);
            var result = query.Handle();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            GetBookQuery.BookViewModel result;
            var query = new GetBookQuery(_context, _mapper);
            query.BookId = id;
            var validator = new GetBookQueryValidator();
            validator.ValidateAndThrow(query);
            result = query.Handle();

            return Ok(result);
        }

        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookCommand.CreateBookModel newBook)
        {
            var command = new CreateBookCommand(_context, _mapper);

            command.Model = newBook;
            var validator = new CreateBookCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();

            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] UpdateBookCommand.UpdateBookModel model)
        {
            var command = new UpdateBookCommand(_context);
            command.BookId = id;
            command.Model = model;
            var validator = new UpdateBookCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            var command = new DeleteBookCommand(_context);
            command.BookId = id;
            var validator = new DeleteBookCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();

            return Ok();
        }
    }
}