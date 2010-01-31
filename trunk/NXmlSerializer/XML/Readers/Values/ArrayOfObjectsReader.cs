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

using System.Collections;
using NSerializer.Framework;
using NSerializer.Framework.Readers;
using NSerializer.Framework.Types;


namespace NSerializer.XML.Readers.Values
{
    internal class ArrayOfObjectsReader : IObjectReader
    {
        private readonly IObjectReader objectReader;
        private readonly IReadObjectsCache readObjects;
        private readonly ITypeFinder typeFinder;

        public ArrayOfObjectsReader(IReadObjectsCache readObjects, IObjectReader objectReader, ITypeFinder typeFinder)
        {
            this.readObjects = readObjects;
            this.objectReader = objectReader;
            this.typeFinder = typeFinder;
        }

        public bool CanRead(INXmlElementReader nodeReader)
        {
            return nodeReader.Name == "array";
        }

        public object Get(INXmlElementReader nodeReader)
        {
            var valuesList = new ArrayList();

            using (var itemNodes = nodeReader.GetNextChildNode("items"))
            {
                INXmlElementReader itemNode;
                while ((itemNode = itemNodes.GetNextChildNode()) != null)
                {
                    if (objectReader.CanRead(itemNode))
                    {
                        valuesList.Add(objectReader.Get(itemNode));
                    }

                    itemNode.Dispose();
                }
            }

            var elementType = typeFinder.Get(nodeReader.Attributes.Get("type")).GetElementType();
            var objectInstance = valuesList.ToArray(elementType.GetTargetType());

            readObjects.Add(nodeReader.Attributes.GetInteger("ID"), objectInstance);

            return objectInstance;
        }
    }
}