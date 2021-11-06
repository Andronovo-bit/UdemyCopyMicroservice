using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Order.Application.Mapping
{
    public static class ObjectMapper
    {
        private static readonly Lazy<IMapper> lazy = new Lazy<IMapper>(() =>  // Ne zaman çağrılırsa o zaman burası çalışacak(initilize)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CustomMapping>();
            });

            return config.CreateMapper();
        });

        public static IMapper Mapper => lazy.Value;
    }
}
