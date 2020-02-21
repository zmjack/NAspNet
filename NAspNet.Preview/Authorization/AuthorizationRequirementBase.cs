using Microsoft.AspNetCore.Authorization;

namespace NAspNet
{
    public abstract class AuthorizationRequirementBase : IAuthorizationRequirement
    {
        public string AuthenticationType { get; set; }

        public AuthorizationRequirementBase(string authenticationType)
        {
            AuthenticationType = authenticationType;
        }
    }

}
