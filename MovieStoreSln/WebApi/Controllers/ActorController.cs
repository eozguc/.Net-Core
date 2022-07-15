using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApi.Application.ActorOperations.Commands.CreateActor;
using WebApi.Application.ActorOperations.Commands.DeleteActor;
using WebApi.Application.ActorOperations.Commands.UpdateActor;
using WebApi.Application.ActorOperations.Queries.GetActorDetail;
using WebApi.Application.ActorOperations.Queries.GetActors;
using WebApi.DbOperations;

namespace WebApi.Controllers
{
    [ApiController]                                             // → http response döner
    [Route("[controller]s")]                                    // → kullanılacak yöntem
    public class ActorController : ControllerBase               // → Bütün Controller'lar ControllerBase'den kalıtım alır.
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public ActorController(IMovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetActors()
        {
            GetActorsQuery query = new GetActorsQuery(_context, _mapper);
            var result = query.Handle();
            return Ok(result);
        }

        [HttpGet("{id}")]       // → route'dan alır
        public IActionResult GetActorDetail(int id)
        {
            GetActorDetailQuery query = new GetActorDetailQuery(_context, _mapper);
            query.ActorId = id;
            GetActorDetailQueryValidator validator = new GetActorDetailQueryValidator();
            validator.ValidateAndThrow(query);
            var result = query.Handle();
            return Ok(result);
        }
        
        [HttpPost]
        public IActionResult CreateActor([FromBody] CreateActorViewModel model)
        {
            CreateActorCommand command = new CreateActorCommand(_context, _mapper);
            command.Model = model;
            CreateActorCommandValidator validator = new CreateActorCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateActor(int id, [FromBody] UpdateActorViewModel model)
        {
            UpdateActorCommand command = new UpdateActorCommand(_context);
            command.ActorId = id;
            command.Model = model;
            UpdateActorCommandValidator validator = new UpdateActorCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteActor(int id)
        {
            DeleteActorCommand command = new DeleteActorCommand(_context);
            command.ActorId = id;
            DeleteActorCommandValidator validator = new DeleteActorCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            
            return Ok();
        }

        // [HttpDelete("{id}")]
        // public IActionResult DeleteActor(int id)
        // {
        //     try
        //     {
        //         DeleteActorCommand command = new DeleteActorCommand(_context);
        //         command.ActorId = id;
        //         DeleteActorCommandValidator validator = new DeleteActorCommandValidator();
        //         validator.ValidateAndThrow(command);
        //         var result = command.Handle();
        //     }
        //     catch (Exception ex)
        //     {
        //         return BadRequest(ex.Message);
        //     }
        //     return Ok();
        // }
    }
}