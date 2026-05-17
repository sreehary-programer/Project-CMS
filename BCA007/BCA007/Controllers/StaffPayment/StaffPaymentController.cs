using BCA007.Shared.DTOs;
using BCA007.Shared.Service.Users;
using Microsoft.AspNetCore.Mvc;

namespace BCA007.Controllers.StaffPayment
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffPaymentController : ControllerBase
    {
        private readonly IStaffPaymentService _service;

        public StaffPaymentController(IStaffPaymentService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StaffPaymentViewDto>>> GetAllStaffPayments()
        {
            return Ok(await _service.GetAllStaffPaymentsAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StaffPaymentDto>> GetStaffPaymentById(int id)
        {
            var payment = await _service.GetStaffPaymentByIdAsync(id);
            if (payment == null)
            {
                return NotFound();
            }
            return Ok(payment);
        }

        [HttpPost]
        public async Task<ActionResult<StaffPaymentDto>> AddStaffPayment(StaffPaymentDto payment)
        {
            try
            {
                var newPayment = await _service.AddStaffPaymentAsync(payment);
                return CreatedAtAction(nameof(GetStaffPaymentById), new { id = newPayment.Id }, newPayment);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException?.Message ?? ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult<StaffPaymentDto>> UpdateStaffPayment(StaffPaymentDto payment)
        {
            try
            {
                var updatedPayment = await _service.UpdateStaffPaymentAsync(payment);
                if (updatedPayment == null)
                {
                    return NotFound();
                }
                return Ok(updatedPayment);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException?.Message ?? ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteStaffPayment(int id)
        {
            var result = await _service.DeleteStaffPaymentAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}
