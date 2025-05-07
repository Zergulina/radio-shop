using Grpc.Core;
using ImageService.BLL.Interfaces;

namespace ImageService.GrpcServices
{
    public class ProductImageService : ProductImageGrpc.ProductImageGrpcBase
    {
        private readonly IProductImageService _productImageService;
        public ProductImageService(IProductImageService productImageService)
        {
            _productImageService = productImageService;
        }

        public async override Task<ProductImageGrpcCreateResponse> Create(ProductImageGrpcCreateRequest request, ServerCallContext context)
        {
            try
            {
                string id = await _productImageService.CreateAsync(new BLL.Dtos.ImageDto { ImageData = new MemoryStream(request.ImageData.ToByteArray()), ImageType = request.ImageExtention });
                return new ProductImageGrpcCreateResponse { Success = true, Id = id };
            }
            catch
            {
                return new ProductImageGrpcCreateResponse { Success = false };
            }
        }

        public async override Task<ProductImageGrpcDeleteResponse> Delete(ProductImageGrpcDeleteRequest request, ServerCallContext context)
        {
            try {
                await _productImageService.DeleteAsync(request.Id);
                return new ProductImageGrpcDeleteResponse { Success = true };
            } catch
            {
                return new ProductImageGrpcDeleteResponse { Success = false };
        }
    }
}
