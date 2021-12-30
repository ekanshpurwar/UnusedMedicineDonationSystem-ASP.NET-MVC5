using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UMDS.Models
{
    public class CustomErrorHandler : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            var errMsg = filterContext.Exception.Message;
            var controllerName = filterContext.RouteData.Values["controller"].ToString();
            var actionName = filterContext.RouteData.Values["action"].ToString();

            filterContext.ExceptionHandled = true;

            HandleErrorInfo errModel = new HandleErrorInfo(filterContext.Exception, controllerName, actionName);

           

            ViewResult viewResult = new ViewResult
            {
                ViewName = "Error"
            };
            viewResult.ViewData = new ViewDataDictionary();
            viewResult.ViewData.Model = errModel;

            filterContext.Result = viewResult;
        }
    }
}