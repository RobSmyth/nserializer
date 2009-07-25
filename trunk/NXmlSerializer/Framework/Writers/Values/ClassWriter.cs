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
using NSerializer.Framework.Types;
using NSerializer.XML.Document;
using NSerializer.XML.Document.Writers;


namespace NSerializer.Framework.Writers.Values
{
    public class ClassWriter : IObjectWriter
    {
        private readonly IBaseTypeMembersWriter baseTypeMembersWriter;
        private readonly IMemberWriter memberWriter;
        private readonly IDocumentWriter ownerDocument;
        private readonly ITypeNamesCache typeNamesCache;
        private readonly IValuesCache valuesCache;

        public ClassWriter(IDocumentWriter ownerDocument, IValuesCache valuesCache, IMemberWriter memberWriter,
                           IBaseTypeMembersWriter baseTypeMembersWriter, ITypeNamesCache typeNamesCache)
        {
            this.ownerDocument = ownerDocument;
            this.valuesCache = valuesCache;
            this.memberWriter = memberWriter;
            this.baseTypeMembersWriter = baseTypeMembersWriter;
            this.typeNamesCache = typeNamesCache;
        }

        public bool CanWrite(object instance, Type referencedAsType)
        {
            return !valuesCache.Contains(instance);
        }

        public void Write(object instance, INodeWriter parentNode, Type referencedAsType)
        {
            var objectID = valuesCache.Add(instance);
            var typeName = typeNamesCache.GetTypeName(instance.GetType());

            using (var classNode = ownerDocument.CreateClassElement(typeName, objectID, parentNode))
            {
                using (var membersNode = ownerDocument.CreateMembersElement(classNode))
                {
                    memberWriter.Write(instance, membersNode, instance.GetType());
                }

                baseTypeMembersWriter.WriteMembers(instance, classNode, instance.GetType());
            }
        }
    }
}