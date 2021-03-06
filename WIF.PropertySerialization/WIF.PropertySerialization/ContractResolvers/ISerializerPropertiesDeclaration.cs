﻿using System;
using System.Collections.Generic;

namespace WIF.PropertySerialization.ContractResolvers
{
    public interface ISerializerPropertiesDeclaration
    {
        Type DeclarationType { get; }
        void RegisterProperties(params string[] properties);         
        List<string> Properties { get; }
        bool ShouldSerialize(string propertyName);

    }
}
