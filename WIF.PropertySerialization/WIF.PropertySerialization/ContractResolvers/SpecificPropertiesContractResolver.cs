using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace WIF.PropertySerialization.ContractResolvers
{
    public class SpecificPropertiesContractResolver : DefaultContractResolver
    {
        public SpecificPropertiesContractResolver(List<ISerializerPropertyDeclaration> declarations)
        {
            var allDeclarations = declarations ?? new List<ISerializerPropertyDeclaration>();
            
            foreach (var declaration in allDeclarations)
            {
                if (!propertyDeclarations.ContainsKey(declaration.DeclarationType))
                {
                    propertyDeclarations.Add(declaration.DeclarationType, declaration);
                }
            }

            camelCasePropertyResolver = new CamelCasePropertyNamesContractResolver();
        }

        /// <summary>
        /// This will store a list of the properties which are to be serialized
        /// </summary>
        private readonly Dictionary<Type, ISerializerPropertyDeclaration> propertyDeclarations = new Dictionary<Type, ISerializerPropertyDeclaration>();

        /// <summary>
        /// This will be used to serialize the property names only
        /// </summary>
        private readonly CamelCasePropertyNamesContractResolver camelCasePropertyResolver;

        /// <summary>
        /// This method creates the JsonProperty
        /// Note that we create a JsonProperty for ALL the properties, but determine whether to serialize based on
        /// the "ShouldSerialize" property
        /// Using the CamelCasePropertynamesContractResolver makes sure we get the correct naming for JavaScript        
        /// </summary>                
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var prop = base.CreateProperty(member, memberSerialization);
             
            prop.PropertyName = camelCasePropertyResolver.GetResolvedPropertyName(prop.PropertyName);

            if (propertyDeclarations.ContainsKey(member.DeclaringType))
            {
                // This property is what needs to be set in order to determine whether 
                // the specific property should be serialized as part of the object or not
                prop.ShouldSerialize = instance =>
                {
                    var rules = propertyDeclarations[instance.GetType()];

                    return rules.ShouldSerialize(prop.PropertyName);
                };
            } 

            return prop;
        }
    }
}