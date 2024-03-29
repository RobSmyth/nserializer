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

// Project site: http://code.google.com/p/nserializer/

using System.Reflection;
using NSerializer.XML.Readers.Members;


namespace NSerializer.Framework.Types
{
    public class DefaultTypeFinder : ITypeFinder
    {
        private readonly ITypeFinder typeFinder;

        public DefaultTypeFinder(Assembly seedAssembly, IDataTypeFactory dataTypeFactory)
        {
            var typeFinderConduit = new TypeFinderConduit();

            var typesCache = new CachedTypesFinder(dataTypeFactory);
            ITypeFinder genericTypeFinder = new GenericTypeFinder(typesCache, typeFinderConduit, dataTypeFactory);
            ITypeFinder typeInAssemblyFinder = new TypeInAssemblyFinder(seedAssembly, typesCache, dataTypeFactory);
            ITypeFinder typeInReferencedAssemblyFinder = new TypeInReferencedAssemblyFinder(seedAssembly, typesCache,
                                                                                            typeFinder, dataTypeFactory);

            typeFinder =
                new TypeFinder(typesCache, genericTypeFinder, typeInAssemblyFinder, typeInReferencedAssemblyFinder);

            typeFinderConduit.SetTarget(typeFinder);
        }

        public IDataType GetType(string typeName)
        {
            return typeFinder.GetType(typeName);
        }
    }
}