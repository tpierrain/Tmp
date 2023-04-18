using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HassanCehef.BookingReferenceService.Controllers
{
    [Route("booking_reference")]
    public class ValuesController : Controller
    {
        // GET api/values
        [HttpGet]
        public string Get()
        {
            return GetNewBookingReference();
            //return new string[] { "value1", "value2" };
        }

        private static readonly Random random = new Random();

        private static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private string GetNewBookingReference()
        {
            return RandomString(6);
        }
    }
}
