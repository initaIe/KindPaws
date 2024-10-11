using KindPaws.Application.Providers;
using KindPaws.Domain.Shared.Others;
using Microsoft.Extensions.Logging;
using Minio;
using Minio.DataModel.Args;

namespace KindPaws.Infrastructure.Providers;

public class MinioProvider : IFileProvider
{
    private readonly ILogger _logger;
    private readonly IMinioClient _minioClient;

    public MinioProvider(
        IMinioClient minioClient,
        ILogger logger)
    {
        _minioClient = minioClient;
        _logger = logger;
    }

    public async Task<Result<string, Error>> UploadFileAsync(
        ObjectUploadData objectUploadData,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var bucketExistArgs = new BucketExistsArgs()
                .WithBucket(objectUploadData.BucketName);

            var bucketExist = await _minioClient.BucketExistsAsync(bucketExistArgs, cancellationToken);

            if (!bucketExist)
            {
                var makeBucketArgs = new MakeBucketArgs()
                    .WithBucket(objectUploadData.BucketName);

                await _minioClient.MakeBucketAsync(makeBucketArgs, cancellationToken);
            }

            var putObjectArgs = new PutObjectArgs()
                .WithBucket(objectUploadData.BucketName)
                .WithStreamData(objectUploadData.Stream)
                .WithObjectSize(objectUploadData.Stream.Length)
                .WithObject(objectUploadData.Name);

            var putObjectResponse = await _minioClient.PutObjectAsync(putObjectArgs, cancellationToken);

            // TODO тут ли оно надо???
            _logger.LogInformation("MINIO added new object {ObjName}", putObjectResponse.ObjectName);
            
            return putObjectResponse.ObjectName;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occured while uploading file");
            
            return Error.Failure("object.upload.failure", "Object upload failure in minio");
        }
    }
}