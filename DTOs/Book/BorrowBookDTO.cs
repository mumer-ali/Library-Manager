namespace server.DTOs.Book
{
    public class BorrowBookDTO
    {
        public string Status { get; set; } = string.Empty;
        public string BorrowedBy { get; set; } = string.Empty;
        public DateTime? BorrowDate { get; set; }
        public DateTime? ReturnDate { get; set; }
    }
}