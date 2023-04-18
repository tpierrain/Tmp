using HassanCehef.TrainDataService.Models;
using Microsoft.AspNetCore.Mvc;

namespace HassanCehef.TrainDataService.Controllers
{
    [Route("reserve")]
    public class ReserveController : Controller
    {
        // POST on 
        // http://localhost:50680/api/reserve
        // with JSON payload:
        //  {
        //      "train_id": "5FSdR",
        //      "seats": ["4A", "5A"],
        //      "booking_reference": "Td98kms"
        //  }
        //
        private readonly IProvideTrain trainProvider;

        public ReserveController(IProvideTrain trainProvider)
        {
            this.trainProvider = trainProvider;
        }

        // POST api/data_for_train
        [HttpPost]
        public void Post([FromBody] TrainUpdateDTO value)
        {
            trainProvider.UpdateTrainReservations(value);
        }
    }
}