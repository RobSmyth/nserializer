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
using NSerializer.Framework;
using NSerializer.Framework.Readers;
using NSerializer.Framework.Readers.Values;
using NSerializer.Framework.Types;
using NSerializer.Logging;
using NSerializer.XML.Readers.Members;


namespace NSerializer.XML.Readers.Values
{
    public class NodeReader : IObjectReader, IBaseTypeMembersReader
    {
        private readonly List<IBaseTypeMembersReader> baseTypeReaders = new List<IBaseTypeMembersReader>();
        private readonly List<IObjectReader> readers = new List<IObjectReader>();

        public NodeReader(ITypeFinder typeFinder, IDocumentObjectsRepository docObjectRepository, IReadObjectsCache readObjects, IMemberReader memberReader, params IObjectReader[] objectReaders)
        {
            foreach (var objectReader in objectReaders)
            {
                readers.Add(objectReader);
            }
            readers.Add(new ClassReader(readObjects, memberReader, typeFinder, docObjectRepository, this, this));
            readers.Add(new ValueTypeReader(memberReader, typeFinder));
            readers.Add(new VersionReader());
            readers.Add(new NullReader());

            baseTypeReaders.Add(new ListReader(readObjects, docObjectRepository, this, typeFinder));
            baseTypeReaders.Add(new DictionaryReader(readObjects, docObjectRepository, this, typeFinder));
            baseTypeReaders.Add(new ClassReader(readObjects, memberReader, typeFinder, docObjectRepository, this, this));
        }

        public bool CanRead(INXmlElementReader nodeReader)
        {
            var canRead = false;
            foreach (var reader in readers)
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
            foreach (var reader in readers)
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