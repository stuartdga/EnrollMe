using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using APICommon;
using EnrollMeDB;

namespace EnrollMe.Controllers
{
    public class InstructorName
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
    }

    [EnableCorsCustom]
    public class InstructorsController : ApiController
    {
        private APIResponseMessage apiResponse = new APIResponseMessage();

        // GET: api/Intructors
        public HttpResponseMessage Get()
        {
            try
            {
                apiResponse.Request = Request;
                var controller = new EnrollMeDB.Controller.InstructorsController();
                var instructors = controller.GetAll();
                if (instructors != null)
                {
                    return apiResponse.CreateResponse(HttpStatusCode.OK, instructors);
                }
                else
                {
                    apiResponse.Links = Helper.SetLinks(Request.RequestUri.ToString(), "api", "Instructors", "Get");
                    return apiResponse.CreateErrorResponse(HttpStatusCode.NoContent, "", "Instructor was not found");
                }
            }
            catch (Exception ex)
            {
                // log your exception
                return apiResponse.CreateErrorResponse(HttpStatusCode.Conflict, "", "An exception has occurred");
            }

        }

        // GET: api/Intructors/5
        public HttpResponseMessage Get(int id)
        {
            try
            {
                apiResponse.Request = Request;
                var controller = new EnrollMeDB.Controller.InstructorsController();
                var instructors = controller.Get(id);
                if (instructors != null)
                {
                    apiResponse.Links = Helper.SetLinks(Request.RequestUri.ToString(), "api", "Instructors", "Get");
                    return apiResponse.CreateResponse(HttpStatusCode.OK, instructors);
                }
                else
                {
                    return apiResponse.CreateErrorResponse(HttpStatusCode.NoContent, "", "Instructor was not found");
                }
            }
            catch (Exception ex)
            {
                // log your exception
                return apiResponse.CreateErrorResponse(HttpStatusCode.Conflict, "", "An exception has occurred");
            }
        }

        // POST: api/Intructors
        public HttpResponseMessage Post(InstructorName instructorName)
        {
            try
            {
                if (instructorName == null)
                {
                    return apiResponse.CreateErrorResponse(HttpStatusCode.BadRequest, "", "Instructor name not provided");
                }
                apiResponse.Request = Request;
                var controller = new EnrollMeDB.Controller.InstructorsController();
                var instructor = controller.Add(instructorName.FirstName, instructorName.MiddleName, instructorName.LastName);
                if (instructor != null)
                {
                    apiResponse.Links = Helper.SetLinks(Request.RequestUri.ToString(), "api", "Instructors", "Post");
                    return apiResponse.CreateResponse(HttpStatusCode.OK, instructor);
                }
                else
                {
                    return apiResponse.CreateErrorResponse(HttpStatusCode.NoContent, "", "Instructor was not added");
                }
            }
            catch (Exception ex)
            {
                // log your exception
                return apiResponse.CreateErrorResponse(HttpStatusCode.Conflict, "", "An exception has occurred");
            }
        }

        //// PUT: api/Intructors/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE: api/Intructors/5
        //public void Delete(int id)
        //{
        //}
    }
}
