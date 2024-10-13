using KindPaws.Domain.Shared.Others;

namespace KindPaws.Infrastructure.Providers;

public static class MinioErrors
{
    public static Error BucketNotFound(
        string? name = null)
    {
        name = name == null ? "Bucket" : $"Bucket '{name}'";
        return Error.Validation("bucket.is.not.found", $"{name} not found");
    }

    public static Error ObjectNotFound(
        string? objectName = null,
        string? bucketName = null)
    {
        objectName = objectName == null ? "Object" : $"Object '{objectName}'";
        bucketName = bucketName == null ? "Bucket" : $"Bucket '{bucketName}'";
        return Error.Validation("object.is.not.found", $"{objectName} in {bucketName} not found");
    }

    public static Error Failure(
        string? actionName = null)
    {
        actionName = actionName == null ? "Action" : $"Action '{actionName}'";
        return Error.Validation("action.failure", $"{actionName} is failure");
    }
}