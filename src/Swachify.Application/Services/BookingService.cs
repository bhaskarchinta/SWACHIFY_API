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
    private readonly IEmailService _emailService;

    public BookingService(MyDbContext db, IEmailService emailService)
    {
      _db = db;
      _emailService = emailService;
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
      booking.full_name = booking.full_name;
      booking.address = booking.address;
      booking.phone = booking.phone;
      booking.email = booking.email;
      booking.status_id = booking.status_id;
      _db.service_bookings.Add(booking);
      await _db.SaveChangesAsync(ct);
      if (!string.IsNullOrEmpty(booking.email))
      {
        var subject = $"Thank You for Choosing Swachify Cleaning Service!";

        var body = $@"
<!DOCTYPE html>
<html lang=""en"">
  <head>
    <meta charset=""UTF-8"" />
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"" />
    <title>Booking Confirmation - Swachify</title>
  </head>
  <body style=""margin:0; padding:0; background-color:#EDFDFE; font-family:Segoe UI, Arial, sans-serif;"">
    <table align=""center"" cellpadding=""0"" cellspacing=""0"" width=""600"" 
           style=""background-color:#ffffff; border-radius:10px; box-shadow:0 4px 15px rgba(0,0,0,0.08);"">
      
      <!-- Header -->
      <tr>
        <td align=""center"" bgcolor=""#16A34A"" 
            style=""padding:20px 0; border-top-left-radius:10px; border-top-right-radius:10px;"">
          <h1 style=""color:#ffffff; font-size:26px; margin:0;"">Thank You for Choosing Swachify!</h1>
        </td>
      </tr>

      <!-- Body -->
      <tr>
        <td style=""padding:30px 40px; color:#333333; font-size:16px; line-height:1.7;"">
          <p>Dear <strong>{booking.full_name ?? "Customer"}</strong>,</p>

          <p>Thank you for choosing <strong style=""color:#16A34A;"">Swachify Cleaning Service</strong>!</p>

          <p>We’ve received your booking request for the service 
             <strong style=""color:#16A34A;"">{booking.service?.service_name ?? "Selected Service"}</strong>.</p>

          <p>Our team will assign an agent to your service shortly and confirm your service details soon.</p>

          <p>If you have any questions, feel free to contact us at 
            <a href=""mailto:support@swachify.com"" style=""color:#16A34A; text-decoration:none;"">support@swachify.com</a> 
            or call us at <strong>+91 98765 43210</strong>.
          </p>

          <p>Warm regards,<br>
          <strong>Team Swachify</strong><br>
          <em>“Making Spaces Shine, Every Time!”</em></p>
        </td>
      </tr>

      <!-- Footer -->
      <tr>
        <td align=""center"" bgcolor=""#F7FDFD"" 
            style=""padding:20px; font-size:14px; color:#777777; border-bottom-left-radius:10px; border-bottom-right-radius:10px;"">
          &copy; 2025 Swachify. All rights reserved.
        </td>
      </tr>

      <tr>
        <td align=""center"" bgcolor=""#F3F4F6"" 
            style=""padding:15px; border-bottom-left-radius:10px; border-bottom-right-radius:10px;"">
          <a href=""https://swachify.com"" 
             style=""color:#2563EB; text-decoration:none; font-weight:bold;"">Visit Our Website</a>
        </td>
      </tr>

    </table>
  </body>
</html>";


        await _emailService.SendEmailAsync(booking.email, subject, body);
      }

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
      existing.full_name = updatedBooking.full_name;
      existing.address = updatedBooking.address;
      existing.phone = updatedBooking.phone;
      existing.email = updatedBooking.email;
      existing.status_id = updatedBooking.status_id;
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
