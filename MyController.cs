﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Http;

namespace WebAPISelfHost
{
    public class MyController: ApiController
    {
        public string Get()
        {           
            return "Hello from my controller in Self-Hosting";
        }
    }
}
