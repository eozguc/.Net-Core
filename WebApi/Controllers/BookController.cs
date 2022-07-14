using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System;
using WebApi.BookOperations;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.DeleteBookValidator;
using WebApi.BookOperations.GetBookDetails;
using WebApi.BookOperations.GetBooks;
using WebApi.BookOperations.UpdateBook;
using WebApi.DBOperations;
namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public BookController(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetBooks()
        {
            GetBooksQuery query = new GetBooksQuery(_context, _mapper);
            var result = query.Handle();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            BookDetailViewModel result;
            try
            {
                GetBookDetailQuery getBookDetail = new GetBookDetailQuery(_context, _mapper);
                getBookDetail.GetById = id;
                GetBookDetailsValidator validator = new GetBookDetailsValidator();
                validator.ValidateAndThrow(getBookDetail);
                result = getBookDetail.Handle();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
            return Ok(result);
        }

        /*  [HttpGet]
         public Book GetById([FromQuery] string id)
         {
            var result =BookList.Where(book=>book.Id ==Convert.ToInt32(id)).SingleOrDefault();
            return result;
         }*/

        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel newBook)
        {

            CreateBookCommand command = new CreateBookCommand(_context, _mapper);

            try
            {
                command.Model = newBook;
                CreateBookValidator validator = new CreateBookValidator();
                //ValidationResult validationResult =
                validator.ValidateAndThrow(command);
                command.Handle();
             

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel updatedBook)
        {
            try
            {
                UpdateBookCommand command = new UpdateBookCommand(_context);
                command.UpdateBookId = id;
                command.Model = updatedBook;

                UpdateBookValidator validator = new UpdateBookValidator();
                validator.ValidateAndThrow(command);
                command.Handle();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
            return Ok();


        }

        [HttpDelete]
        public IActionResult DeleteBook(int id)
        {

            try
            {
                DeleteBookCommand deleteBook = new DeleteBookCommand(_context);
                deleteBook.DeleteId = id;
                DeleteBookValidator validator = new DeleteBookValidator();
                validator.ValidateAndThrow(deleteBook);
                deleteBook.Handle();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

            return Ok();
        }

    }
}