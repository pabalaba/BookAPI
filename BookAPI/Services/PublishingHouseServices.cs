using AutoMapper;
using BookAPI.Data;
using BookAPI.Data.Entities;
using BookAPI.Models;
using BookAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookAPI.Services
{
    public class PublishingHouseServices: IPublishingHouseServices
    {
        private readonly BookContext _context;
        private readonly IMapper _mapper;

        public PublishingHouseServices(BookContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> CreateItem(PublishingHouseBaseModel value)
        {
            var entity = _mapper.Map<PublishingHouse>(value);
            await _context.PublishingHouse.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<(List<PublishingHouseModel> publishers, int tot)> GetAll(int page, int limit)
        {
            var tot = await _context.Book.CountAsync();

            page = (page < 0) ? 1 : page;
            var startRow = (page - 1) * limit;

            var publishers = await _context.PublishingHouse
                                        .Skip(startRow)
                                        .Take(limit)
                                        .ToListAsync();

            var list = _mapper.Map<List<PublishingHouse>, List<PublishingHouseModel>>(publishers);

            return (list, tot);
        }

        public async Task<PublishingHouseModel> GetItem(int id)
        {
            return _mapper.Map<PublishingHouseModel>(await _context.PublishingHouse.FirstOrDefaultAsync(house => house.Id == id));
        }

        public async Task UpdateItem(int id, PublishingHouseBaseModel value)
        {
            var fromDb = await _context.PublishingHouse.FirstOrDefaultAsync(house => house.Id == id);
            if (fromDb == null)
                throw new System.Exception("Item not found");
            _context.PublishingHouse.Update(fromDb);
            if (!string.IsNullOrEmpty(value.Name))
                fromDb.Name = value.Name;
            await _context.SaveChangesAsync();
        }
    }
}
