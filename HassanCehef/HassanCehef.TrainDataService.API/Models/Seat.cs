using System.Text;

namespace HassanCehef.TrainDataService.Models
{
    public class  Seat
    {
        public string booking_reference { get; set; }
        public string seat_number { get; set; }
        public string coach { get; set; }

        public Seat(string coach, string seatNumber, string bookingReference)
        {
            booking_reference = bookingReference;
            seat_number = seatNumber;
            this.coach = coach;
        }

        protected bool Equals(Seat other)
        {
            return string.Equals(seat_number, other.seat_number) && string.Equals(coach, other.coach);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;

            return Equals((Seat) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((seat_number != null ? seat_number.GetHashCode() : 0) * 397) ^ (coach != null ? coach.GetHashCode() : 0);
            }
        }

        public override string ToString()
        {
            return $"\"{seat_number}{coach}\": {{\"booking_reference\": \"{booking_reference}\", \"seat_number\": \"{seat_number}\", \"coach\": \"{coach}\"}}";
        }
    }
}