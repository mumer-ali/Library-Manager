using Microsoft.AspNetCore.Mvc;
using server.Data;
using AutoMapper;
using server.DTOs.Book;
using server.Models;
using Microsoft.AspNetCore.Authorization;

namespace server.Controllers
{
    [Authorize]
    [Route("api/books")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly LibraryContext _context;
        private readonly IMapper _mapper;
        public BookController(LibraryContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var books = _context.Books.ToList().Select(book => _mapper.Map<Book, GetBookDTO>(book));
            return Ok(books);
        }
        [HttpGet("{bookid}")]
        public IActionResult GetById([FromRoute] string bookid)
        {
            var book = _context.Books.FirstOrDefault(a => a.BookId == bookid);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<Book, GetBookDTO>(book));
        }
        [HttpPost("add")]
        public IActionResult Add([FromBody] GetBookDTO bookDTO)
        {
            var book = _mapper.Map<GetBookDTO, Book>(bookDTO);
            _context.Books.Add(book);
            _context.SaveChanges();
            return Ok(_mapper.Map<Book, GetBookDTO>(book));
        }
        [HttpDelete("remove/{bookid}")]
        public IActionResult Remove([FromRoute] string bookid)
        {
            var book = _context.Books.FirstOrDefault(x => x.BookId == bookid);
            if (book == null)
            {
                return NotFound();
            }
            _context.Books.Remove(book);
            _context.SaveChanges();
            return NoContent();
        }
        [HttpPut("update/{bookid}")]
        public IActionResult Update([FromRoute] string bookid, [FromBody] BorrowBookDTO updatedBookDTO)
        {
            var existingBook = _context.Books.FirstOrDefault(b => b.BookId == bookid);
            if (existingBook == null)
            {
                return NotFound();
            }
            var existingMember = _context.Members.FirstOrDefault(m => m.Username == updatedBookDTO.BorrowedBy);
            if (existingMember == null)
            {
                return NotFound();
            }
            // Update the non-key properties of the existing book
            existingBook.Status = updatedBookDTO.Status;
            existingBook.BorrowedBy = existingMember;
            existingBook.BorrowDate = updatedBookDTO.BorrowDate;
            existingBook.ReturnDate = updatedBookDTO.ReturnDate;
            _context.SaveChanges();
            return Ok(_mapper.Map<Book, GetBookDTO>(existingBook));
        }
    }
}