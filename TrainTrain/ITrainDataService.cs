using System.Collections.Generic;
using System.Threading.Tasks;

namespace TrainTrain
{
    public interface ITrainDataService
    {
        Task<string> GetTrain(string trainId);
        Task Reserve(string trainId, string bookingRef, List<Seat> availableSeats);
    }
}