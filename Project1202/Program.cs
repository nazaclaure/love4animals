using System.Net.Http.Json;
using Project1202.Dto;

Console.OutputEncoding = System.Text.Encoding.UTF8;

void PrintTitle(string text)
{
    Console.ForegroundColor = ConsoleColor.Blue;
    Console.WriteLine(text);
    Console.ResetColor();
}

void PrintError(string text)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine(text);
    Console.ResetColor();
}

void PrintSuccess(string text)
{
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine(text);
    Console.ResetColor();
}

void PrintOption(string number, string text)
{
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.Write(number);
    Console.ResetColor();
    Console.WriteLine(text);
}

void PrintSeparator()
{
    Console.ForegroundColor = ConsoleColor.DarkGray;
    Console.WriteLine("  ·  ·  ·  ·  ·  ·  ·  ·  ·  ·  ·  ·  ·  ·  ·");
    Console.ResetColor();
}

PrintTitle("=== Bienvenidos al Sistema de Inventario de SafeWildlife ===");

bool on = true;
HttpClient http = new HttpClient();

while (on)
{
    Console.WriteLine();
    PrintTitle("MENU DE INVENTARIO");
    PrintOption("1. ", "Búsqueda de Insumos");
    PrintOption("2. ", "Ordenar Insumos");
    PrintOption("3. ", "Buscar Órdenes");
    PrintOption("4. ", "Programar Envío");
    PrintOption("5. ", "Contabilidad");
    PrintOption("6. ", "Salir");
    Console.Write("\nSelecciona una opción: ");
    string? option = Console.ReadLine();

    switch (option)
    {
        case "1":
            PrintTitle("\nModulo: Búsqueda de Insumos");

            bool searchMenu = true;

            while (searchMenu)
            {
                Console.WriteLine();
                PrintTitle("Buscar Insumo");
                PrintOption("1. ", "Buscar producto por código de barras");
                PrintOption("2. ", "Buscar productos por categoría");
                PrintOption("3. ", "Volver al menú principal");
                Console.Write("\nSelecciona una opción: ");
                string? subOption = Console.ReadLine();

                switch (subOption)
                {
                    case "1":
                        Console.Write("Ingrese el código de barras: ");
                        string? productCode = Console.ReadLine();

                        if (string.IsNullOrWhiteSpace(productCode))
                        {
                            PrintError("Error: Código inválido.");
                            break;
                        }

                        UriBuilder uriBuilder = new UriBuilder("https://world.openfoodfacts.net")
                        {
                            Path = $"api/v2/products/{productCode}"
                        };
                        uriBuilder.Query = "fields=code,product_name,categories_tags,image_url,status_verbose";
                        string url = uriBuilder.ToString();

                        var httpResponse = await http.GetAsync(url);

                        if (!httpResponse.IsSuccessStatusCode)
                        {
                            PrintError("Error al consultar el producto.");
                            break;
                        }

                        var response = await httpResponse.Content.ReadFromJsonAsync<GetProductDto>();

                        if (response == null || response.Status != 1)
                        {
                            PrintError("Producto no encontrado.");
                            break;
                        }

                        List<string> cleanCategories1 = new List<string>();
                        foreach (var cat in response.Product.Categories_tags ?? new List<string>())
                        {
                            cleanCategories1.Add(cat.Replace("en:", ""));
                        }

                        PrintTitle("\nProducto encontrado:");
                        Console.WriteLine($"Código    : {response.Code}");
                        Console.WriteLine($"Nombre    : {response.Product.Product_name}");
                        Console.WriteLine($"Categorías: {string.Join(", ", cleanCategories1)}");
                        Console.WriteLine($"Imagen    : {response.Product.Image_url}");
                        Console.WriteLine($"Estado    : {response.Status_verbose}");
                        break;

                    case "2":
                        Console.Write("Ingrese la categoría (ej: snacks): ");
                        string? category = Console.ReadLine();

                        if (string.IsNullOrWhiteSpace(category))
                        {
                            PrintError("Error: Categoría inválida.");
                            break;
                        }

                        Console.WriteLine("Buscando productos, por favor espere...");

                        UriBuilder searchBuilder = new UriBuilder("https://world.openfoodfacts.net/api/v2/search");
                        searchBuilder.Query = $"fields=code,product_name,categories_tags,image_url&countries_tags_en=Bolivia&categories_tags={category}";
                        string searchUrl = searchBuilder.ToString();

                        var searchResponse = await http.GetAsync(searchUrl);

                        if (!searchResponse.IsSuccessStatusCode)
                        {
                            PrintError("Error al buscar productos.");
                            break;
                        }

                        var searchResult = await searchResponse.Content.ReadFromJsonAsync<SearchProductDto>();

                        if (searchResult == null || searchResult.Products == null || !searchResult.Products.Any())
                        {
                            PrintError("No se encontraron productos en esa categoría.");
                            break;
                        }

                        PrintTitle("\nProductos encontrados:\n");

                        foreach (var product in searchResult.Products)
                        {
                            List<string> cleanCategories2 = new List<string>();
                            foreach (var cat in product.Categories_tags ?? new List<string>())
                            {
                                cleanCategories2.Add(cat.Replace("en:", ""));
                            }

                            Console.WriteLine($"Código    : {product.Code}");
                            Console.WriteLine($"Nombre    : {product.Product_name}");
                            Console.WriteLine($"Categorías: {string.Join(", ", cleanCategories2)}");
                            Console.WriteLine($"Imagen    : {product.Image_url}");
                            PrintSeparator();
                        }

                        break;

                    case "3":
                        searchMenu = false;
                        break;

                    default:
                        PrintError("Error: Opción inválida.");
                        break;
                }
            }

            break;

        case "2":
            PrintTitle("\nModulo: Ordenar Insumos");

            Console.Write("Nombre y apellido: ");
            string? userName = Console.ReadLine();
            Console.Write("Dirección de entrega: ");
            string? address = Console.ReadLine();
            Console.Write("¿Por qué necesitas estos productos?: ");
            string? justification = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(address) || string.IsNullOrWhiteSpace(justification))
            {
                PrintError("Todos los campos son obligatorios.");
                break;
            }

            List<OrderProductDto> orderProducts = new List<OrderProductDto>();
            bool addProducts = true;

            while (addProducts)
            {
                Console.Write("\nCódigo de barras del producto: ");
                string? orderCode = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(orderCode))
                {
                    PrintError("Código inválido.");
                    continue;
                }

                Console.WriteLine("Verificando producto...");
                UriBuilder orderUri = new UriBuilder("https://world.openfoodfacts.net")
                {
                    Path = $"api/v2/products/{orderCode}"
                };
                orderUri.Query = "fields=code,product_name";

                var orderHttpResponse = await http.GetAsync(orderUri.ToString());
                var orderProduct = await orderHttpResponse.Content.ReadFromJsonAsync<GetProductDto>();

                if (orderProduct == null || orderProduct.Status != 1)
                {
                    PrintError("Ese producto no existe, verifica el código.");
                    continue;
                }

                Console.WriteLine($"Producto: {orderProduct.Product.Product_name}");
                Console.WriteLine($"Código  : {orderProduct.Code}");

                var existing = orderProducts.FirstOrDefault(p => p.Code == orderProduct.Code);

                if (existing != null)
                {
                    string? addMore = "";
                    while (addMore?.ToUpper() != "S" && addMore?.ToUpper() != "N")
                    {
                        Console.Write($"{orderProduct.Product.Product_name} ya está en la lista con {existing.Quantity} unidades. ¿Quieres agregar más? (S/N): ");
                        addMore = Console.ReadLine();
                        if (addMore?.ToUpper() != "S" && addMore?.ToUpper() != "N")
                            PrintError("Opción inválida, ingresa S o N.");
                    }

                    if (addMore?.ToUpper() == "S")
                    {
                        int moreQuantity = 0;
                        bool validMore = false;
                        while (!validMore)
                        {
                            Console.Write("¿Cuántos más?: ");
                            string? moreInput = Console.ReadLine();

                            if (!int.TryParse(moreInput, out moreQuantity) || moreQuantity <= 0)
                                PrintError("Cantidad inválida, no puede ser 0 ni negativa. Ingresa de nuevo.");
                            else
                                validMore = true;
                        }

                        int newQuantity = existing.Quantity + moreQuantity;
                        orderProducts.Remove(existing);
                        orderProducts.Add(new OrderProductDto(
                            Code: existing.Code,
                            Name: existing.Name,
                            Quantity: newQuantity,
                            Total: null
                        ));
                        PrintSuccess($"Cantidad actualizada: {orderProduct.Product.Product_name} x{newQuantity}");
                    }
                }
                else
                {
                    int quantity = 0;
                    bool validQuantity = false;
                    while (!validQuantity)
                    {
                        Console.Write("¿Cuántos necesitas?: ");
                        string? quantityInput = Console.ReadLine();

                        if (!int.TryParse(quantityInput, out quantity) || quantity <= 0)
                            PrintError("La cantidad no puede ser 0 ni negativa. Ingresa de nuevo.");
                        else
                            validQuantity = true;
                    }

                    orderProducts.Add(new OrderProductDto(
                        Code: orderProduct.Code,
                        Name: orderProduct.Product.Product_name,
                        Quantity: quantity,
                        Total: null
                    ));

                    PrintSuccess($"Producto agregado: {orderProduct.Product.Product_name} x{quantity}");
                }

                string? anotherProduct = "";
                while (anotherProduct?.ToUpper() != "S" && anotherProduct?.ToUpper() != "N")
                {
                    Console.Write("¿Agregar otro producto? (S/N): ");
                    anotherProduct = Console.ReadLine();
                    if (anotherProduct?.ToUpper() != "S" && anotherProduct?.ToUpper() != "N")
                        PrintError("Opción inválida, ingresa S o N.");
                }

                if (anotherProduct?.ToUpper() != "S")
                    addProducts = false;
            }

            if (!orderProducts.Any())
            {
                PrintError("No agregaste ningún producto.");
                break;
            }

            PrintTitle("\n¿Esto es lo que quieres pedir?");
            Console.WriteLine($"Nombre       : {userName}");
            Console.WriteLine($"Dirección    : {address}");
            Console.WriteLine($"Justificación: {justification}");
            Console.WriteLine("\nProductos:");
            foreach (var p in orderProducts)
            {
                Console.WriteLine($"- {p.Name} | Código: {p.Code} | Cantidad: {p.Quantity}");
            }

            string? confirm = "";
            while (confirm?.ToUpper() != "S" && confirm?.ToUpper() != "N")
            {
                Console.Write("\nConfirmar (S/N): ");
                confirm = Console.ReadLine();
                if (confirm?.ToUpper() != "S" && confirm?.ToUpper() != "N")
                    PrintError("Opción inválida, ingresa S o N.");
            }

            if (confirm?.ToUpper() != "S")
            {
                PrintError("Orden cancelada.");
                break;
            }

            OrderDto newOrder = new OrderDto(
                OrderId: Guid.NewGuid().ToString(),
                User: userName,
                Datetime: DateTime.Now,
                Products: orderProducts,
                OrderStatus: "Pendiente",
                Address: address,
                Justification: justification,
                ShippingTotal: null,
                ShippingDate: null,
                OrderTotal: null
            );

            string file = "orders.json";
            List<OrderDto> orders = new List<OrderDto>();

            if (File.Exists(file))
            {
                string content = File.ReadAllText(file);
                orders = System.Text.Json.JsonSerializer.Deserialize<List<OrderDto>>(content) ?? new List<OrderDto>();
            }

            orders.Add(newOrder);
            File.WriteAllText(file, System.Text.Json.JsonSerializer.Serialize(orders, new System.Text.Json.JsonSerializerOptions { WriteIndented = true }));

            PrintSuccess("\nOrden guardada!");
            PrintSuccess($"Tu número de orden es: {newOrder.OrderId}");

            break;

        case "3":
            PrintTitle("\nModulo: Buscar Órdenes");

            if (!File.Exists("orders.json"))
            {
                PrintError("No hay órdenes registradas.");
                break;
            }

            string ordersContent = File.ReadAllText("orders.json");
            List<OrderDto> allOrders = System.Text.Json.JsonSerializer.Deserialize<List<OrderDto>>(ordersContent) ?? new List<OrderDto>();

            if (!allOrders.Any())
            {
                PrintError("No hay órdenes registradas.");
                break;
            }

            bool ordersMenu = true;

            while (ordersMenu)
            {
                Console.WriteLine();
                PrintTitle("Buscar Órdenes");
                PrintOption("1. ", "Ver todas las órdenes");
                PrintOption("2. ", "Ver detalle de una orden");
                PrintOption("3. ", "Volver al menú principal");
                Console.Write("\nSelecciona una opción: ");
                string? ordersOption = Console.ReadLine();

                switch (ordersOption)
                {
                    case "1":
                        int ordersPage = 1;
                        int pageSize = 20;
                        bool keepViewing = true;

                        while (keepViewing)
                        {
                            var pageOrders = allOrders
                                .Skip((ordersPage - 1) * pageSize)
                                .Take(pageSize)
                                .ToList();

                            if (!pageOrders.Any())
                            {
                                PrintError("No hay más órdenes.");
                                break;
                            }

                            PrintTitle($"\nMostrando página {ordersPage}:\n");

                            foreach (var order in pageOrders)
                            {
                                Console.WriteLine($"N° Orden : {order.OrderId}");
                                Console.WriteLine($"Usuario  : {order.User}");
                                Console.WriteLine($"Fecha    : {order.Datetime:dd/MM/yyyy HH:mm}");
                                PrintSeparator();
                            }

                            bool hayMas = allOrders.Skip(ordersPage * pageSize).Any();
                            if (!hayMas)
                            {
                                keepViewing = false;
                            }
                            else
                            {
                                string? next = "";
                                while (next?.ToUpper() != "S" && next?.ToUpper() != "N")
                                {
                                    Console.Write("¿Ver más? (S/N): ");
                                    next = Console.ReadLine();
                                    if (next?.ToUpper() != "S" && next?.ToUpper() != "N")
                                        PrintError("Opción inválida, ingresa S o N.");
                                }

                                if (next?.ToUpper() != "S")
                                    keepViewing = false;
                                else
                                    ordersPage++;
                            }
                        }

                        break;

                    case "2":
                        Console.Write("Ingresa el ID de la orden: ");
                        string? searchId = Console.ReadLine();

                        if (string.IsNullOrWhiteSpace(searchId))
                        {
                            PrintError("ID inválido.");
                            break;
                        }

                        var foundOrder = allOrders.FirstOrDefault(o => o.OrderId == searchId);

                        if (foundOrder == null)
                        {
                            PrintError("No se encontró ninguna orden con ese ID.");
                            break;
                        }

                        PrintTitle("\n--- Detalle de la Orden ---");
                        Console.WriteLine($"N° Orden     : {foundOrder.OrderId}");
                        Console.WriteLine($"Usuario      : {foundOrder.User}");
                        Console.WriteLine($"Fecha        : {foundOrder.Datetime:dd/MM/yyyy HH:mm}");
                        Console.WriteLine($"Estado       : {foundOrder.OrderStatus}");
                        Console.WriteLine($"Dirección    : {foundOrder.Address}");
                        Console.WriteLine($"Justificación: {foundOrder.Justification}");
                        Console.WriteLine($"Total productos : {foundOrder.OrderTotal - foundOrder.ShippingTotal ?? 0}");
                        Console.WriteLine($"Costo de envío  : {foundOrder.ShippingTotal?.ToString() ?? "N/A"}");
                        Console.WriteLine($"Fecha de envío  : {foundOrder.ShippingDate?.ToString("dd/MM/yyyy") ?? "N/A"}");
                        Console.WriteLine($"Total de la orden: {foundOrder.OrderTotal?.ToString() ?? "N/A"}");

                        PrintTitle("\n--- Insumos ---");
                        Console.WriteLine($"{"Código",-15} | {"Nombre",-25} | {"Cantidad",-10} | {"Total"}");
                        Console.WriteLine(new string('-', 60));
                        foreach (var p in foundOrder.Products)
                        {
                            Console.WriteLine($"{p.Code,-15} | {p.Name,-25} | {p.Quantity,-10} | {p.Total?.ToString() ?? "N/A"}");
                        }

                        break;

                    case "3":
                        ordersMenu = false;
                        break;

                    default:
                        PrintError("Opción inválida.");
                        break;
                }
            }

            break;

        case "4":
            PrintTitle("\nModulo: Programar Envíos");

            if (!File.Exists("orders.json"))
            {
                PrintError("No hay órdenes registradas.");
                break;
            }

            string shippingContent = File.ReadAllText("orders.json");
            List<OrderDto> shippingOrders = System.Text.Json.JsonSerializer.Deserialize<List<OrderDto>>(shippingContent) ?? new List<OrderDto>();

            Console.Write("Ingresa el ID de la orden: ");
            string? shippingId = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(shippingId))
            {
                PrintError("ID inválido.");
                break;
            }

            shippingId = shippingId.Trim();

            var shippingOrder = shippingOrders.FirstOrDefault(o => o.OrderId == shippingId);

            if (shippingOrder == null)
            {
                PrintError("No se encontró ninguna orden con ese ID.");
                break;
            }

            if (shippingOrder.OrderStatus != "Pendiente")
            {
                PrintError($"Esta orden ya tiene el estado '{shippingOrder.OrderStatus}', solo se pueden enviar órdenes Pendiente.");
                break;
            }

            List<OrderProductDto> updatedProducts = new List<OrderProductDto>();

            foreach (var product in shippingOrder.Products)
            {
                Console.WriteLine($"\nProducto: {product.Name} | Código: {product.Code} | Cantidad: {product.Quantity}");

                double productTotal = 0;
                bool validPrice = false;
                while (!validPrice)
                {
                    Console.Write($"Precio por unidad de {product.Name} (ej: 10 o con centavos: 10,50): ");
                    string? priceInput = Console.ReadLine();

                    string cleanPrice = priceInput?.Replace(",", ".") ?? "";
                    if (!double.TryParse(cleanPrice, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double unitPrice) || unitPrice <= 0)
                        PrintError("Precio inválido, no puede ser 0 ni negativo. Ingresa de nuevo.");
                    else
                    {
                        productTotal = unitPrice * product.Quantity;
                        Console.WriteLine($"Total calculado: {productTotal}");
                        validPrice = true;
                    }
                }

                updatedProducts.Add(new OrderProductDto(
                    Code: product.Code,
                    Name: product.Name,
                    Quantity: product.Quantity,
                    Total: productTotal
                ));
            }

            Console.WriteLine($"\nDirección de entrega: {shippingOrder.Address}");

            double shippingTotal = 0;
            bool validShipping = false;
            while (!validShipping)
            {
                Console.Write("Costo de envío al misionero (ej: 10 o con centavos: 10,50): ");
                string? shippingInput = Console.ReadLine();

                string cleanShipping = shippingInput?.Replace(",", ".") ?? "";
                if (!double.TryParse(cleanShipping, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out shippingTotal) || shippingTotal <= 0)
                    PrintError("Costo de envío inválido, no puede ser 0 ni negativo. Ingresa de nuevo.");
                else
                    validShipping = true;
            }

            DateTime shippingDate = DateTime.Now;
            bool validDate = false;
            while (!validDate)
            {
                Console.Write("Fecha de envío (DD/MM/YYYY): ");
                string? dateInput = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(dateInput))
                {
                    PrintError("La fecha no puede estar vacía. Ingresa de nuevo.");
                    continue;
                }

                if (!DateTime.TryParseExact(dateInput, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out shippingDate))
                {
                    PrintError("Formato inválido, usa DD/MM/YYYY. Ingresa de nuevo.");
                    continue;
                }

                if (shippingDate.Date < DateTime.Now.Date)
                {
                    PrintError("La fecha no puede ser del pasado. Ingresa de nuevo.");
                    continue;
                }

                validDate = true;
            }

            double productsTotal = 0;
            foreach (var p in updatedProducts)
            {
                productsTotal += p.Total ?? 0;
            }
            double orderTotal = productsTotal + shippingTotal;

            PrintTitle("\n--- Detalle del Envío ---");
            Console.WriteLine($"N° Orden     : {shippingOrder.OrderId}");
            Console.WriteLine($"Usuario      : {shippingOrder.User}");
            Console.WriteLine($"Dirección    : {shippingOrder.Address}");
            Console.WriteLine($"Justificación: {shippingOrder.Justification}");

            PrintTitle("\nProductos:");
            Console.WriteLine($"{"Nombre",-25} | {"Código",-15} | {"Cantidad",-10} | {"Total"}");
            Console.WriteLine(new string('-', 65));
            foreach (var p in updatedProducts)
            {
                Console.WriteLine($"{p.Name,-25} | {p.Code,-15} | {p.Quantity,-10} | {p.Total}");
            }

            Console.WriteLine($"\nTotal productos  : {productsTotal}");
            Console.WriteLine($"Costo de envío   : {shippingTotal}");
            PrintSuccess($"Total de la orden: {orderTotal}");
            Console.WriteLine($"Fecha de envío   : {shippingDate:dd/MM/yyyy}");
            Console.WriteLine($"Estado           : Enviado");

            string? confirmShipping = "";
            while (confirmShipping?.ToUpper() != "S" && confirmShipping?.ToUpper() != "N")
            {
                Console.Write("\n¿Confirmar envío? (S/N): ");
                confirmShipping = Console.ReadLine();
                if (confirmShipping?.ToUpper() != "S" && confirmShipping?.ToUpper() != "N")
                    PrintError("Opción inválida, ingresa S o N.");
            }

            if (confirmShipping?.ToUpper() != "S")
            {
                PrintError("Envío cancelado.");
                break;
            }

            OrderDto updatedOrder = new OrderDto(
                OrderId: shippingOrder.OrderId,
                User: shippingOrder.User,
                Datetime: shippingOrder.Datetime,
                Products: updatedProducts,
                OrderStatus: "Enviado",
                Address: shippingOrder.Address,
                Justification: shippingOrder.Justification,
                ShippingTotal: shippingTotal,
                ShippingDate: shippingDate,
                OrderTotal: orderTotal
            );

            int index = shippingOrders.FindIndex(o => o.OrderId == shippingId);
            shippingOrders[index] = updatedOrder;
            File.WriteAllText("orders.json", System.Text.Json.JsonSerializer.Serialize(shippingOrders, new System.Text.Json.JsonSerializerOptions { WriteIndented = true }));

            PrintSuccess("\nEnvío registrado!");

            break;

        case "5":
            PrintTitle("\nModulo: Contabilidad");

            if (!File.Exists("orders.json"))
            {
                PrintError("No hay órdenes registradas.");
                break;
            }

            string accountingContent = File.ReadAllText("orders.json");
            List<OrderDto> accountingOrders = System.Text.Json.JsonSerializer.Deserialize<List<OrderDto>>(accountingContent) ?? new List<OrderDto>();

            if (!accountingOrders.Any())
            {
                PrintError("No hay órdenes registradas.");
                break;
            }

            Console.Write("Rango inicial (MM/YYYY): ");
            string? startInput = Console.ReadLine();
            Console.Write("Rango final (MM/YYYY): ");
            string? endInput = Console.ReadLine();

            bool validStart = DateTime.TryParseExact(startInput, "MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime startDate);
            bool validEnd = DateTime.TryParseExact(endInput, "MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime endDate);

            int startMonth = 0;
            int endMonth = 0;
            int.TryParse(startInput?.Split('/')[0], out startMonth);
            int.TryParse(endInput?.Split('/')[0], out endMonth);

            if (!validStart || !validEnd || startMonth > 12 || endMonth > 12 || startMonth < 1 || endMonth < 1)
            {
                PrintError("Formato inválido, usa MM/YYYY y asegúrate que el mes esté entre 01 y 12.");
                break;
            }

            if (startDate > endDate)
            {
                PrintError("El rango inicial no puede ser mayor al final.");
                break;
            }

            List<OrderDto> periodOrders = new List<OrderDto>();
            foreach (var order in accountingOrders)
            {
                if (order.Datetime >= startDate && order.Datetime < endDate.AddMonths(1) && order.OrderStatus == "Enviado")
                    periodOrders.Add(order);
            }

            if (!periodOrders.Any())
            {
                PrintError("No hay órdenes enviadas en ese periodo.");
                break;
            }

            PrintTitle($"\n--- Contabilidad {startInput} - {endInput} ---\n");

            double grandTotal = 0;

            foreach (var order in periodOrders)
            {
                Console.WriteLine($"N° Orden : {order.OrderId}");
                Console.WriteLine($"Usuario  : {order.User}");
                Console.WriteLine($"Fecha    : {order.Datetime:dd/MM/yyyy}");
                Console.WriteLine($"Estado   : {order.OrderStatus}");
                Console.WriteLine($"Total productos: {order.OrderTotal - order.ShippingTotal ?? 0}");
                Console.WriteLine($"Costo envío    : {order.ShippingTotal?.ToString() ?? "N/A"}");
                Console.WriteLine($"Total orden    : {order.OrderTotal?.ToString() ?? "Sin calcular"}");
                PrintSeparator();

                grandTotal += order.OrderTotal ?? 0;
            }

            PrintSuccess($"\nTotal gastado en el periodo: {grandTotal}");

            break;

        case "6":
            Console.Write("¿Seguro que deseas salir? (S/N): ");
            string? confirmation = "";
            while (confirmation?.ToUpper() != "S" && confirmation?.ToUpper() != "N")
            {
                confirmation = Console.ReadLine();
                if (confirmation?.ToUpper() != "S" && confirmation?.ToUpper() != "N")
                {
                    PrintError("Opción inválida, ingresa S o N.");
                    Console.Write("¿Seguro que deseas salir? (S/N): ");
                }
            }

            if (confirmation?.ToUpper() == "S")
            {
                on = false;
                PrintSuccess("Saliendo del sistema");
            }
            break;

        default:
            PrintError("Error: Opción inválida.");
            break;
    }

    Console.WriteLine();
}