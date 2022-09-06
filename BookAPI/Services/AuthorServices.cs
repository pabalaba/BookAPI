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
    public class AuthorServices: IAuthorServices
    {
        private readonly BookContext _context;
        private readonly IMapper _mapper;

        public AuthorServices(BookContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> CreateItem(AuthorBaseModel value)
        {
            var entity = _mapper.Map<Author>(value);
            await _context.Author.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<(List<AuthorModel> authors, int tot)> GetAll(int page, int limit)
        {
            var tot = await _context.Book.CountAsync();

            page = (page < 0) ? 1 : page;
            var startRow = (page - 1) * limit;

            var authors = await _context.Author
                                        .Skip(startRow)
                                        .Take(limit)
                                        .ToListAsync();

            var list = _mapper.Map<List<Author>, List<AuthorModel>>(authors);

            return (list, tot);
        }

        public async Task<AuthorModel> GetItem(int id)
        {
            return _mapper.Map<AuthorModel>(await _context.Author.FirstOrDefaultAsync(author => author.Id == id));
        }

        public async Task UpdateItem(int id, AuthorBaseModel value)
        {
            var fromDb = await _context.Author.FirstOrDefaultAsync(author => author.Id == id);
            if (fromDb == null)
                throw new System.Exception("Item not found");
            _context.Author.Update(fromDb);
            if (value.DateOfBirth != null )
                fromDb.DateOfBirth = value.DateOfBirth;
            if (!string.IsNullOrEmpty(value.Name))
                fromDb.Name = value.Name;
            if (!string.IsNullOrEmpty(value.Surname))
                fromDb.Surname = value.Surname;
            await _context.SaveChangesAsync();
        }
    }
}
