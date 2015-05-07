using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Hosting;
using APICommon;
using EnrollMe;
using EnrollMe.Controllers;

namespace EnrollMe.Tests
{
    [TestClass]
    public class InstructorsTests
    {
        private EnrollMe.Controllers.InstructorsController controller = new EnrollMe.Controllers.InstructorsController();

        [TestMethod]
        public void Instructors_GetAll()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost/EnrollMe/api/Instructors/0");
            request.SetConfiguration(new HttpConfiguration());
            var controller = new InstructorsController();
            controller.Request = request;
            var response = controller.Get();
            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.NoContent);
        }

        [TestMethod]
        public void Instructors_GetById()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost/EnrollMe/api/Instructors/0");
            request.SetConfiguration(new HttpConfiguration());
            var controller = new InstructorsController();
            controller.Request = request;
            var response = controller.Get(0);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.NoContent);
        }

        [TestMethod]
        public void Instructors_Post()
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost/EnrollMe/api/Instructors/");
            request.SetConfiguration(new HttpConfiguration());
            var controller = new InstructorsController();
            controller.Request = request;
            var instructorName = new InstructorName();
            request.Content = new StringContent(EnrollMe.Controllers.Helper.SerializeJson(instructorName));
            request.Content.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse("application/json");
            var response = controller.Post(instructorName);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.Conflict);

            response = controller.Post(null);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.BadRequest);

            instructorName.FirstName = "First";
            instructorName.MiddleName = "Middle";
            instructorName.LastName = "Last";
            response = controller.Post(instructorName);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            var data = ((APIResponse)(((System.Net.Http.ObjectContent)(response.Content)).Value)).Data;
            Assert.AreEqual((data.ReturnModel as EnrollMeDB.Instructors).LastName, instructorName.LastName);
            Assert.IsTrue(((APIResponse)(((System.Net.Http.ObjectContent)(response.Content)).Value)).Links.Count() > 0);

            response = controller.Post(instructorName);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            data = ((APIResponse)(((System.Net.Http.ObjectContent)(response.Content)).Value)).Data;
            Assert.AreEqual((data.ReturnModel as EnrollMeDB.Instructors).LastName, instructorName.LastName);
            Assert.IsTrue(((APIResponse)(((System.Net.Http.ObjectContent)(response.Content)).Value)).Links.Count() > 0);
        }

    }
}
