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
using NSerializer.Types;
using NSerializer.XML.Document;
using NSerializer.XML.Document.Writers;


namespace NSerializer.Framework.Writers.Values
{
    public class ArrayOfObjectsWriter : IObjectWriter
    {
        private readonly IObjectWriter objectWriter;
        private readonly IDocumentWriter ownerDocument;
        private readonly ITypeNamesCache typeNamesCache;
        private readonly IValuesCache valuesCache;

        public ArrayOfObjectsWriter(IDocumentWriter ownerDocument, IValuesCache valuesCache,
                                    IObjectWriter objectWriter,
                                    ITypeNamesCache typeNamesCache)
        {
            this.ownerDocument = ownerDocument;
            this.valuesCache = valuesCache;
            this.objectWriter = objectWriter;
            this.typeNamesCache = typeNamesCache;
        }

        public bool CanWrite(object instance, Type referencedAsType)
        {
            return instance.GetType().IsArray;
        }

        public void Write(object instance, INodeWriter parentNode, Type referencedAsType)
        {
            var valueID = valuesCache.Add(instance);
            var typeName = typeNamesCache.GetTypeName(instance.GetType());

            using (var arrayNode = ownerDocument.CreateArrayElement(
                typeName, valueID, parentNode))
            {
                using (var itemsNode = ownerDocument.CreateItemsElement(arrayNode))
                {
                    foreach (var listItem in (IEnumerable) instance)
                    {
                        var itemType = listItem == null ? instance.GetType().GetElementType() : listItem.GetType();
                        objectWriter.Write(listItem, itemsNode, itemType);
                    }
                }
            }
        }
    }
}