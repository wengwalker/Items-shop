## API reference

This document provides a high-level, static overview of the HTTP API exposed by **Items-shop**.  
All endpoints are available under the host configured in `docs/how-to-run.md` (by default `http://localhost:8080`).

> **Note**  
> For the most accurate, machine-readable description of the API, use the Swagger / OpenAPI definition exposed by the running application at `/swagger`.

### Common conventions

- **Base path**: all endpoints use versioned routes under `/api/v1/...`.
- **Content type**: JSON (`application/json`) for request and response bodies.
- **Validation**: requests are validated with FluentValidation; validation errors are returned as RFC 7807 ProblemDetails with a `validation` problem type.
- **Errors**: non-success results use ProblemDetails with an HTTP status code and descriptive message.

---

### Catalogs module

#### Products

- **POST** `/api/v1/products`
  - **Description**: Creates a new product.
  - **Request body** (`CreateProductRequest`):
    - `name` *(string, required)*
    - `description` *(string, required)*
    - `price` *(decimal, required)* – product price.
    - `quantity` *(long, required)* – initial stock quantity.
    - `categoryId` *(guid, required)* – identifier of an existing category.
  - **Responses**:
    - `201 Created` – `ProductResponse` with created product.
    - `400 Bad Request` – validation problem (for example, missing fields, negative price/quantity).
    - `404 Not Found` – referenced category does not exist.

- **DELETE** `/api/v1/products/{productId}`
  - **Description**: Deletes a product.
  - **Path parameters**:
    - `productId` *(guid, required)* – product identifier.
  - **Responses**:
    - `204 No Content` – product deleted.
    - `400 Bad Request` – validation problem.
    - `404 Not Found` – product not found.

- **GET** `/api/v1/products/{productId}`
  - **Description**: Returns a single product by its identifier.
  - **Path parameters**:
    - `productId` *(guid, required)* – product identifier.
  - **Responses**:
    - `200 OK` – `ProductResponse`.
    - `400 Bad Request` – validation problem.
    - `404 Not Found` – product not found.

- **GET** `/api/v1/products`
  - **Description**: Returns a list of products.
  - **Query parameters**:
    - `name` *(string, optional)* – filter by product name (partial match).
    - `sortType` *(enum, optional)* – sort order; see `ItemsShop.Common.Application.Enums.QuerySortType` (for example, `Ascending`, `Descending`).
  - **Responses**:
    - `200 OK` – list of `ProductResponse`.
    - `400 Bad Request` – validation problem (invalid query values).

- **PATCH** `/api/v1/products/{productId}/category`
  - **Description**: Changes the category of a product.
  - **Path parameters**:
    - `productId` *(guid, required)* – product identifier.
  - **Request body**:
    - `categoryId` *(guid, required)* – new category identifier.
  - **Responses**:
    - `200 OK` – `ProductResponse` with updated category.
    - `400 Bad Request` – validation problem.
    - `404 Not Found` – product or category not found.

- **PATCH** `/api/v1/products/{productId}/description`
  - **Description**: Updates the description of a product.
  - **Path parameters**:
    - `productId` *(guid, required)* – product identifier.
  - **Request body**:
    - `description` *(string, required)* – new description.
  - **Responses**:
    - `200 OK` – updated `ProductResponse`.
    - `400 Bad Request` – validation problem.
    - `404 Not Found` – product not found.

- **PATCH** `/api/v1/products/{productId}/price`
  - **Description**: Updates the price of a product.
  - **Path parameters**:
    - `productId` *(guid, required)* – product identifier.
  - **Request body**:
    - `price` *(decimal, required)* – new price.
  - **Responses**:
    - `200 OK` – updated `ProductResponse`.
    - `400 Bad Request` – validation problem.
    - `404 Not Found` – product not found.

- **PATCH** `/api/v1/products/{productId}/quantity`
  - **Description**: Updates the available quantity of a product.
  - **Path parameters**:
    - `productId` *(guid, required)* – product identifier.
  - **Request body**:
    - `quantity` *(long, required)* – new stock quantity.
  - **Responses**:
    - `200 OK` – updated `ProductResponse`.
    - `400 Bad Request` – validation problem.
    - `404 Not Found` – product not found.

> For all `PATCH` endpoints above, see Swagger UI for the exact request contracts and response schemas.

#### Categories

- **POST** `/api/v1/categories`
  - **Description**: Creates a new category.
  - **Request body**:
    - `name` *(string, required)* – category name.
    - `description` *(string, optional)* – category description.
  - **Responses**:
    - `201 Created` – created category.
    - `400 Bad Request` – validation problem.
    - `409 Conflict` – category with the same name already exists.

