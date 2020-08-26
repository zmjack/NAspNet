using System.Linq;
using System.Security.Claims;

namespace NAspNet
{
    public static class ClaimsIdentityEx
    {
        public static ClaimsIdentity Create(string userName, string[] roles)
        {
            var authenticationType = $"{nameof(NAspNet)}.{nameof(ClaimsIdentityEx)}.{nameof(Create)}";
            var identity = new ClaimsIdentity(authenticationType, ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            identity.AddClaim(new Claim(ClaimsIdentity.DefaultNameClaimType, userName));
            if (roles != null) identity.AddClaims(roles.Select(x => new Claim(ClaimsIdentity.DefaultRoleClaimType, x)));

            return identity;
        }
    }
}
