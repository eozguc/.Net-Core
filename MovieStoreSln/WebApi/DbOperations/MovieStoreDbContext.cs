using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Entities;

namespace WebApi.DbOperations
{
    public class MovieStoreDbContext : DbContext, IMovieStoreDbContext
    {
        public MovieStoreDbContext(DbContextOptions<MovieStoreDbContext> options) : base(options) { }

        // ***** Entities → MovieStoreDbContext ***** //
        public DbSet<Movie> Movies { get; set; }            // <tekil> Çoğul
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Director> Directors { get; set; }
        public DbSet<MovieActor> MovieActors { get; set; }
        public DbSet<CustomerFavoritGenre> CustomerFavoritGenres { get; set; }
        public DbSet<OrderMovie> OrderMovies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MovieActor>(ConfigureMovieActor);
            modelBuilder.Entity<CustomerFavoritGenre>(ConfigureCustomerFavoritGenre);

            base.OnModelCreating(modelBuilder);
        }

        private void ConfigureMovieActor(EntityTypeBuilder<MovieActor> modelBuilder)
        {
            //modelBuilder.ToTable("MovieActor");
            modelBuilder.HasKey(mc => new { mc.MovieId, mc.ActorId });
            modelBuilder.HasOne(mc => mc.Movie).WithMany(g => g.MovieActors).HasForeignKey(mg => mg.MovieId);
            modelBuilder.HasOne(mc => mc.Actor).WithMany(g => g.MovieActors).HasForeignKey(mg => mg.ActorId);
        }
        private void ConfigureCustomerFavoritGenre(EntityTypeBuilder<CustomerFavoritGenre> modelBuilder)
        {
            modelBuilder.HasKey(sc => new { sc.GenreId, sc.CustomerId });
            modelBuilder.HasOne<Customer>(am => am.Customer).WithMany(a => a.CustomerFavoritGenres).HasForeignKey(am => am.CustomerId);
            modelBuilder.HasOne<Genre>(am => am.Genre).WithMany(m => m.CustomerFavoritGenres).HasForeignKey(am => am.GenreId);
        }

        public override int SaveChanges() => base.SaveChanges();
    }
}