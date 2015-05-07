using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace APICommon
{
    public class APIResponseMessage
    {
        public HttpRequestMessage Request { get; set; } 
        public List<LinkModel> Links { get; set; }

        public APIResponseMessage()
        {
        }

        public APIResponseMessage(HttpRequestMessage request)
        {
            Request = request;
        }

        public HttpResponseMessage CreateResponse(HttpStatusCode status, dynamic model)
        {
            if (Request == null)
                throw new ArgumentNullException("HttpRequestMessage cannot be null");
            var responseObject = new APIResponse();
            if (Links == null)
                Links = new List<LinkModel>();
            responseObject.Links = this.Links;
            responseObject.Data = new DataModel()
            {
                InfoMessage = "Success",
            };
            responseObject.Data.ReturnModel = model;
            HttpResponseMessage message = Request.CreateResponse(status, responseObject);
            return message;
        }

        public HttpResponseMessage CreateErrorResponse(HttpStatusCode status, string errorId, string errorMessage)
        {
            if (Request == null)
                throw new ArgumentNullException("HttpRequestMessage cannot be null");
            if (Links == null)
                Links = new List<LinkModel>();
            HttpResponseMessage message = Request.CreateResponse(
                status,
                new APIResponse()
                {
                    Links = this.Links,
                    Data = new DataModel()
                    {
                        ErrorId = errorId,
                        ErrorMessage = errorMessage,
                    }
                });
            return message;
        }
    }

}