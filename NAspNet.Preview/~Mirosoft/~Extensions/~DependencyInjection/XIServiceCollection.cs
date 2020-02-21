using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel;

namespace NAspNet
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class XIServiceCollection
    {
        public static void RewriteAuthorizationProvider<TAuthorizationPolicyProvider>(this IServiceCollection @this)
            where TAuthorizationPolicyProvider : CustomExtraAuthorizationPolicyProvider, new()
        {
            @this.AddSingleton<IAuthorizationPolicyProvider, TAuthorizationPolicyProvider>();

            foreach (var handlerType in new TAuthorizationPolicyProvider().RequirementHandlerTypes)
                @this.AddSingleton(typeof(IAuthorizationHandler), handlerType);
        }

    }
}
