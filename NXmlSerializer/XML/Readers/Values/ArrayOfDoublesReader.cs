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
using NSerializer.Types;


namespace NSerializer.XML.Readers.Values
{
    public class ArrayOfDoublesReader : IObjectReader
    {
        private readonly IReadObjectsCache readObjects;
        private readonly ITypeFinder typeFinder;

        public ArrayOfDoublesReader(IReadObjectsCache readObjects, ITypeFinder typeFinder)
        {
            this.readObjects = readObjects;
            this.typeFinder = typeFinder;
        }

        public bool CanRead(INXmlElementReader nodeReader)
        {
            return nodeReader.Name == "arrayOfDoubles";
        }

        public object Get(INXmlElementReader nodeReader)
        {
            object objectInstance;
            
            using (var itemNodesReader = nodeReader.GetNextChildNode("items"))
            {
                var innerText = itemNodesReader.GetPayload().Trim('\r', '\n', ',', '\t').Replace("\r\n", string.Empty);
                var values = innerText.Split(',');

                var valuesList = new ArrayList();
                foreach (var valueText in values)
                {
                    valuesList.Add(double.Parse(valueText));
                }

                var typeName = nodeReader.Attributes.Get("type");
                var objectType = typeFinder.Get(typeName);

                objectInstance = valuesList.ToArray(objectType.GetElementType());

                readObjects.Add(nodeReader.Attributes.GetInteger("ID"), objectInstance);
            }

            return objectInstance;
        }
    }
}