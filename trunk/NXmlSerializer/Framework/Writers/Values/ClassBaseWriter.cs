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
using NSerializer.Framework.Document;
using NSerializer.Framework.Types;
using NSerializer.XML.Document;


namespace NSerializer.Framework.Writers.Values
{
    public class ClassBaseWriter : IBaseTypeMembersWriter
    {
        private readonly IBaseTypeMembersWriter baseTypeMembersWriter;
        private readonly IMemberWriter memberWriter;
        private readonly IDocumentWriter ownerDocument;
        private readonly ITypeNamesCache typeNamesCache;

        public ClassBaseWriter(IDocumentWriter ownerDocument, IMemberWriter memberWriter,
                               IBaseTypeMembersWriter baseTypeMembersWriter, ITypeNamesCache typeNamesCache)
        {
            this.ownerDocument = ownerDocument;
            this.memberWriter = memberWriter;
            this.baseTypeMembersWriter = baseTypeMembersWriter;
            this.typeNamesCache = typeNamesCache;
        }

        public bool CanWriteMembers(object instance, Type type)
        {
            return true;
        }

        public void WriteMembers(object instance, ISerialiserNode parentNode, Type type)
        {
            var typeName = typeNamesCache.GetTypeName(type);

            using (
                var baseClassNode = ownerDocument.CreateBaseClassElement(typeName, parentNode))
            {
                using (var membersNode = ownerDocument.CreateMembersElement(baseClassNode))
                {
                    memberWriter.Write(instance, membersNode, type);
                }
                baseTypeMembersWriter.WriteMembers(instance, baseClassNode, type);
            }
        }
    }
}