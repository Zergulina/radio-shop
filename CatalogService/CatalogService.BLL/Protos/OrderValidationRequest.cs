using System;

namespace CatalogService.BLL.Protos;

internal record OrderValidationRequest
{
    public int OrderId {get; init;}
    public List<int> ProductIds {get; init;}
}
