using BookAPI.Models;
using BookAPI.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookAPI.Controllers
{
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Produces("application/json")]    
    [Route("api/Books")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookServices _services;

        public BookController(IBookServices services)
        {
            _services = services;
        }

        /// <summary>
        /// Ottieni tutti i libri
        /// </summary>
        /// <param name="page">Numero Pagina</param>
        /// <param name="limit">Limite massimo di elementi restituiti per pagina</param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(PagedModel<BookModel>), StatusCodes.Status200OK)]        
        public async Task<ActionResult> GetAll(int page = 1, int limit = 10)
        {
            var result = await _services.GetAll(page, limit);
            return Ok(new PagedModel<BookModel>(result.books, page, limit, result.tot));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(BookModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetItem(int id)
        {
            var item = await _services.GetItem(id);

            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]        
        public async Task<ActionResult> Post([FromBody] BookInsertModel value)
        {
            if (value == null)
                return BadRequest("Item null");

            var id = await _services.PostItem(value);

            return CreatedAtAction(nameof(GetItem), new { id = id }, id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Patch(int id, [FromBody] BookUpdateModel value)
        {
            try
            {
                await _services.UpdateItem(id, value);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }

            return NoContent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _services.DeleteItem(id);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }

            return NoContent();
        }
    }
}
