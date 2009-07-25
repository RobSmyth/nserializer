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
using NSerializer.Framework.Types;


namespace NSerializer.Types
{
    public class NullTypeNamesCache : ITypeNamesCache
    {
        private readonly Dictionary<Type, string> cache = new Dictionary<Type, string>();

        public string[] Names
        {
            get
            {
                var names = new List<string>();
                foreach (var pair in cache)
                {
                    names.Add(pair.Value);
                }
                return names.ToArray();
            }
        }

        public string GetTypeName(Type type)
        {
            string name;
            if (cache.ContainsKey(type))
            {
                name = cache[type];
            }
            else
            {
                name = new TypeNameDemangler(type).ToString();
                cache.Add(type, name);
            }

            return name;
        }
    }
}