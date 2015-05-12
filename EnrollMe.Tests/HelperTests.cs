using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EnrollMe;
using EnrollMe.Controllers;
using APICommon;

namespace EnrollMe.Tests
{
    [TestClass]
    public class HelperTests
    {
        public const string APIURL = "http://localhost/api/Instructors";
        public const string ROUTENAME = "DefaultApi";
        public const string ROUTETEMPLATE = "api/{controller}/{id}";

        [TestMethod]
        public void SetLinks_Instructors()
        {
            var controller = new InstructorsController();
            controller.Request = new HttpRequestMessage(HttpMethod.Get, APIURL);
            controller.Configuration = new HttpConfiguration();
            controller.Configuration.Routes.MapHttpRoute(
                name: ROUTENAME,
                routeTemplate: ROUTETEMPLATE,
                defaults: new { id = RouteParameter.Optional });
            List<LinkModel> links = EnrollMe.Controllers.Helper.SetLinks(controller.Request.GetUrlHelper(), InstructorsController.ROUTENAME, "Instructors", "Get");
            Assert.IsTrue(links.Count > 0);
            Assert.IsTrue(links[0].Href.Contains(APIURL));
        }
    }
}
