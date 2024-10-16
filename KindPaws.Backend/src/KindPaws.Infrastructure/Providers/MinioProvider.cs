using KindPaws.Application.Providers;
using KindPaws.Application.Providers.DTOs;
using KindPaws.Domain.Shared.Others;
using Microsoft.Extensions.Logging;
using Minio;
using Minio.DataModel.Args;
using Minio.Exceptions;

namespace KindPaws.Infrastructure.Providers;

public class MinioProvider : IFileProvider
{
    public const int MaxDegreeOfParallelism = 5;
    
    private readonly ILogger<MinioProvider> _logger;
    private readonly IMinioClient _minioClient;

    public MinioProvider(
        IMinioClient minioClient,
        ILogger<MinioProvider> logger)
    {
        _minioClient = minioClient;
        _logger = logger;
    }

    public async Task<Result<Error>> UploadObjectsAsync(
        UploadObjectsData uploadObjectsData,
        CancellationToken cancellationToken = default)
    {
        var semaphoreSlim = new SemaphoreSlim(MaxDegreeOfParallelism);
        
        var ensureBucketExistsResult = await EnsureBucketExistsAsync(
            uploadObjectsData.BucketName,
            cancellationToken);

        if (ensureBucketExistsResult.IsFailure)
            return ensureBucketExistsResult.Error;


        List<Task> tasks = [];
        foreach (var uploadObjectContent in uploadObjectsData.UploadObjectsContent)
        {
            await semaphoreSlim.WaitAsync(cancellationToken);
            
            var task = AddObjectAsync(
                uploadObjectsData.BucketName,
                uploadObjectContent.ObjectName,
                uploadObjectContent.ObjectStream,
                cancellationToken);

            semaphoreSlim.Release(); // TODO: release finally
            
            tasks.Add(task);

            // if (addObjectResult.IsFailure)
            //     return addObjectResult.Error;
        }
        await Task.WhenAll(tasks);

        return true;
    }

    public async Task<Result<Error>> DeleteObjectAsync(
        DeleteObjectData deleteObjectData,
        CancellationToken cancellationToken = default)
    {
        var isBucketExistsResult = await BucketExistsAsync(
            deleteObjectData.BucketName,
            cancellationToken);

        if (isBucketExistsResult.IsFailure)
            return isBucketExistsResult.Error;

        if (!isBucketExistsResult.Value)
            return MinioErrors.BucketNotFound(deleteObjectData.BucketName);

        var isObjectExistResult = await ObjectExistsAsync(
            deleteObjectData.BucketName,
            deleteObjectData.ObjectName,
            cancellationToken);

        if (isObjectExistResult.IsFailure)
            return MinioErrors.ObjectNotFound(deleteObjectData.ObjectName, deleteObjectData.BucketName);

        var removeObjectResult = await RemoveObjectAsync(
            deleteObjectData.BucketName,
            deleteObjectData.ObjectName,
            cancellationToken);

        if (removeObjectResult.IsFailure)
            return removeObjectResult.Error;

        return true;
    }

    public async Task<Result<string, Error>> GetObjectLinkAsync(
        GetObjectData getObjectData,
        CancellationToken cancellationToken = default)
    {
        var isBucketExistsResult = await BucketExistsAsync(
            getObjectData.BucketName,
            cancellationToken);

        if (isBucketExistsResult.IsFailure)
            return isBucketExistsResult.Error;

        if (!isBucketExistsResult.Value)
            return MinioErrors.BucketNotFound(getObjectData.BucketName);


        var isObjectExistResult = await ObjectExistsAsync(
            getObjectData.BucketName,
            getObjectData.ObjectName,
            cancellationToken);

        if (isObjectExistResult.IsFailure)
            return MinioErrors.ObjectNotFound(getObjectData.ObjectName, getObjectData.BucketName);


        var getObjectLinkResult = await GetObjectLink(
            getObjectData.BucketName,
            getObjectData.ObjectName);

        if (getObjectLinkResult.IsFailure)
            return getObjectLinkResult.Error;

        return getObjectLinkResult.Value;
    }

    #region Heleprs

