using Hospital_Hub_Portal.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Hub_Portal.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class CityController : Controller
    {
        private readonly HospitalHubContext context;

        public CityController(HospitalHubContext _context)
        {
            context = _context;
        }

        //Complete this =------------=

        //#region GetAll City with All the Count of the Hospital
        //public IActionResult GetAllCity()
        //{
        //    var cities = context.HhCities
        //        .Select(city => new
        //        {
        //            city.CityId,
        //            city.CityName,
        //            HospitalName = context.HhHospitals
        //                .Where(h => h.CityId == city.CityId)
        //                .Select(h => 
        //        }
        //        )
        //        .Where(city => city.CityId == )
        //}

        #region GetAllCity
        [HttpGet]
        public IActionResult GetAllCity()
        {
            var city = context.HhCities.ToList();
            return Ok(city);
        }
        #endregion

        #region GetCityByID
        [HttpGet("{id}")]
        public IActionResult GetCityByID(int id)
        {
            var city = context.HhCities.Find(id);
            if (city == null)
            {
                return BadRequest();
            }
            return Ok(city);
        }
        #endregion

        #region AddCity
        [HttpPost]
        public IActionResult AddCity([FromBody] HhCity hhCity)
        {
            if (hhCity == null)
            {
                return BadRequest("City data is null");
            }

            hhCity.CreatedDate = DateTime.Now;
            hhCity.ModifiedDate = null;

            context.HhCities.Add(hhCity);
            context.SaveChanges();

            return CreatedAtAction(nameof(GetCityByID), new { id = hhCity.CityId }, hhCity);
        }
        #endregion
    }
}
