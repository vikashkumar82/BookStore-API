using AutoMapper;
using BookStore_API.Data;
using BookStore_API.Model;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Net;

namespace BookStore_API.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly BookStoreContext _context;
        private readonly IMapper _mapper;

        public BookRepository(BookStoreContext context, IMapper mapper)
        {
           _context = context;
            _mapper = mapper;
        }
        public async Task<List<BookModel>> getAllBooksAsync()
        {
            var records = await _context.Books.ToListAsync();
            return _mapper.Map<List<BookModel>>(records);
        }

        public async Task<BookModel> getBooksByIDAsync(int bookId)
        {
            //This logic works when we dont have primary key but in this scenario we have "Id" as a primary key
            //var records = await _context.Books.Where(x => x.Id == bookId).Select(x => new BookModel()
            //{
            //    Id = x.Id,
            //    Title = x.Title,
            //    Description = x.Description,
            //}).FirstOrDefaultAsync();
            //return records;

            var book = await _context.Books.FindAsync(bookId);
            return _mapper.Map<BookModel>(book);
        }

        public async Task<int> addBookAsync(BookModel bookModel)
        {
            var book = new Books()
            {
                Title = bookModel.Title,
                Description = bookModel.Description
            };
            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return book.Id;
        }

        public async Task updateBooksByIDAsync(int bookId, BookModel bookModel)
        {
            var book = new Books()
            {
                Id = bookId,
                Title = bookModel.Title,
                Description = bookModel.Description
            };

            _context.Books.Update(book);
            await _context.SaveChangesAsync();
        }

        public async Task updateBooksByPatch(int bookId, JsonPatchDocument bookModel)
        {
            var book = await _context.Books.FindAsync(bookId);
            if(book != null)
            {
                bookModel.ApplyTo(book);
                await _context.SaveChangesAsync();
            }
        }

        public async Task deleteBookAsync(int bookId)
        {
            var book = new Books() { Id=bookId};
           _context.Books.Remove(book);
            await _context.SaveChangesAsync();
        }
    }
}
