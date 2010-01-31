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
using System.Reflection;
using NSerializer.Types;
using NSerializer.XML.Readers.Members;


namespace NSerializer.Framework.Types
{
    public class TypeInAssemblyFinder : ITypeFinder
    {
        private readonly Assembly seedAssembly;
        private readonly ITypesCache typesCache;
        private readonly ITypeFinder typeFinder;

        public TypeInAssemblyFinder(Assembly seedAssembly, ITypesCache typesCache, ITypeFinder typeFinder)
        {
            this.seedAssembly = seedAssembly;
            this.typesCache = typesCache;
            this.typeFinder = typeFinder;
        }

        public IDataType GetType(string typeName)
        {
            var foundType = seedAssembly.GetType(typeName);
            if (foundType != null)
            {
                typesCache.Add(typeName, foundType);
                return new DataType(foundType);
            }
            return null;
        }
    }
}