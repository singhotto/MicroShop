using AutoMapper;
using MicroShop.Repository.Model;
using MicroShop.Shared.Dto;
using System.Diagnostics.CodeAnalysis;

namespace MicroShop.Business.Profiles;

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
        CreateMap<CartDto, CartItem>();
        CreateMap<CartInsertDto, CartItem>();
        CreateMap<CartItem, CartDto>();
    }
}