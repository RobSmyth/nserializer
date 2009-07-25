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


namespace NSerializer.Framework.Types
{
    public class TypeNamesCache : ITypeNamesCache
    {
        private readonly Dictionary<Type, int> cache = new Dictionary<Type, int>();
        private readonly ITypeNameMapper typeNamesMapper;

        public TypeNamesCache(ITypeNameMapper typeNamesMapper)
        {
            this.typeNamesMapper = typeNamesMapper;
        }

        public string[] Names
        {
            get
            {
                var names = new List<string>();
                foreach (var pair in cache)
                {
                    string typeName;

                    if (typeNamesMapper.CanHandle(pair.Key))
                    {
                        typeName = typeNamesMapper.GetTypeName(pair.Key);
                    }
                    else
                    {
                        // >>> map embedded types (array and generics) here <<<
                        typeName = new TypeNameDemangler(pair.Key).ToString();
                    }

                    names.Add(typeName);
                }
                return names.ToArray();
            }
        }

        public string GetTypeName(Type type)
        {
            Console.WriteLine("GetTypeName {0}", type.FullName);//>>>
            int id;
            if (cache.ContainsKey(type))
            {
                id = cache[type];
            }
            else
            {
                id = cache.Count;
                cache.Add(type, id);
            }

            return string.Format("_{0}", id);
        }
    }
}