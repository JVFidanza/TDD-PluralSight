using System;
using DeskBooker.Core.DataInterface;
using Moq;
using Xunit;

namespace DeskBooker.Core.Processor;

public class DeskBookingRequestProcessorTests
{
    private readonly DeskBookingRequestProcessor _processor;
    private readonly DeskBookingRequest _request;
    private readonly Mock<IDeskBookingRepository> _deskBookingRepositoryMock;

    public DeskBookingRequestProcessorTests()
    {
        _deskBookingRepositoryMock = new Mock<IDeskBookingRepository>();
        _processor = new DeskBookingRequestProcessor(_deskBookingRepositoryMock.Object);
        _request = new DeskBookingRequest
        {
            FirstName = "JV",
            LastName = "FA",
            Email = "vitor.fidanza@gmail.com",
            Date = new DateTime(2022, 4, 28)
        };
    }
    [Fact]
    public void ShouldReturnDeskBookingResultWithRequestValues()
    {
        //Act
        DeskBookingResult result = _processor.BookDesk(_request);
        
        //Assert
        Assert.NotNull(result);
        Assert.Equal(_request.FirstName, result.FirstName);
        Assert.Equal(_request.Email, result.Email);
        Assert.Equal(_request.LastName, result.LastName);
        Assert.Equal(_request.Date, result.Date);
        
    }

    [Fact]
    public void ShouldThrowExceptionIfRequestIsNull()
    {
        var exception = Assert.Throws<ArgumentNullException>(() => _processor.BookDesk(null));
        
        Assert.Equal("request", exception.ParamName);
    }

    [Fact]
    public void ShouldSaveDeskBooking()
    {
        DeskBooking? savedDeskBooking = null;
        _deskBookingRepositoryMock.Setup(x => x.Save(It.IsAny<DeskBooking>()))
            .Callback<DeskBooking>((deskBooking) =>
            {
                savedDeskBooking = deskBooking;
            });
        _processor.BookDesk(_request);
        _deskBookingRepositoryMock.Verify(x => x.Save(It.IsAny<DeskBooking>()), Times.Once);
        Assert.NotNull(savedDeskBooking);
        Assert.Equal(_request.FirstName, savedDeskBooking?.FirstName);
        Assert.Equal(_request.LastName, savedDeskBooking?.LastName);
        Assert.Equal(_request.Email, savedDeskBooking?.Email);
        Assert.Equal(_request.Date, savedDeskBooking?.Date);
    }
}