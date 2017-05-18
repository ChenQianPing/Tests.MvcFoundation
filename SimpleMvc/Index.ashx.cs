using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SimpleMvc.Controllers;

namespace SimpleMvc
{
    /*
     * 
    /// <summary>
    /// 模拟MVC程序的单一入口
    /// </summary>
    public class Index : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            // 获取Controller名称
            var controllerName = context.Request.QueryString["c"];

            // 声明IControoler接口-根据Controller Name找到对应的Controller
            IController controller = null;

            if (string.IsNullOrEmpty(controllerName))
            {
                controllerName = "home";
            }

            switch (controllerName.ToLower())
            {
                case "home":
                    controller = new HomeController();
                    break;
                case "product":
                    controller = new ProductController();
                    break;
                default:
                    controller = new HomeController();
                    break;
            }

            controller.Execute(context);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }

    */
}


/*
 * 有了Controller之后，需要借助一个入口来指引请求到达指定Controller，
 * 所以这里我们实现一个最简单的一般处理程序，
 * 它将url中的参数进行解析并实例化指定的Controller进行后续请求处理.
 * 
 * 该一般处理程序接收http请求的两个参数controller和action，
 * 并通过controller的参数名称生成对应的Controller实例对象，
 * 将HttpContext对象作为参数传递给对应的Controller对象进行后续处理。
 */
