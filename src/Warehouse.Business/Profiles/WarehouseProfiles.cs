using AutoMapper;
using System.Diagnostics.CodeAnalysis;
using Warehouse.Repository.Model;
using Warehouse.Shared.Dto;

namespace Warehouse.Business.Profiles;

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

        CreateMap<CategoryDto, Category>();
        CreateMap<Category, CategoryDto > ();
        CreateMap<CategoryInsertDto, Category>();
        CreateMap<Category, CategoryInsertDto>();
    }
}