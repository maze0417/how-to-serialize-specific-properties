using System;
using System.Web.Http;
using WIF.PropertySerialization.Filters;
using WIF.PropertySerialization.Models;
using WIF.PropertySerialization.WIFContractResolvers;
using WIF.PropertySerialization.WIFContractResolversFactories;

namespace WIF.PropertySerialization.Controllers
{
    [RoutePrefix("api/users")]
    public class UsersController : ApiController
    {
        private User exampleUser = new User
        {
            ID = "123",
            FirstName = "John",
            LastName = "Doe",
            EmailAddress = "john_doe@witinfive.io",
            IsBlocked = false,
            InternalNotesAboutUser = "This user should not be trusted.",
            LastLoggedIn = DateTime.Now
        };

        [HttpGet]
        [SpecificPropertiesContractResolver(typeof(MinimumPropertiesResolverFactory))]        
        public IHttpActionResult GetUser()
        {
            return Ok(exampleUser); 
        }

        [HttpGet]
        [Route("detailed")]
        [SpecificPropertiesContractResolver(typeof(MorePropertiesResolverFactory))]
        public IHttpActionResult GetDetailedUser()
        {
            return Ok(exampleUser);
        }
    }
}