    private async Task<Result<bool, Error>> BucketExistsAsync(
        string bucketName,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var bucketExistArgs = new BucketExistsArgs()
                .WithBucket(bucketName);

            var bucketExists = await _minioClient.BucketExistsAsync(bucketExistArgs, cancellationToken);

            return bucketExists;
        }
        catch (MinioException e)
        {
            _logger.LogError(e, "{ActionName} is failure; Bucket '{BucketName}'",
                nameof(BucketExistsAsync),
                bucketName);

            return MinioErrors.Failure(nameof(BucketExistsAsync));
        }
    }

    private async Task<Result<Error>> AddBucketAsync(
        string bucketName,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var makeBucketArgs = new MakeBucketArgs()
                .WithBucket(bucketName);

            await _minioClient.MakeBucketAsync(makeBucketArgs, cancellationToken);

            return true;
        }
        catch (MinioException e)
        {
            _logger.LogError(e, "{ActionName} is failure; Bucket '{BucketName}'",
                nameof(BucketExistsAsync),
                bucketName);

            return MinioErrors.Failure(nameof(AddBucketAsync));
        }
    }

    private async Task<Result<Error>> AddObjectAsync(
        string bucketName,
        string objectName,
        Stream objectStream,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var putObjectArgs = new PutObjectArgs()
                .WithBucket(bucketName)
                .WithStreamData(objectStream)
                .WithObjectSize(objectStream.Length)
                .WithObject(objectName);

            await _minioClient.PutObjectAsync(putObjectArgs, cancellationToken);

            return true;
        }
        catch (MinioException e)
        {
            _logger.LogError(e, "{ActionName} is failure; Object '{ObjectName}' in bucket '{BucketName}'",
                nameof(BucketExistsAsync),
                objectName,
                bucketName);

            return MinioErrors.Failure(nameof(AddObjectAsync));
        }
    }

    private async Task<Result<string, Error>> GetObjectLink(
        string bucketName,
        string objectName)
    {
        try
        {
            var presignedGetObjectArgs = new PresignedGetObjectArgs()
                .WithBucket(bucketName)
                .WithObject(objectName)
                .WithExpiry(60 * 60 * 24);

            var url = await _minioClient.PresignedGetObjectAsync(presignedGetObjectArgs);

            return url;
        }
        catch (MinioException e)
        {
            _logger.LogError(e, "{ActionName} is failure; Object '{ObjectName}' in bucket '{BucketName}'",
                nameof(GetObjectLink),
                objectName,
                bucketName);

            return MinioErrors.Failure(nameof(GetObjectLink));
        }
    }

    private async Task<Result<Error>> ObjectExistsAsync(
        string bucketName,
        string objectName,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var statObjectArgs = new StatObjectArgs()
                .WithBucket(bucketName)
                .WithObject(objectName);

            await _minioClient.StatObjectAsync(statObjectArgs, cancellationToken);

            return true;
        }
        catch (MinioException e)
        {
            _logger.LogError(e, "{ActionName} is failure; Object '{ObjectName}' in bucket '{BucketName}'",
                nameof(ObjectExistsAsync),
                objectName,
                bucketName);

            return MinioErrors.Failure(nameof(ObjectExistsAsync));
        }
    }

    private async Task<Result<Error>> EnsureBucketExistsAsync(
        string bucketName,
        CancellationToken cancellationToken = default)
    {
        var isBucketExistResult = await BucketExistsAsync(bucketName, cancellationToken);

        if (isBucketExistResult.IsFailure)
            return isBucketExistResult.Error;

        if (!isBucketExistResult.Value)
        {
            var addBucketResult = await AddBucketAsync(bucketName, cancellationToken);

            if (addBucketResult.IsFailure)
                return addBucketResult.Error;
        }

        return true;
    }

    private async Task<Result<Error>> RemoveObjectAsync(
        string bucketName,
        string objectName,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var removeObjectArgs = new RemoveObjectArgs()
                .WithBucket(bucketName)
                .WithObject(objectName);

            await _minioClient.RemoveObjectAsync(removeObjectArgs, cancellationToken);

            return true;
        }
        catch (MinioException e)
        {
            _logger.LogError(e, "{ActionName} is failure; Object '{ObjectName}' in bucket '{BucketName}'",
                nameof(RemoveObjectAsync),
                objectName,
                bucketName);

            return MinioErrors.Failure(nameof(RemoveObjectAsync));
        }
    }

    #endregion
}