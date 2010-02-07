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
using NSerializer.Framework.Document;
using NSerializer.Framework.Readers;


namespace NSerializer.XML.Readers
{
    public class PayloadReader
    {
        private readonly IObjectReader objectReader;

        public PayloadReader(IObjectReader objectReader)
        {
            this.objectReader = objectReader;
        }

        public Payload Read(XmlStreamReader inputStream)
        {
            Payload payload;

            inputStream.Seek(0, SeekOrigin.Begin);
            if (inputStream.Seek("<c type=\"!0\""))
            {
                using (var nodeReader = new XmlElementReaderFactory(inputStream).Create())
                {
                    payload = (Payload) objectReader.Get(nodeReader);
                }
            }
            else
            {
                throw new UnableToReadXMLTextException("Missing payload.");
            }

            return payload;
        }
    }
}