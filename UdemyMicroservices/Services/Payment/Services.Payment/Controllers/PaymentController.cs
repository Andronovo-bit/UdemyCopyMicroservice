using Microsoft.AspNetCore.Mvc;
using Shared.Library.ControllerBases;
using Shared.Library.Dtos;

namespace Services.Payment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : CustomBaseController
    {
        [HttpPost]
        public IActionResult ReceiverPayment()
        {
            return CreateActionResultInstance<NoContent>(Response<NoContent>.Success(200));
        }
    }
}
