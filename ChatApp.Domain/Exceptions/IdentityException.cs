

namespace ChatApp.Domain.Exceptions
{
    public static class IdentityException
    {
        public class EmailAlreadyExists : BadRequestException {
            public EmailAlreadyExists(string Email) : base($"{Email} already exists")
            {
                
            }
        }
        public class InvalidUser : NotFoundException
        {
            public InvalidUser() : base($"Email or Password invalid")
            {

            }
        }
    }
}
