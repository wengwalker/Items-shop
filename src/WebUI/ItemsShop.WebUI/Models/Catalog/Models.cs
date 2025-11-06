namespace ItemsShop.WebUI.Models.Catalog;

public sealed record CategoryDto(Guid Id, string Name, string? Description);

public sealed record ProductDto(
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    long Quantity,
    DateTime CreatedAt,
    DateTime UpdatedAt,
    Guid CategoryId);

public sealed record CartDto(Guid CartId, DateTime LastUpdated);

public sealed class CartItemDto
{
    public Guid Id { get; init; }
    public int Quantity { get; set; }
    public Guid CartId { get; init; }
    public Guid ProductId { get; init; }

    public CartItemDto(Guid id, int quantity, Guid cartId, Guid productId)
    {
        Id = id;
        Quantity = quantity;
        CartId = cartId;
        ProductId = productId;
    }
}

// Requests
public sealed record CreateCategoryRequest(string Name, string? Description);
public sealed record UpdateCategoryNameRequest(string Name);
public sealed record UpdateCategoryDescriptionRequest(string? Description);

public sealed record CreateProductRequest(string Name, string Description, decimal Price, long Quantity, Guid CategoryId);
public sealed record UpdateProductCategoryRequest(Guid CategoryId);
public sealed record UpdateProductPriceRequest(decimal Price);
public sealed record UpdateProductQuantityRequest(long Quantity);
public sealed record UpdateProductDescriptionRequest(string Description);

public sealed record CreateCartItemRequest(int Quantity, Guid ProductId);


