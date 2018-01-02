using ApiDemo.Models;
using JsonApiDotNetCore.Controllers;
using JsonApiDotNetCore.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ApiDemo.Api.Controllers
{
    [Route("api/reviews")]
    public class ReviewsController : JsonApiController<Review>
    {
        public ReviewsController(
            IJsonApiContext jsonApiContext, 
            IResourceService<Review> resourceService, 
            ILoggerFactory loggerFactory) 
            : base(jsonApiContext, resourceService, loggerFactory)
        { 
        }
    }
}        