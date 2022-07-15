using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DbOperations;

namespace WebApi.Application.DirectorOperations.Queries.GetDirectors
{
    public class GetDirectorsQuery
    {
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetDirectorsQuery(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<DirectorsViewModel> Handle()
        {
            var directors = _dbContext.Directors.Include(a=>a.Movies).OrderBy(x => x.Id);
            List<DirectorsViewModel> returnObj = _mapper.Map<List<DirectorsViewModel>>(directors);
            return returnObj;
        }
    }

    public class DirectorsViewModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public List<DirectorMoviesVm> Movies { get; set; }

        public struct DirectorMoviesVm
        {
            public int Id { get; set; }
            public string Title { get; set; }   
        }
    }
}