
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;
using Web.Services.Abstract;

namespace Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IIdentityService identityService;

        public AuthController(IIdentityService identityService)
        {
            this.identityService = identityService;
        }

        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SigninInput signinInput)
        {
            if (!ModelState.IsValid)
            {

            }

            var response = await identityService.SignIn(signinInput);

            if (!response.IsSuccessful)
            {
                response.Errors?.ForEach(x =>
                 ModelState.AddModelError(string.Empty, x)
                );
            }

            return RedirectToAction(nameof(Index), "Home");

        }
    }
}
