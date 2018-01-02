using ApiDemo.Models;
using JsonApiDotNetCore.Controllers;
using JsonApiDotNetCore.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ApiDemo.Controllers
{
    [Route("api/books")]
    public class BooksController : JsonApiController<Book>
    {
        public BooksController(
            IJsonApiContext jsonApiContext, 
            IResourceService<Book> resourceService, 
            ILoggerFactory loggerFactory) 
            : base(jsonApiContext, resourceService, loggerFactory)
        { 
        }
    }
}
