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
using System.Text;
using NSerializer.Framework;
using NSerializer.Framework.Writers;
using NSerializer.TypeFinders;
using NSerializer.XML.Document;
using NSerializer.XML.Document.Writers;


namespace NSerializer.XML.Writers.Values
{
    public class ArrayOfDoublesXmlWriter : IObjectWriter
    {
        private readonly IDocumentWriter ownerDocument;
        private readonly ITypeNamesCache typeNamesCache;
        private readonly IValuesCache valuesCache;

        public ArrayOfDoublesXmlWriter(IDocumentWriter ownerDocument, IValuesCache valuesCache,
                                       ITypeNamesCache typeNamesCache)
        {
            this.ownerDocument = ownerDocument;
            this.valuesCache = valuesCache;
            this.typeNamesCache = typeNamesCache;
        }

        public bool CanWrite(object instance, Type referencedAsType)
        {
            return instance.GetType().IsArray && instance.GetType().GetElementType() == typeof (double);
        }

        public void Write(object instance, INodeWriter parentNode, Type referencedAsType)
        {
            var valueID = valuesCache.Add(instance);
            var typeName = typeNamesCache.GetTypeName(instance.GetType());

            using (var arrayNode = ownerDocument.CreateTypedElement("arrayOfDoubles",
                                                             typeName, parentNode))
            {
                arrayNode.AddAttribute("ID", valueID.ToString());

                using (var itemsNode = ownerDocument.CreateElement("items", arrayNode))
                {
                    var values = (Array) instance;
                    itemsNode.AddAttribute("count", values.Length);

                    var innerText = new StringBuilder();
                    var itemsPerLineCount = 0;
                    foreach (var listItem in values)
                    {
                        innerText.AppendFormat("{0},", listItem);
                        if (++itemsPerLineCount == 10)
                        {
                            itemsPerLineCount = 0;
                            innerText.AppendFormat("\r\n");
                        }
                    }

                    itemsNode.InnerText = innerText.ToString();
                }
            }
        }
    }
}