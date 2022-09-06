using AutoMapper;
using BookAPI.Data;
using BookAPI.Data.Entities;
using BookAPI.Models;
using BookAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookAPI.Services
{
    public class BookServices: IBookServices
    {
        private readonly BookContext _context;
        private readonly IMapper _mapper;

        public BookServices(BookContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<(List<BookModel> books, int tot)> GetAll(int page, int limit)
        {
            var tot = await _context.Book.CountAsync();

            page = (page < 0) ? 1 : page;
            var startRow = (page - 1) * limit;

            var books =  await _context.Book.Include(p => p.PublishingHouse)
                                        .Include(a => a.Authors)
                                        .Skip(startRow)
                                        .Take(limit)
                                        .ToListAsync();

            var list = _mapper.Map<List<Book>,List<BookModel>>(books);

            return (list, tot);
        }

        public async Task<BookModel> GetItem(int id)
        {
            var book = await _context.Book.Include(p => p.PublishingHouse).Include(a => a.Authors).FirstOrDefaultAsync(x => x.Id == id);
            return _mapper.Map<BookModel>(book);
        }

        public async Task<int> PostItem(BookInsertModel book)
        {
            var entity = _mapper.Map<Book>(book);
            await _context.Book.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task DeleteItem(int id)
        {
            var book = await _context.Book.FirstOrDefaultAsync(x => x.Id == id);

            if(book == null)
            {
                throw new Exception("Book Not Found");
            }

            _context.Book.Remove(book);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateItem(int id, BookUpdateModel item)
        {
            var book = await _context.Book.FirstOrDefaultAsync(x => x.Id == id);
            if (book == null)
            {
                throw new Exception("Book Not Found");
            }
            
            if(!string.IsNullOrEmpty(item.Title))
                book.Title = item.Title;

            if(!string.IsNullOrEmpty(item.ISBN))
                book.ISBN = item.ISBN;

            _context.Book.Update(book);
            await _context.SaveChangesAsync();
        }

    }
}
