using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApi.Application.MovieActorOperations.Commands.CreateMovieActor;
using WebApi.Application.MovieActorOperations.Commands.DeleteMovieActor;
using WebApi.Application.MovieActorOperations.Queries.GetActorMovies;
using WebApi.Application.MovieActorOperations.Queries.GetMovieActors;
using WebApi.DbOperations;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class MovieActorController : ControllerBase
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public MovieActorController(IMovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetMovieActors(int movieId, int actorId)
        {
            if (actorId == 0)
            {
                GetMovieActorsQuery query = new GetMovieActorsQuery(_context, _mapper);
                query.MovieId = movieId;
                var result = query.Handle();
                return Ok(result);
            }
            else if (movieId == 0)
            {
                GetActorMoviesQuery query = new GetActorMoviesQuery(_context, _mapper);
                query.ActorId = actorId;
                var result = query.Handle();
                return Ok(result);
            }
            else
            {
                return BadRequest("ActorId yada MovieId girilmelidir!");
            }

        }

        [HttpPost]
        public IActionResult CreateMovieActor([FromBody] CreateMovieActorViewModel model)
        {
            CreateMovieActorCommand command = new CreateMovieActorCommand(_context, _mapper);
            command.Model = model;
            CreateMovieActorCommandValidator validator = new CreateMovieActorCommandValidator();
            validator.ValidateAndThrow(command);
            var result = command.Handle();
            if (result)
                return Ok();
            return BadRequest();
        }

        [HttpDelete]
        public IActionResult DeleteMovieActor(int movieId, int actorId)
        {
            DeleteMovieActorCommand command = new DeleteMovieActorCommand(_context);
            command.MovieId = movieId;
            command.ActorId = actorId;
            DeleteMovieActorCommandValidator validator = new DeleteMovieActorCommandValidator();
            validator.ValidateAndThrow(command);
            var result = command.Handle();
            if (result)
                return Ok();
            return BadRequest();
        }
    }
}