using System;
using DeskBooker.Core.DataInterface;

namespace DeskBooker.Core.Processor;

public class DeskBookingRequestProcessor
{
    private readonly IDeskBookingRepository _deskBookingRepository;

    public DeskBookingRequestProcessor(IDeskBookingRepository deskBookingRepository)
    {
        _deskBookingRepository = deskBookingRepository;
    }

    public DeskBookingResult BookDesk(DeskBookingRequest request)
    {
        if (request == null)
        {
            throw new ArgumentNullException(nameof(request));
        }

        
        _deskBookingRepository.Save(Create<DeskBooking>(request));

        return Create<DeskBookingResult>(request);
    }

    private static T Create<T>(DeskBookingBase request) where T : DeskBookingBase, new()
    {
        return new T()
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            Date = request.Date
        };
    }
}