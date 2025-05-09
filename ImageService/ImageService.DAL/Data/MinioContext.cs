using Minio;
using Microsoft.Extensions.Configuration;
using Minio.DataModel.Args;
using System.Net.Mime;

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
                .WithObjectSize(fileStream.Length)
                .WithContentType(contentType));
        }

        public async Task<(Stream, string)> GetFileAsync(string bucketName, string objectName)
        {
            var memoryStream = new MemoryStream();
            await _minioClient.GetObjectAsync(new GetObjectArgs()
                .WithBucket(bucketName)
                .WithObject(objectName)
                .WithCallbackStream(stream => {
                    stream.CopyTo(memoryStream);
                    memoryStream.Position = 0;
                }));

            var statArgs = new StatObjectArgs()
                .WithBucket(bucketName)
                .WithObject(objectName);

            var stat = await _minioClient.StatObjectAsync(statArgs);
            var contentType = stat.ContentType;

            return (memoryStream, contentType);
        }

        public async Task DeleteFileAsync(string bucketName, string objectName)
        {
            await _minioClient.RemoveObjectAsync(new RemoveObjectArgs()
                .WithBucket(bucketName)
                .WithObject(objectName));
        }
    }
}
