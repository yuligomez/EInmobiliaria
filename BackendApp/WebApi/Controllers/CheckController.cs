using System;
using System.Collections.Generic;
using System.Linq;
using BusinessLogicInterface;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Model;
using Model.In;
using Model.Out;

namespace WebApi.Controllers
{
    [Route("api/checks")]
    public class CheckController : ControllerBaseApi
    {
        private readonly ICheckLogic checkLogic;
        private readonly IRentalLogic rentalLogic;
        public CheckController(ICheckLogic checkLogic, IRentalLogic rentalLogic)
        {
            this.checkLogic = checkLogic;
            this.rentalLogic = rentalLogic;
        }
        // GET: api/checks
        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<Check> checks = this.checkLogic.GetAll();
            return Ok(checks.Select(m => new CheckDetailInfoModel(m)).ToList());
        }
        // GET: api/checks/{id}
        [HttpGet("{id}", Name = "GetCheck")]
        public IActionResult GetBy([FromRoute] int id)
        {
            Check check = this.checkLogic.GetById(id);
            return Ok(new CheckDetailInfoModel(check));
        }
        // POST: api/checks
        [HttpPost]
        public IActionResult Post([FromBody] CheckModel checkmodel)
        {
            IEnumerable<Check> checksToEntity = checkmodel.ToEntityCheck();
            IEnumerable<Rental> rentals = checkmodel.ToEntityRental();
            IEnumerable<Rental> updateRental = rentals.Select(x => 
            {
                Rental update = this.rentalLogic.Update(x.Id,x);
                return update;
            });
            IEnumerable<Check> checks = checksToEntity.Select( x => 
            {
                return this.checkLogic.Add(x);
            });
            List<RentalBasicInfoModel> rentsOut = updateRental.Select(m => new RentalBasicInfoModel(m)).ToList();
            List<CheckBasicInfoModel> checksOut = checks.Select(m => new CheckBasicInfoModel(m)).ToList();
            PostCheckOut result = new PostCheckOut(rentsOut, checksOut);
            return Ok(result);
        }

        // PUT: api/checks
        [HttpPut("{id}")]
        public IActionResult Put([FromRoute] int id, [FromBody] CheckPutModel checkModel)
        {
            Check checkToUpdate = checkModel.ToEntity();
            Check checkUpdated = this.checkLogic.Update(id, checkToUpdate);
            CheckDetailInfoModel checkModelOut = new CheckDetailInfoModel(checkUpdated);
            return CreatedAtRoute("GetCheck", new { id = checkModelOut.Id }, checkModelOut);
        }

        // DELETE: api/checks/{id}
        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            this.checkLogic.Delete(id);
            return Ok();
        }
    }
}