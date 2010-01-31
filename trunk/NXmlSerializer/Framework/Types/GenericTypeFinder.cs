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
using System.Text.RegularExpressions;
using NSerializer.Types;
using NSerializer.XML.Readers.Members;


namespace NSerializer.Framework.Types
{
    internal class GenericTypeFinder : ITypeFinder
    {
        private readonly ITypeFinder typeFinder;
        private readonly ITypesCache typesCache;

        public GenericTypeFinder(ITypesCache typesCache, ITypeFinder typeFinder)
        {
            this.typesCache = typesCache;
            this.typeFinder = typeFinder;
        }

        public IDataType GetType(string typeName)
        {
            Type foundType = null;

            if (typeName.EndsWith("]]"))
            {
                var regex = new Regex(@"^(?<collectionType>.+?)\[\[(?<paramterTypes>.+?)\]\]$");
                var matches = regex.Matches(typeName);

                if (matches.Count == 1)
                {
                    var parameters = matches[0].Groups["paramterTypes"].Value;
                    var valueTypeName = matches[0].Groups["collectionType"].Value;

                    var valueType = typeFinder.GetType(valueTypeName);

                    if (valueType != null)
                    {
                        var paramterTypes = GetParamterTypes(parameters);
                        foundType = valueType.GetTargetType().MakeGenericType(paramterTypes);
                    }

                    if (foundType != null)
                    {
                        typesCache.Add(typeName, foundType);
                    }
                }
            }

            return foundType == null ? null : new DataType(foundType);
        }

        private Type[] GetParamterTypes(string parameters)
        {
            var parameterTypeNames = parameters.Split(',');
            var paramterTypes = new Type[parameterTypeNames.Length];
            for (var index = 0; index < paramterTypes.Length; index++)
            {
                var parameterTypeName = parameterTypeNames[index].Trim(new[] {' ', '[', ']'});
                paramterTypes[index] = typeFinder.GetType(parameterTypeName).GetTargetType();
            }
            return paramterTypes;
        }
    }
}