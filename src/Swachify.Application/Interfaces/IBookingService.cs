using Swachify.Infrastructure.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Swachify.Application.Interfaces
{
    public interface IBookingService
    {
        Task<List<service_booking>> GetAllAsync(CancellationToken ct = default);
        Task<service_booking?> GetByIdAsync(long id, CancellationToken ct = default);
        Task<long> CreateAsync(service_booking booking, CancellationToken ct = default);
        Task<bool> UpdateAsync(long id, service_booking updatedBooking, CancellationToken ct = default);
        Task<bool> DeleteAsync(long id, CancellationToken ct = default);
    }
}
