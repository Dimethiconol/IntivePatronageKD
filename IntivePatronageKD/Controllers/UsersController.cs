using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IntivePatronageKD.Entities;
using IntivePatronageKD.Services;
using Microsoft.AspNetCore.Mvc;

namespace IntivePatronageKD.Controllers
{
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }



        [HttpPost]
        public ActionResult CreateUser([FromBody]User user)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var id = _userService.Create(user);

            return Created($"/api/users/{id}", null);
        }



        [HttpGet]
        public ActionResult<IEnumerable<User>> GetAll()
        {
            var users = _userService.GetAll();

            return Ok(users);
        }



        [HttpGet("{id}")]
        public ActionResult<User> Get([FromRoute] int id)
        {
            var user = _userService.GetUser(id);

            if (user is null) return NotFound();

            return Ok(user);
        }



        [HttpPut("{id}")]
        public ActionResult Update([FromBody]User user, [FromRoute]int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var isUpdated = _userService.Update(id, user);
            if (!isUpdated) return NotFound();

            return Ok();
        }



        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute]int id)
        {
            var isDeleted = _userService.Delete(id);

            if (isDeleted) return NoContent();

            return NotFound();
        }
    }
}
