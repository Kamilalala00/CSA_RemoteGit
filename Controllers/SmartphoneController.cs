using KamilaClient.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KamilaClient.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SmartphoneController : ControllerBase
    {
        private static List<Smartphone> _smartphoneList = new List<Smartphone>();

        [HttpGet]
        public ActionResult<IEnumerable<Smartphone>> Get()
        {
            return _smartphoneList;
        }

        [HttpGet("{id}")]
        public ActionResult<Smartphone> Get(int id)
        {
            if (_smartphoneList.Count <= id) throw new IndexOutOfRangeException("Нет такого у нас");

            return _smartphoneList[id];
        }

        [HttpPost]
        public void Post([FromBody] Smartphone value)
        {
            _smartphoneList.Add(value);
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Smartphone value)
        {
            if (_smartphoneList.Count <= id) throw new IndexOutOfRangeException("Нет такого у нас");

            _smartphoneList[id] = value;
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            if (_smartphoneList.Count <= id) throw new IndexOutOfRangeException("Нет такого у нас");

            _smartphoneList.RemoveAt(id);
        }
    }
}
