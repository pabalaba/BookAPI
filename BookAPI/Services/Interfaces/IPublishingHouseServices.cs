using BookAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookAPI.Services.Interfaces
{
    public interface IPublishingHouseServices
    {
        public Task<(List<PublishingHouseModel> publishers, int tot)> GetAll(int page, int limit);
        public Task<PublishingHouseModel> GetItem(int id);
        public Task<int> CreateItem(PublishingHouseBaseModel value);
        public Task UpdateItem(int id, PublishingHouseBaseModel value);
    }
}
