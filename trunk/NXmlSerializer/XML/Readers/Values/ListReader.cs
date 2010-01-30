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
using NSerializer.Types;
using NSerializer.XML.Readers.Members;


namespace NSerializer.XML.Readers.Values
{
    internal class ListReader : IObjectReader, IBaseTypeMembersReader
    {
        private readonly IInstanceRepository docObjectRepository;
        private readonly IObjectReader objectReader;
        private readonly IReadObjectsCache readObjects;
        private readonly ITypeFinder typeFinder;

        public ListReader(IReadObjectsCache readObjects, IInstanceRepository docObjectRepository,
                          IObjectReader objectReader, ITypeFinder typeFinder)
        {
            this.readObjects = readObjects;
            this.docObjectRepository = docObjectRepository;
            this.objectReader = objectReader;
            this.typeFinder = typeFinder;
        }

        public bool CanRead(INXmlElementReader nodeReader)
        {
            return nodeReader.Name == "list";
        }

        public object Get(INXmlElementReader nodeReader)
        {
            var typeName = nodeReader.Attributes.Get("type");
            var type = typeFinder.Get(typeName);

            var typeAccessor = new TypeAccessor(type, docObjectRepository);
            var instance = typeAccessor.GetInstance();
            readObjects.Add(nodeReader.Attributes.GetInteger("ID"), instance);

            ((IBaseTypeMembersReader) this).ReadMembers(instance, nodeReader, new DestinationType(type));

            return instance;
        }

        void IBaseTypeMembersReader.ReadMembers(object instance, INXmlElementReader nodeReader, DestinationType type)
        {
            var list = (IList) instance;

            using (var itemNodes = nodeReader.GetNextChildNode("items"))
            {
                var numberOfItems = itemNodes.Attributes.GetInteger("count");

                while (numberOfItems-- > 0)
                {
                    INXmlElementReader itemNode;
                    using (itemNode = itemNodes.GetNextChildNode())
                    {
                        list.Add(objectReader.Get(itemNode));
                    }
                }
            }
        }
    }
}