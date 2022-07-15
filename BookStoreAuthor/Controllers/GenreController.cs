using AutoMapper;
using BookStoreAuthor.Application.GenreOperations.Commands.CreateGenre;
using BookStoreAuthor.Application.GenreOperations.Commands.DeleteGenre;
using BookStoreAuthor.Application.GenreOperations.Commands.UpdateGenre;
using BookStoreAuthor.Application.GenreOperations.Queries.GetGenreDetail;
using BookStoreAuthor.Application.GenreOperations.Queries.GetGenres;
using BookStoreAuthor.DBOperations;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAuthor.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class GenreController : Controller
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GenreController(IMapper mapper, IBookStoreDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        [HttpGet]
        public ActionResult GetGenres()
        {
            var query = new GetGenresQuery(_context, _mapper);
            var obj = query.Handle();
            return Ok(obj);
        }

        [HttpGet("id")]
        public ActionResult GetGenreDetail(int id)
        {
            var query = new GetGenreDetailQuery(_context, _mapper);
            query.GenreId = id;
            var validator = new GetGenreDetailQueryValidator();
            validator.ValidateAndThrow(query);

            var obj = query.Handle();
            return Ok(obj);
        }

        [HttpPost]
        public IActionResult AddGenre([FromBody] CreateGenreModel newGenre)
        {
            var command = new CreateGenreCommand(_context);
            command.Model = newGenre;

            var validator = new CreateGenreCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();
            return Ok();
        }

        [HttpPut("id")]
        public IActionResult UpdateGenre(int id, [FromBody] UpdateGenreModel updateGenre)
        {
            var command = new UpdateGenreCommand(_context);
            command.GenreId = id;
            command.Model = updateGenre;

            var validator = new UpdateGenreCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();
            return Ok();
        }

        [HttpDelete("id")]
        public IActionResult DeleteGenre(int id)
        {
            var command = new DeleteGenreCommand(_context);
            command.GenreId = id;

            var validator = new DeleteGenreCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();
            return Ok();
        }
    }
}