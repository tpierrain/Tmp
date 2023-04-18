using Microsoft.AspNetCore.Mvc;

namespace TrainTrain.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservationsController : Controller
{
    // POST api/reservations
    [HttpPost]
    public async Task<string> Post([FromBody] ReservationRequestDto reservationRequest)
    {
        var manager = new WebTicketManager();
        return await manager.Reserve(reservationRequest.train_id, reservationRequest.number_of_seats);
    }
}

