using Microsoft.AspNetCore.Mvc;
using WSConvertisseur.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WSConvertisseur.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevisesController : ControllerBase
    {
        public List<Devise> lesDevises;

        public DevisesController()
        {
            lesDevises = new List<Devise> {};
            lesDevises.Add(new Devise(1, "Dollar", 1.08));
            lesDevises.Add(new Devise(2, "Franc Suisse", 1.07));
            lesDevises.Add(new Devise(3, "Yen", 120));
        }

        // GET: api/<DevisesController>
        [HttpGet]
        public IEnumerable<Devise> GetAll()
        {
            return lesDevises;
        }

        // GET api/<DevisesController>/5
        [HttpGet("{id}", Name = "GetDevise")]
        public ActionResult<Devise> GetById(int id)
        {
            Devise? devise = lesDevises.FirstOrDefault((d) => d.Id == id);
            if (devise == null)
            {
                return NotFound();
            }
            return devise;
        }

        // POST api/<DevisesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<DevisesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<DevisesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        
    }
}
