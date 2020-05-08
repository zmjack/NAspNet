using Microsoft.AspNetCore.Http;
using NStandard;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;

namespace NAspNet
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class XHttpRequest
    {
        /// <summary>
        /// Returns $"{@this.PathBase}{@this.Path}".
        ///     eg. /controller/action
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static string UrlPath(this HttpRequest @this) => $"{@this.PathBase}{@this.Path}";

        /// <summary>
        /// Returns $"{@this.PathBase}{@this.Path}{@this.QueryString}".
        ///     eg. /controller/action?id=1
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static string UrlPathQuery(this HttpRequest @this) => $"{@this.PathBase}{@this.Path}{@this.QueryString}";

        /// <summary>
        /// Returns $"{@this.Scheme}://{@this.Host}".
        ///     eg. http://dawnx.net
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static string UrlSchemeHost(this HttpRequest @this) => $"{@this.Scheme}://{@this.Host}";

        /// <summary>
        /// Returns $"{@this.Scheme}://{@this.Host}{@this.PathBase}".
        ///     eg. http://dawnx.net{Configuration["ASPNETCORE_APPL_PATH"]}
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static string UrlSchemeHostBase(this HttpRequest @this) => $"{@this.Scheme}://{@this.Host}{@this.PathBase}";

        /// <summary>
        /// Returns $"{@this.Scheme}://{@this.Host}{@this.PathBase}{@this.Path}".
        ///     eg. http://dawnx.net/controller/action
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static string UrlSchemeHostPath(this HttpRequest @this)
            => $"{@this.Scheme}://{@this.Host}{@this.PathBase}{@this.Path}";

        /// <summary>
        /// Returns $"{@this.Scheme}://{@this.Host}{@this.PathBase}{@this.Path}{@this.QueryString}".
        ///     eg. http://dawnx.net/controller/action?id=1
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static string Url(this HttpRequest @this)
            => $"{@this.Scheme}://{@this.Host}{@this.PathBase}{@this.Path}{@this.QueryString}";

        /// <summary>
        /// Gets the request body string(UTF-8).
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static string BodyString(this HttpRequest @this) => BodyString(@this, Encoding.UTF8);

        /// <summary>
        /// Gets the request body string.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string BodyString(this HttpRequest @this, Encoding encoding)
        {
            var memory = new MemoryStream();
            @this.Body.CopyTo(memory, 256 * 1024);
            return memory.ToArray().String(encoding);
        }

    }
}
