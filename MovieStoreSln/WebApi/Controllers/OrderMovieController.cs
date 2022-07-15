using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApi.Application.OrderMovieOperations.Commands.CreateOrderMovie;
using WebApi.Application.OrderMovieOperations.Commands.UpdateOrderMovie;
using WebApi.Application.OrderMovieOperations.Queries.GetOrderMovieDetail;
using WebApi.Application.OrderMovieOperations.Queries.GetOrderMoviesByCustomerIdDetail;
using WebApi.DbOperations;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]s")]
    public class OrderMovieController : ControllerBase
    {
        private readonly IMovieStoreDbContext _context;         // → readonly → değiştirilmesini istemiyorum
        private readonly IMapper _mapper;
        public OrderMovieController(IMovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult CreateOrderMovie([FromBody] CreateOrderMovieModel model)
        {
            CreateOrderMovieCommand command = new CreateOrderMovieCommand(_context, _mapper);
            command.Model = model;
            CreateOrderMovieCommandValidator validator = new CreateOrderMovieCommandValidator();
            validator.ValidateAndThrow(command);
            var result = command.Handle();
            if (result)
                return Ok();
            return BadRequest();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateOrderMovie(int id, [FromBody] UpdateOrderMovieModel model)
        {
            UpdateOrderMovieCommand command = new UpdateOrderMovieCommand(_context);
            command.OrderId = id;
            command.Model = model;

            UpdateOrderMovieCommandValidator validator = new UpdateOrderMovieCommandValidator();
            validator.ValidateAndThrow(command);

            var result = command.Handle();
            if (result)
                return Ok();
            return BadRequest();
        }

        [HttpGet("{id}")]
        public IActionResult GetOrderMovieDetail(int id)
        {
            GetOrderMovieDetailQuery query = new GetOrderMovieDetailQuery(_context, _mapper);
            query.OrderId = id;
            GetOrderMovieDetailQueryValidator validator = new GetOrderMovieDetailQueryValidator();
            validator.ValidateAndThrow(query);
            var result = query.Handle();
            return Ok(result);
        }

        [HttpGet]
        public IActionResult GetOrderMoviesByCutomerId(int customerId)
        {
            GetOrderMoviesByCutomerIdQuery query = new GetOrderMoviesByCutomerIdQuery(_context, _mapper);
            query.CustomerId = customerId;
            GetOrderMoviesByCustomerIdQueryValidator validator = new GetOrderMoviesByCustomerIdQueryValidator();
            validator.ValidateAndThrow(query);
            var result = query.Handle();
            return Ok(result);
        }
    }
}