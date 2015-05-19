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
    public class ClassesTests
    {
        private EnrollMe.Controllers.ClassesController _controller = new EnrollMe.Controllers.ClassesController();
        private EnrollMe.Controllers.InstructorsController _instructorsController = new EnrollMe.Controllers.InstructorsController();
        public const string APIURL = "http://localhost/api/Classes";
        public const string ROUTENAME = "DefaultApi";
        public const string ROUTETEMPLATE = "api/{controller}/{id}";
        private int instructorId;

        [TestInitialize]
        public void TestInit()
        {
            _controller.Request = new HttpRequestMessage(HttpMethod.Get, APIURL);
            _controller.Configuration = new HttpConfiguration();
            _controller.Configuration.Routes.MapHttpRoute(
                name: ROUTENAME,
                routeTemplate: ROUTETEMPLATE,
                defaults: new { id = RouteParameter.Optional });
            var instructorName = new InstructorName
            {
                FirstName = "asdf",
                MiddleName = "asdf",
                LastName = "asdf",
            };
            _instructorsController.Request.Content = new StringContent(EnrollMe.Controllers.Helper.SerializeJson(instructorName));
            _instructorsController.Request.Content.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse("application/json");
            var response = _instructorsController.Post(instructorName);
            var data = ((APIResponse)(((System.Net.Http.ObjectContent)(response.Content)).Value)).Data;
            instructorId = (data.ReturnModel as EnrollMeDB.Instructors).InstructorId;
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _instructorsController.Delete(instructorId);
        }

        [TestMethod]
        public void Classes_GetAll()
        {
            var response = _controller.Get();
            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.NoContent);
        }

        [TestMethod]
        public void Classes_GetById()
        {
            var response = _controller.Get(0);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.NoContent);
        }

        [TestMethod]
        public void Classes_PostDelete()
        {
            var classObject = new ClassObject();
            classObject.ClassName = "asdf";
            classObject.DayOfClass = "asdf";
            classObject.TimeOfClass = "asdf";
            classObject.Location = "asdf";
            _controller.Request.Content = new StringContent(EnrollMe.Controllers.Helper.SerializeJson(classObject));
            _controller.Request.Content.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse("application/json");
            var response = _controller.Post(classObject);

            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            var data = ((APIResponse)(((System.Net.Http.ObjectContent)(response.Content)).Value)).Data;
            Assert.AreEqual((data.ReturnModel as EnrollMeDB.Classes).ClassName, classObject.ClassName);
            Assert.IsTrue(((APIResponse)(((System.Net.Http.ObjectContent)(response.Content)).Value)).Links.Count() > 0);

            _controller.Request.Content = new StringContent(EnrollMe.Controllers.Helper.SerializeJson(classObject));
            _controller.Request.Content.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse("application/json");
            response = _controller.Post(classObject);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            Assert.AreEqual((data.ReturnModel as EnrollMeDB.Classes).ClassName, classObject.ClassName);
            Assert.IsTrue(((APIResponse)(((System.Net.Http.ObjectContent)(response.Content)).Value)).Links.Count() > 0);

            response = _controller.Delete((data.ReturnModel as EnrollMeDB.Classes).ClassId);
            Assert.AreEqual(1, ((APICommon.APIResponse)(((System.Net.Http.ObjectContent)(response.Content)).Value)).Data.ReturnModel);
        }
    }
}
