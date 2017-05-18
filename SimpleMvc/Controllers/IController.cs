using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SimpleMvc.Controllers
{
    public interface IController
    {
        void Execute(HttpContextWrapper context);
    }
}

/*
 * IController接口只定义了一个方法声明，它接收一个HttpContext的上下文对象。
 */
