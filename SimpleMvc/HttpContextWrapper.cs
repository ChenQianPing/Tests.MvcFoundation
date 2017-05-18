using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleMvc
{
    public class HttpContextWrapper
    {
        public HttpContext Context { get; set; }
        public IDictionary<string, string> RouteData { get; set; }
    }
}

/**
 *
 * 这里由于要使用到RouteData这个路由表的Dictionary对象，
 * 所以我们需要改写一下传递的对象由原来的HttpContext类型转换为自定义的包装类HttpContextWrapper.
 * 
 * 可以看出，其实就是简单地包裹了一下，添加了一个RouteData的路由表属性。
 */
