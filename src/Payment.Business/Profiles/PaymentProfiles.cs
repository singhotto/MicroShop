using AutoMapper;
using Payment.Repository.Model;
using Payment.Shared.Dto;
using System.Diagnostics.CodeAnalysis;

namespace Payment.Business.Profiles;

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
        CreateMap<TransactionDto, Transaction>();
        CreateMap<TransactionOrderInsertDto, Transaction>();
        CreateMap<Transaction, TransactionDto>();

        CreateMap<Transaction, OrderDto>();
        CreateMap<TransactionOrderInsertDto, OrderDto> ();
    }
}