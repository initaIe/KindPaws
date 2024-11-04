namespace KindPaws.Core;

public static class Constants
{
    public static class Database
    {
        /// <summary>
        ///     Database section name.
        /// </summary>
        public const string Postgres = nameof(Postgres);
    }

    public static class FileProvider
    {
        /// <summary>
        ///     Pet photos bucket name.
        /// </summary>
        public const string PetPhotosBucketName = "pet-photos";
    }
}