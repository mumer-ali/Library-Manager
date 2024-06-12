using System.ComponentModel.DataAnnotations;

namespace server.Models
{
    public class Member
    {
        [Key]
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public List<Book> BorrowedBooks { get; set; } = new List<Book>();
    }
}