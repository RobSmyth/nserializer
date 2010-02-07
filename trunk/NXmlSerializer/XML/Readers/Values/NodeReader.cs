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
using NSerializer.Framework;
using NSerializer.Framework.Readers;
using NSerializer.Framework.Types;
using NSerializer.XML.Readers.Members;


namespace NSerializer.XML.Readers.Values
{
    public class NodeReader : IObjectReader, IBaseTypeMembersReader
    {
        private readonly IBaseTypeMembersReader[] baseTypeReaders;
        private readonly IObjectReader[] objectReaders;

        public NodeReader(ITypeFinder typeFinder, IDocumentObjectsRepository docObjectRepository,
                          IReadObjectsCache readObjects, IMemberReader memberReader,
                          params IObjectReader[] objectReaders)
        {
            this.objectReaders = objectReaders;

            var baseTypeReadersList = new List<IBaseTypeMembersReader>
                                          {
                                              new ListReader(readObjects, docObjectRepository, this, typeFinder),
                                              new DictionaryReader(readObjects, docObjectRepository, this, typeFinder),
                                              new ClassReader(readObjects, memberReader, typeFinder, docObjectRepository,
                                                              this, this)
                                          };
            baseTypeReaders = baseTypeReadersList.ToArray();
        }

        public bool CanRead(INXmlElementReader nodeReader)
        {
            var canRead = false;
            foreach (var reader in objectReaders)
            {
                canRead = reader.CanRead(nodeReader);
                if (canRead)
                {
                    break;
                }
            }
            return canRead;
        }

        public object Get(INXmlElementReader nodeReader)
        {
            object readObject = null;
            foreach (var reader in objectReaders)
            {
                if (reader.CanRead(nodeReader))
                {
                    readObject = reader.Get(nodeReader);
                    break;
                }
            }
            return readObject;
        }

        void IBaseTypeMembersReader.ReadMembers(object instance, INXmlElementReader nodeReader, IDataType type)
        {
            foreach (var reader in baseTypeReaders)
            {
                if (reader.CanRead(nodeReader))
                {
                    reader.ReadMembers(instance, nodeReader, type);
                    break;
                }
            }
        }
    }
}