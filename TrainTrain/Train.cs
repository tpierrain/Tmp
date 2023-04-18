using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace TrainTrain
{
    public class Train
    {
        public Train(string trainTopol)
        {
            this.Seats = new List<Seat>();
            //var sample =
            //"{\"seats\": {\"1A\": {\"booking_reference\": \"\", \"seat_number\": \"1\", \"coach\": \"A\"}, \"2A\": {\"booking_reference\": \"\", \"seat_number\": \"2\", \"coach\": \"A\"}}}";

            // Forced to workaround with dynamic parsing since the received JSON is invalid format ;-(
            dynamic parsed = JsonConvert.DeserializeObject(trainTopol);

            foreach (var token in ((Newtonsoft.Json.Linq.JContainer)parsed))
            {
                var allStuffs = ((Newtonsoft.Json.Linq.JObject) ((Newtonsoft.Json.Linq.JContainer) token).First);

                foreach (var stuff in allStuffs)
                {
                    var seat = stuff.Value.ToObject<SeatJsonPoco>();
                    this.Seats.Add(new Seat(seat.coach, int.Parse(seat.seat_number), seat.booking_reference));
                    if (!string.IsNullOrEmpty(seat.booking_reference))
                    {
                        this.ReservedSeats++;
                    }
                }
            }
        }

        public int GetMaxSeat()
        {
            return this.Seats.Count;
        }

        public int ReservedSeats { get; set; }
        public List<Seat> Seats { get; set; }

        public bool HasLessThanThreshold(int i)
        {
            return ReservedSeats < i;
        }
    }

    public class TrainJsonPoco
    {
        public List<SeatJsonPoco> seats { get; set;  }

        public TrainJsonPoco()
        {
            this.seats = new List<SeatJsonPoco>();
        }
    }

    public class SeatJsonPoco
    {
        public string booking_reference { get; set; }
        public string seat_number { get; set; }
        public string coach { get; set; }

        public SeatJsonPoco()
        {
        }
    }
}