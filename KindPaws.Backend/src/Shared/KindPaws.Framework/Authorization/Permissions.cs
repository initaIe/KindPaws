namespace KindPaws.Framework.Authorization;

public static class Permissions
{
    public static class Accounts
    {
    }

    public static class Volunteers
    {
    }

    public static class Specie
    {
        public const string CreateBreed = "create.breed";
        public const string ReadBreed = "read.breed";
        public const string UpdateBreed = "update.breed";
        public const string HardDeleteBreed = "hard.delete.breed";
        public const string SoftDeleteBreed = "soft.delete.breed";

        public const string CreateSpecie = "create.specie";
        public const string ReadSpecie = "read.specie";
        public const string UpdateSpecie = "update.specie";
        public const string HardDeleteSpecie = "hard.delete.specie";
        public const string SoftDeleteSpecie = "soft.delete.specie";
    }
}