using Hospital_Hub_Portal.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Hub_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DoctorReviewController : Controller
    {
        private readonly HospitalHubContext context;
        public DoctorReviewController(HospitalHubContext _context)
        {
            context = _context;
        }

        #region GetAllDoctorReviews
        [HttpGet]
        public IActionResult GetAllDoctorReviews()
        {
            var reviews = context.HhDoctorReviews.ToList();
            return Ok(reviews);
        }
        #endregion

        #region GetDoctorReviewById
        [HttpGet("{id}")]
        public IActionResult GetDoctorReviewById(int id)
        {
            var review = context.HhDoctorReviews.Find(id);
            if (review == null)
            {
                return NotFound();
            }
            return Ok(review);
        }
        #endregion

        #region AddDoctorReview
        [HttpPost]
        public IActionResult AddDoctorReview([FromBody] HhDoctorReview hhDoctorReview)
        {
            if (hhDoctorReview == null)
            {
                return BadRequest("Doctor review data is null");
            }
            hhDoctorReview.CreatedDate = DateTime.Now;
            hhDoctorReview.ModifiedDate = null;
            context.HhDoctorReviews.Add(hhDoctorReview);
            context.SaveChanges();
            return CreatedAtAction(nameof(GetDoctorReviewById), new { id = hhDoctorReview.ReviewId }, hhDoctorReview);
        }
        #endregion

        #region UpdateDoctorReview
        [HttpPut("{id}")]
        public IActionResult UpdateDoctorReview(int id, [FromBody] HhDoctorReview hhDoctorReview)
        {
            if (hhDoctorReview == null || hhDoctorReview.ReviewId != id)
            {
                return BadRequest("Doctor review data is null or ID mismatch");
            }
            var existingReview = context.HhDoctorReviews.Find(id);
            if (existingReview == null)
            {
                return NotFound();
            }
            existingReview.Rating = hhDoctorReview.Rating;
            existingReview.ReviewText = hhDoctorReview.ReviewText;
            existingReview.ModifiedDate = DateTime.Now;
            context.SaveChanges();
            return NoContent();
        }
        #endregion

        #region DeleteDoctorReview
        [HttpDelete("{id}")]
        public IActionResult DeleteDoctorReview(int id)
        {
            var review = context.HhDoctorReviews.Find(id);
            if (review == null)
            {
                return NotFound();
            }
            context.HhDoctorReviews.Remove(review);
            context.SaveChanges();
            return NoContent();
        }
        #endregion
    }
}
