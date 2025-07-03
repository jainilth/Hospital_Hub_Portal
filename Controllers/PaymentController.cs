using Hospital_Hub_Portal.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Hub_Portal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : Controller
    {
        private readonly HospitalHubContext context;
        public PaymentController(HospitalHubContext _context)
        {
            context = _context;
        }

        #region GetAllPayments
        [HttpGet]
        public IActionResult GetAllPayments()
        {
            var payments = context.HhPayments.ToList();
            return Ok(payments);
        }
        #endregion

        #region GetPaymentById
        [HttpGet("{id}")]
        public IActionResult GetPayment(int id)
        {
            var payment = context.HhPayments.Find(id);
            if (payment == null)
            {
                return NotFound();
            }
            return Ok(payment);
        }
        #endregion

        #region AddPayment
        [HttpPost]
        public IActionResult AddPayment([FromBody] HhPayment hhPayment)
        {
            if (hhPayment == null)
            {
                return BadRequest("Payment data is null");
            }
            hhPayment.CreatedDate = DateTime.Now;
            hhPayment.ModifiedDate = null;
            context.HhPayments.Add(hhPayment);
            context.SaveChanges();
            return CreatedAtAction(nameof(GetPayment), new { id = hhPayment.PaymentId }, hhPayment);
        }
        #endregion

        #region UpdatePayment
        [HttpPut("{id}")]
        public IActionResult UpdatePayment(int id, [FromBody] HhPayment hhPayment)
        {
            if (hhPayment == null || hhPayment.PaymentId != id)
            {
                return BadRequest("Payment data is null or ID mismatch");
            }
            var existingPayment = context.HhPayments.Find(id);
            if (existingPayment == null)
            {
                return NotFound();
            }
            existingPayment.PaymentAmount = hhPayment.PaymentAmount;
            existingPayment.PaymentDate = hhPayment.PaymentDate;
            existingPayment.ModifiedDate = DateTime.Now;
            context.SaveChanges();
            return NoContent();
        }
        #endregion

        #region DeletePayment
        [HttpDelete("{id}")]
        public IActionResult DeletePayment(int id)
        {
            var payment = context.HhPayments.Find(id);
            if (payment == null)
            {
                return NotFound();
            }
            context.HhPayments.Remove(payment);
            context.SaveChanges();
            return NoContent();
        }
        #endregion
    }
}
