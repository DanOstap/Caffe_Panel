using Caffe_Panel.DataBase;
using Caffe_Panel.DataBase.Service;
using Caffe_Panel.Models;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Mvc;

namespace Caffe_Panel.Controller
{
    [Route("caffe/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUsersService usersService;
        public UserController(IUsersService usersService)
        {
            this.usersService = usersService;
        }
        [HttpPost]
        public async Task<ActionResult<User>> Create([FromBody] User model) {
            var newUser = await usersService.CreateUser(model);
            return Ok(newUser);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllUsers() {
            var users = await usersService.FindAll();
            return Ok(users);
        }
        [HttpGet("/byId")]
        public async Task<IActionResult> GetUserById(int id) {
            var user = await usersService.FindOneById(id);
            if (user == null) {
                return BadRequest($"Can't found user with id: {id}");
            }
            return Ok(user);
        }
        [HttpGet("/byName")]
        public async Task<IActionResult> GetUserByName(string name) {
            var user = await usersService.FindOneByName(name);
            if (user == null) {
                return BadRequest($"Can't found user with name: {name}");
            }
            return Ok(user);
        }
        [HttpPut]
        public async Task<ActionResult<User>> UpdateUser(int id,[FromBody] User model) {
            if (id != model.Id) {
                return NotFound();
            }
            var user = await usersService.Update(id, model);
            if (user == null) {
                return null;
            }
            return Ok(user);
        }
        [HttpDelete("/byId")]
        public async Task<ActionResult<User>> RemoveUserById(int id) {
            var user = await usersService.RemoveById(id);
            if (user == null) { 
                return NotFound();
            }
            return Ok(user);
        }
        [HttpDelete("/byName")]
        public async Task<ActionResult<User>> RemoveUserByName(string name) {
            var user = await usersService.RemoveByName(name);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
    }
}
