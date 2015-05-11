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
        [TestMethod]
        public void SetLinks_Instructors()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost/EnrollMe/api/Instructors");
            request.SetConfiguration(new HttpConfiguration());
            var controller = new InstructorsController();
            controller.Request = request;
            //List<LinkModel> links = EnrollMe.Controllers.Helper.SetLinks(request.GetUrlHelper(), InstructorsController.ROUTENAME, "Instructors", "Get");
            //Assert.IsTrue(links.Count > 0);
        }
    }
}
