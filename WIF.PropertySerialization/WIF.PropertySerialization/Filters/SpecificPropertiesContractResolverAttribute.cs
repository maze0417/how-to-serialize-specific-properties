using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Http.Filters;
using WIF.PropertySerialization.ContractResolvers;

namespace WIF.PropertySerialization.Filters
{
    /// <summary>
    /// Sets the formatter to an instance of the required type
    /// </summary>
    public class SpecificPropertiesContractResolverAttribute : ActionFilterAttribute
    {
        private readonly JsonMediaTypeFormatter formatterForType = new JsonMediaTypeFormatter();

        public SpecificPropertiesContractResolverAttribute(Type type)
        {
            formatterForType.SerializerSettings.ContractResolver = ((ISpecificPropertiesContractResolverFactory)Activator.CreateInstance(type)).GetResolver();            
        } 

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            ObjectContent content = actionExecutedContext.Response.Content as ObjectContent;

            if (content != null)
            {
                actionExecutedContext.Response.Content = new ObjectContent(content.ObjectType, content.Value, formatterForType);
            }
        }
    }
}