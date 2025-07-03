using Hospital_Hub_Portal.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Hub_Portal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabBookingController : Controller
    {
        private readonly HospitalHubContext context;
        public LabBookingController(HospitalHubContext _context)
        {
            context = _context;
        }

        #region GetAllLabBookings
        [HttpGet]
        public IActionResult GetAllLabBookings()
        {
            var bookings = context.HhLabBookings.ToList();
            return Ok(bookings);
        }
        #endregion

        #region GetLabBookingById
        [HttpGet("{id}")]
        public IActionResult GetLabBookingById(int id)
        {
            var booking = context.HhLabBookings.Find(id);
            if (booking == null)
            {
                return NotFound();
            }
            return Ok(booking);
        }
        #endregion

        #region AddLabBooking
        [HttpPost]
        public IActionResult AddLabBooking([FromBody] HhLabBooking hhLabBooking)
        {
            if (hhLabBooking == null)
            {
                return BadRequest("Lab booking data is null");
            }
            hhLabBooking.CreatedDate = DateTime.Now;
            hhLabBooking.ModifiedDate = null;
            context.HhLabBookings.Add(hhLabBooking);
            context.SaveChanges();
            return CreatedAtAction(nameof(GetLabBookingById), new { id = hhLabBooking.BookingId }, hhLabBooking);
        }
        #endregion

        #region UpdateLabBooking
        [HttpPut("{id}")]
        public IActionResult UpdateLabBooking(int id, [FromBody] HhLabBooking hhLabBooking)
        {
            if (hhLabBooking == null || hhLabBooking.BookingId != id)
            {
                return BadRequest("Lab booking data is null or ID mismatch");
            }
            var existingBooking = context.HhLabBookings.Find(id);
            if (existingBooking == null)
            {
                return NotFound();
            }
            existingBooking.UserId = hhLabBooking.UserId;
            existingBooking.TestId = hhLabBooking.TestId;
            existingBooking.BookingDateTime = hhLabBooking.BookingDateTime;
            existingBooking.BookingStatus = hhLabBooking.BookingStatus;
            existingBooking.ModifiedDate = DateTime.Now;
            context.HhLabBookings.Update(existingBooking);
            context.SaveChanges();
            return NoContent();
        }
        #endregion

        #region DeleteLabBooking
        [HttpDelete("{id}")]
        public IActionResult DeleteLabBooking(int id)
        {
            var booking = context.HhLabBookings.Find(id);
            if (booking == null)
            {
                return NotFound();
            }
            context.HhLabBookings.Remove(booking);
            context.SaveChanges();
            return NoContent();
        }
        #endregion
    }
}
