using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities
{
    public class Author
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string? NameSurname { get; set; }

        public DateTime AuthorDateOfBirth { get; set; }

        public bool IsActive { get; set; } = true;
        
    }
}