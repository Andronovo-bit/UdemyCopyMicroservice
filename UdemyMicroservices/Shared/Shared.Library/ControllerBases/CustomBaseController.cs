using Microsoft.AspNetCore.Mvc;
using Shared.Library.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Library.ControllerBases
{
    public class CustomBaseController: ControllerBase
    {

        public IActionResult CreateActionResultInstance<T>(Response<T> response)
        {
            return new ObjectResult(response)
            {
                StatusCode = response.StatusCode
            };
        }

    }
}
