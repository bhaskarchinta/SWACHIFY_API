using Microsoft.EntityFrameworkCore;
using Swachify.Application.Interfaces;
using Swachify.Infrastructure.Data;
using Swachify.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Swachify.Application.Services
{
    public class BookingService : IBookingService
    {
        private readonly MyDbContext _db;

        public BookingService(MyDbContext db)
        {
            _db = db;
        }

        public async Task<List<service_booking>> GetAllAsync(CancellationToken ct = default)
        {
            return await _db.service_bookings
                .Include(b => b.dept)
                .Include(b => b.service)
                .Include(b => b.slot)
                .OrderByDescending(b => b.created_date)
                .ToListAsync(ct);
        }

        public async Task<service_booking?> GetByIdAsync(long id, CancellationToken ct = default)
        {
            return await _db.service_bookings
                .Include(b => b.dept)
                .Include(b => b.service)
                .Include(b => b.slot)
                .FirstOrDefaultAsync(b => b.id == id, ct);
        }

        public async Task<long> CreateAsync(service_booking booking, CancellationToken ct = default)
        {
            booking.created_date = DateTime.Now;
            booking.modified_date = DateTime.Now;
            booking.is_active = true;
            booking.booking_id ??= Guid.NewGuid().ToString();

            _db.service_bookings.Add(booking);
            await _db.SaveChangesAsync(ct);
            return booking.id;
        }

        public async Task<bool> UpdateAsync(long id, service_booking updatedBooking, CancellationToken ct = default)
        {
            var existing = await _db.service_bookings.FirstOrDefaultAsync(b => b.id == id, ct);
            if (existing == null) return false;

            existing.dept_id = updatedBooking.dept_id;
            existing.service_id = updatedBooking.service_id;
            existing.slot_id = updatedBooking.slot_id;
            existing.modified_by = updatedBooking.modified_by;
            existing.modified_date = DateTime.UtcNow;
            existing.preferred_date = updatedBooking.preferred_date;
            existing.is_active = updatedBooking.is_active;

            await _db.SaveChangesAsync(ct);
            return true;
        }

        public async Task<bool> DeleteAsync(long id, CancellationToken ct = default)
        {
            var booking = await _db.service_bookings.FirstOrDefaultAsync(b => b.id == id, ct);
            if (booking == null) return false;

            _db.service_bookings.Remove(booking);
            await _db.SaveChangesAsync(ct);
            return true;
        }
    }
}
