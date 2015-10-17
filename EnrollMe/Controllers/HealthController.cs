using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using APICommon;

namespace EnrollMe.Controllers
{
    public class HealthController : ApiController
    {
        [HttpGet]
        public string Get()
        {
            return "I am working as expected for GET.  Build Time: " + APICommon.Helper.GetAssemblyBuildTimestamp().ToString();
        }

        [HttpGet]
        public string Get(string action)
        {
            if (action.Equals("exception", StringComparison.InvariantCultureIgnoreCase))
            {
                throw new ApplicationException("Test Exception"); 
            }

            return "I am working as expected for GET.  Build Time: " + APICommon.Helper.GetAssemblyBuildTimestamp().ToString();
        }

        [HttpPost]
        public HttpResponseMessage Post([FromBody] TestObject testObject)
        {
            var responseObject = new ResponseObject() {
                id = testObject.id,
                build = APICommon.Helper.GetAssemblyBuildTimestamp(),
            };
            var response = Request.CreateResponse(HttpStatusCode.OK, responseObject);    
            response.Headers.Add("id", testObject.id.ToString());
            return response;
        }
    }

    public class TestObject
    {
        public int id { get; set; }
        public DateTime? buildtime { get; set; }
    }
    public class ResponseObject
    {
        public int id { get; set; }
        public DateTime? build { get; set; }
    }

}
