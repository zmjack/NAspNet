using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel;

namespace NAspNet
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class XHttpResponse
    {
        public static void ClearCookies(this HttpResponse @this, IRequestCookieCollection cookies)
        {
            foreach (var key in cookies.Keys)
                @this.Cookies.Delete(key);
        }

        public static void ClearCookies(this HttpResponse @this, IRequestCookieCollection cookies, Action<CookieOptions> setOptions)
        {
            var cookieOptions = new CookieOptions();
            setOptions(cookieOptions);

            foreach (var key in cookies.Keys)
                @this.Cookies.Delete(key, cookieOptions);
        }

        public static void PurgeCookies(this HttpResponse @this, IRequestCookieCollection cookies) => ClearCookies(@this, cookies, x => x.Path = "");

    }
}
