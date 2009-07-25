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
using NSerializer.Exceptions;


namespace NSerializer.XML.Readers.Values
{
    internal class NodeAttributesReader
    {
        private readonly Dictionary<string, string> attributes = new Dictionary<string, string>();

        public void Read(NXmlElementReader reader)
        {
        }

        public void Read(XmlStreamReader streamReader)
        {
            var attributesText = streamReader.ReadRestOfLine().Trim();
            if (!attributesText.EndsWith(">"))
            {
                throw new NXmlReaderFormatException("Missing element closing brace on same line element is declared");
            }

            attributesText = attributesText.Trim(' ', '>', '\t');

            // >>> better to use a regex here <<<

            var attributeElements = attributesText.Split('=', '"');
            for (var index = 0; index < attributeElements.Length - 1;)
            {
                var name = attributeElements[index++].Trim(' ', '\t');
                index++;
                var value = attributeElements[index++].Trim(' ', '\t');

                attributes.Add(name, value);
            }
        }

        public string Get(string attributeName)
        {
            return attributes[attributeName];
        }

        public int GetIntegerAttribute(string attributeName)
        {
            return int.Parse(attributes[attributeName]);
        }
    }
}