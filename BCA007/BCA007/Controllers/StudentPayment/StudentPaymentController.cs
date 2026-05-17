using BCA007.Shared.DTOs;
using BCA007.Shared.Service.Student;
using Microsoft.AspNetCore.Mvc;

namespace BCA007.Controllers.StudentPayment
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentPaymentController : ControllerBase
    {
        private readonly IStudentPaymentService _service;

        public StudentPaymentController(IStudentPaymentService service)
        {
            _service = service;
        }

        [HttpGet("Fees")]
        public async Task<ActionResult<IEnumerable<StudentFeeViewDto>>> GetAllStudentFees()
        {
            return Ok(await _service.GetAllStudentFeesAsync());
        }

        [HttpGet("Fees/{id}")]
        public async Task<ActionResult<StudentFeeDto>> GetStudentFeeById(int id)
        {
            var fee = await _service.GetStudentFeeByIdAsync(id);
            if (fee == null) return NotFound();
            return Ok(fee);
        }

        [HttpPost("Fees")]
        public async Task<ActionResult<StudentFeeDto>> AddStudentFee(StudentFeeDto fee)
        {
            var newFee = await _service.AddStudentFeeAsync(fee);
            return CreatedAtAction(nameof(GetStudentFeeById), new { id = newFee.Id }, newFee);
        }

        [HttpPut("Fees")]
        public async Task<ActionResult<StudentFeeDto>> UpdateStudentFee(StudentFeeDto fee)
        {
            var updatedFee = await _service.UpdateStudentFeeAsync(fee);
            if (updatedFee == null) return NotFound();
            return Ok(updatedFee);
        }

        [HttpDelete("Fees/{id}")]
        public async Task<ActionResult<bool>> DeleteStudentFee(int id)
        {
            var result = await _service.DeleteStudentFeeAsync(id);
            if (!result) return NotFound();
            return Ok(result);
        }

        [HttpGet("Payments")]
        public async Task<ActionResult<IEnumerable<StudentPaymentViewDto>>> GetAllStudentPayments()
        {
            return Ok(await _service.GetAllStudentPaymentsAsync());
        }

        [HttpPost("Payments")]
        public async Task<ActionResult<StudentPaymentDto>> AddStudentPayment(StudentPaymentDto payment)
        {
            var newPayment = await _service.AddStudentPaymentAsync(payment);
            return Ok(newPayment);
        }

        [HttpPut("Payments")]
        public async Task<ActionResult<StudentPaymentDto>> UpdateStudentPayment(StudentPaymentDto payment)
        {
            var updatedPayment = await _service.UpdateStudentPaymentAsync(payment);
            if (updatedPayment == null) return NotFound();
            return Ok(updatedPayment);
        }

        [HttpDelete("Payments/{id}")]
        public async Task<ActionResult<bool>> DeleteStudentPayment(int id)
        {
            var result = await _service.DeleteStudentPaymentAsync(id);
            if (!result) return NotFound();
            return Ok(result);
        }

        [HttpGet("Payments/{id}")]
        public async Task<ActionResult<StudentPaymentDto>> GetStudentPaymentById(int id)
        {
            var payment = await _service.GetStudentPaymentByIdAsync(id);
            if (payment == null) return NotFound();
            return Ok(payment);
        }

        [HttpGet("FeeTypes")]
        public async Task<ActionResult<IEnumerable<FeeTypeDto>>> GetAllFeeTypes()
        {
            return Ok(await _service.GetAllFeeTypesAsync());
        }
        [HttpGet("Statuses")]
        public async Task<ActionResult<IEnumerable<StatusDto>>> GetAllStatuses()
        {
            return Ok(await _service.GetAllStatusesAsync());
        }

        [HttpPut("Fees/{id}/Status")]
        public async Task<ActionResult<bool>> UpdateStudentFeeStatus(int id, [FromBody] int statusId)
        {
            var result = await _service.UpdateStudentFeeStatusAsync(id, statusId);
            if (!result) return NotFound();
            return Ok(result);
        }
    }
}
