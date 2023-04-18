using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HassanCehef.TrainDataService.Models
{
    public class Train
    {
        private string trainId;

        public List<Seat> Seats
        {
            get
            {
                var res = new List<Seat>();
                foreach (var coach in this.Coaches.Values)
                {
                    res.AddRange(coach.Seats);
                }

                return res;
            }
        }

        public Train(string trainId)
        {
            this.trainId = trainId;
            this.Coaches = new Dictionary<string, Coach>();
        }

        public Dictionary<string, Coach> Coaches { get; set; }

        public override string ToString()
        {
            var awkwardJson = new StringBuilder("{\"seats\": {");
            var firstElement = true;
            foreach (var seat in this.Seats)
            {
                if (!firstElement)
                {
                    awkwardJson.Append(", ");
                }
                else
                {
                    firstElement = false;
                }

                awkwardJson.Append($"{seat.ToString()}");
            }

            awkwardJson.Append("}}");

            return awkwardJson.ToString();
        }

        public void Add(Coach coach)
        {
            this.Coaches.Add(coach.Name, coach);
        }

        public void Reserve(List<Seat> seats, string bookingReference)
        {
            foreach (var seat in seats)
            {
                var coach = this.Coaches[seat.coach];
                coach.UpsertSeat(seat);
            }
        }
    }
}