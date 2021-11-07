using MediatR;
using Microsoft.EntityFrameworkCore;
using Service.Order.Infrastructure;
using Services.Order.Application.Dtos;
using Services.Order.Application.Mapping;
using Services.Order.Application.Queries;
using Shared.Library.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Order.Application.Handlers
{
    public class GetOrderByUserIdQueryHandler : IRequestHandler<GetOrdersByUserIdQuery, Response<List<OrderDto>>>
    {

        private readonly OrderDbContext _context;

        public GetOrderByUserIdQueryHandler(OrderDbContext context)
        {
            _context = context;
        }

        public async Task<Response<List<OrderDto>>> Handle(GetOrdersByUserIdQuery request, CancellationToken cancellationToken)
        {
            var orders = _context.Orders
                .Include(t => t.OrderItems)
                .Where(k => k.BuyerId == request.UserId)
                .AsNoTracking();

            if (!orders.Any())
            {
                return Response<List<OrderDto>>.Success(new List<OrderDto>(), 200);

            }

            var ordersDto = ObjectMapper.Mapper.Map<List<OrderDto>>(await orders.ToListAsync());

            return Response<List<OrderDto>>.Success(ordersDto, 200);

        }
    }
}
