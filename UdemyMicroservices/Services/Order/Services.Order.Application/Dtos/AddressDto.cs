﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Order.Application.Dtos
{
    public class AddressDto
    {
        public string Province { get; set; }

        public string District { get; set; }

        public string Street { get; set; }

        public string ZipCode { get; set; }

        public int MyProperty { get; set; }

        public string Line { get; set; }
    }
}