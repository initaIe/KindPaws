using KindPaws.Core.Dtos;
using KindPaws.SharedKernel.ErrorManagement;
using KindPaws.SharedKernel.Others;
using KindPaws.SharedKernel.ValueObjectsManagement.ValueObjects;
using KindPaws.Volunteers.Application.Abstractions;
using Microsoft.Extensions.Logging;
using Minio;
using Minio.DataModel.Args;
using Minio.Exceptions;

namespace KindPaws.Volunteers.Infrastructure.Providers;

public class MinioProvider : IFileProvider
{
    private const int MaxDegreeOfParallelism = 10;

    private readonly ILogger<MinioProvider> _logger;
    private readonly IMinioClient _minioClient;

    public MinioProvider(
        IMinioClient minioClient,
        ILogger<MinioProvider> logger)
    {
        _minioClient = minioClient;
        _logger = logger;
    }

    public async Task<Result<IReadOnlyList<FilePath>, ErrorList>> UploadObjectsAsync(
        IEnumerable<UploadFileData> uploadFilesData,
        CancellationToken cancellationToken = default)
    {
        var semaphoreSlim = new SemaphoreSlim(MaxDegreeOfParallelism);
        var filesList = uploadFilesData.ToList();

        await IfBucketsNotExistCreateBuckets(
            filesList.Select(b => b.BucketName),
            cancellationToken);

        var tasks = filesList.Select(async file =>
            await AddObjectAsync(
                file.BucketName,
                file.FilePath.Value,
                file.Stream,
                semaphoreSlim,
                cancellationToken));

        var pathsResult = await Task.WhenAll(tasks);

        if (pathsResult.Any(r => r.IsFailure))
            return new ErrorList(pathsResult.Select(p => p.Error));

        var results = pathsResult
            .Select(p => FilePath.Create(p.Value).Value).ToList();

        _logger.LogInformation("MINIO success uploaded files; File names: {fileNames}",
            results.Select(f => f.Value));

        return results;
    }

    public async Task<Result<Error>> DeleteObjectAsync(
        DeleteFileData deleteFileData,
        CancellationToken cancellationToken = default)
    {
        var isBucketExistsResult = await BucketExistsAsync(
            deleteFileData.BucketName,
            cancellationToken);

        if (isBucketExistsResult.IsFailure)
            return isBucketExistsResult.Error;

        if (!isBucketExistsResult.Value)
            return MinioErrors.BucketNotFound(deleteFileData.BucketName);

        var isObjectExistResult = await ObjectExistsAsync(
            deleteFileData.BucketName,
            deleteFileData.FileName,
            cancellationToken);

        if (isObjectExistResult.IsFailure)
            return true;

        var removeObjectResult = await RemoveObjectAsync(
            deleteFileData.BucketName,
            deleteFileData.FileName,
            cancellationToken);

        if (removeObjectResult.IsFailure)
            return removeObjectResult.Error;

        return true;
    }

    public async Task<Result<string, Error>> GetObjectLinkAsync(
        GetFileData getFileData,
        CancellationToken cancellationToken = default)
    {
        var isBucketExistsResult = await BucketExistsAsync(
            getFileData.BucketName,
            cancellationToken);

        if (isBucketExistsResult.IsFailure)
            return isBucketExistsResult.Error;

        if (!isBucketExistsResult.Value)
            return MinioErrors.BucketNotFound(getFileData.BucketName);

        var isObjectExistResult = await ObjectExistsAsync(
            getFileData.BucketName,
            getFileData.FileName,
            cancellationToken);

        if (isObjectExistResult.IsFailure)
            return MinioErrors.ObjectNotFound(getFileData.FileName, getFileData.BucketName);

        var getObjectLinkResult = await GetObjectLink(
            getFileData.BucketName,
            getFileData.FileName);

        if (getObjectLinkResult.IsFailure)
            return getObjectLinkResult.Error;

        return getObjectLinkResult.Value;
    }

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

    private async Task<Result<string, Error>> AddObjectAsync(
        string bucketName,
        string objectName,
        Stream objectStream,
        SemaphoreSlim semaphoreSlim,
        CancellationToken cancellationToken = default)
    {
        await semaphoreSlim.WaitAsync(cancellationToken);

        try
        {
            var putObjectArgs = new PutObjectArgs()
                .WithBucket(bucketName)
                .WithStreamData(objectStream)
                .WithObjectSize(objectStream.Length)
                .WithObject(objectName);

            await _minioClient.PutObjectAsync(putObjectArgs, cancellationToken);

            return objectName;
        }
        catch (MinioException e)
        {
            _logger.LogError(e, "{ActionName} is failure; Object '{ObjectName}' in bucket '{BucketName}'",
                nameof(BucketExistsAsync),
                objectName,
                bucketName);

            return MinioErrors.Failure(nameof(AddObjectAsync));
        }
        finally
        {
            semaphoreSlim.Release();
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

    // todo: ДУМАТЬ ЧЕ ДЕЛАТЬ С ЭТОЙ ХУЙНЕЙ
    private async Task<Result> IfBucketsNotExistCreateBuckets(
        IEnumerable<string> bucketNames,
        CancellationToken cancellationToken = default)
    {
        HashSet<string> distinctBucketNames = [..bucketNames];
        foreach (var bucketName in distinctBucketNames)
        {
            var isBucketExistResult = await BucketExistsAsync(bucketName, cancellationToken);

            if (isBucketExistResult.IsFailure)
                continue;

            if (!isBucketExistResult.Value)
            {
                var addBucketResult = await AddBucketAsync(bucketName, cancellationToken);

                if (addBucketResult.IsFailure)
                    continue;
            }
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
}