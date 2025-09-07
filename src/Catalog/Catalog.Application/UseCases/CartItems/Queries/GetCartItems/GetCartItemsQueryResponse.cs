using Catalog.Domain.DTOs;

namespace Catalog.Application.UseCases.CartItems.Queries.GetCartItems;

public record GetCartItemsQueryResponse(
    List<CartItemDto> Items);
