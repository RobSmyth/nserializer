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
using NSerializer.Framework;
using NSerializer.Framework.Document;
using NSerializer.Framework.Readers;
using NSerializer.TypeFinders;
using NSerializer.XML.Readers.Values;


namespace NSerializer.XML.Readers
{
    public class PayloadReader
    {
        private readonly IApplicationObjectsRepository appObjectRepository;
        private readonly IDocumentObjectsRepository docObjectRepository;
        private readonly ITypeFinder typeFinder;

        public PayloadReader(ITypeFinder typeFinder,
                             IApplicationObjectsRepository appObjectRepository,
                             IDocumentObjectsRepository docObjectRepository)
        {
            this.typeFinder = typeFinder;
            this.appObjectRepository = appObjectRepository;
            this.docObjectRepository = docObjectRepository;
        }

        public Payload Read(XmlStreamReader inputStream)
        {
            Payload payload;

            inputStream.Seek(0, SeekOrigin.Begin);
            if (inputStream.Seek("<c type=\"_0\""))
            {
                using (var nodeReader = new XmlElementReaderFactory(inputStream).Create())
                {
                    IObjectReader objectReader =
                        new DefaultValueReader(typeFinder,
                                               appObjectRepository,
                                               docObjectRepository);

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