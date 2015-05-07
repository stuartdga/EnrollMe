using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Hosting;
using APICommon;

namespace APICommonTests
{
    [TestClass]
    public class APIResponseMessageTests
    {
        [TestInitialize]
        public void Initialize()
        {
            HttpContext.Current = new HttpContext(
                new HttpRequest("blank", "http://localhost", "blank"),
                new HttpResponse(new System.IO.StringWriter()));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateResponse_NullRequest()
        {
            System.Web.HttpContext.Current = new HttpContext(new HttpRequest("blank", "http://localhost", "blank"),
                                                 new HttpResponse(new System.IO.StringWriter()));
            var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost");
            var response = new APIResponseMessage();
            var message = response.CreateResponse(System.Net.HttpStatusCode.OK, null);
        }

        [TestMethod]
        public void CreateResponse()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost");
            request.SetConfiguration(new HttpConfiguration());
            var response = new APIResponseMessage(request);
            response.Links = new List<LinkModel>();
            var message = response.CreateResponse(HttpStatusCode.OK, null);
            Assert.AreEqual(message.StatusCode, HttpStatusCode.OK);

            var testObject = new TestObject(){
                TestId = 1,
                TestData = "asdf",
            };
            message = response.CreateResponse(HttpStatusCode.OK, testObject);
            var data = ((APICommon.APIResponse)(((System.Net.Http.ObjectContent)(message.Content)).Value)).Data;
            Assert.AreEqual(data.ReturnModel, testObject);
        }

        [TestMethod]
        public void CreateErrorResponse()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost");
            request.SetConfiguration(new HttpConfiguration());
            var response = new APIResponseMessage(request);
            response.Links = new List<LinkModel>();
            string errorMessage = "Error message";
            var message = response.CreateErrorResponse(HttpStatusCode.Conflict, "", errorMessage);
            var data = ((APICommon.APIResponse)(((System.Net.Http.ObjectContent)(message.Content)).Value)).Data;
            Assert.AreEqual(data.ErrorMessage, errorMessage);
        }
    }

    public class TestObject
    {
        public int TestId { get; set; }
        public string TestData { get; set; }
    }
}