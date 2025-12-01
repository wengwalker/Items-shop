using ItemsShop.Catalogs.Features.Features.Products.GetProduct;
using ItemsShop.Catalogs.PublicApi;
using ItemsShop.Catalogs.PublicApi.Contracts;
using ItemsShop.Common.Domain.Results;

namespace ItemsShop.Catalogs.Features.InternalApi;

internal sealed class CatalogModuleApi(IGetProductHandler handler) : ICatalogModuleApi
{
    public async Task<Result<ProductResponse>> GetProductAsync(GetProductRequest request, CancellationToken cancellationToken)
    {
        return await handler.HandleAsync(request, cancellationToken);
    }
}
