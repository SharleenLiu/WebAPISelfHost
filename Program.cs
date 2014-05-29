using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Http.SelfHost;
using System.Web.Http;
using System.Net.Http;

namespace WebAPISelfHost
{
    //Jon Flanders - Introduction to the ASP.NET Web API
    //The ASP.NET Web API provides you with choices when it comes to hosting your endpoints
    //Self Hosting gives your control over the hosting process
    //ASP.NET hosting allows you to defer process issues to IIS - but still retain control over configuration
    //overall
    //ASP.NET hosting exposes it through GlobalConfiguration.Configuration - change in global.asax.cs
    //Self-hosting you create an instance of HttpConfiguration directly
    public class Program
    {
        //use your own HttpMessageHandler demo
        //simulate it like Web API in ASP.NET, added MyController.cs
        //use Fiddler to Get http://localhost:8999/api/My
        //HttpClient and HttpServer are symmetrical        
        static void Main(string[] args)
        {
            var config = new HttpSelfHostConfiguration("http://localhost:8999");
            config.Routes.MapHttpRoute("default", "api/{controller}/{id}", new { id = RouteParameter.Optional });
            var server = new HttpSelfHostServer(config);
            var task = server.OpenAsync();
            task.Wait();
            Console.WriteLine("Self host server is up and running");
            Console.WriteLine("Hit enter to call server with client");
            Console.ReadLine();
            //HttpSelfHostServer: HttpServer: DelegatingHandler: HttpMessageHandler
            var client = new HttpClient(server);
            client.GetAsync("http://localhost:8999/api/my").ContinueWith((t) =>
            {
                var result = t.Result;
                result.Content.ReadAsStringAsync().ContinueWith((rt) =>
                    {
                        Console.WriteLine("client got response " + rt.Result);
                    });
            });

            Console.ReadLine();
            
        }
    }
}
