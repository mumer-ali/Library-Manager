using System.ComponentModel.DataAnnotations;

namespace server.Models
{
    public class Book
    {
        [Key]
        public string BookId { get; set; } = string.Empty;
        public string BookName { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public Member? BorrowedBy { get; set; } 
        public DateTime? BorrowDate { get; set; } 
        public DateTime? ReturnDate { get; set; } 
    }
}