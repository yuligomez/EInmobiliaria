using System;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Model.In;
using Model.Out;
using SessionInterface;

namespace WebApi.Controllers
{
    [Route("api/sessions")]
    public class SessionController : ControllerBaseApi
    {
        private readonly ISessionLogic sessionLogic;
        public SessionController(ISessionLogic sessionLogic)
        {
            this.sessionLogic = sessionLogic;
        }
        [HttpPost]
        public IActionResult Post([FromBody] SessionModel sessionModel)
        {
            User newPerson = sessionModel.ToEntity();
            Guid token = this.sessionLogic.Login(newPerson);
            SessionBasicModel sessionModelToRetrun = new SessionBasicModel(token);
            return Ok(sessionModelToRetrun);
        }
    }
}