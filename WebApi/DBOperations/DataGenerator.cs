using Microsoft.EntityFrameworkCore;
using WebApi.Entities;

namespace WebApi.DBOperations
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using ( var context = new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
            {
                if(context.Books.Any())
                {
                    return;
                }

                context.Authors.AddRange
                (
                    new Author
                    {
                        NameSurname = "Halil İnalcık",
                        AuthorDateOfBirth = new DateTime(1916,07,09)
                    },
                    new Author
                    {
                        NameSurname = "Orhan Pamuk",
                        AuthorDateOfBirth = new DateTime(1952,06,07)
                    },
                    new Author
                    {
                        NameSurname = "Edgar Allan Poe",
                        AuthorDateOfBirth = new DateTime(1809,01,19)
                    }
                );

                context.Genres.AddRange
                (
                    new Genre
                    {
                        Name = "Personal Growth",
                    },
                    new Genre
                    {
                        Name = "Science Fiction",
                    },
                    new Genre
                    {
                        Name = "Romance",
                    }
                );

                context.Books.AddRange
                (
                    new Book
                    {
                        Title = "Lean Stratup",
                        GenreId = 1, 
                        PageCount = 200,
                        PublishDate = new DateTime(2001,06,12),
                        AuthorId = 1
                    },
                    new Book
                    {
                        Title = "Herland",
                        GenreId = 2, 
                        PageCount = 250,
                        PublishDate = new DateTime(2010,05,23),
                        AuthorId = 2
                    },
                    new Book
                    {
                        Title = "Dune",
                        GenreId = 2, 
                        PageCount = 540,
                        PublishDate = new DateTime(2001,06,12),
                        AuthorId = 3
                    }
                );

                context.Users.AddRange
                (
                    new User
                    {
                        Name =  "Süleyman",
                        Surname = "Süleymanoğlu",
                        Email = "Süleyman@gmail.com",
                        Password =  "456789987654"
                    },
                    new User
                    {
                        Name =  "Muhammet",
                        Surname = "Muhammetoğlu",
                        Email = "Muhammet@gmail.com",
                        Password =  "987654321657"
                    }
                );

                context.SaveChanges();
            }
        } 
    }
}