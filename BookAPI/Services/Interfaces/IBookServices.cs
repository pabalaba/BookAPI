using BookAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookAPI.Services.Interfaces
{
    public interface IBookServices
    {
        public Task<(List<BookModel> books, int tot)> GetAll(int page, int limit);
        public Task<BookModel> GetItem(int id);
        public Task<int> PostItem(BookInsertModel book);
        public Task DeleteItem(int id);
        public Task UpdateItem(int id, BookUpdateModel item);
    }
}
