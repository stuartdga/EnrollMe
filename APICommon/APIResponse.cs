using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APICommon
{
    public class APIResponse
    {
        public List<LinkModel> Links { get; set; }

        public DataModel Data { get; set; }
    }

    public enum RestRequestVerb
    {
        GET = 1,
        POST,
        PUT,
        DELETE,
    }

    public class LinkModel
    {
        public string Description { get; private set; }
        public string Href { get; private set; }
        public string Verb { get; private set; }

        public LinkModel(string description, string href, RestRequestVerb verb)
        {
            Description = description;
            Href = href;
            Verb = verb.ToString();
        }

        public LinkModel(string description, string urlFormat, string controller, string queryString, RestRequestVerb verb)
            : this(description, String.Format(urlFormat, controller, queryString), verb)
        { }

    }

    public class DataModel
    {
        public string InfoMessage { get; set; }
        public string ErrorId { get; set; }
        public string ErrorMessage { get; set; }
        public object ReturnModel { get; set; }

        /// <summary>
        /// Represents the build time of the current assembly. Used as a form of build version.
        /// </summary>
        public DateTime? APIBuildTime
        {
            get
            {
                return Helper.GetAssemblyBuildTimestamp();
            }
        }
    }

}