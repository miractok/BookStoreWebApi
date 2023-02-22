using WebApi.DBOperations;
using WebApi.Entities;

namespace TestSetup
{
    public static class Authors
    {
        public static void AddAuthors(this BookStoreDbContext context)
        {
            context.Authors.AddRange(
                new Author{NameSurname = "Halil İnalcık", AuthorDateOfBirth = new DateTime(1916,07,09)},
                new Author{NameSurname = "Orhan Pamuk", AuthorDateOfBirth = new DateTime(1952,06,07)},
                new Author{NameSurname = "Edgar Allan Poe",AuthorDateOfBirth = new DateTime(1809,01,19)});
        }
    }
}