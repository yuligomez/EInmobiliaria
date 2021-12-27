using System;
using System.Collections.Generic;
using System.Linq;
using BusinessLogicInterface;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Model.In;
using Model.Out;

namespace WebApi.Controllers
{
    [Route("api/rentals")]
    public class RentalController : ControllerBaseApi
    {
        private readonly IRentalLogic rentalLogic;
        public RentalController(IRentalLogic rentalLogic)
        {
            this.rentalLogic = rentalLogic;
        }
        // GET: api/rentals
        [HttpGet]
        public IActionResult Get()
        {
            DateTime date = DateTime.Today;
            IEnumerable<Rental> rentals = this.rentalLogic.GetAll(date);
            return Ok(rentals.Select(m => new RentalDetailInfoModel(m)).ToList());
        }
        // GET: api/rentals/{id}
        [HttpGet("{id}", Name = "GetRental")]
        public IActionResult GetBy([FromRoute] int id)
        {
            Rental rental = this.rentalLogic.GetById(id);
            return Ok(new RentalDetailInfoModel(this.rentalLogic.GetById(id)));
        }
        // POST: api/rentals
        [HttpPost]
        public IActionResult Post([FromBody] RentalModel rentalmodel)
        {
            Rental rentalAparmentToEntity = rentalmodel.ToEntity();
            Rental rentalAprment = this.rentalLogic.Add(rentalAparmentToEntity);
            RentalDetailInfoModel rentalModelOut = new RentalDetailInfoModel(rentalAprment);
            return CreatedAtRoute("GetRental", new { id = rentalModelOut.Id }, rentalModelOut);
        }
        // PUT: api/rentals
        [HttpPut("{id}")]
        public IActionResult Put([FromRoute] int id, [FromBody] RentalModel rentalModel)
        {
            Rental rentalToUpdate = rentalModel.ToEntity();
            Rental rentalUpdated = this.rentalLogic.Update(id, rentalToUpdate);
            RentalDetailInfoModel rentalModelOut = new RentalDetailInfoModel(rentalUpdated);
            return CreatedAtRoute("GetRental", new { id = rentalModelOut.Id }, rentalModelOut);
        }
        // DELETE: api/rentals/{id}
        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            this.rentalLogic.Delete(id);
            return Ok();
        }
    }
}