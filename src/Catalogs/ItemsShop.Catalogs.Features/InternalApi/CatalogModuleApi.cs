using ItemsShop.Catalogs.Features.Features.Products.GetProduct;
using ItemsShop.Catalogs.PublicApi;
using ItemsShop.Catalogs.PublicApi.Contracts;
using ItemsShop.Common.Domain.Results;
using Mediator.Lite.Interfaces;

namespace ItemsShop.Catalogs.Features.InternalApi;

internal sealed class CatalogModuleApi(
    IRequestHandler<GetProductQuery, Result<GetProductResponse>> getProductHandler) : ICatalogModuleApi
{
    public async Task<Result<GetProductResponse>> GetProductAsync(GetProductRequest request, CancellationToken cancellationToken)
    {
        return await getProductHandler.Handle(new GetProductQuery(request.ProdutId), cancellationToken);
    }
}
