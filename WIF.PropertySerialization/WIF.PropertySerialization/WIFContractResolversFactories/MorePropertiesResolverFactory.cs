using System.Collections.Generic;
using WIF.PropertySerialization.ContractResolvers;
using WIF.PropertySerialization.Models;

namespace WIF.PropertySerialization.WIFContractResolversFactories
{
    public class MorePropertiesResolverFactory : ISpecificPropertiesContractResolverFactory
    {
        public SpecificPropertiesContractResolver GetResolver()
        {
            // create an instance of the specificpropertiescontractresolver 
            // set all the properties for the types needed
            return new SpecificPropertiesContractResolver(new List<ISerializerPropertyDeclaration>
            {
                UserProperties 
            });
        }

        private static ISerializerPropertyDeclaration UserProperties =
           new SerializerPropertyDeclaration<User>(new string[]
           {
               "ID",
               "FirstName",
               "LastName",
               "EmailAddress",
               "LastLoggedIn"
           }); 
    }
}