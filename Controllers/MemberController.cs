using Microsoft.AspNetCore.Mvc;
using server.Data;
using AutoMapper;
using server.DTOs.Member;
using server.Models;
using Microsoft.AspNetCore.Authorization;

namespace server.Controllers
{
    [Authorize]
    [Route("api/members")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly LibraryContext _context;
        private readonly IMapper _mapper;
        public MemberController(LibraryContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var members = _context.Members.ToList().Select(member => _mapper.Map<Member, GetMemberDTO>(member));
            return Ok(members);
        }
        [HttpGet("{username}")]
        public IActionResult GetById([FromRoute] string username)
        {
            var member = _context.Members.FirstOrDefault(a => a.Username == username);
            if (member == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<Member, GetMemberDTO>(member));
        }
        [HttpPost("add")]
        public IActionResult Add([FromBody] GetMemberDTO memberDTO)
        {
            var member = _mapper.Map<GetMemberDTO, Member>(memberDTO);
            _context.Members.Add(member);
            _context.SaveChanges();
            return Ok(_mapper.Map<Member, GetMemberDTO>(member));
        }
        [HttpDelete("remove/{username}")]
        public IActionResult Remove([FromRoute] string username)
        {
            var member = _context.Members.FirstOrDefault(x => x.Username == username);
            if (member == null)
            {
                return NotFound();
            }
            _context.Members.Remove(member);
            _context.SaveChanges();
            return NoContent();
        }
    }
}