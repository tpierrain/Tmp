using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrainTrain.Api.Models
{
    public class ReservationRequestDto
    {
        public string train_id { get; set; }
        public int number_of_seats { get; set; }
    }
}
