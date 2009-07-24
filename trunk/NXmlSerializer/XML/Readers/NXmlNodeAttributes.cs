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

using System.Collections.Generic;
using System.Text.RegularExpressions;
using NSerializer.Exceptions;


namespace NSerializer.XML.Readers
{
    public class NXmlNodeAttributes
    {
        private readonly Dictionary<string, string> attributes = new Dictionary<string, string>();

        public NXmlNodeAttributes(string line)
        {
            var regex = new Regex("(?<attributeAssignment> (?<name>[^\\s=]+) =\" (?<value>.+?) \" )",
                                  RegexOptions.IgnorePatternWhitespace);

            var matches = regex.Matches(line);
            for (var index = 0; index < matches.Count; index++)
            {
                var name = matches[index].Groups["name"].Value;
                var value = matches[index].Groups["value"].Value;

                attributes.Add(name, value);
            }
        }

        public string Get(string name)
        {
            if (!attributes.ContainsKey(name))
            {
                throw new NXmlReaderFormatException(string.Format("Unknown attribute {0}.", name));
            }
            return attributes[name];
        }

        public int GetInteger(string name)
        {
            return int.Parse(Get(name));
        }
    }
}