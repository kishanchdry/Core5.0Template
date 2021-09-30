using AutoMapper;
using Data.Entities;
using Data.Entities.Identity;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ayurveda.Web.Helper
{
    /// <summary>
    /// Mapping extention
    /// </summary>
    public class MappingProfiles : Profile
    {
        /// <summary>
        /// Mapping details
        /// </summary>
        public MappingProfiles()
        {
            //CreateMap<ProductCategoryEntity, ProductModel>();
            //.ForMember(d => d.ProductBrand, o => o.MapFrom(s => s.ProductBrand.Name))
            //.ForMember(d => d.ProductType, o => o.MapFrom(s => s.ProductType.Name))
            //.ForMember(d => d.PictureUrl, o => o.MapFrom<ProductUrlResolver>());
            //cfg.CreateMap<Order, OrderEntity>().ForMember(dest => dest.OrderStatus, opt => opt.MapFrom<CustomResolver, string>(src => src.OrderStatus)); ;
            //cfg.CreateMap<OrderEntity, Order>().ForMember(dest => dest.OrderStatus, opt => opt.MapFrom<CustomResolver, int>(src => src.OrderStatus)); ;
            //ForMember(e => e.CategoryId, x => x.MapFrom(r => r.ProductCategoryId));
            CreateMap<ExceptionModel, ExceptionEntity>().ReverseMap();
            CreateMap<UserModel, User>().ReverseMap();
        }
    }

    /// <summary>
    /// Custome resolver 
    /// </summary>
    public class CustomResolver : IMemberValueResolver<object, object, long, long>
    {
        /// <summary>
        /// Resolve custom mapping
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <param name="sourceMember"></param>
        /// <param name="destMember"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public long Resolve(object source, object destination, long sourceMember, long destMember, ResolutionContext context)
        {
            destMember = sourceMember;

            return destMember;
        }
    }
}
