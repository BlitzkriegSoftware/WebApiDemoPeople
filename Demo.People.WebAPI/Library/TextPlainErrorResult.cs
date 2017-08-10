using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace Demo.People.WebAPI.Library
{
    /// <summary>
    /// Global Error Handling -> Response Formatter
    /// </summary>
    public class TextPlainErrorResult : IHttpActionResult
    {
        /// <summary>
        /// Http Status Code
        /// </summary>
        public HttpStatusCode StatusCode { get; set; }
        /// <summary>
        /// Request
        /// </summary>
        public HttpRequestMessage Request { get; set; }
        /// <summary>
        /// Content
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// Execute Async
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<HttpResponseMessage> IHttpActionResult.ExecuteAsync(CancellationToken cancellationToken)
        {
            HttpResponseMessage response = new HttpResponseMessage(this.StatusCode);
            response.Content = new StringContent(Content);
            response.RequestMessage = Request;
            return Task.FromResult(response);
        }
    }
}