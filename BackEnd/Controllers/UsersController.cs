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
        

        private readonly IMainService _service;

        /// <summary>Initializes a new instance of the <see cref="UsersController" /> class.</summary>
        /// <param name="mainservice">The mainservice.</param>
        public UsersController(IMainService mainservice)
        {
            _service = mainservice;
        }



        // GET: api/Users
        /// <summary>Gets the users.</summary>
        /// <returns>Result</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = await _service.GetActiveUsers();
            return Ok(users);
        }

        // GET: api/Users/5
        /// <summary>Gets the user by its identifier.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Result</returns>
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
        /// <summary>Updates the user.</summary>
        /// <param name="id">The identifier.</param>
        /// <param name="user">The user.</param>
        /// <returns>Result</returns>
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
        /// <summary>Creates the user.</summary>
        /// <param name="user">The user.</param>
        /// <returns>Result</returns>
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
        /// <summary>Deletes the user.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Result</returns>
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
