using System;
namespace Utilities
{
    public class ApiRoutes
    {
        public static class Base
        {
            /// <summary>
            /// Local
            /// </summary>
            public static readonly string BaseUrl = "http://localhost:52416/";

            //Android
            //public static readonly string BaseUrl = "http://localhost:52416/";

            /// <summary>
            /// Live
            /// </summary>
            //public static readonly string BaseUrl = "http://api.lycmcs.com/";
        }

        public static class Identity
        {
            private static readonly string BaseUrl = "api/Identity/";

            public static readonly string AuthenticateUser = $"{BaseUrl}AuthenticateUser";
            public static readonly string GetRoles = $"{BaseUrl}GetRoles";
            public static readonly string GetAllUsers = $"{BaseUrl}GetAllUsers";
            public static readonly string GetAllCustomers = $"{BaseUrl}GetAllCustomers";
            public static readonly string AddNewRole = $"{BaseUrl}AddNewRole";
            public static readonly string RegisterUser = $"{BaseUrl}RegisterUser";
            public static readonly string DeleteRole = $"{BaseUrl}DeleteRole";
            public static readonly string DeleteStaff = $"{BaseUrl}DeleteStaff";
            public static readonly string UpdateRole = $"{BaseUrl}UpdateRole";
            public static readonly string UpdateUser = $"{BaseUrl}UpdateUser";
            public static readonly string GetAllCustomersByBranchId = $"{BaseUrl}GetAllCustomersByBranchId";
        }

        public static class Customer
        {
            private static readonly string BaseUrl = "api/Customer/";

            public static readonly string AuthenticateUser = $"{BaseUrl}AddNewCustomer";
            public static readonly string GetAllCustomers = $"{BaseUrl}GetAllCustomers";
            public static readonly string GetAllCustomersByBranchId = $"{BaseUrl}GetAllCustomersByBranchId";

        }
    }
}

