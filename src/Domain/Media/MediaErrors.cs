using SharedKernel;

namespace Domain.Media;
public static class MediaErrors
{
    public static Error NotFound(Guid mediaId) => Error.NotFound(
       "Media.NotFound",
       $"The Media with the Id = '{mediaId}' was not found");
    public static Error NoMediaFound => Error.NotFound(
        "Media.NoMediaFound",
        "No Media found or repository returned");
    public static Error FailedToFetchMedia(string message) => Error.Failure(
        "Media.FailedToFetchMedia",
        $"No Media found or repository returned, error message is {message}");

    public static Error NotAuthorizedToCreate => Error.Conflict(
        "Media.NotAuthorizedToCreate",
        "You are not authorized to create media for this course");

    public static Error FailedToUploadMedia(string message) => Error.Failure(
        "Media.FailedToUploadMedia",
        $"Failed to upload media or repository, error message is {message}");
}
