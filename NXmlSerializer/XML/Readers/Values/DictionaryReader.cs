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
using System.Collections;
using NSerializer.Framework;
using NSerializer.Framework.Readers;
using NSerializer.Framework.Types;
using NSerializer.Types;
using NSerializer.XML.Readers.Members;


namespace NSerializer.XML.Readers.Values
{
    internal class DictionaryReader : IObjectReader, IBaseTypeMembersReader
    {
        private readonly IInstanceRepository docObjectRepository;
        private readonly IObjectReader objectReader;
        private readonly IReadObjectsCache readObjects;
        private readonly ITypeFinder typeFinder;

        public DictionaryReader(IReadObjectsCache readObjects, IInstanceRepository docObjectRepository,
                                IObjectReader objectReader, ITypeFinder typeFinder)
        {
            this.readObjects = readObjects;
            this.docObjectRepository = docObjectRepository;
            this.objectReader = objectReader;
            this.typeFinder = typeFinder;
        }

        public bool CanRead(INXmlElementReader nodeReader)
        {
            return nodeReader.Name == "dict";
        }

        public object Get(INXmlElementReader nodeReader)
        {
            var typeName = nodeReader.Attributes.Get("type");
            var type = typeFinder.Get(typeName);

            var typeAccessor = new TypeAccessor(type.GetTargetType(), docObjectRepository);
            var instance = typeAccessor.GetInstance();
            readObjects.Add(nodeReader.Attributes.GetInteger("ID"), instance);

            ((IBaseTypeMembersReader)this).ReadMembers(instance, nodeReader, type);

            return instance;
        }

        void IBaseTypeMembersReader.ReadMembers(object instance, INXmlElementReader nodeReader, IDataType type)
        {
            var dictionary = (IDictionary) instance;

            using (var itemNodesReader = nodeReader.GetNextChildNode("items"))
            {
                var numberOfItemPairs = itemNodesReader.Attributes.GetInteger("count");
                for (var itemIndex = 0; itemIndex < numberOfItemPairs; itemIndex++)
                {
                    object key;
                    object value;

                    using (var keyReader = itemNodesReader.GetNextChildNode())
                    {
                        key = objectReader.Get(keyReader);
                    }

                    using (var valueReader = itemNodesReader.GetNextChildNode())
                    {
                        value = objectReader.Get(valueReader);
                    }

                    dictionary.Add(key, value);
                }
            }
        }
    }
}