using ApiDemo.Models;
using JsonApiDotNetCore.Controllers;
using JsonApiDotNetCore.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ApiDemo.Controllers
{
    [Route("api/authors")]
    public class AuthorsController : JsonApiController<Author>
    {
        public AuthorsController(
            IJsonApiContext jsonApiContext, 
            IResourceService<Author> resourceService, 
            ILoggerFactory loggerFactory) 
            : base(jsonApiContext, resourceService, loggerFactory)
        { 
        }
    }
}