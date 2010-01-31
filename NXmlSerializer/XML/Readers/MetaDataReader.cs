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

using NSerializer.Exceptions;
using NSerializer.Framework;
using NSerializer.Framework.Document;
using NSerializer.Framework.Readers;
using NSerializer.Framework.Types;
using NSerializer.XML.Readers.Values;
using NSerializer.XML.Readers.Members;


namespace NSerializer.XML.Readers
{
    public class MetaDataReader
    {
        private readonly ITypeFinder typeFinder;
        private readonly IDataTypeFactory dataTypeFactory;

        public MetaDataReader(ITypeFinder typeFinder, IDataTypeFactory dataTypeFactory)
        {
            this.typeFinder = typeFinder;
            this.dataTypeFactory = dataTypeFactory;
        }

        public MetaData Read(XmlStreamReader inputStream)
        {
            MetaData metaData;

            if (inputStream.SeekFromEnd(string.Format("<c type=\"{0}\"", typeof (MetaData).FullName)))
            {
                using (var nodeReader = new XmlElementReaderFactory(inputStream).Create())
                {
                    IObjectReader objectReader =
                        new DefaultValueReader(typeFinder,
                                               new NullApplicationObjectRepository(),
                                               new NullDocumentObjectRepository(), dataTypeFactory);

                    metaData = (MetaData) objectReader.Get(nodeReader);
                }
            }
            else
            {
                throw new UnableToReadXMLTextException("Missing metadata.");
            }

            return metaData;
        }
    }
}