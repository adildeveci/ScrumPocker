namespace ScrumPocker.Core.Constants
{
    public static class Role
    { 
        public const string User = "User";
        public const string UnregisteredUser = "UnregisteredUser"; 
    }
    public static class RoleCombination
    { 
        public const string LoggedUserRoles = Role.User + "," + Role.UnregisteredUser;
    }
}
