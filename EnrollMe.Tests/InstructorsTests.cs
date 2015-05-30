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
        private EnrollMe.Controllers.InstructorsController _controller = new EnrollMe.Controllers.InstructorsController();
        public const string APIURL = "http://localhost/api/Instructors";
        public const string ROUTENAME = "DefaultApi";
        public const string ROUTETEMPLATE = "api/{controller}/{id}";
        private string _value;

        [TestInitialize]
        public void TestInit()
        {
            _controller.Request = new HttpRequestMessage(HttpMethod.Get, APIURL);
            _controller.Configuration = new HttpConfiguration();
            _controller.Configuration.Routes.MapHttpRoute(
                name: ROUTENAME,
                routeTemplate: ROUTETEMPLATE,
                defaults: new { id = RouteParameter.Optional });
            _value = Helper.GetNewValue();
        }

        [TestMethod]
        public void Instructors_GetAll()
        {
            var response = _controller.Get();
            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.NoContent);
        }

        [TestMethod]
        public void Instructors_GetById()
        {
            var response = _controller.Get(0);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.NoContent);
        }

        [TestMethod]
        public void Instructors_PostDelete()
        {
            var instructorName = new InstructorName();

            //var response = _controller.Post(null);
            //Assert.AreEqual(response.StatusCode, HttpStatusCode.BadRequest);

            instructorName.FirstName = _value;
            instructorName.MiddleName = _value;
            instructorName.LastName = _value;
            _controller.Request.Content = new StringContent(EnrollMe.Controllers.Helper.SerializeJson(instructorName));
            _controller.Request.Content.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse("application/json");
            var response = _controller.Post(instructorName);

            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            var data = ((APIResponse)(((System.Net.Http.ObjectContent)(response.Content)).Value)).Data;
            Assert.AreEqual((data.ReturnModel as EnrollMeDB.Instructors).LastName, instructorName.LastName);
            Assert.IsTrue(((APIResponse)(((System.Net.Http.ObjectContent)(response.Content)).Value)).Links.Count() > 0);

            _controller.Request.Content = new StringContent(EnrollMe.Controllers.Helper.SerializeJson(instructorName));
            _controller.Request.Content.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse("application/json");
            response = _controller.Post(instructorName);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            Assert.AreEqual((data.ReturnModel as EnrollMeDB.Instructors).LastName, instructorName.LastName);
            Assert.IsTrue(((APIResponse)(((System.Net.Http.ObjectContent)(response.Content)).Value)).Links.Count() > 0);

            response = _controller.Delete((data.ReturnModel as EnrollMeDB.Instructors).InstructorId);
            Assert.AreEqual(1, ((APICommon.APIResponse)(((System.Net.Http.ObjectContent)(response.Content)).Value)).Data.ReturnModel);
        }

    }
}
