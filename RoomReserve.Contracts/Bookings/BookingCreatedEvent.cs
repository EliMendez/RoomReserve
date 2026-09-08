namespace RoomReserve.Contracts.Bookings
{
    public record BookingCreatedEvent(
        int BookingId,
        int RoomId,
        DateOnly BookingDate,
        TimeOnly StartTime,
        TimeOnly EndTime,
        int NumberOfAttendees,
        decimal Total,
        string Email
    );
}
