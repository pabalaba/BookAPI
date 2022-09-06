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
    [Route("api/Authors")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorServices _services;

        public AuthorController(IAuthorServices services)
        {
            _services = services;
        }

        /// <summary>
        /// Ottieni tutti gli autori
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(PagedModel<AuthorModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAll(int page = 1, int limit = 10)
        {
            var result = await _services.GetAll(page, limit);
            return Ok(new PagedModel<AuthorModel>(result.authors, page, limit, result.tot));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(AuthorModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetItem(int id)
        {
            if (id < 0)
                return BadRequest();
            var item = await _services.GetItem(id);
            if (item == null)
                return NotFound();
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
        public async Task<ActionResult> Post([FromBody] AuthorBaseModel value)
        {
            if (value == null)
                return BadRequest("Item is null");
            var id = await _services.CreateItem(value);
            return CreatedAtAction(nameof(GetItem), new { id = id }, id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Put(int id, [FromBody] AuthorBaseModel value)
        {
            var item = await _services.GetItem(id);
            if (item == null)
                return NotFound("Item is null");
            try
            {
                await _services.UpdateItem(id, value);
            }
            catch (Exception)
            {
                return NotFound();
            }
            return NoContent();
        }     
    }
}
