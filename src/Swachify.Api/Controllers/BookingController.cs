using Microsoft.AspNetCore.Mvc;
using Swachify.Application.Interfaces;
using Swachify.Application.DTOs;
using Swachify.Infrastructure.Models;

namespace Swachify.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }


        [HttpGet("getall")]
        public async Task<ActionResult<IEnumerable<BookingDto>>> GetAll(CancellationToken ct)
        {
            var bookings = await _bookingService.GetAllAsync(ct);
            var result = bookings.Select(b => new BookingDto(
                b.id,
                b.booking_id,
                b.dept_id,
                b.service_id,
                b.slot_id,
                b.created_by,
                b.created_date,
                b.modified_by,
                b.modified_date,
                b.is_active,
                b.preferred_date,
                b.full_name,
                b.phone,
                b.email,
                b.address,
                b.status_id
            ));
            return Ok(result);
        }


        [HttpGet("{id:long}")]
        public async Task<ActionResult<BookingDto>> GetById(long id, CancellationToken ct)
        {
            var b = await _bookingService.GetByIdAsync(id, ct);
            if (b == null) return NotFound();

            var dto = new BookingDto(
                b.id,
                b.booking_id,
                b.dept_id,
                b.service_id,
                b.slot_id,
                b.created_by,
                b.created_date,
                b.modified_by,
                b.modified_date,
                b.is_active,
                b.preferred_date,
                b.address,
                b.full_name,
                b.email,
                b.phone,
                b.status_id
            );

            return Ok(dto);
        }


        [HttpPost]
        public async Task<ActionResult> Create([FromBody] BookingDto dto, CancellationToken ct)
        {
            var booking = new service_booking
            {
                booking_id = dto.BookingId,
                dept_id = dto.DeptId,
                service_id = dto.ServiceId,
                slot_id = dto.SlotId,
                created_by = dto.CreatedBy,
                preferred_date = dto.PreferredDate,
                is_active = dto.IsActive ?? true,
                full_name = dto.full_name,
                address = dto.address,
                phone = dto.phone,
                email = dto.email,
                status_id=dto.status_id,
            };

            var id = await _bookingService.CreateAsync(booking, ct);
            return CreatedAtAction(nameof(GetById), new { id }, id);
        }


        [HttpPut("{id:long}")]
        public async Task<ActionResult> Update(long id, [FromBody] BookingDto dto, CancellationToken ct)
        {
            var booking = new service_booking
            {
                dept_id = dto.DeptId,
                service_id = dto.ServiceId,
                slot_id = dto.SlotId,
                modified_by = dto.ModifiedBy,
                preferred_date = dto.PreferredDate,
                is_active = dto.IsActive,
                full_name = dto.full_name,
                address = dto.address,
                phone = dto.phone,
                email = dto.email,
                status_id=dto.status_id
            };

            var updated = await _bookingService.UpdateAsync(id, booking, ct);
            if (!updated) return NotFound();

            return NoContent();
        }


        [HttpDelete("{id:long}")]
        public async Task<ActionResult> Delete(long id, CancellationToken ct)
        {
            var deleted = await _bookingService.DeleteAsync(id, ct);
            if (!deleted) return NotFound();

            return NoContent();
        }
    }
}
