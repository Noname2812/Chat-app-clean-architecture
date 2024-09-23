

namespace ChatApp.Domain.Exceptions
{
    public static class IdentityException
    {
        public class EmailAlreadyExists : BadRequestException {
            public EmailAlreadyExists(string Email) : base($"{Email} already exists")
            {
                
            }
        }
        public class InvalidUser : BadRequestException
        {
            public InvalidUser() : base($"Email or Password invalid")
            {

            }
        }
        public class UserNotFound : BadRequestException
        {
            public UserNotFound(Guid? userId) : base($"The user whose id is {userId} does not exist")
            {
            }
        }
        public class RefreshTokenInvalidOrExpried : BadRequestException {
            public RefreshTokenInvalidOrExpried(): base("Refresh token invalid or expried time")
            {
                
            }
        }
    }
}
