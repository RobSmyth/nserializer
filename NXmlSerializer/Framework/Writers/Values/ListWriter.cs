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
using NSerializer.Framework.Document;
using NSerializer.Framework.Types;
using NSerializer.XML.Document;
using NSerializer.XML.Document.Writers;


namespace NSerializer.Framework.Writers.Values
{
    public class ListWriter : IObjectWriter, IBaseTypeMembersWriter
    {
        private readonly Type listType;
        private readonly IObjectWriter objectWriter;
        private readonly IDocumentWriter ownerDocument;
        private readonly ITypeNamesCache typeNamesCache;
        private readonly IValuesCache valuesCache;

        public ListWriter(Type listType, IDocumentWriter ownerDocument, IValuesCache valuesCache,
                          IObjectWriter objectWriter, ITypeNamesCache typeNamesCache)
        {
            this.listType = listType;
            this.ownerDocument = ownerDocument;
            this.valuesCache = valuesCache;
            this.objectWriter = objectWriter;
            this.typeNamesCache = typeNamesCache;
        }

        public bool CanWriteMembers(object instance, Type type)
        {
            return CanWrite(instance, type);
        }

        public void WriteMembers(object instance, ISerialiserNode parentNode, Type type)
        {
            var typeName = typeNamesCache.GetTypeName(type);
            using (var listNode = ownerDocument.CreateListElement(typeName, parentNode))
            {
                WriteItems(listNode, (IList) instance);
            }
        }

        public bool CanWrite(object instance, Type referencedAsType)
        {
            return referencedAsType.Name == listType.Name;
        }

        public void Write(object instance, INodeWriter parentNode, Type type)
        {
            var valueID = valuesCache.Add(instance);
            var typeName = typeNamesCache.GetTypeName(type);

            using (var listNode = ownerDocument.CreateListElement(typeName,
                                                                  valueID, parentNode))
            {
                WriteItems(listNode, (IList) instance);
            }
        }

        private void WriteItems(INodeWriter listNode, IList items)
        {
            using (var itemsNode = ownerDocument.CreateItemsElement(listNode))
            {
                itemsNode.AddAttribute("count", items.Count);

                foreach (var item in items)
                {
                    objectWriter.Write(item, itemsNode, item.GetType());
                }
            }
        }
    }
}