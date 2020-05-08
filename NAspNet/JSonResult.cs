using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Template;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace NAspNet
{
    public class JsonResult<TValue> : JsonResult
    {
        public JsonResult(TValue value) : base(value) { }
        public JsonResult(TValue value, JsonSerializerSettings serializerSettings) : base(value, serializerSettings) { }

        public new TValue Value { get; set; }

    }
}
