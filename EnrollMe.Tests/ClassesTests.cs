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
        private EnrollMe.Controllers.ClassesController _classesController = new EnrollMe.Controllers.ClassesController();
        private EnrollMe.Controllers.InstructorsController _instructorsController = new EnrollMe.Controllers.InstructorsController();
        public const string APIURL = "http://localhost/api/";
        public const string ROUTENAME = "DefaultApi";
        public const string ROUTETEMPLATE = "api/{controller}/{id}";
        private int _instructorId;
        private string _value;

        [TestInitialize]
        public void TestInit()
        {
            _classesController.Request = new HttpRequestMessage(HttpMethod.Get, APIURL + "Classes");
            _classesController.Configuration = new HttpConfiguration();
            _classesController.Configuration.Routes.MapHttpRoute(
                name: ROUTENAME,
                routeTemplate: ROUTETEMPLATE,
                defaults: new { id = RouteParameter.Optional });
            _value = Helper.GetNewValue();
            var instructorName = new InstructorName
            {
                FirstName = _value,
                MiddleName = _value,
                LastName = _value,
            };
            _instructorsController.Request = new HttpRequestMessage(HttpMethod.Post, APIURL + "Instructors");
            _instructorsController.Request.Content = new StringContent(EnrollMe.Controllers.Helper.SerializeJson(instructorName));
            _instructorsController.Request.Content.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse("application/json");
            _instructorsController.Configuration = new HttpConfiguration();
            _instructorsController.Configuration.Routes.MapHttpRoute(
                name: ROUTENAME,
                routeTemplate: ROUTETEMPLATE,
                defaults: new { id = RouteParameter.Optional });
            var response = _instructorsController.Post(instructorName);
            var data = ((APIResponse)(((System.Net.Http.ObjectContent)(response.Content)).Value)).Data;
            _instructorId = (data.ReturnModel as EnrollMeDB.Instructors).InstructorId;
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _instructorsController.Delete(_instructorId);
        }

        [TestMethod]
        public void Classes_GetAll()
        {
            var response = _classesController.Get();
            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.NoContent);
        }

        [TestMethod]
        public void Classes_GetById()
        {
            var response = _classesController.Get(0);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.NoContent);
        }

        [TestMethod]
        public void Classes_PostDelete()
        {
            var classObject = new ClassObject();
            classObject.ClassName = _value;
            classObject.DayOfClass = _value;
            classObject.TimeOfClass = _value;
            classObject.Location = _value;
            classObject.InstructorId = _instructorId;
            _classesController.Request.Content = new StringContent(EnrollMe.Controllers.Helper.SerializeJson(classObject));
            _classesController.Request.Content.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse("application/json");
            var response = _classesController.Post(classObject);

            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            var data = ((APIResponse)(((System.Net.Http.ObjectContent)(response.Content)).Value)).Data;
            Assert.AreEqual((data.ReturnModel as EnrollMeDB.Classes).ClassName, classObject.ClassName);
            Assert.IsTrue(((APIResponse)(((System.Net.Http.ObjectContent)(response.Content)).Value)).Links.Count() > 0);

            _classesController.Request.Content = new StringContent(EnrollMe.Controllers.Helper.SerializeJson(classObject));
            _classesController.Request.Content.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse("application/json");
            response = _classesController.Post(classObject);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            Assert.AreEqual((data.ReturnModel as EnrollMeDB.Classes).ClassName, classObject.ClassName);
            Assert.IsTrue(((APIResponse)(((System.Net.Http.ObjectContent)(response.Content)).Value)).Links.Count() > 0);

            response = _classesController.Delete((data.ReturnModel as EnrollMeDB.Classes).ClassId);
            Assert.AreEqual(1, ((APICommon.APIResponse)(((System.Net.Http.ObjectContent)(response.Content)).Value)).Data.ReturnModel);
        }
    }
}
