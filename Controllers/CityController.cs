using Hospital_Hub_Portal.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Hub_Portal.Controllers
{
    [Route("/api/[contrroller]")]
    [ApiController]
    public class CityController : Controller
    {
        private readonly HospitalHubContext context;

        public CityController(HospitalHubContext _context)
        {
            context = _context;
        }

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
    }
}
