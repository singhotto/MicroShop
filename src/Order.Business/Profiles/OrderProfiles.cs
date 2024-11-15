using AutoMapper;
using Order.Repository.Model;
using Order.Shared.Dto;
using System.Diagnostics.CodeAnalysis;

namespace Order.Business.Profiles;

/// <summary>
/// Marker per <see cref="AutoMapper"/>.
/// </summary>
public sealed class AssemblyMarker {
    AssemblyMarker() { }
}

[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)]
public class InputFileProfile : Profile {
    public InputFileProfile()
    {
        CreateMap<ProductDto, Product>();
        CreateMap<ProductInsertDto, Product>();
        CreateMap<Product, ProductDto>();
        CreateMap<KafkaPaymentProductDto, ProductDto>();

        CreateMap<OrderDto, Orders>();
        CreateMap<OrderProductDto, Orders>();
        CreateMap<OrderProductDto, OrderProductList>();
        CreateMap<OrderInsertDto, Orders>();
        CreateMap<KafkaOrderInsertDto, Orders>()
            .ForMember(dest => dest.Order_Id, opt => opt.Ignore())
            .ForMember(dest => dest.User, opt => opt.Ignore())
            .ForMember(dest => dest.Products, opt => opt.Ignore());

        CreateMap<Orders, OrderDto>()
            .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products));

        CreateMap<ProductOrderDto, OrderProductList>();
        CreateMap<ProductOrderInsertDto, OrderProductList>();
        CreateMap<OrderProductList, ProductOrderDto>();
    }
}