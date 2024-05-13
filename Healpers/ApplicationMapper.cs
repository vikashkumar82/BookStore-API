using AutoMapper;
using BookStore_API.Data;
using BookStore_API.Model;

namespace BookStore_API.Healpers
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper() 
        {
            CreateMap<Books, BookModel>();
        }
    }
}
