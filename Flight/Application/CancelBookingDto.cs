using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public class CancelBookingDto
    {
        public Guid Flight {  get; set; }
        public string PassengerEmail { get; set; }
        public int NumberOfSeats { get; set; }


        public CancelBookingDto(Guid flightId, string passengerEmail, int numberOfSeats)
        {
            Flight = flightId;
            PassengerEmail = passengerEmail;
            NumberOfSeats = numberOfSeats;             
        }

    }
}
