namespace TrainTrain
{
    public class Seat
    {
        public string CoachName { get; }
        public int SeatNumber { get; }
        public string BookingRef { get; set;  }

        public Seat(string coachName, int seatNumber) : this(coachName, seatNumber, string.Empty)
        {
        }

        public Seat(string coachName, int seatNumber, string bookingRef)
        {
            this.CoachName = coachName;
            this.SeatNumber = seatNumber;
            this.BookingRef = bookingRef;
        }
    }
}