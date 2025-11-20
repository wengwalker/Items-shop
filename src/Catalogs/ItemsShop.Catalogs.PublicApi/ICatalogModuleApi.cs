using ItemsShop.Catalogs.PublicApi.Contracts;
using ItemsShop.Common.Domain.Results;

namespace ItemsShop.Catalogs.PublicApi;

public interface ICatalogModuleApi
{
    Task<Result<GetProductResponse>> GetProductAsync(GetProductRequest request, CancellationToken cancellationToken);
}
