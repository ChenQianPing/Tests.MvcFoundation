using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Compilation;
using SimpleMvc.Controllers;

namespace SimpleMvc
{
    public class MvcHandler : IHttpHandler
    {
        // 路由表
        private readonly IDictionary<string, string> _routeData;

        // 所有控制器的类型集合
        private static readonly IList<Type> AlloctionControllerTypes;

        // 当前类第一次加载时调用静态构造函数
        static MvcHandler()
        {
            AlloctionControllerTypes = new List<Type>();

            // 获得当前所有引用的程序集
            var assemblies = BuildManager.GetReferencedAssemblies();
            // 遍历所有的程序集
            foreach (Assembly assembly in assemblies)
            {
                // 获取当前程序集中所有的类型
                var allTypes = assembly.GetTypes();
                // 遍历所有的类型
                foreach (var type in allTypes)
                {
                    // 如果当前类型满足条件
                    if (type.IsClass && !type.IsAbstract && type.IsPublic
                        && typeof(IController).IsAssignableFrom(type))
                    {
                        // 将所有Controller加入集合
                        AlloctionControllerTypes.Add(type);
                    }
                }
            }
        }

        /**
         * 此段代码利用反射加载了所有实现了IController接口的Controller类，
         * 并存入了一个静态集合alloctionControllerTypes里面，便于后面所有请求进行匹配.
         */

        public MvcHandler(IDictionary<string, string> routeData)
        {
            this._routeData = routeData;
        }


        public bool IsReusable
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public void ProcessRequest(HttpContext context)
        {
            var controllerName = _routeData["{controller}"];

            if (string.IsNullOrEmpty(controllerName))
            {
                // 指定默认控制器
                controllerName = "home";
            }

            IController controller = null;
            // 通过反射的方式加载具体实例
            foreach (var controllerItem in AlloctionControllerTypes)
            {
                if (controllerItem.Name.Equals($"{controllerName}Controller", StringComparison.InvariantCultureIgnoreCase))
                {
                    controller = Activator.CreateInstance(controllerItem) as IController;
                    break;
                }
            }

            var requestContext = new HttpContextWrapper()
            {
                Context = context,
                RouteData = _routeData
            };
            controller?.Execute(requestContext);
        }
    }
}


/*
 *  模拟ASP.NET管道工作，实现MvcHandler.
 *  在ASP.NET请求处理管道中，
 *  具体的处理工作都是转交给了实现IHttpHandler接口的Handler对象进行处理。
 *  因此，这里我们也遵照这个规则，实现一个MvcHandler来代替刚才的Index.ashx来进行路由工作.
 *  
 *  
 *  主要流程：
 *  1.MvcHandler；
 *  2.执行ProcessRequest方法；找到对应的Controller.
 *  3.创建Controller实例.
 *  4.调用Controller的Execute方法.
 *  5.XXXController，控制权交给Controller.
 *  
 *  核心就在于获取路由数据，指定处理程序，也就是理解并模拟路由机制。
 *  路由模块就是一个很简单的HttpModule，
 *  而ASP.NET MVC帮我们实现了UrlRoutingModule从而使我们轻松实现了路由机制，
 *  该机制获取了路由数据，并制定处理程序（如MvcHandler），
 *  执行MvcHandler的ProcessRequest方法找到对应的Controller类型，
 *  最后将控制权交给对应的Controller对象，
 *  
 *  就相当于前台小妹妹帮你找到了面试官，你可以跟着面试官去进行相应的面试了（Actioin），
 *  希望你能得到好的结果（ActionResult）。
 *  
 *  
 *  用一句简单地话来描述以上关键点：

　　路由（Route）就相当于一个公司的前台小姐，
  她负责带你（请求）找到跟你面试的面试官（控制器Controller），
  面试官可能会面试不同的职位（Action），你（请求）也会拿到不同的结果（ActionResult）；
 *  
 */
