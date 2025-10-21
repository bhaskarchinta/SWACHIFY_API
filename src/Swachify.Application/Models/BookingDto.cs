namespace Swachify.Application.DTOs
{
    public record BookingDto(
        long Id,
        string? BookingId,
        long DeptId,
        long ServiceId,
        long SlotId,
        long? CreatedBy,
        DateTime? CreatedDate,
        long? ModifiedBy,
        DateTime? ModifiedDate,
        bool? IsActive,
        DateOnly? PreferredDate
    );
}
