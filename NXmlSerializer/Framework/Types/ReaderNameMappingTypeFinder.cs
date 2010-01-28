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
using NSerializer.Types;
using System.Text.RegularExpressions;


namespace NSerializer.Framework.Types
{
    public class ReaderNameMappingTypeFinder : ITypeFinder
    {
        private readonly string[] names;
        private readonly ITypeFinder typeFinder;

        public ReaderNameMappingTypeFinder(ITypeFinder typeFinder, string[] names)
        {
            this.typeFinder = typeFinder;
            this.names = names;
        }

        public Type Get(string typeName)
        {
            while (typeName[0] == '!')
            {
                var regex = new Regex(@"^!(?<id>\d+)(?<remainder>.*)$", RegexOptions.IgnorePatternWhitespace);
                var matches = regex.Matches(typeName);
                var id = matches[0].Groups["id"].Value;
                var remainder = matches[0].Groups["remainder"].Value;
                var typeIndex = int.Parse(id);
                typeName = names[typeIndex] + remainder;
            }

            var isArray = typeName.EndsWith("[]");
            if (isArray)
            {
                return typeFinder.Get(typeName.Substring(0, typeName.Length - "[]".Length)).MakeArrayType();
            }
            return typeFinder.Get(typeName);
        }
    }
}