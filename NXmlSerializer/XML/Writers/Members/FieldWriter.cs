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

using System.Reflection;
using NSerializer.Framework;
using NSerializer.Framework.Document;
using NSerializer.Framework.Writers;
using NSerializer.XML.Document;


namespace NSerializer.XML.Writers.Members
{
    public class FieldWriter : IValueMemberWriter
    {
        private readonly IObjectWriter objectWriter;
        private readonly IDocumentWriter ownerDocument;

        public FieldWriter(IDocumentWriter ownerDocument, IObjectWriter objectWriter)
        {
            this.ownerDocument = ownerDocument;
            this.objectWriter = objectWriter;
        }

        public bool CanWrite(object owningObject, MemberInfo memberInfo)
        {
            return (memberInfo is FieldInfo) &&
                   memberInfo.GetCustomAttributes(typeof (NSerializerIgnoreAttribute), false).Length != 1;
        }

        public void Write(object owningObject, ISerialiserNode parentNode, MemberInfo memberInfo)
        {
            var fieldInfo = memberInfo as FieldInfo;
            if (fieldInfo != null)
            {
                var fieldValue = fieldInfo.GetValue(owningObject);

                using (var node = ownerDocument.CreateElement("f", parentNode))
                {
                    node.AddAttribute("name", fieldInfo.Name);

                    var type = fieldValue == null ? new NullType() : fieldInfo.FieldType;
                    objectWriter.Write(fieldValue, node, type);
                }
            }
        }
    }
}