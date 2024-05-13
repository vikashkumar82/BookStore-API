using BookStore_API.Model;
using Microsoft.AspNetCore.JsonPatch;

namespace BookStore_API.Repository
{
    public interface IBookRepository
    {
        Task<List<BookModel>> getAllBooksAsync();
        Task<BookModel> getBooksByIDAsync(int bookId);
        Task<int> addBookAsync(BookModel bookModel);
        Task updateBooksByIDAsync(int bookId, BookModel bookModel);
        Task updateBooksByPatch(int bookId, JsonPatchDocument bookModel);

        Task deleteBookAsync(int bookId);
    }
}
