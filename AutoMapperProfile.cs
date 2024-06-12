using AutoMapper;
using server.DTOs.Admin;
using server.DTOs.Book;
using server.DTOs.Member;
using server.Models;

public class AutoMapperProfile : Profile 
{
    public AutoMapperProfile()
    {
        CreateMap<Book, GetBookDTO>();
        CreateMap<Member, GetMemberDTO>();
        CreateMap<Admin, GetAdminDTO>();
        CreateMap<GetBookDTO, Book>();
        CreateMap<GetMemberDTO, Member>();
        CreateMap<GetAdminDTO, Admin>();
    }
}