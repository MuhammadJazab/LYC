using System;
namespace Utilities
{
    public class Constants
    {
        public static class Messages
        {
            public const string InvalidUsers = "Invalid email or password";
            public const string LoginSuccessfully = "LoginSuccessfully";
            public const string UserNotExist = "User does not exit";
        }

        public static class SchemaName
        {
            public const string MCS = "mcs";
        }

        public static class SessionKey
        {
            public const string Token = "Token";
            public const string ExpiredToken = "TokenExpired";
            public const string ExpiredTokenMessage = "Session is expired. login again to use the services.";
        }

        public static class UserRoles
        {
            public const string Admin = "Admin";
            public const string SuperAdmin = "SuperAdmin";
            public const string User = "User";
            public const string Customer = "Customer";
            public const string Chef = "Chef";
            public const string Employee = "Employee";
            public const string Doctor = "Doctor";
            public const string Accountant = "Accountant";
            public const string Guest = "Guest";
        }

        public static class MealTypes
        {
            public const string Breakfast = "Breakfast";
            public const string Lunch = "Lunch";
            public const string Dinner = "Dinner";
            public const string Brunch = "Brunch";
            public const string Supper = "Supper";
        }

        public static class CorsConstants
        {
            public const string CorsPolicy = "CorsSettings";

            public static string[] AllowedUrls = new string[]
            {
                "https://lycmcs.com",
                "http://lycmcs.com",
                "https://202.75.45.11",
                "http://localhost:4200",
                "https://localhost:4200",
                "http://localhost:52416",
                "https://localhost:52416"
            };
        }

    }
}

