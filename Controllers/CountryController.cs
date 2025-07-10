using Hospital_Hub_Portal.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Hub_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly HospitalHubContext context;

        public CountryController(HospitalHubContext _context)
        {
            context = _context;
        }

        #region Get Country With Count Of State And City and Hospital
        [HttpGet]
        public IActionResult GetAllCountries()
        {
            var countries = context.HhCountries
                .Select(country => new
                {
                    country.CountryId,
                    country.CountryName,
                    countryCode = country.CountryName.Substring(0, 2).ToUpper(),
                    StateCount = context.HhStates.Count(s => s.CountryId == country.CountryId),
                    CityCount = context.HhCities.Count(c => c.State.CountryId == country.CountryId),
                }).ToList();

            return Ok(countries);
        }
        #endregion

        //#region GetAllCountries
        //[HttpGet]
        //public IActionResult GetAllCountries()
        //{
        //    var countries = context.HhCountries.ToList();
        //    return Ok(countries);
        //}
        //#endregion

        #region GetCountryById
        [HttpGet("{id}")]
        public IActionResult GetCountryById(int id)
        {
            var country = context.HhCountries.Find(id);
            if (country == null)
            {
                return NotFound();
            }
            return Ok(country);
        }
        #endregion

        #region AddCountry
        [HttpPost]
        public IActionResult AddCountry([FromBody] HhCountry hhCountry)
        {
            if (hhCountry == null)
            {
                return BadRequest("Country data is null");
            }

            hhCountry.CreatedDate = DateTime.Now;
            hhCountry.ModifiedDate = null;

            context.HhCountries.Add(hhCountry);
            context.SaveChanges();

            return CreatedAtAction(nameof(GetCountryById), new { id = hhCountry.CountryId }, hhCountry);
        }
        #endregion

        #region UpdateCountry
        [HttpPut("{id}")]
        public IActionResult UpdateCountry(int id, [FromBody] HhCountry hhCountry)
        {
            if (id != hhCountry.CountryId)
            {
                return BadRequest("Country ID mismatch");
            }

            var existingCountry = context.HhCountries.Find(id);
            if (existingCountry == null)
            {
                return NotFound();
            }

            existingCountry.CountryName = hhCountry.CountryName;
            existingCountry.ModifiedDate = DateTime.Now;

            context.SaveChanges();
            return NoContent();
        }
        #endregion

        #region DeleteCountry
        [HttpDelete("{id}")]
        public IActionResult DeleteCountry(int id)
        {
            var country = context.HhCountries.Find(id);
            if (country == null)
            {
                return NotFound();
            }

            context.HhCountries.Remove(country);
            context.SaveChanges();

            return NoContent();
        }
        #endregion
    }
}
