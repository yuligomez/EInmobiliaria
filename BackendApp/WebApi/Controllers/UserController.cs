using Microsoft.AspNetCore.Mvc;
using Model.In;
using BusinessLogicInterface;
using System.Linq;
using Model.Out;
using Domain.Entities;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace WebApi.Controllers
{

    [Route("api/users")]
    public class UserController : ControllerBaseApi
    {
        private readonly IUserLogic userLogic;
        public UserController(IUserLogic userLogic)
        {
            this.userLogic = userLogic;
        }
        // GET: api/users
        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<User> users = this.userLogic.GetAll();
            return Ok(users.Select(m => new UserBasicInfoModel(m)).ToList());
        }
        // GET: api/users/{id}
        [HttpGet("{id}", Name = "GetUser")]
        public IActionResult GetBy([FromRoute] int id)
        {
            User user = this.userLogic.GetById(id);
            return Ok(new UserDetailInfoModel(this.userLogic.GetById(id)));
        }
        // POST: api/users
        [HttpPost]
        public IActionResult Post([FromBody] UserModel usermodel)
        {
            User user = this.userLogic.Add(usermodel.ToEntity());
            return CreatedAtRoute("GetUser", new { id = user.Id }, new UserBasicInfoModel(user));
        }
        // PUT: api/users
        [HttpPut("{id}")]
        public IActionResult Put([FromRoute] int id, [FromBody] UserModel userModel)
        {
            User userToUpdate = userModel.ToEntity(false);
            User userUpdated = this.userLogic.Update(id, userToUpdate);
            UserDetailInfoModel userModelOut = new UserDetailInfoModel(userUpdated);
            return CreatedAtRoute("GetUser", new { id = userModelOut.Id }, userModelOut);
        }
        // DELETE: api/users/{id}
        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            this.userLogic.Delete(id);
            return Ok();
        }
    }
}
