using AutoMapper;
using WebApi.Entities;
using System.Linq;
using WebApi.Application.ActorOperations.Queries.GetActors;
using WebApi.Application.MovieOperations.Queries.GetMovies;
using WebApi.Application.DirectorOperations.Queries.GetDirectors;
using WebApi.Application.MovieOperations.Queries.GetMovieDetail;
using WebApi.Application.DirectorOperations.Queries.GetDirectorDetail;
using WebApi.Application.ActorOperations.Queries.GetActorDetail;
using WebApi.Application.ActorOperations.Commands.CreateActor;
using WebApi.Application.MovieOperations.Commands.CreateMovie;
using WebApi.Application.DirectorOperations.Commands.CreateDirector;
using WebApi.Application.GenreOperations.Queries.GetGenres;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using WebApi.Application.GenreOperations.Commands.CreateGenre;
using static WebApi.Application.MovieActorOperations.Queries.GetMovieActors.GetMovieActorsQuery;
using static WebApi.Application.MovieActorOperations.Queries.GetActorMovies.GetActorMoviesQuery;
using WebApi.Application.MovieActorOperations.Commands.CreateMovieActor;
using WebApi.Application.CustomerOperations.Commands.CreateCustomer;
using WebApi.Application.CustomerOperations.Queries.GetCustomerDetail;
using WebApi.Application.OrderMovieOperations.Commands.CreateOrderMovie;
using WebApi.Application.OrderMovieOperations.Queries.GetOrderMovieDetail;
using WebApi.Application.OrderMovieOperations.Queries.GetOrderMoviesByCustomerIdDetail;

namespace WebApi.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // ***** Actor ***** //
            ////GetActors 
            CreateMap<Actor, ActorsViewModel>().ForMember(dest => dest.Movies, opt => opt.MapFrom(src => src.MovieActors.Select(ma => ma.Movie).ToList()));
            CreateMap<Movie, ActorsViewModel.ActorMoviesVm>();
            ////GetActorDetail
            CreateMap<Actor, ActorDetailViewModel>().ForMember(dest => dest.Movies, opt => opt.MapFrom(src => src.MovieActors.Select(ma => ma.Movie).ToList()));
            CreateMap<Movie, ActorDetailViewModel.ActorMoviesVm>();
            ////CreateActor
            CreateMap<CreateActorViewModel, Actor>();


            // ***** Movie ***** //
            ////GetMovies
            CreateMap<Movie, MoviesViewModel>().ForMember(dest => dest.Actors, opt => opt.MapFrom(src => src.MovieActors.Select(ma => ma.Actor).ToList()))
                                                .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name.ToString()))
                                                .ForMember(dest => dest.Director, opt => opt.MapFrom(src => src.Director.FullName.ToString()));
            CreateMap<Actor, MoviesViewModel.MovieActorVm>();
            ////GetMovieDetail
            CreateMap<Movie, MovieDetailViewModel>().ForMember(dest => dest.Actors, opt => opt.MapFrom(src => src.MovieActors.Select(ma => ma.Actor).ToList()))
                                                .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name.ToString()))
                                                .ForMember(dest => dest.Director, opt => opt.MapFrom(src => src.Director.FullName.ToString()));
            CreateMap<Actor, MovieDetailViewModel.MovieActorVm>();
            ///CreateMovie
            CreateMap<CreateMovieViewModel, Movie>();


            // ***** Director ***** //
            ////GetDirectors
            CreateMap<Director, DirectorsViewModel>().ForMember(dest => dest.Movies, opt => opt.MapFrom(src => src.Movies.ToList()));
            CreateMap<Movie, DirectorsViewModel.DirectorMoviesVm>();
            ////GetDirectorDetail
            CreateMap<Director, DirectorDetailViewModel>().ForMember(dest => dest.Movies, opt => opt.MapFrom(src => src.Movies.ToList()));
            CreateMap<Movie, DirectorDetailViewModel.DirectorMoviesVm>();
            ////CreateDirector
            CreateMap<CreateDirectorViewModel, Director>();


            // ***** Genre ***** //
            ////GetGenres
            CreateMap<Genre, GenresViewModel>();
            ////GetGenreDetail
            CreateMap<Genre, GenreDetailViewModel>();
            ////CreateGenre
            CreateMap<CreateGenreViewModel, Genre>();


            // ***** MovieActor ***** //
            ////GetMovieActors
            CreateMap<MovieActor, MovieActorsViewModel>().ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Actor.FullName))
                                                    .ForMember(dest => dest.ActorId, opt => opt.MapFrom(src => src.Actor.Id));
            ////GetActorMovies
            CreateMap<MovieActor, ActorMoviesViewModel>().ForMember(dest => dest.Director, opt => opt.MapFrom(src => src.Movie.Director.FullName))
                                                    .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Movie.Genre.Name))
                                                    .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Movie.Title))
                                                    .ForMember(dest => dest.ReleaseDate, opt => opt.MapFrom(src => src.Movie.ReleaseDate));
            ////CreateMovieActor
            CreateMap<CreateMovieActorViewModel, MovieActor>();


            // ***** Customer ***** //
            ////GetCustomerDetail
            CreateMap<Customer, CustomerDetailViewModel>().ForMember(dest => dest.FavoritGenres, opt => opt.MapFrom(src => src.CustomerFavoritGenres.Select(cfg => cfg.Genre).ToList()))
                                                    .ForMember(dest => dest.OrderMovies, opt => opt.MapFrom(src => src.OrderMovies.ToList()));
            CreateMap<Genre, CustomerDetailViewModel.CustomerFavoritGenreVm>();
            CreateMap<OrderMovie, CustomerDetailViewModel.OrderMovieVm>();
            CreateMap<Movie, CustomerDetailViewModel.MovieDetailVm>();
            ////CreateCustomer
            CreateMap<CreateCustomerModel, Customer>();


            // ***** OrderMovie ***** //
            ////CreateOrderMovie
            CreateMap<CreateOrderMovieModel, OrderMovie>();
            ////GetOrderMovieDetail
            CreateMap<OrderMovie, OrderMovieDetailViewModel>().ForMember(dest => dest.Customer, opt => opt.MapFrom(src => src.Customer.FullName))
                                                    .ForMember(dest => dest.Movie, opt => opt.MapFrom(src => src.Movie.Title));
            ////GetOrderMoviesByCustomerId
            CreateMap<OrderMovie, OrderMoviesByCustomerIdViewModel>().ForMember(dest => dest.Movie, opt => opt.MapFrom(src => src.Movie.Title));
        }
    }
}