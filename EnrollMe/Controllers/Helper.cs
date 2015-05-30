using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using APICommon;

namespace EnrollMe.Controllers
{
    public class Helper
    {
        public static List<LinkModel> SetLinks(System.Web.Http.Routing.UrlHelper urlHelper, string routeName, string controller, string method, int id = 0)
        {
            object route = null;
            List<LinkModel> links = new List<LinkModel>();
            switch (controller.ToUpper())
            {
                case ("INSTRUCTORS"):
                    switch (method.ToUpper())
                    {
                        case "GET":
                        case "POST":
                        case "DELETE":
                            route = new { controller = controller };
                            links.Add(new LinkModel("get instructors", urlHelper.Link(routeName, route), RestRequestVerb.GET));
                            links.Add(new LinkModel("post instructor", urlHelper.Link(routeName, route), RestRequestVerb.POST));
                            if (id > 0)
                            {
                                route = new { controller = controller, id = id };
                                links.Add(new LinkModel("get instructor", urlHelper.Link(routeName, route), RestRequestVerb.GET));
                                links.Add(new LinkModel("delete instructor", urlHelper.Link(routeName, route), RestRequestVerb.DELETE));
                            }

                            break;
                    }
                    break;
                case ("CLASSES"):
                    switch (method.ToUpper())
                    {
                        case "GET":
                        case "POST":
                        case "DELETE":
                            route = new { controller = controller };
                            links.Add(new LinkModel("get classes", urlHelper.Link(routeName, route), RestRequestVerb.GET));
                            links.Add(new LinkModel("post classes", urlHelper.Link(routeName, route), RestRequestVerb.POST));
                            if (id > 0)
                            {
                                route = new { controller = controller, id = id };
                                links.Add(new LinkModel("get class", urlHelper.Link(routeName, route), RestRequestVerb.GET));
                                links.Add(new LinkModel("delete class", urlHelper.Link(routeName, route), RestRequestVerb.DELETE));
                            }

                            break;
                    }
                    break;
                default:
                    break;
            }

            return links;
        }

        public static string SerializeJson(object obj)
        {
            var json = JsonConvert.SerializeObject(obj, Newtonsoft.Json.Formatting.None,
                new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            return json;
        }

    }
}