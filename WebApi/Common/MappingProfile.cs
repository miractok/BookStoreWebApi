using AutoMapper;
using WebApi.Application.BookOperations.Queries.GetBooks;
using WebApi.Application.BookOperations.Queries.GetBookDetails;
using WebApi.Entities;
using static WebApi.Application.BookOperations.Command.CreateBook.CreateBookCommand;
using WebApi.Application.GenreOperations.Queries.GetGenres;
using WebApi.Application.GenreOperations.Queries.GetGenreDetails;
using WebApi.Application.AuthorOperations.Queries.GetAuthors;
using WebApi.Application.AuthorOperations.Queries.GetAuthorDetails;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using static WebApi.Application.UserOperations.Commands.CreateUser.CreateUserCommand;
using WebApi.Application.UserOperations.Queries.GetUsers;
using WebApi.Application.UserOperations.Queries.GetUserDetails;

namespace WebApi.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateBookModel ,Book>();
            CreateMap<Book, BooksViewIdModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name)).ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author.NameSurname));
            CreateMap<Book, BooksViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name)).ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author.NameSurname));
            CreateMap<Genre, GenresViewModel>();
            CreateMap<Genre, GenreDetailViewModel>();
            CreateMap<Author, AuthorsViewModel>();
            CreateMap<Author, AuthorDetailViewModel>();
            CreateMap<CreateAuthorModel, Author>();
            CreateMap<CreateUserModel, User>();
            CreateMap<User, UsersViewModel>();
            CreateMap<User, UserDetailViewModel>();
        }
    }
}