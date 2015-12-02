using System;
using System.Web.Mvc;

namespace ProductsMVC.Filters
{
    public class RangeExceptionAttribute : FilterAttribute, IExceptionFilter
    {
        void IExceptionFilter.OnException(ExceptionContext filterContext)
        {
/*            if(!filterContext.ExceptionHandled && filterContext.Exception is ArgumentOutOfRangeException)
            {
                filterContext.Result = new RedirectResult();
                filterContext.ExceptionHandled = true;
            }
*/
    }
    }
}