- **DELETE** `/api/v1/categories/{categoryId}`
  - **Description**: Deletes a category.
  - **Path parameters**:
    - `categoryId` *(guid, required)* – category identifier.
  - **Responses**:
    - `204 No Content` – category deleted.
    - `400 Bad Request` – validation problem.
    - `404 Not Found` – category not found.

- **GET** `/api/v1/categories`
  - **Description**: Returns a list of categories.
  - **Query parameters**:
    - `name` *(string, optional)* – filter by category name.
    - `sortType` *(enum, optional)* – sort order; see `QuerySortType`.
  - **Responses**:
    - `200 OK` – list of category responses.
    - `400 Bad Request` – validation problem.

- **PATCH** `/api/v1/categories/{categoryId}/description`
  - **Description**: Updates a category description.
  - **Path parameters**:
    - `categoryId` *(guid, required)* – category identifier.
  - **Request body**:
    - `description` *(string, optional)* – new description.
  - **Responses**:
    - `200 OK` – updated category.
    - `400 Bad Request` – validation problem.
    - `404 Not Found` – category not found.

- **PATCH** `/api/v1/categories/{categoryId}/name`
  - **Description**: Updates a category name.
  - **Path parameters**:
    - `categoryId` *(guid, required)* – category identifier.
  - **Request body**:
    - `name` *(string, required)* – new name.
  - **Responses**:
    - `200 OK` – updated category.
    - `400 Bad Request` – validation problem.
    - `404 Not Found` – category not found.

#### Carts

- **POST** `/api/v1/carts`
  - **Description**: Creates a new cart.
  - **Request body**: *none* (cart is created with default state).
  - **Responses**:
    - `201 Created` – created cart.
    - `500 Internal Server Error` – unexpected error (see ProblemDetails).

- **DELETE** `/api/v1/carts/{cartId}`
  - **Description**: Deletes a cart and its items.
  - **Path parameters**:
    - `cartId` *(guid, required)* – cart identifier.
  - **Responses**:
    - `204 No Content` – cart deleted.
    - `400 Bad Request` – validation problem.
    - `404 Not Found` – cart not found.

- **GET** `/api/v1/carts/{cartId}`
  - **Description**: Returns a cart by id
  - **Path parameters**:
    - `cartId` *(guid, required)* – cart identifier.
  - **Responses**:
    - `200 OK` – `CartResponse`.
    - `400 Bad Request` – validation problem.
    - `404 Not Found` – cart not found.

#### Cart items

- **POST** `/api/v1/carts/{cartId}/items`
  - **Description**: Adds an item to a cart.
  - **Path parameters**:
    - `cartId` *(guid, required)* – cart identifier.
  - **Request body**:
    - `productId` *(guid, required)* – product identifier.
    - `quantity` *(int, required)* – quantity to add.
  - **Responses**:
    - `201 Created` – created `CartItemResponse`.
    - `400 Bad Request` – validation problem.
    - `404 Not Found` – cart or product not found.

- **DELETE** `/api/v1/carts/{cartId}/items/{itemId}`
  - **Description**: Removes an item from a cart.
  - **Path parameters**:
    - `cartId` *(guid, required)* – cart identifier.
    - `itemId` *(guid, required)* – cart item identifier.
  - **Responses**:
    - `204 No Content` – item removed.
    - `400 Bad Request` – validation problem.
    - `404 Not Found` – cart or item not found.

- **GET** `/api/v1/carts/{cartId}/items/{itemId}`
  - **Description**: Returns a single cart item.
  - **Path parameters**:
    - `cartId` *(guid, required)* – cart identifier.
    - `itemId` *(guid, required)* – cart item identifier.
  - **Responses**:
    - `200 OK` – `CartItemResponse`.
    - `400 Bad Request` – validation problem.
    - `404 Not Found` – cart or item not found.

- **GET** `/api/v1/carts/{cartId}/items`
  - **Description**: Returns items for a specific cart.
  - **Path parameters**:
    - `cartId` *(guid, required)* – cart identifier.
  - **Responses**:
    - `200 OK` – list of `CartItemResponse`.
    - `400 Bad Request` – validation problem.
    - `404 Not Found` – cart not found.

- **PATCH** `/api/v1/carts/{cartId}/items/{itemId}`
  - **Description**: Updates the quantity of a cart item.
  - **Path parameters**:
    - `cartId` *(guid, required)* – cart identifier.
    - `itemId` *(guid, required)* – cart item identifier.
  - **Request body**:
    - `quantity` *(int, required)* – new quantity.
  - **Responses**:
    - `200 OK` – updated `CartItemResponse`.
    - `400 Bad Request` – validation problem.
    - `404 Not Found` – cart or item not found.

