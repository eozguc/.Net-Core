using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebApi.DBOperations;
using System;
using WebApi.BookOperations.GetBooks;
using WebApi.BookOperations.CreateBook;
using static WebApi.BookOperations.CreateBook.CreateBookCommand;
using WebApi.BookOperations.UpdateBook;
using WebApi.BookOperations.GetBookDetail;
using static WebApi.BookOperations.GetBookDetail.GetBookDetailQuery;
using WebApi.BookOperations.DeleteBook;

namespace WebApi.AddControllers 
{
    [ApiController] 
    [Route("[controller]s")] 
    public class BookController : ControllerBase 
    {
        private readonly BookStoreDbContext _context; 

        public BookController (BookStoreDbContext context) 
        {
            _context = context; 
        }
        
        [HttpGet]
        public IActionResult GetBooks() 
        {
            GetBooksQuery query = new GetBooksQuery(_context); 
            var result = query.Handle(); 
            return Ok(result); 
        }

        [HttpGet("{id}")] 
        public IActionResult GetById(int id) 
        {
            BookDetailViewModel result;
            try
            {
                GetBookDetailQuery query = new GetBookDetailQuery(_context);
                query.BookId = id;
                result = query.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(result);
        }


        //  [HttpGet]
        // public Book Get([FromQuery] string id){
        //     var book = BookList.Where(book=> book.Id == Convert.ToInt32(id)).SingleOrDefault();
        //     return book;
        // }

        

        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel newBook) 
        {
            CreateBookCommand command = new CreateBookCommand(_context); 
            try 
            {
                command.Model = newBook;
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
            UpdateBookCommand command = new UpdateBookCommand(_context);
            try
            {
                command.BookId = id;
                command.Model = updatedBook;
                command.Handle();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }  
            return Ok();
        }
        
        [HttpDelete]
        public IActionResult DeleteBook(int id)
        {
            DeleteBookCommand command = new DeleteBookCommand(_context);
            try
            {
                command.BookId = id;
                command.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            } 
            return Ok();
        }
    }
}