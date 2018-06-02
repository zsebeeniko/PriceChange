using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using SmartPrice.IoC.DI;

namespace WebAPI
{
    public class StructureMapConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.DependencyResolver = new DependencyResolver(Resolver.Container);
        }
    }
}