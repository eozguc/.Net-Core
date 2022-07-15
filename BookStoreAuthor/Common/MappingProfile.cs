using AutoMapper;
using BookStoreAuthor.Application.AuthorOperations.Queries.GetAuthorDetail;
using BookStoreAuthor.Application.AuthorOperations.Queries.GetAuthors;
using BookStoreAuthor.Application.BookOperations.Commands.CreateBook;
using BookStoreAuthor.Application.BookOperations.Queries.GetBook;
using BookStoreAuthor.Application.BookOperations.Queries.GetBooks;
using BookStoreAuthor.Application.GenreOperations.Queries.GetGenreDetail;
using BookStoreAuthor.Application.GenreOperations.Queries.GetGenres;
using BookStoreAuthor.Entities;

namespace BookStoreAuthor.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateBookCommand.CreateBookModel, Book>();
            CreateMap<Book, GetBookQuery.BookViewModel>().ForMember(dest => dest.Genre,
                opt => opt.MapFrom(src => src.Genre.Name));
            CreateMap<Book, BooksViewModel>().ForMember(dest => dest.Genre,
                opt => opt.MapFrom(src => src.Genre.Name));
            CreateMap<Genre, GenresViewModel>();
            CreateMap<Genre, GenreDetailViewModel>();
            CreateMap<Author, AuthorsViewModel>().ForMember(dest => dest.Name,
                opt =>
                    opt.MapFrom(src => src.FirstName + " " + src.LastName));
            CreateMap<Author, AuthorDetailViewModel>().ForMember(dest => dest.Name,
                opt =>
                    opt.MapFrom(src => src.FirstName + " " + src.LastName));
        }
    }
}