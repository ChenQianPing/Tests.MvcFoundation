using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleMvc.Controllers
{
    public class HomeController : IController
    {
        private HttpContext _currentContext;
        public void Execute(HttpContextWrapper context)
        {
            _currentContext = context.Context;

            // 获取Action名称
            var actionName = "index";
            if (context.RouteData.ContainsKey("{action}"))
            {
                actionName = context.RouteData["{action}"];
            }

            switch (actionName.ToLower())
            {
                case "index":
                    this.Index();
                    break;
                case "add":
                    this.Add();
                    break;
                default:
                    this.Index();
                    break;
            }
        }

        // action 1 : Index
        public void Index()
        {
            _currentContext.Response.Write("Home Index Success!");
        }

        // action 2 : Add
        public void Add()
        {
            _currentContext.Response.Write("Home Add Success!");
        }
    }
}