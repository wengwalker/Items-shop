using System.Net.Http.Json;
using ItemsShop.WebUI.Models.Catalog;

namespace ItemsShop.WebUI.Services;

public class CatalogApiClient
{
    private readonly IHttpClientFactory _httpClientFactory;

    public CatalogApiClient(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    private HttpClient CreateClient() => _httpClientFactory.CreateClient("CatalogApi");

    // Categories
    public async Task<IReadOnlyList<CategoryDto>> GetCategoriesAsync(string? name = null, string? orderType = null, CancellationToken ct = default)
    {
        var query = new List<string>();
        if (!string.IsNullOrWhiteSpace(name)) query.Add($"name={Uri.EscapeDataString(name)}");
        if (!string.IsNullOrWhiteSpace(orderType)) query.Add($"orderType={Uri.EscapeDataString(orderType)}");
        var qs = query.Count > 0 ? "?" + string.Join("&", query) : string.Empty;
        var client = CreateClient();
        var resp = await client.GetFromJsonAsync<GetCategoriesResponseDto>($"/api/v1/categories{qs}", ct);
        return resp?.Categories ?? Array.Empty<CategoryDto>();
    }

    public async Task<CategoryDto?> GetCategoryAsync(Guid categoryId, CancellationToken ct = default)
    {
        var client = CreateClient();
        var resp = await client.GetFromJsonAsync<GetCategoryResponseDto>($"/api/v1/categories/{categoryId}", ct);
        return resp?.Category;
    }

    public async Task<CategoryDto?> CreateCategoryAsync(CreateCategoryRequest request, CancellationToken ct = default)
    {
        var client = CreateClient();
        var httpResp = await client.PostAsJsonAsync("/api/v1/categories", request, ct);
        httpResp.EnsureSuccessStatusCode();
        var created = await httpResp.Content.ReadFromJsonAsync<CreateCategoryResponseDto>(cancellationToken: ct);
        return created is null ? null : new CategoryDto(created.CategoryId, created.Name, created.Description);
    }

    public async Task UpdateCategoryNameAsync(Guid id, string name, CancellationToken ct = default)
    {
        var client = CreateClient();
        var httpResp = await client.PutAsJsonAsync($"/api/v1/categories/{id}/name", new UpdateCategoryNameRequest(name), ct);
        httpResp.EnsureSuccessStatusCode();
    }

    public async Task UpdateCategoryDescriptionAsync(Guid id, string? description, CancellationToken ct = default)
    {
        var client = CreateClient();
        var httpResp = await client.PutAsJsonAsync($"/api/v1/categories/{id}/description", new UpdateCategoryDescriptionRequest(description), ct);
        httpResp.EnsureSuccessStatusCode();
    }

    public async Task DeleteCategoryAsync(Guid id, CancellationToken ct = default)
    {
        var client = CreateClient();
        var httpResp = await client.DeleteAsync($"/api/v1/categories/{id}", ct);
        httpResp.EnsureSuccessStatusCode();
    }

    // Products
    public async Task<IReadOnlyList<ProductDto>> GetProductsAsync(string? name = null, string? orderType = null, CancellationToken ct = default)
    {
        var query = new List<string>();
        if (!string.IsNullOrWhiteSpace(name)) query.Add($"name={Uri.EscapeDataString(name)}");
        if (!string.IsNullOrWhiteSpace(orderType)) query.Add($"orderType={Uri.EscapeDataString(orderType)}");
        var qs = query.Count > 0 ? "?" + string.Join("&", query) : string.Empty;
        var client = CreateClient();
        var resp = await client.GetFromJsonAsync<GetProductsResponseDto>($"/api/v1/products{qs}", ct);
        return resp?.Products ?? Array.Empty<ProductDto>();
    }

    public async Task<ProductDto?> GetProductAsync(Guid id, CancellationToken ct = default)
    {
        var client = CreateClient();
        var resp = await client.GetFromJsonAsync<GetProductResponseDto>($"/api/v1/products/{id}", ct);
        return resp?.Product;
    }

    public async Task<ProductDto?> CreateProductAsync(CreateProductRequest request, CancellationToken ct = default)
    {
        var client = CreateClient();
        var httpResp = await client.PostAsJsonAsync("/api/v1/products", request, ct);
        httpResp.EnsureSuccessStatusCode();
        var created = await httpResp.Content.ReadFromJsonAsync<CreateProductResponseDto>(cancellationToken: ct);
        if (created is null) return null;
        return new ProductDto(created.Id, created.Name, created.Description, created.Price, created.Quantity, created.CreatedAt, created.UpdatedAt, created.CategoryId);
    }

    public async Task UpdateProductCategoryAsync(Guid id, Guid categoryId, CancellationToken ct = default)
    {
        var client = CreateClient();
        var httpResp = await client.PutAsJsonAsync($"/api/v1/products/{id}/category", new UpdateProductCategoryRequest(categoryId), ct);
        httpResp.EnsureSuccessStatusCode();
    }

    public async Task UpdateProductPriceAsync(Guid id, decimal price, CancellationToken ct = default)
    {
        var client = CreateClient();
        var httpResp = await client.PutAsJsonAsync($"/api/v1/products/{id}/price", new UpdateProductPriceRequest(price), ct);
        httpResp.EnsureSuccessStatusCode();
    }

    public async Task UpdateProductQuantityAsync(Guid id, long quantity, CancellationToken ct = default)
    {
        var client = CreateClient();
        var httpResp = await client.PutAsJsonAsync($"/api/v1/products/{id}/quantity", new UpdateProductQuantityRequest(quantity), ct);
        httpResp.EnsureSuccessStatusCode();
    }

    public async Task UpdateProductDescriptionAsync(Guid id, string description, CancellationToken ct = default)
    {
        var client = CreateClient();
        var httpResp = await client.PutAsJsonAsync($"/api/v1/products/{id}/description", new UpdateProductDescriptionRequest(description), ct);
        httpResp.EnsureSuccessStatusCode();
    }

    public async Task DeleteProductAsync(Guid id, CancellationToken ct = default)
    {
        var client = CreateClient();
        var httpResp = await client.DeleteAsync($"/api/v1/products/{id}", ct);
        httpResp.EnsureSuccessStatusCode();
    }

    // Carts & Items
    public async Task<CartDto?> CreateCartAsync(CancellationToken ct = default)
    {
        var client = CreateClient();
        var httpResp = await client.PostAsync("/api/v1/carts", content: null, ct);
        httpResp.EnsureSuccessStatusCode();
        var created = await httpResp.Content.ReadFromJsonAsync<CreateCartResponseDto>(cancellationToken: ct);
        return created is null ? null : new CartDto(created.CartId, created.LastUpdated);
    }

    public async Task<CartDto?> GetCartAsync(Guid cartId, CancellationToken ct = default)
    {
        var client = CreateClient();
        var resp = await client.GetFromJsonAsync<GetCartResponseDto>($"/api/v1/carts/{cartId}", ct);
        return resp is null ? null : new CartDto(resp.CartId, resp.LastUpdated);
    }

    public async Task DeleteCartAsync(Guid cartId, CancellationToken ct = default)
    {
        var client = CreateClient();
        var httpResp = await client.DeleteAsync($"/api/v1/carts/{cartId}", ct);
        httpResp.EnsureSuccessStatusCode();
    }

    public async Task<IReadOnlyList<CartItemDto>> GetCartItemsAsync(Guid cartId, CancellationToken ct = default)
    {
        var client = CreateClient();
        var resp = await client.GetFromJsonAsync<GetCartItemsResponseDto>($"/api/v1/carts/{cartId}/items", ct);
        return resp?.Items ?? Array.Empty<CartItemDto>();
    }

    public async Task<CartItemDto?> GetCartItemAsync(Guid cartId, Guid itemId, CancellationToken ct = default)
    {
        var client = CreateClient();
        var resp = await client.GetFromJsonAsync<GetCartItemResponseDto>($"/api/v1/carts/{cartId}/items/{itemId}", ct);
        return resp?.Item;
    }

    public async Task<CartItemDto?> CreateCartItemAsync(Guid cartId, CreateCartItemRequest request, CancellationToken ct = default)
    {
        var client = CreateClient();
        var httpResp = await client.PostAsJsonAsync($"/api/v1/carts/{cartId}/items", request, ct);
        httpResp.EnsureSuccessStatusCode();
        var created = await httpResp.Content.ReadFromJsonAsync<CreateCartItemResponseDto>(cancellationToken: ct);
        return created is null ? null : new CartItemDto(created.CartItemId, created.Quantity, created.CartId, created.ProductId);
    }

    public async Task UpdateCartItemQuantityAsync(Guid cartId, Guid itemId, int quantity, CancellationToken ct = default)
    {
        var client = CreateClient();
        var httpResp = await client.PutAsJsonAsync($"/api/v1/carts/{cartId}/items/{itemId}", new { Quantity = quantity }, ct);
        httpResp.EnsureSuccessStatusCode();
    }

    public async Task DeleteCartItemAsync(Guid cartId, Guid itemId, CancellationToken ct = default)
    {
        var client = CreateClient();
        var httpResp = await client.DeleteAsync($"/api/v1/carts/{cartId}/items/{itemId}", ct);
        httpResp.EnsureSuccessStatusCode();
    }

    // DTOs matching API responses
    private sealed record GetCategoriesResponseDto(IReadOnlyList<CategoryDto> Categories);
    private sealed record GetCategoryResponseDto(CategoryDto Category);
    private sealed record CreateCategoryResponseDto(Guid CategoryId, string Name, string? Description);

    private sealed record GetProductsResponseDto(IReadOnlyList<ProductDto> Products);
    private sealed record GetProductResponseDto(ProductDto Product);
    private sealed record CreateProductResponseDto(Guid Id, string Name, string Description, decimal Price, long Quantity, DateTime CreatedAt, DateTime UpdatedAt, Guid CategoryId);

    private sealed record CreateCartResponseDto(Guid CartId, DateTime LastUpdated);
    private sealed record GetCartResponseDto(Guid CartId, DateTime LastUpdated);

    private sealed record GetCartItemsResponseDto(IReadOnlyList<CartItemDto> Items);
    private sealed record GetCartItemResponseDto(CartItemDto Item);
    private sealed record CreateCartItemResponseDto(Guid CartItemId, int Quantity, Guid CartId, Guid ProductId);
}


