using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using APICommon;

namespace EnrollMe.Controllers
{
    public class Helper
    {
        public static List<LinkModel> SetLinks(string url, string routeKey, string controller, string method)
        {
            int i = url.IndexOf(routeKey);
            url = url.Remove(i + routeKey.Length);

            List<LinkModel> links = new List<LinkModel>();
            switch (controller.ToUpper())
            {
                case ("INSTRUCTORS"):
                    switch (method.ToUpper())
                    {
                        case "GET":
                        case "POST":
                        case "DELETE":
                            links.Add(new LinkModel("get instructor", url + "/insrutctors/{id}", RestRequestVerb.GET));
                            links.Add(new LinkModel("get instructors", url + "/insrutctors", RestRequestVerb.GET));
                            links.Add(new LinkModel("post instructor", url + "/insrutctors", RestRequestVerb.POST));
                            links.Add(new LinkModel("delete instructor", url + "/insrutctors/{id}", RestRequestVerb.DELETE));
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