---

### Orders module

#### Orders

- **POST** `/api/v1/orders`
  - **Description**: Creates a new order (typically from a cart).
  - **Request body**: *none* in the current implementation (order is created from existing state; see Swagger for details).
  - **Responses**:
    - `201 Created` – created `OrderResponse`.
    - `400 Bad Request` – validation problem.
    - `404 Not Found` – referenced entities (for example, cart) not found.

- **DELETE** `/api/v1/orders/{orderId}`
  - **Description**: Deletes an order.
  - **Path parameters**:
    - `orderId` *(guid, required)* – order identifier.
  - **Responses**:
    - `204 No Content` – order deleted.
    - `400 Bad Request` – validation problem.
    - `404 Not Found` – order not found.

- **GET** `/api/v1/orders`
  - **Description**: Returns a list of orders with optional filtering.
  - **Query parameters** (all optional):
    - `sortType` – `QuerySortType` for sorting.
    - `status` – `OrderStatus` enum.
    - `biggerOrEqualPrice`, `lessOrEqualPrice` *(decimal)* – price range filters.
    - `createdBefore`, `createdAfter` *(datetime)* – creation date range.
    - `updatedBefore`, `updatedAfter` *(datetime)* – update date range.
  - **Responses**:
    - `200 OK` – list of `OrderResponse`.
    - `400 Bad Request` – validation problem.

- **PATCH** `/api/v1/orders/{orderId}/price`
  - **Description**: Adjusts the total price of an order (for example, due to discounts or corrections).
  - **Path parameters**:
    - `orderId` *(guid, required)* – order identifier.
  - **Request body**:
    - `price` *(decimal, required)* – new total price.
  - **Responses**:
    - `200 OK` – updated `OrderResponse`.
    - `400 Bad Request` – validation problem.
    - `404 Not Found` – order not found.

#### Order items

- **POST** `/api/v1/orders/{orderId}/items`
  - **Description**: Adds an item to an existing order.
  - **Path parameters**:
    - `orderId` *(guid, required)* – order identifier.
  - **Request body**:
    - `productId` *(guid, required)* – product identifier.
    - `quantity` *(long, required)* – quantity to add.
  - **Responses**:
    - `201 Created` – created `OrderItemResponse`.
    - `400 Bad Request` – validation problem.
    - `404 Not Found` – order or product not found.

- **DELETE** `/api/v1/orders/{orderId}/items/{itemId}`
  - **Description**: Deletes an item from an order.
  - **Path parameters**:
    - `orderId` *(guid, required)* – order identifier.
    - `itemId` *(guid, required)* – order item identifier.
  - **Responses**:
    - `204 No Content` – item deleted.
    - `400 Bad Request` – validation problem.
    - `404 Not Found` – order or item not found.

- **GET** `/api/v1/orders/{orderId}/items/{itemId}`
  - **Description**: Returns a single order item.
  - **Path parameters**:
    - `orderId` *(guid, required)* – order identifier.
    - `itemId` *(guid, required)* – order item identifier.
  - **Responses**:
    - `200 OK` – `OrderItemResponse`.
    - `400 Bad Request` – validation problem.
    - `404 Not Found` – order or item not found.

- **GET** `/api/v1/orders/{orderId}/items`
  - **Description**: Returns items belonging to a given order.
  - **Path parameters**:
    - `orderId` *(guid, required)* – order identifier.
  - **Responses**:
    - `200 OK` – list of `OrderItemResponse`.
    - `400 Bad Request` – validation problem.
    - `404 Not Found` – order not found.

- **PATCH** `/api/v1/orders/{orderId}/items/{itemId}`
  - **Description**: Updates the quantity of an order item.
  - **Path parameters**:
    - `orderId` *(guid, required)* – order identifier.
    - `itemId` *(guid, required)* – order item identifier.
  - **Request body**:
    - `quantity` *(long, required)* – new quantity.
  - **Responses**:
    - `200 OK` – updated `OrderItemResponse`.
    - `400 Bad Request` – validation problem.
    - `404 Not Found` – order or item not found.

---

### Health checks and diagnostics

- **GET** `/healthz`
  - **Description**: Liveness / readiness health check endpoint for the host.
  - Used by Docker health check.

Additional metrics and traces are exported via OpenTelemetry to the collector configured in `docker-compose.yml`. See Grafana and Jaeger dashboards when running with Docker.
