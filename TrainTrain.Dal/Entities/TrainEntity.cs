using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TrainTrain.Dal.Entities
{
    public class TrainEntity
    {
        [Key]
        public string TrainId { get; set; }

        public virtual List<SeatEntity> Seats { get; set; } = new List<SeatEntity>();
    }
}