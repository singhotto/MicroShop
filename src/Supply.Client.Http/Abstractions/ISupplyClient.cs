using Supply.Shared.Dto;

namespace Supply.Client.Http.Abstractions;
public interface ISupplyClient {

    Task<SupplierDto?> AddSupplierHttp(string supplierEmail, string accessToken);
}
