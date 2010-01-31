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
using NSerializer.Exceptions;
using NSerializer.Types;
using NSerializer.XML.Readers.Members;


namespace NSerializer.Framework.Types
{
    public class TypeInReferencedAssemblyFinder : ITypeFinder
    {
        private readonly Assembly seedAssembly;
        private readonly ITypesCache typesCache;
        private readonly ITypeFinder typeFinder;

        public TypeInReferencedAssemblyFinder(Assembly seedAssembly, ITypesCache typesCache, ITypeFinder typeFinder)
        {
            this.seedAssembly = seedAssembly;
            this.typesCache = typesCache;
            this.typeFinder = typeFinder;
        }

        public IDataType GetType(string typeName)
        {
            var assemblyNameFilter = new PassOnceNameFilter();

            var foundType = SearchReferencedAssemblies(typeName, seedAssembly, assemblyNameFilter);

            if (foundType != null)
            {
                typesCache.Add(typeName, foundType);
            }

            return new DataType(foundType, typeFinder);
        }

        private static Type SearchReferencedAssemblies(string typeName, Assembly assembly,
                                                       PassOnceNameFilter assemblyNameFilter)
        {
            Type foundType = null;

            var assemblies = assembly.GetReferencedAssemblies();

            foreach (var assemblyName in assemblies)
            {
                if (assemblyNameFilter.Pass(assemblyName.Name))
                {
                    Assembly referencedAssembly;
                    try
                    {
                        referencedAssembly = Assembly.Load(assemblyName);
                    }
                    catch (Exception exception)
                    {
                        throw new NSerializerException(
                            string.Format(
                                "Error while tring to find type '{0}'.\nUnable to load assembly '{1}' which is refrenced by assembly '{2}'.",
                                typeName, assemblyName.Name, assembly.FullName),
                            exception);
                    }

                    foundType = referencedAssembly.GetType(typeName);
                    if (foundType == null)
                    {
                        foundType = SearchReferencedAssemblies(typeName, referencedAssembly, assemblyNameFilter);
                    }

                    if (foundType != null)
                    {
                        break;
                    }
                }
            }

            return foundType;
        }
    }
}