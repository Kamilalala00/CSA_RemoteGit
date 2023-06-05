using KamilaClient.Models;
using KamilaClient.Storage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace KamilaClient.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SmartphoneController : ControllerBase
    {
        private IStorage<Smartphone> _smartphoneList;

        public SmartphoneController(IStorage<Smartphone> SmartphoneList)
        {
            _smartphoneList = SmartphoneList;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Smartphone>> Get()
        {
            return Ok(_smartphoneList.All);
        }

        [HttpGet("{id}")]
        public ActionResult<Smartphone> Get(Guid id)
        {
            if (!_smartphoneList.Has(id)) return NotFound("No such");

            return Ok(_smartphoneList[id]);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Smartphone value)
        {
            var validationResult = value.Validate();

            if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

            _smartphoneList.Add(value);

            return Ok($"{value.ToString()} has been added");
        }

        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] Smartphone value)
        {
            if (!_smartphoneList.Has(id)) return NotFound("No such");

            var validationResult = value.Validate();

            if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

            var previousValue = _smartphoneList[id];
            _smartphoneList[id] = value;

            return Ok($"{previousValue.ToString()} has been updated to {value.ToString()}");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            if (!_smartphoneList.Has(id)) return NotFound("No such");

            var valueToRemove = _smartphoneList[id];
            _smartphoneList.RemoveAt(id);

            return Ok($"{valueToRemove.ToString()} has been removed");
        }
    }
}
