using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace NAspNet
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class XController
    {
        public static JsonResult<TValue> Json<TValue>(this Controller @this, TValue data) => new JsonResult<TValue>(data);
        public static JsonResult<TValue> Json<TValue>(this Controller @this, TValue data, JsonSerializerSettings serializerSettings) => new JsonResult<TValue>(data, serializerSettings);
    }
}
