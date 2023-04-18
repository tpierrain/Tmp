using System.Collections.Generic;

namespace HassanCehef.TrainDataService.Models
{
    public class TrainUpdateDTO
    {
        public string train_id { get; set; }
        public List<string> seats { get; set; }
        public string booking_reference { get; set; }
    }
}