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
    [Route("api/elements")]
    public class ElementController : ControllerBaseApi
    {
        private readonly IElementLogic elementLogic;
        public ElementController(IElementLogic elementLogic)
        {
            this.elementLogic = elementLogic;
        }
        // GET: api/elements
        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<Element> elements = this.elementLogic.GetAll();
            return Ok(elements.Select(m => new ElementBasicInfoModel(m)).ToList());
        }
        // GET: api/elements/{id}
        [HttpGet("{id}", Name = "GetElement")]
        public IActionResult GetBy([FromRoute] int id)
        {
            IEnumerable<Element> element = this.elementLogic.GetAll().Where(x => x.ApartmentId == id);
            return Ok(element.Select(m => new ElementDetailInfoModel(m)));

        }
        // POST: api/elements
        [HttpPost]
        public IActionResult Post([FromBody] ElementModel elementmodel)
        {
            Element element = this.elementLogic.Add(elementmodel.ToEntity());
            return CreatedAtRoute("GetElement", new { id = element.Id }, new ElementDetailInfoModel(element));
        }
        // PUT: api/elements
        [HttpPut("{id}")]
        public IActionResult Put([FromRoute] int id, [FromBody] ElementModel elementModel)
        {
            Element elementToUpdate = elementModel.ToEntity(false);
            Element elementUpdated = this.elementLogic.Update(id, elementToUpdate);
            ElementDetailInfoModel elementModelOut = new ElementDetailInfoModel(elementUpdated);
            return CreatedAtRoute("GetElement", new { id = elementModelOut.Id }, elementModelOut);
        }
        // DELETE: api/elements/{id}
        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            this.elementLogic.Delete(id);
            return Ok();
        }
    }
}