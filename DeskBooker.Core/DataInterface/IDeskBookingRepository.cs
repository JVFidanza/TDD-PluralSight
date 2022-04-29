using DeskBooker.Core.Processor;

namespace DeskBooker.Core.DataInterface;

public interface IDeskBookingRepository
{
    void Save(DeskBooking deskBooking);
}