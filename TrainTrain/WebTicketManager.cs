using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace TrainTrain
{
    public class WebTicketManager
    {
        private const string UriBookingReferenceService = "https://localhost:7264/";
        private const string UriTrainDataService = "https://localhost:7177";
        private readonly ITrainCaching _trainCaching;
        private readonly ITrainDataService _trainDataService;
        private readonly IBookingReferenceService _bookingReferenceService;

        public WebTicketManager():this(new TrainDataService(UriTrainDataService), new BookingReferenceService(UriBookingReferenceService))
        {
        }

        public WebTicketManager(ITrainDataService trainDataService, IBookingReferenceService bookingReferenceService)
        {
            _trainDataService = trainDataService;
            _bookingReferenceService = bookingReferenceService;
            _trainCaching = new TrainCaching();
            _trainCaching.Clear();
        }

        public async Task<string> Reserve(string trainId, int seatsRequestedCount)
        {
            List<Seat> availableSeats = new List<Seat>();
            int count = 0;
            var result = string.Empty;
            string bookingRef;

            // get the train
            var JsonTrain = await _trainDataService.GetTrain(trainId);

            result = JsonTrain;

            var trainInst = new Train(JsonTrain);
            if ((trainInst.ReservedSeats + seatsRequestedCount) <= Math.Floor(ThreasholdManager.GetMaxRes() * trainInst.GetMaxSeat()))
            {
                var numberOfReserv = 0;
                // find seats to reserve
                for (int index = 0, i = 0; index < trainInst.Seats.Count; index++)
                {
                    var each = trainInst.Seats[index];
                    if (each.BookingRef == "")
                    {
                        i++;
                        if (i <= seatsRequestedCount)
                        {
                            availableSeats.Add(each);
                        }
                    }
                }

                foreach (var a in availableSeats)
                {
                    count++;
                }

                var reservedSets = 0;


                if (count != seatsRequestedCount)
                {
                    return string.Format("{{\"train_id\": \"{0}\", \"booking_reference\": \"\", \"seats\": []}}",
                        trainId);
                }
                else
                {
                    bookingRef = await _bookingReferenceService.GetBookingReference();

                    foreach (var availableSeat in availableSeats)
                    {
                        availableSeat.BookingRef = bookingRef;
                        numberOfReserv++;
                        reservedSets++;
                    }
                }

                if (numberOfReserv == seatsRequestedCount)
                {
                    await _trainCaching.Save(trainId, trainInst, bookingRef);

                    await _trainDataService.Reserve(trainId, bookingRef, availableSeats);

                    var todod = "[TODOD]";


                        return string.Format(
                            "{{\"train_id\": \"{0}\", \"booking_reference\": \"{1}\", \"seats\": {2}}}", 
                            trainId,
                            bookingRef, 
                            dumpSeats(availableSeats));
                    
                }
            }

            return string.Format("{{\"train_id\": \"{0}\", \"booking_reference\": \"\", \"seats\": []}}", trainId);
        }

        private string dumpSeats(IEnumerable<Seat> seats)
        {
            var sb = new StringBuilder("[");

            var firstTime = true;
            foreach (var seat in seats)
            {
                if (!firstTime)
                {
                    sb.Append(", ");
                }
                else
                {
                    firstTime = false;
                }

                sb.Append(string.Format("\"{0}{1}\"", seat.SeatNumber, seat.CoachName));
            }

            sb.Append("]");

            return sb.ToString();
        }
    }
}