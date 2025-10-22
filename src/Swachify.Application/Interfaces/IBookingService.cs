using Swachify.Application.DTOs;
using Swachify.Infrastructure.Models;


namespace Swachify.Application.Interfaces
{
    public interface IBookingService
    {
        Task<List<AllBookingsDtos>> GetAllAsync(CancellationToken ct = default);
        Task<List<AllBookingsDtos>> GetAllBookingByUserIDAsync(long userid);    
        Task<service_booking?> GetByIdAsync(long id, CancellationToken ct = default);
        Task<long> CreateAsync(service_booking booking, CancellationToken ct = default);
        Task<bool> UpdateAsync(long id, service_booking updatedBooking, CancellationToken ct = default);
        Task<bool> DeleteAsync(long id, CancellationToken ct = default);
    }
}
