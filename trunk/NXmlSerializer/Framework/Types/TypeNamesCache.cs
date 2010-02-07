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

using System;
using System.Collections.Generic;


namespace NSerializer.Framework.Types
{
    public class TypeNamesCache : ITypeNamesCache
    {
        private readonly SortedList<Type, int> cache;
        private readonly ITypeNameMapper typeNamesMapper;
        private readonly List<Type> typesCache = new List<Type>();

        public TypeNamesCache(ITypeNameMapper typeNamesMapper)
        {
            this.typeNamesMapper = typeNamesMapper;
            cache = new SortedList<Type, int>(new TypeComparer());
        }

        public MetaDataTypeName[] Names
        {
            get
            {
                var names = new List<MetaDataTypeName>();

                for (var index = 0; index < typesCache.Count; index++)
                {
                    names.Add(new MetaDataTypeName(index, GetNormalisedName(typesCache[index])));
                }

                return names.ToArray();
            }
        }

        public string GetTypeName(Type type)
        {
            int id;
            if (cache.ContainsKey(type))
            {
                id = cache[type];
            }
            else
            {
                id = cache.Count;
                cache.Add(type, typesCache.Count);
                typesCache.Add(type);
            }

            return string.Format("!{0}", id);
        }

        private string GetNormalisedName(Type type)
        {
            string typeName;
            if (typeNamesMapper.CanHandle(type))
            {
                typeName = typeNamesMapper.GetTypeName(type);
            }
            else
            {
                // >>> map embedded types (array and generics) here <<<

                if (type.IsArray)
                {
                    typeName = string.Format("{0}[]", GetTypeName(type.GetElementType()));
                }
                else
                {
                    typeName = new TypeNameDemangler(type).ToString();
                }
            }
            return typeName;
        }
    }

    public class TypeComparer : IComparer<Type>
    {
        public int Compare(Type x, Type y)
        {
            return x.FullName.CompareTo(y.FullName);
        }
    }
}