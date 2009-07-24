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
using System.Collections.Generic;
using NSerializer.Framework;
using NSerializer.Framework.Document;
using NSerializer.Framework.Writers;
using NSerializer.XML.Document;
using NSerializer.XML.Document.Writers;


namespace NSerializer.XML.Writers.Values
{
    public class DictionaryWriter : IObjectWriter, IBaseTypeMembersWriter
    {
        private readonly IObjectWriter objectWriter;
        private readonly IDocumentWriter ownerDocument;
        private readonly ITypeNamesCache typeNamesCache;
        private readonly IValuesCache valuesCache;

        public DictionaryWriter(IDocumentWriter ownerDocument, IValuesCache valuesCache, IObjectWriter objectWriter,
                                ITypeNamesCache typeNamesCache)
        {
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

            using (var dictNode = ownerDocument.CreateTypedElement("dict", typeName, parentNode))
            {
                WriteItems(dictNode, (IDictionary) instance);
            }
        }

        public bool CanWrite(object instance, Type referencedAsType)
        {
            return referencedAsType.Name == (typeof (Dictionary<,>)).Name;
        }

        public void Write(object instance, INodeWriter parentNode, Type type)
        {
            var valueId = valuesCache.Add(instance);
            var typeName = typeNamesCache.GetTypeName(instance.GetType());

            using (var dictNode = ownerDocument.CreateTypedElement("dict", typeName,
                                                                   parentNode))
            {
                dictNode.AddAttribute("ID", valueId.ToString());

                WriteItems(dictNode, (IDictionary) instance);
            }
        }

        private void WriteItems(ISerialiserNode dictNode, IDictionary items)
        {
            using (var itemsNode = ownerDocument.CreateElement("items", dictNode))
            {
                itemsNode.AddAttribute("count", items.Count);

                foreach (DictionaryEntry item in items)
                {
                    objectWriter.Write(item.Key, itemsNode, item.Key.GetType());
                    objectWriter.Write(item.Value, itemsNode, item.Value.GetType());
                }
            }
        }
    }
}