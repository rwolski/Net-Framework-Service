using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using WebApp.API.Models;

namespace WebApp.API
{
    public class MongoConfig
    {
        public static void RegisterTypes()
        {
            MongoEntityMapper.Map<Entity>();
            MongoEntityMapper.Map<OzLottoDrawModel>();
            
        }
    }
}