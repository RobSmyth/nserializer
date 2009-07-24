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
    public class NXmlElementReader : INXmlElementReader
    {
        private readonly XmlStreamReader inputStream;

        public NXmlElementReader(XmlStreamReader inputStream, NXmlNodeAttributes attributes, string name)
        {
            this.inputStream = inputStream;
            Attributes = attributes;
            Name = name;
        }

        public string Name { get; private set; }

        public NXmlNodeAttributes Attributes { get; private set; }

        public INXmlElementReader GetNextChildNode()
        {
            INXmlElementReader reader = null;

            if (inputStream.Seek('<', '>'))
            {
                inputStream.Seek(1, SeekOrigin.Current);
                var closingElement = inputStream.Peek() == '/';
                inputStream.Seek(-1, SeekOrigin.Current);

                if (!closingElement)
                {
                    reader = new XmlElementReaderFactory(inputStream).Create();
                }
            }

            return reader;
        }

        public string GetPayload()
        {
            return inputStream.ReadInnerText();
        }

        public INXmlElementReader GetNextChildNode(string name)
        {
            var reader = GetNextChildNode();

            if (reader != null && reader.Name != name)
            {
                throw new NXmlReaderFormatException(
                    string.Format("Found node '{0}' when expect node '{1}.", reader.Name, name));
            }

            return reader;
        }

        public void Dispose()
        {
            var closingElement = string.Format("</{0}>", Name);
            inputStream.Seek(closingElement);
            inputStream.Seek(closingElement.Length, SeekOrigin.Current);
        }
    }
}