using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BackEnd.Data;
using BackEnd.Interfaces;
using BackEnd.Repositories;
using BackEnd.Services;

namespace BackEnd.Controllers
{
    [Route("api/Users")]
    [ApiController]

    public class UsersController : ControllerBase
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        

        private readonly IUserService _service;
        
        public UsersController(IUserService mainservice)
        {
            _service = mainservice;
        }



        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = await _service.GetActiveUsers();
            return Ok(users);
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _service.GetUser(id);

            if (user == null)
            {
                //return NotFound();
                return StatusCode(500);
            }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public ActionResult PutUser(int id, User user)
        {
            if (!_service.UpdateUser(id, user).Result) 
            { 
                return StatusCode(500); 
            }

            return NoContent();
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<User> PostUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!_service.CreateUser(user).Result)
            {
                return StatusCode(500);
            }
            return NoContent();
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public ActionResult DeleteUser(int id)
        {
            if (!_service.DeleteUser(id).Result)
            {
                return StatusCode(500);
            }
            return NoContent();
        }
       

    }


}
