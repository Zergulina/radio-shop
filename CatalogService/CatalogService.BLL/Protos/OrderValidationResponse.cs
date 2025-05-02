using System;

namespace CatalogService.BLL.Protos;

public record OrderValidationResponse
{
    public int OrderId {get; init;}
    public bool IsValid {get; init;}
    public List<int> InvalidProductIds {get; init;} 
}
