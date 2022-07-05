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
        

        private readonly IUserService _userService;
        
        public UsersController()
        {
            _userService = new UserService(new UserRepository(new LRS_DBContext()));
        }



        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = await _userService.GetUsers();
            return Ok(users);
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _userService.GetUserByID(id);

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
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _userService.UpdateUser(user);

            try
            {
                await _userService.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_userService.UserExists(user))
                {
                    //return NotFound();
                    return StatusCode(500);
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _userService.InsertUser(user);
            await _userService.Save();
            return NoContent();
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _userService.GetUserByID(id);
            if (user == null)
            {
                //return NotFound();
                return StatusCode(500);
            }

            _userService.DeleteUser(user);
            await _userService.Save();

            return NoContent();
        }
       

    }


}
