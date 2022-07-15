using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebApi.DbOperations;
using AutoMapper;
using FluentValidation.Results;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using WebApi.Application.CustomerOperations.Commands.CreateCustomer;
using WebApi.TokenOperations.Models;
using WebApi.Application.TokenOperations.Commands.CreateToken;
using Microsoft.AspNetCore.Authorization;
using WebApi.Application.CustomerOperations.Commands.RefreshToken;
using WebApi.Application.CustomerOperations.Queries.GetCustomerDetail;

namespace WebApi.Controllers
{

    [ApiController]
    [Route("[controller]s")]
    public class CustomerController : ControllerBase
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;
        readonly IConfiguration _configuration;

        public CustomerController(IMovieStoreDbContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateCustomerModel newCustomer)
        {
            CreateCustomerCommand command = new CreateCustomerCommand(_context, _mapper);
            command.Model = newCustomer;
            command.Handle();

            return Ok();
        }

        [HttpPost("connect/token")]
        public ActionResult<Token> CreateToken([FromBody] CreateTokenModel login)
        {
            CreateTokenCommand command = new CreateTokenCommand(_context, _mapper, _configuration);
            command.Model = login;
            var token = command.Handle();

            return token;

        }

        [HttpGet("refreshToken")]
        public ActionResult<Token> RefreshToken([FromQuery] string token)
        {
            RefreshTokenCommand command = new RefreshTokenCommand(_context, _configuration);
            command.RefreshToken = token;
            var resultToken = command.Handle();

            return resultToken;

        }

        [HttpGet("{id}")]
        public IActionResult GetCustomerDetail(int id)
        {
            GetCustomerDetailQuery query = new GetCustomerDetailQuery(_context, _mapper);
            query.CustomerId = id;
            var result = query.Handle();
            return Ok(result);
        }
    }
}