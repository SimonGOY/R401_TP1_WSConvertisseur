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

        /// <summary>
        /// Constructor
        /// </summary>
        public DevisesController()
        {
            lesDevises = new List<Devise> {};
            lesDevises.Add(new Devise(1, "Dollar", 1.08));
            lesDevises.Add(new Devise(2, "Franc Suisse", 1.07));
            lesDevises.Add(new Devise(3, "Yen", 120));
        }

        /// <summary>
        /// Get all currencies.
        /// </summary>
        /// <returns>Http response</returns>
        /// <response code="200">List of currencies (even if empty)</response>
        // GET: api/<DevisesController>
        [ProducesResponseType(200)]
        [HttpGet]
        public IEnumerable<Devise> GetAll()
        {
            return lesDevises;
        }

        /// <summary>
        /// Get a single currency.
        /// </summary>
        /// <returns>Http response</returns>
        /// <param name="id">The id of the currency</param>
        /// <response code="200">When the currency id is found</response>
        /// <response code="404">When the currency id is not found</response>
        // GET api/<DevisesController>/5
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
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

        /// <summary>
        /// Set a new currency.
        /// </summary>
        /// <returns>Http response</returns>
        /// <param name="devise">The detail of the currency to add</param>
        /// <response code="201">When the currency is added</response>
        /// <response code="400">When the currency can't be added</response>
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [HttpPost]
        public ActionResult<Devise> Post([FromBody] Devise devise)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            lesDevises.Add(devise);
            return CreatedAtRoute("GetDevise", new { id = devise.Id }, devise);
        }

        /// <summary>
        /// Update a currency.
        /// </summary>
        /// <returns>Http response</returns>
        /// <param name="id">The id of the currency</param>
        /// <response code="204">When the currency is pdated</response>
        /// <response code="400">When the input format is not correct</response>
        /// <response code="404">When the currency id is not found</response>
        // DELETE api/<DevisesController>/5
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        // PUT api/<DevisesController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Devise devise)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != devise.Id)
            {
                return BadRequest();
            }
            int index = lesDevises.FindIndex((d) => d.Id == id);
            if (index < 0)
            {
                return NotFound();
            }
            lesDevises[index] = devise;
            return NoContent();
        }

        /// <summary>
        /// Delete one currency.
        /// </summary>
        /// <returns>Http response</returns>
        /// <param name="id">The id of the currency</param>
        /// <response code="204">When the currency id is found</response>
        /// <response code="404">When the currency id is not found</response>
        // DELETE api/<DevisesController>/5
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Devise? devise = lesDevises.FirstOrDefault((d) => d.Id == id);
            if (devise == null)
            {
                return NotFound();
            }
            lesDevises.Remove(devise);
            return NoContent();
        }    
    }
}
