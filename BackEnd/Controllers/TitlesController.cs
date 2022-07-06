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
        private readonly IUserService _service;

        public TitlesController(IUserService mainservice)
        {
            _service = mainservice;
        }


        // GET: api/Titles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserTitle>>> GetTitles()
        {
            var titles = await _service.GetTitles();
            return Ok(titles);
        }

        // GET: api/Titles/5
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
