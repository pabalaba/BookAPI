using BookAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookAPI.Services.Interfaces
{
    public interface IAuthorServices
    {
        public Task<(List<AuthorModel> authors, int tot)> GetAll(int page, int limit);
        public Task<AuthorModel> GetItem(int id);
        public Task<int> CreateItem(AuthorBaseModel value);
        public Task UpdateItem(int id, AuthorBaseModel value);
    }
}
