using System.Net;

namespace PostcodesApi.App.Common.Responses
{
    public abstract class Response
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess => new HttpResponseMessage(StatusCode).IsSuccessStatusCode;
    }
}
