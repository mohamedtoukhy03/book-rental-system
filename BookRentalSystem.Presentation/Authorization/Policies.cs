namespace BookRentalSystem.Presentation.Authorization
{
    public static class Policies
    {
        public const string AdminOnly = "AdminOnly";
        public const string SuperAdminOnly = "SuperAdminOnly";
        public const string UserOnly = "UserOnly";
    }
}
