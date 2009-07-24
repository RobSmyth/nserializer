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
using NSerializer.Framework.Writers;
using NSerializer.XML.Document;
using NSerializer.XML.Document.Writers;


namespace NSerializer.XML.Writers.Values
{
    public class PrimitiveValueTypeXmlWriter : IObjectWriter
    {
        private readonly IDocumentWriter ownerDocument;

        public PrimitiveValueTypeXmlWriter(IDocumentWriter ownerDocument)
        {
            this.ownerDocument = ownerDocument;
        }

        public bool CanWrite(object instance, Type referencedAsType)
        {
            return referencedAsType.IsPrimitive || referencedAsType == typeof (string);
        }

        public void Write(object instance, INodeWriter parentNode, Type referencedAsType)
        {
            using (var valueNode = ownerDocument.CreateElement("primitive", parentNode))
            {
                valueNode.AddAttribute("type", instance.GetType().FullName);
                valueNode.InnerText = instance.ToString();
            }
        }
    }
}