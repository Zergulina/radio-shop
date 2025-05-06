using Minio;
using Microsoft.Extensions.Configuration;
using Minio.DataModel.Args;

namespace ImageService.DAL.Data
{
    internal class MinioContext
    {
        private readonly IMinioClient _minioClient;

        public MinioContext(IConfiguration configuration)
        {
            var minioConfig = configuration.GetSection("Minio");
            _minioClient = new MinioClient()
                .WithEndpoint(minioConfig["Endpoint"])
                .WithCredentials(minioConfig["AccessKey"], minioConfig["SecretKey"])
                .WithSSL(bool.Parse(minioConfig["WithSSL"]))
                .Build();
        }

        public async Task CreateBucketAsync(string bucketName)
        {
            var exists = await _minioClient.BucketExistsAsync(new BucketExistsArgs().WithBucket(bucketName));
            if (!exists)
            {
                await _minioClient.MakeBucketAsync(new MakeBucketArgs().WithBucket(bucketName));
            }
        }

        public async Task UploadFileAsync(string bucketName, string objectName, Stream fileStream, string contentType)
        {
            await _minioClient.PutObjectAsync(new PutObjectArgs()
                .WithBucket(bucketName)
                .WithObject(objectName)
                .WithStreamData(fileStream)
                .WithContentType(contentType));
        }

        public async Task<Stream> GetFileAsync(string bucketName, string objectName)
        {
            var memoryStream = new MemoryStream();
            await _minioClient.GetObjectAsync(new GetObjectArgs()
                .WithBucket(bucketName)
                .WithObject(objectName)
                .WithCallbackStream(stream => stream.CopyTo(memoryStream)));
            memoryStream.Position = 0;
            return memoryStream;
        }

        public async Task DeleteFileAsync(string bucketName, string objectName)
        {
            await _minioClient.RemoveObjectAsync(new RemoveObjectArgs()
                .WithBucket(bucketName)
                .WithObject(objectName));
        }
    }
}
