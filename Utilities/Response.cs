using Utilities.Enums;

namespace Utilities
{
    public class Response
    {
        public ResponseStatus Status { get; set; }
        public string Message { get; set; }
        public object ResultData { get; set; }
    }

    public static class ResponseMessages
    {
        public const string Successfull = "Request successfull";
        public const string SuccessfullWithError = "Request successfully completed with an error.";
        public const string UnSuccessfull = "Unable to perform the operation. Try again or contact the Administrator";
        public const string GenericUnsuccessfull = "An error occure while retrieving data.";
        public const string UserNotFound = "User not found";
        public const string DataNotFound = "Data not found";
        public const string UserAlreadyExist = "User already exists!";
        public const string AlreadyExist = "Already exists!";
        public const string TokenDescription = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"";
        public const string RegistrationFailed = "User creation failed! Please check user details and try again.";
        public const string AccountDeleted = "Unable to login, Either your account is blocke or deleted. Contact administrator.";
    }
}

