namespace server.DTOs.Book
{
    public class GetBookDTO
    {
        public string BookId { get; set; } = string.Empty;
        public string BookName { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
    }
}