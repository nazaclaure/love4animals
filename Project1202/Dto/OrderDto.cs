namespace Project1202.Dto;

public record OrderDto(
    string OrderId,
    string User,
    DateTime Datetime,
    List<OrderProductDto> Products,
    string OrderStatus,
    string Address,
    string Justification,
    double? ShippingTotal,
    DateTime? ShippingDate,
    double? OrderTotal
);