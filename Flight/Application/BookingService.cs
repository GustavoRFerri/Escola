using Data;

namespace Application
{
    public class BookingService
    {
        public Entities Entities { get; set; }

        public BookingService(Entities entities)
        {
            Entities = entities;
        }


        public void Book(BookDto bookDto)
        {
            var flight = Entities.Flights.Find(bookDto.FlightId);

            flight.Book(bookDto.PassengerEmail, bookDto.NumberOfSeats);
            Entities.SaveChanges();
        }

        public IEnumerable<BookingRm> FindBookings(Guid flightId)
        {
            return Entities.Flights
                .Find(flightId)
                .BookingList
                .Select(booking => new BookingRm(
                    booking.Email,
                    booking.NumberOfSeats
                    ));

            //return new[]
            //{
            //    new BookingRm(passengerEmail: "a@b.com", numberOfSeats: 2)
            //};
        }

        public void CancelBooking(CancelBookingDto cancelBookingDto)
        {
            var flight = Entities.Flights.Find(cancelBookingDto.Flight);
            flight.CancelBooking(cancelBookingDto.PassengerEmail, cancelBookingDto.NumberOfSeats);
            Entities.SaveChanges();            
        }

        public object GetRemainingNumberOfSeats(Guid flightId)
        {
            return Entities.Flights.Find(flightId).RemainingNumberOfSeats;
        }
    }
}
;