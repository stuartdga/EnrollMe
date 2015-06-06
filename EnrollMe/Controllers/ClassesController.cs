using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using APICommon;
using EnrollMeDB;

namespace EnrollMe.Controllers
{
    public class ClassObject
    {
        public string ClassName { get; set; }
        public string DayOfClass { get; set; }
        public string TimeOfClass { get; set; }
        public string Location { get; set; }
        public int InstructorId { get; set; }
        public string Organization { get; set; }
    }

    public class ClassesController : ApiController
    {
        private APIResponseMessage apiResponse = new APIResponseMessage();
        public const string ROUTENAME = "DefaultApi";
        private EnrollMeDB.Controller.ClassesDBController _controller = new EnrollMeDB.Controller.ClassesDBController();
        private string _organization = System.Configuration.ConfigurationManager.AppSettings["Organization"].ToString();

        // GET: api/Classes
        public HttpResponseMessage Get()
        {
            try
            {
                apiResponse.Request = Request;
                var classes = _controller.Get(_organization);
                if (classes != null)
                {
                    apiResponse.Links = Helper.SetLinks(Url, ROUTENAME, "Classes", "Get");
                    return apiResponse.CreateResponse(HttpStatusCode.OK, classes);
                }
                else
                {
                    return apiResponse.CreateErrorResponse(HttpStatusCode.OK, "", "No classes found");
                }
            }
            catch (Exception ex)
            {
                // log your exception
                return apiResponse.CreateErrorResponse(HttpStatusCode.Conflict, "", "An exception has occurred");
            }

        }

        // GET: api/Classes/5
        public HttpResponseMessage Get(int id)
        {
            try
            {
                apiResponse.Request = Request;
                var controller = new EnrollMeDB.Controller.ClassesDBController();
                EnrollMeDB.Classes classes = controller.Get(id);
                if (classes != null)
                {
                    apiResponse.Links = Helper.SetLinks(Url, ROUTENAME, "Instructors", "Get", id);
                    return apiResponse.CreateResponse(HttpStatusCode.OK, classes);
                }
                else
                {
                    return apiResponse.CreateErrorResponse(HttpStatusCode.NotFound, "", "Class was not found");
                }
            }
            catch (Exception ex)
            {
                // log your exception
                return apiResponse.CreateErrorResponse(HttpStatusCode.InternalServerError, "", "An exception has occurred");
            }
        }

        [HttpGet]
        public HttpResponseMessage Get([FromUri] string className, string dayOfClass, string timeOfClass, string location, string organization)
        {
            try
            {
                apiResponse.Request = Request;
                var classes = _controller.Get(className, dayOfClass, timeOfClass, location, organization);
                if (classes != null)
                {
                    apiResponse.Links = Helper.SetLinks(Url, ROUTENAME, "Classes", "Get");
                    return apiResponse.CreateResponse(HttpStatusCode.OK, classes);
                }
                else
                {
                    return apiResponse.CreateErrorResponse(HttpStatusCode.NotFound, "", "Class was not found");
                }
            }
            catch (Exception ex)
            {
                // log your exception
                return apiResponse.CreateErrorResponse(HttpStatusCode.InternalServerError, "", "An exception has occurred");
            }
        }

        // POST: api/Classes
        public HttpResponseMessage Post(ClassObject classObject)
        {
            try
            {
                if (classObject == null)
                {
                    return apiResponse.CreateErrorResponse(HttpStatusCode.BadRequest, "", "Class information not provided");
                }
                apiResponse.Request = Request;
                var classes = _controller.Add(classObject.ClassName, classObject.DayOfClass, classObject.TimeOfClass, 
                                                classObject.Location, classObject.InstructorId, classObject.Organization);
                if (classes != null)
                {
                    apiResponse.Links = Helper.SetLinks(Url, ROUTENAME, "Classes", "Post");
                    return apiResponse.CreateResponse(HttpStatusCode.OK, classes);
                }
                else
                {
                    return apiResponse.CreateErrorResponse(HttpStatusCode.BadRequest, "", "Class was not added");
                }
            }
            catch (Exception ex)
            {
                // log your exception
                return apiResponse.CreateErrorResponse(HttpStatusCode.InternalServerError, "", "An exception has occurred");
            }
        }

        //// PUT: api/Classes/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        // DELETE: api/Classes/5
        public HttpResponseMessage Delete(int id)
        {
            apiResponse.Request = Request;
            int result = _controller.Remove(id);
            if (result > 0)
            {
                apiResponse.Links = Helper.SetLinks(Url, ROUTENAME, "Classes", "Delete", id);
                return apiResponse.CreateResponse(HttpStatusCode.OK, result);
            }
            else
            {
                return apiResponse.CreateErrorResponse(HttpStatusCode.BadRequest, "", "Class was not found");
            }
        }
    }
}
