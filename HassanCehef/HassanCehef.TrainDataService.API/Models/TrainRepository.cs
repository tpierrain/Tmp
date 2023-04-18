using System;
using System.Collections.Generic;
using System.Linq;

namespace HassanCehef.TrainDataService.Models
{
    public class TrainRepository : IProvideTrain
    {
        readonly object syncRoot = new object();
        readonly Dictionary<string, Train> _trains = new Dictionary<string, Train>();

        public Train GetTrain(string trainId)
        {
            lock (this.syncRoot)
            {
                if (!_trains.ContainsKey(trainId))
                {
                    // First time, we create the train with default value
                    var train = new Train(trainId);
                    foreach (var c in "ABCDEFGHIJKL")
                    {
                        var coach = new Coach(c.ToString());

                        for (var i = 1; i < 42; i++)
                        {
                            var seat = new Seat(coach.Name, i.ToString(), string.Empty);
                            coach.Seats.Add(seat);
                        }

                        train.Add(coach);
                    }

                    _trains.Add(trainId, train);
                }
            }

            lock (this.syncRoot)
            {
                return _trains[trainId];
            }
        }

        public void UpdateTrainReservations(TrainUpdateDTO trainUpdateDto)
        {
            if (string.IsNullOrEmpty(trainUpdateDto.train_id))
            {
                throw new InvalidOperationException("Must have a non-null or non-empty train_id");
            }

            var train = GetTrain(trainUpdateDto.train_id);

            var seats = new List<Seat>();
            foreach (var seatInString in trainUpdateDto.seats)
            {
                // BUGFIX: 25/04/17
                var seatNumber = string.Concat(seatInString.Where(c => char.IsDigit(c)));
                var coach = string.Concat(seatInString.Where(c => char.IsLetter(c)));
                // END OF BUGFIX: 25/04/17

                var s = new Seat(coach, seatNumber, trainUpdateDto.booking_reference);
                seats.Add(s);
            }

            train.Reserve(seats, trainUpdateDto.booking_reference);
        }
    }

    public class Coach
    {
        public Coach(string coachName)
        {
            this.Name = coachName;
            this.Seats = new List<Seat>();
        }

        public List<Seat> Seats { get; set; }
        public string Name { get; set; }

        public void UpsertSeat(Seat seat)
        {
            this.Seats.Remove(seat);
            this.Seats.Add(seat);
        }
    }
}