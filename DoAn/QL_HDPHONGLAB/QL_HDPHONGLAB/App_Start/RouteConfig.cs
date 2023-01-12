﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace QL_HDPHONGLAB
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //Mặc định
            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Login", action = "FormDangNhap", id = UrlParameter.Optional }
            //);

            //Admin
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { Controller = "TrangChu", action = "Trangchu", id = UrlParameter.Optional },
                namespaces: new string[] { "QL_HDPHONGLAB.Areas.Admin.Controllers" }
            ).DataTokens.Add("area", "Admin");
        }


    }
}
