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

using System.IO;
using NSerializer.Exceptions;


namespace NSerializer.XML.Readers
{
    public class XmlElementReaderFactory
    {
        private readonly XmlStreamReader inputStream;

        public XmlElementReaderFactory(XmlStreamReader inputStream)
        {
            this.inputStream = inputStream;
        }

        public INXmlElementReader Create()
        {
            inputStream.Seek(1, SeekOrigin.Current);
            var nodeName = inputStream.ReadWord();

            var line = inputStream.ReadRestOfLine().Trim();

            if (!line.EndsWith(">"))
            {
                throw new NXmlReaderFormatException("Missing element closing brace on same line element is declared");
            }

            var attributes = new NXmlNodeAttributes(line.Trim('>', '/'));

            INXmlElementReader reader;
            if (line.EndsWith("/>"))
            {
                reader = new NXmlCompactElementReader(attributes, nodeName);
            }
            else
            {
                reader = new NXmlElementReader(inputStream, attributes, nodeName);
            }
            return reader;
        }
    }
}