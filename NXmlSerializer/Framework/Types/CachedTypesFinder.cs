#region Copyright

// The contents of this file are subject to the Mozilla Public License
//  Version 1.1 (the "License"); you may not use this file except in compliance
//  with the License. You may obtain a copy of the License at
//  
//  http://www.mozilla.org/MPL/
//  
//  Software distributed under the License is distributed on an "AS IS"
//  basis, WITHOUT WARRANTY OF ANY KIND, either express or implied. See the
//  License for the specific language governing rights and limitations under 
//  the License.
//  
//  The Initial Developer of the Original Code is Robert Smyth.
//  Portions created by Robert Smyth are Copyright (C) 2008.
//  
//  All Rights Reserved.

#endregion

using System;
using System.Collections.Generic;
using NSerializer.Types;
using NSerializer.XML.Readers.Members;


namespace NSerializer.Framework.Types
{
    internal class CachedTypesFinder : ITypesCache, ITypeFinder
    {
        private readonly Dictionary<string, IDataType> cachedTypes = new Dictionary<string, IDataType>();
        private readonly IDataTypeFactory dataTypeFactory;

        public CachedTypesFinder(IDataTypeFactory dataTypeFactory)
        {
            this.dataTypeFactory = dataTypeFactory;
        }

        public IDataType GetType(string typeName)
        {
            IDataType foundType;
            if (!cachedTypes.TryGetValue(typeName, out foundType))
            {
                return null;
            }
            return foundType;
        }

        public void Add(string typeName, Type type)
        {
            cachedTypes.Add(typeName, dataTypeFactory.Create(type));
        }
    }
}