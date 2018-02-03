using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WIF.PropertySerialization.ContractResolvers
{
    /// <summary>
    /// Stores the list of properties to be serialized
    /// </summary>
    /// <typeparam name="T">
    /// The class type for which properties are declared
    /// </typeparam>
    public class SerializerPropertiesDeclaration<T> : ISerializerPropertiesDeclaration
    {
        public SerializerPropertiesDeclaration(params string[] properties)
        {
            this.RegisterProperties(properties);
        }

        public Type DeclarationType
        {
            get
            {
                return typeof(T);
            }
        }

        /// <summary>
        /// Adds the properties to a HashSet for fast retrieval
        /// </summary>        
        public void RegisterProperties(params string[] properties)
        {
            propertiesToSerialize = new HashSet<string>();

            foreach (var prop in properties)
            {
                string propToUpper = prop.ToUpperInvariant();

                if (!propertiesToSerialize.Contains(propToUpper))
                {  
                    propertiesToSerialize.Add(propToUpper);
                }
            }
        }

        /// <summary>
        /// List of the properties to be serialized
        /// </summary>
        public List<string> Properties
        {
            get
            {
                return propertiesToSerialize.ToList();
            }
        }

        private HashSet<string> propertiesToSerialize;

        /// <summary>
        /// Determines whether to serialize a property by checking whether it 
        /// is in the list
        /// </summary>        
        public bool ShouldSerialize(string propertyName)
        {
            if (propertiesToSerialize.Contains(propertyName.ToUpperInvariant()))
            {
                return true;
            }

            return false;
        }
    }
}