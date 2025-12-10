using Microsoft.AspNetCore.Mvc;
using services;
using models;
using Microsoft.AspNetCore.Authorization;

namespace controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserControllers : ControllerBase
    {
        private readonly UserServices _userServices;

        public UserControllers()
        {
            _userServices = new UserServices();
        }

        [HttpGet]
        public ActionResult<List<User>> GetAllUsers()
        {
            var users = _userServices.LoadUsers();
            return Ok(users);
        }

        [Authorize]
        [HttpGet("{id}")]
        public ActionResult<User> GetUserByID(int id)
        {
            var user = _userServices.GetUserByID(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [Authorize]
        [HttpPost]
        public ActionResult AddUser([FromBody] User user)
        {
            _userServices.AddUsers(user);
            return CreatedAtAction(nameof(GetUserByID), new { id = user.ID }, user);
        }

        [Authorize]
        [HttpPut("{id}")]
        public ActionResult UpdateUser(int id, [FromBody] User user)
        {
            var existingUser = _userServices.GetUserByID(id);
            if (existingUser == null)
            {
                return NotFound();
            }

            user.ID = id; 
            _userServices.UpdateUsers(user);
            return NoContent();
        }

        [Authorize(Roles = "Administrator,Staff")]
        [HttpDelete("{id}")]
        public ActionResult DeleteUser(int id)
        {
            var existingUser = _userServices.GetUserByID(id);
            if (existingUser == null)
            {
                return NotFound();
            }

            _userServices.DeleteUsers(id);
            return NoContent();
        }
    }
}