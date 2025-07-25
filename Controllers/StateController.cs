using Hospital_Hub_Portal.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Hub_Portal.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StateController : Controller
    {
        private readonly HospitalHubContext context;
        public StateController(HospitalHubContext _context)
        {
            context = _context;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var country = context.HhStates.ToList();
            return Ok(country);
        }

        #region GetState List And Their Country and Count Of City and Hospital
        [HttpGet]
        public IActionResult GetALlSatate()
        {
            var states = context.HhStates
                    .Select(state => new
                    {
                        state.StateId,
                        state.StateName,
                        CountryName = context.HhCountries
                                .Where(c => c.CountryId == state.CountryId)
                                .Select(c => c.CountryName).FirstOrDefault(),
                        CityCount = context.HhCities.Count(city => city.StateId == state.StateId),
                        HospitalCount = context.HhHospitals.Count(h => h.StateId == state.StateId)
                    })
        .ToList();
            return Ok(states);
        }
        #endregion


        #region Get Country With Count Of State And City and Hospital
        [HttpGet]
        public IActionResult GetAllCountries()
        {
            var countries = context.HhCountries
                .Select(country => new
                {
                    country.CountryId,
                    country.CountryName,
                    StateCount = context.HhStates.Count(s => s.CountryId == country.CountryId),
                    CityCount = context.HhCities.Count(c => c.State.CountryId == country.CountryId),
                }).ToList();

            return Ok(countries);
        }
        #endregion

        #region GetStateById
        [HttpGet("{id}")]
        public IActionResult GetStateById(int id)
        {
            var state = context.HhStates.Find(id);
            if (state == null)
            {
                return NotFound();
            }
            return Ok(state);
        }
        #endregion

        #region AddState
        [HttpPost]
        public IActionResult AddState([FromBody] HhState hhState)
        {
            if (hhState == null)
            {
                return BadRequest("State data is null");
            }
            hhState.CreatedDate = DateTime.Now;
            hhState.ModifiedDate = null;
            context.HhStates.Add(hhState);
            context.SaveChanges();
            return CreatedAtAction(nameof(GetStateById), new { id = hhState.StateId }, hhState);
        }
        #endregion

        #region UpdateState
        [HttpPut("{id}")]
        public IActionResult UpdateState(int id, [FromBody] HhState hhState)
        {
            if (hhState == null || hhState.StateId != id)
            {
                return BadRequest("State data is null or ID mismatch");
            }
            var existingState = context.HhStates.Find(id);
            if (existingState == null)
            {
                return NotFound();
            }
            existingState.StateName = hhState.StateName;
            existingState.ModifiedDate = DateTime.Now;
            context.SaveChanges();
            return NoContent();
        }
        #endregion

        #region DeleteState
        [HttpDelete("{id}")]
        public IActionResult DeleteState(int id)
        {
            var state = context.HhStates.Find(id);
            if (state == null)
            {
                return NotFound();
            }
            context.HhStates.Remove(state);
            context.SaveChanges();
            return NoContent();
        }
        #endregion

        #region GetStaetByCountry
        [HttpGet("{countryId}")]
        public IActionResult GetStatesByCountry(int countryId)
        {
            var states = context.HhStates
                .Where(s => s.CountryId == countryId)
                .ToList();

            return Ok(states);
        }
        #endregion
    }
}
