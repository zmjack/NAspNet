using Microsoft.AspNetCore.Authorization;

namespace NAspNet
{
    public abstract class AuthorizeBaseAttribute : AuthorizeAttribute
    {
        public abstract string PolicyPrefix { get; }
        public string AuthenticationType { get; set; }

        public AuthorizeBaseAttribute(string authenticationType)
        {
            AuthenticationType = authenticationType;
        }
    }

}
