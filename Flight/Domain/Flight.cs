
namespace Domain
{
    public class Flight
    {
        List<Booking> bookingList = new();   
        public IEnumerable<Booking> BookingList => bookingList;
        
        // public List<Booking> BookingList { get; set; } = new List<Booking>();
        public int RemainingNumberOfSeats { get; set; }
        public int seatCapacity { get; set; }

        public Guid Id { get; }

        [Obsolete("Needed by EF")]

        public Flight(int seatCapacity)
        {
            RemainingNumberOfSeats = seatCapacity;
        }

        public object? Book(string passengerEmail, int numberOfSeats)
        {
            if (numberOfSeats > this.RemainingNumberOfSeats)
                return new OverbookingError();

            RemainingNumberOfSeats -= numberOfSeats;

            bookingList.Add(new Booking(passengerEmail, numberOfSeats));
            //RemainingNumberOfSeats = 2;
            // return null;
            // return RemainingNumberOfSeats;

            // return new OverbookingError();
            return RemainingNumberOfSeats;
        }

        public object? CancelBooking(string passengerEmail, int numberOfSeats)
        {
            if (!bookingList.Any(booking => booking.Email == passengerEmail))
                return new BookingNotFoundError();

            RemainingNumberOfSeats +=numberOfSeats;
            return null;
        }
    }
}
