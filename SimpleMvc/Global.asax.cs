using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace SimpleMvc
{
    public class Global : System.Web.HttpApplication
    {

        // 定义路由规则
        private static IList<string> _routes;

        protected void Application_Start(object sender, EventArgs e)
        {
            _routes = new List<string> {"{controller}/{action}", "{controller}"};
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            #region 方式一：伪静态方式实现路由映射服务
            /*
            // 获得当前请求的URL地址
            var executePath = Request.AppRelativeCurrentExecutionFilePath;
            // 获得当前请求的参数数组
            var paraArray = executePath?.Substring(2).Split('/');
            // 如果没有参数则执行默认配置
            if (string.IsNullOrEmpty(executePath) || executePath.Equals("~/") ||
                paraArray.Length == 0)
            {
                return;
            }

            var controllerName = "home";
            if (paraArray.Length > 0)
            {
                controllerName = paraArray[0];
            }

            var actionName = "index";
            if (paraArray.Length > 1)
            {
                actionName = paraArray[1];
            }

            var urlPath = $"~/Index.ashx?controller={controllerName}&action={actionName}";

            // 入口一：单一入口 Index.ashx
            Context.RewritePath(urlPath);

            // 入口二：指定MvcHandler进行后续处理
            //Context.RemapHandler(new MvcHandler());
            */
            #endregion


            #region 方式二：模拟路由表实现映射服务

            // 模拟路由字典
            IDictionary<string, string> routeData = new Dictionary<string, string>();

            // 将URL与路由表中每一条记录进行匹配
            foreach (var item in _routes)
            {
                var executePath = Request.AppRelativeCurrentExecutionFilePath;   // 获得当前请求的参数数组
                                                                                 // 如果没有参数则执行默认配置
                if (string.IsNullOrEmpty(executePath) || executePath.Equals("~/"))
                {
                    executePath += "/home/index";
                }

                var executePathArray = executePath.Substring(2).Split(new[] { '/' },
                    StringSplitOptions.RemoveEmptyEntries);

                var routeKeys = item.Split(new[] { '/' },
                    StringSplitOptions.RemoveEmptyEntries);

                if (executePathArray.Length == routeKeys.Length)
                {
                    for (var i = 0; i < routeKeys.Length; i++)
                    {
                        routeData.Add(routeKeys[i], executePathArray[i]);
                    }

                    // 入口一：单一入口 Index.ashx
                    // Context.RewritePath($"~/Index.ashx?c={routeData["{controller}"]}&a={routeData["{action}"]}");
                    // 入口二：指定MvcHandler进行后续处理
                    Context.RemapHandler(new MvcHandler(routeData));
                    // 只要满足一条规则就跳出循环匹配
                    break;
                }
            }
            #endregion
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}


/*
 * 新建一个Global（全局处理程序），作为路由映射的入口.
 * 
 * 在Global.asax中有一个Application_BeginRequest的事件，它发生在每个Request开始处理之前，因此在这里我们可以进行一些类似于URL重写的工作。
 * 解析URL当然也在这里进行，我们要做的就是将用户输入的类似于MVC形式的URL：http://www.xxx.com/home/index 进行正确的解析，将该请求交由HomeController进行处理。
 * 
 * 这里我们直接在代码中hardcode了一个默认的controller和action名称，分别是home和index。
 　　可以看出，最后我们实际上做的就是解析URL，并通过重定向到Index.ashx进行所谓的Route路由工作。

    http://localhost:14668/home  
    Home Index Success!

    http://localhost:14668/home/add
    Home Add Success!

    http://localhost:14668/home
    http://localhost:14668/home/add
    http://localhost:14668/product
    http://localhost:14668/product/add

 */
