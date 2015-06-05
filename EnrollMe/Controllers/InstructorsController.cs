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
    public class InstructorModel
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Organization { get; set; }
    }

    [EnableCorsCustom]
    public class InstructorsController : ApiController
    {
        private APIResponseMessage apiResponse = new APIResponseMessage();
        public const string ROUTENAME = "DefaultApi";
        private EnrollMeDB.Controller.InstructorsController _controller = new EnrollMeDB.Controller.InstructorsController();

        // GET: api/Intructors
        public HttpResponseMessage Get()
        {
            try
            {
                apiResponse.Request = Request;
                var instructors = _controller.GetAll();
                if (instructors != null)
                {
                    apiResponse.Links = Helper.SetLinks(Url, ROUTENAME, "Instructors", "Get");
                    return apiResponse.CreateResponse(HttpStatusCode.OK, instructors);
                }
                else
                {
                    return apiResponse.CreateErrorResponse(HttpStatusCode.NotFound, "", "No instructors found");
                }
            }
            catch (Exception ex)
            {
                // log your exception
                return apiResponse.CreateErrorResponse(HttpStatusCode.InternalServerError, "", "An exception has occurred");
            }

        }

        // GET: api/Intructors/5
        public HttpResponseMessage Get(int id)
        {
            try
            {
                apiResponse.Request = Request;
                var instructors = _controller.Get(id);
                if (instructors != null)
                {
                    apiResponse.Links = Helper.SetLinks(Url, ROUTENAME, "Instructors", "Get", id);
                    return apiResponse.CreateResponse(HttpStatusCode.OK, instructors);
                }
                else
                {
                    return apiResponse.CreateErrorResponse(HttpStatusCode.NotFound, "", "Instructor was not found");
                }
            }
            catch (Exception ex)
            {
                // log your exception
                return apiResponse.CreateErrorResponse(HttpStatusCode.InternalServerError, "", "An exception has occurred");
            }
        }

        // POST: api/Intructors
        public HttpResponseMessage Post(InstructorModel instructorModel)
        {
            try
            {
                if (instructorModel == null)
                {
                    return apiResponse.CreateErrorResponse(HttpStatusCode.BadRequest, "", "Instructor name not provided");
                }
                apiResponse.Request = Request;
                var instructor = _controller.Add(instructorModel.FirstName, instructorModel.MiddleName,
                                                instructorModel.LastName, instructorModel.Organization);
                if (instructor != null)
                {
                    apiResponse.Links = Helper.SetLinks(Url, ROUTENAME, "Instructors", "Post", instructor.InstructorId);
                    return apiResponse.CreateResponse(HttpStatusCode.OK, instructor);
                }
                else
                {
                    return apiResponse.CreateErrorResponse(HttpStatusCode.NotFound, "", "Instructor was not added");
                }
            }
            catch (Exception ex)
            {
                // log your exception
                return apiResponse.CreateErrorResponse(HttpStatusCode.InternalServerError, "", "An exception has occurred");
            }
        }

        //// PUT: api/Intructors/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE: api/Intructors/5
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                apiResponse.Request = Request;
                int result = _controller.Remove(id);
                if (result > 0)
                {
                    apiResponse.Links = Helper.SetLinks(Url, ROUTENAME, "Instructors", "Delete", id);
                    return apiResponse.CreateResponse(HttpStatusCode.OK, result);
                }
                else
                {
                    return apiResponse.CreateErrorResponse(HttpStatusCode.NotFound, "", "Instructor was not found");
                }
            }
            catch (Exception ex)
            {
                // log your exception
                return apiResponse.CreateErrorResponse(HttpStatusCode.InternalServerError, "", "An exception has occurred");
            }
        }
    }
}
