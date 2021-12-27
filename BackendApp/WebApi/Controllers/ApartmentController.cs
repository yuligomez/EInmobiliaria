using System.Collections.Generic;
using System.Linq;
using BusinessLogicInterface;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Model.In;
using Model.Out;

namespace WebApi.Controllers
{
    [Route("api/apartments")]
    public class ApartmentController : ControllerBaseApi
    {
        private readonly IApartmentLogic apartmentLogic;
        public ApartmentController(IApartmentLogic apartmentLogic)
        {
            this.apartmentLogic = apartmentLogic;
        }
        // GET: api/apartments
        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<Apartment> apartments = this.apartmentLogic.GetAll();
            return Ok(apartments.Select(m => new ApartmentBasicInfoModel(m)).ToList());
        }
        // GET: api/apartments/{id}
        [HttpGet("{id}", Name = "GetApartment")]
        public IActionResult GetBy([FromRoute] int id)
        {
            Apartment apartment = this.apartmentLogic.GetById(id);
            return Ok(new ApartmentBasicInfoModel(apartment));
        }
        // POST: api/apartments
        [HttpPost]
        public IActionResult Post([FromBody] ApartmentModel apartmentmodel)
        {
            Apartment apartmentToEntity = apartmentmodel.ToEntity();
            Apartment apartment = this.apartmentLogic.Add(apartmentToEntity);
            return CreatedAtRoute("GetUser", new { id = apartment.Id }, new ApartmentBasicInfoModel(apartment));
        }
        // PUT: api/apartments
        [HttpPut("{id}")]
        public IActionResult Put([FromRoute] int id, [FromBody] ApartmentModel apartmentModel)
        {
            Apartment apartmentToUpdate = apartmentModel.ToEntity(false);
            Apartment apartmentUpdated = this.apartmentLogic.Update(id, apartmentToUpdate);
            ApartmentBasicInfoModel apartmentModelOut = new ApartmentBasicInfoModel(apartmentUpdated);
            return CreatedAtRoute("GetApartment", new { id = apartmentModelOut.Id }, apartmentModelOut);
        }
        // DELETE: api/apartments/{id}
        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            this.apartmentLogic.Delete(id);
            return Ok();
        }
    }
}