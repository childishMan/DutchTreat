using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DutchTreat.Data.Entities;
using DutchTreat.Models;

namespace DutchTreat.Data
{
    public class DutchMapping : Profile
    {
        public DutchMapping()
        {
            CreateMap<Order, OrderModel>()
                .ForMember(o => o.OrderId,
                    ex => ex.MapFrom(o => o.Id))
                .ReverseMap();

            CreateMap<OrderItem, OrderItemModel>()
                .ReverseMap();
        }
    }
}
