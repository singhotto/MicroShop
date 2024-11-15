using AutoMapper;
using Supply.Repository.Model;
using Supply.Shared.Dto;
using System.Diagnostics.CodeAnalysis;

namespace Supply.Business.Profiles;

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
        CreateMap<Product, KafkaProductInsertDto>();
        CreateMap<KafkaProductInsertDto, Product>();
        CreateMap<ProductInsertDto, Product>()
            .ForMember(dest => dest.User_Id, opt => opt.MapFrom(src => src.Supplier_Id));
        CreateMap<Product, ProductDto>();

        CreateMap<OrderDto, Order>();
        CreateMap<OrderProductDto, Order>();
        CreateMap<Order, OrderDto>()
            .ForMember(dest => dest.User_Id, opt => opt.MapFrom(src => src.User_Id))
            .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products))
            .ForMember(dest => dest.Supplier, opt => opt.MapFrom(src => src.Supplier));

        CreateMap<SupplierDto, Supplier>();
        CreateMap<SupplierInsertDto, Supplier>();
        CreateMap<Supplier, SupplierDto>()
            .ForMember(s => s.Products, opt => opt.MapFrom(src => src.Products));

        CreateMap<ProductOrderDto, ProductOrderList>();
        CreateMap<ProductOrderInsertDto, ProductOrderList>();
        CreateMap<ProductOrderList, ProductOrderDto>();

        CreateMap<CategoryDto, Category>();
        CreateMap<Category, CategoryDto>();

    }
}