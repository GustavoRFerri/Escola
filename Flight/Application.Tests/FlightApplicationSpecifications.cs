using Data;
using Domain;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace Application.Tests
{
    public class FlightApplicationSpecifications
    {

        readonly Entities entities = new Entities(new DbContextOptionsBuilder<Entities>()
                .UseInMemoryDatabase("Flights")
                .Options);

        readonly BookingService bookingService;

        public FlightApplicationSpecifications()
        {
            bookingService = new BookingService(entities: entities);                
        }


        // [Fact]
        [Theory]
        [InlineData("m@m",2)]
        [InlineData("a@a", 2)]
        public void Remembers_bookings(string passengerEmail, int numberOfSeats)
        {
            var flight = new Flight(3);

            entities.Flights.Add(flight);
            bookingService.Book(new BookDto(
                flightId: flight.Id,passengerEmail, numberOfSeats));

            bookingService.FindBookings(flight.Id).Should().ContainEquivalentOf(
                new BookingRm(passengerEmail, numberOfSeats));
        }

        // [Fact]
        [Theory]
        [InlineData(3)]
        [InlineData(10)]
        public void Frees_up_seats_after_booking(int initialCapacity)
        {
            // Given
            var flight = new Flight(initialCapacity);
            entities.Flights.Add(flight);
            
            bookingService.Book(new BookDto(
                flightId: flight.Id,
                passengerEmail: "m@m.com",
                numberOfSeats: 2));

            // When
            bookingService.CancelBooking(
                new CancelBookingDto(flightId: flight.Id,
                passengerEmail: "m@m.com",
                numberOfSeats: 2));

            // Then
            bookingService.GetRemainingNumberOfSeats(flight.Id).Should().Be(initialCapacity);
        }   

    }

}