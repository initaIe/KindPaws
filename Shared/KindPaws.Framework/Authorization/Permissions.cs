namespace KindPaws.Framework.Authorization;

public static class Permissions
{
    public static class Accounts
    {
    }

    public static class Volunteers
    {
        public const string CreateVolunteer = "create.volunteer";
        public const string GetVolunteer = "get.volunteer";
        public const string UpdateVolunteer = "update.volunteer";
        public const string HardDeleteVolunteer = "hard.delete.volunteer";
        public const string SoftDeleteVolunteer = "soft.delete.volunteer";

        public const string AddPet = "add.pet";
        public const string AddPetPhoto = "add.pet.photo";
        public const string DeletePetPhoto = "add.pet.photo";
        public const string SetPetMainPhoto = "set.pet.main.photo";
        public const string UpdatePet = "update.pet";
        public const string UpdatePetPosition = "update.pet.position";
        public const string HardDeletePet = "hard.delete.pet";
        public const string SoftDeletePet = "soft.delete.pet";
    }

    public static class Pets
    {
        public const string GetPet = "get.pet";
    }

    public static class Species
    {
        public const string CreateSpecie = "create.specie";
        public const string GetSpecie = "get.specie";
        public const string UpdateSpecie = "update.specie";
        public const string HardDeleteSpecie = "hard.delete.specie";
        public const string SoftDeleteSpecie = "soft.delete.specie";

        public const string AddBread = "add.breed";
        public const string UpdateBreed = "update.breed";
        public const string HardDeleteBreed = "hard.delete.breed";
        public const string SoftDeleteBreed = "soft.delete.breed";
    }

    public static class Breeds
    {
        public const string GetBreed = "get.breed";
    }
}