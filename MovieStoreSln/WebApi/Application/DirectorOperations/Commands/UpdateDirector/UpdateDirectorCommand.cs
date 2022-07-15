using System;
using System.Linq;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.DirectorOperations.Commands.UpdateDirector
{
    public class UpdateDirectorCommand
    {
        public int DirectorId { get; set; }
        public UpdateDirectorViewModel Model { get; set; }
        private readonly IMovieStoreDbContext _dbContext;

        public UpdateDirectorCommand(IMovieStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool Handle(){
            var director = _dbContext.Directors.SingleOrDefault(a => a.Id == DirectorId);
            if(director is null)
                throw new InvalidOperationException("Güncellemeye çalıştığınız yönetmen bulunamadı!");
            director.FirstName = Model.FirstName == default ? director.FirstName : Model.FirstName;
            director.LastName = Model.LastName == default ? director.LastName : Model.LastName;
            director.IsActive = Model.IsActive == default ? director.IsActive : Model.IsActive;

            int result = _dbContext.SaveChanges();
            return Convert.ToBoolean(result);
        }
    }

    public class UpdateDirectorViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsActive { get; set; }
    }
}