﻿using AutoMapper;
using Discount.API.Entities;
using Discount.Grpc.Protos;

namespace Discount.gRPC.Mapper
{
    public class DiscountProfile : Profile
    {
        public DiscountProfile()
        {
            CreateMap<Coupon, CouponModel>().ReverseMap();
        }
    }
}
