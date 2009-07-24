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
using NSerializer.XML.Document;
using NSerializer.XML.Document.Writers;


namespace NSerializer.Framework.Writers.Values
{
    public class ObjectReferenceWriter : IObjectWriter
    {
        private readonly IDocumentWriter ownerDocument;
        private readonly IValuesCache valuesCache;

        public ObjectReferenceWriter(IDocumentWriter ownerDocument, IValuesCache valuesCache)
        {
            this.ownerDocument = ownerDocument;
            this.valuesCache = valuesCache;
        }

        public bool CanWrite(object target, Type referencedAsType)
        {
            return valuesCache.Contains(target);
        }

        public void Write(object instance, INodeWriter parentNode, Type referencedAsType)
        {
            using (var referenceNode = ownerDocument.CreateObjectRefernceElement(parentNode))
            {
                referenceNode.AddAttribute("ID", valuesCache.GetID(instance));
            }
        }
    }
}