using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Study4.Security.Utility.Attributes
{
    /// <summary>
    /// 檢查 Referer 是否符合來源
    /// </summary>
    public class RefererCheckAttribute : Attribute, IAsyncAuthorizationFilter
    {
        public RefererCheckAttribute()
        {
            //_customMode = Mode;
        }

        public Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            // 處理直接進入，不透過 a 標籤的情境。
            if (!context.HttpContext.Request.Headers["Referer"].Any())
            {
                return Task.CompletedTask;
            }

            if (!context.HttpContext.Request.Headers.TryGetValue("Referer", out StringValues refererValue))
            {
                throw new Exception("Cannot get Referer ");
            }

            if (!context.HttpContext.Request.Headers.TryGetValue("Host", out StringValues hostValue))
            {
                throw new Exception("Cannot get Host");
            }


            if (!refererValue.ToString().Contains(@$"/{hostValue}///"))
            {
                context.Result = new StatusCodeResult(403);
                return Task.CompletedTask;
            }

            return Task.CompletedTask;
        }
    }
}
