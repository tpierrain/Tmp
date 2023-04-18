using Microsoft.AspNetCore.Mvc;
using HassanCehef.TrainDataService.Models;

namespace HassanCehef.TrainDataService.Controllers
{
    [Route("api/data_for_train")]
    public class TrainController : Controller
    {
        private readonly IProvideTrain trainProvider;

        public TrainController(IProvideTrain trainProvider)
        {
            this.trainProvider = trainProvider;
        }

        // GET api/data_for_train/5FSdR
        [HttpGet("{trainId}")]
        public string Get(string trainId)
        {
            return this.trainProvider.GetTrain(trainId).ToString();
        }
    }
}
