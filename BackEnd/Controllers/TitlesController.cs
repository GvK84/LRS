using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BackEnd.Data;
using BackEnd.Interfaces;
using BackEnd.Repositories;
using BackEnd.Services;

namespace BackEnd.Controllers
{
    [Route("api/Titles")]
    [ApiController]
    public class TitlesController : ControllerBase
    {
        private readonly IMainService _service;

        /// <summary>Initializes a new instance of the <see cref="TitlesController" /> class.</summary>
        /// <param name="mainservice">The mainservice.</param>
        public TitlesController(IMainService mainservice)
        {
            _service = mainservice;
        }


        // GET: api/Titles
        /// <summary>Gets the titles.</summary>
        /// <returns>Result</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserTitle>>> GetTitles()
        {
            var titles = await _service.GetTitles();
            return Ok(titles);
        }

        // GET: api/Titles/5
        /// <summary>Gets the title by its identifier.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Result</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<UserTitle>> GetTitle(int id)
        {
            var title = await _service.GetTitle(id);

            if (title == null)
            {
                //return NotFound();
                return StatusCode(500);
            }

            return title;
        }

      
    }
}
