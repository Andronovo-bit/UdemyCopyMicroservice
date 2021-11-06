﻿using AutoMapper;
using Services.Order.Application.Dtos;
using Services.Order.Domain.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Order.Application.Mapping
{
    class CustomMapping:Profile
    {
        public CustomMapping()
        {
            CreateMap<Domain.OrderAggregate.Order, OrderDto>().ReverseMap();
            CreateMap<OrderItem, OrderDto>().ReverseMap();
            CreateMap<Address, AddressDto>().ReverseMap();
        }
    }
}
