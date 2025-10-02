using Infrastructure.Common.ExternalApi.Models;
using Refit;

namespace Infrastructure.Common.ExternalApi.Interfaces;

public interface ICatalogApi
{
    [Get("/api/v1/items/{itemId}/price")]
    Task<ProductItemPriceResponse> GetItemPrice(Guid itemId);
}
