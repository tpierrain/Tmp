using System.ComponentModel.DataAnnotations;

namespace TrainTrain.Dal.Entities
{
    public class SeatEntity
    {
        [Key]
        public int SeatId { get; set; }
        public string CoachName { get; set; }
        public int SeatNumber { get; set; }
        public string BookingRef { get; set; }

        public string TrainId { get; set; }
        public virtual TrainEntity Train { get; set; }
    }
}