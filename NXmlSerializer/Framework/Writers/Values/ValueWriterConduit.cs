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
using NSerializer.XML.Document.Writers;


namespace NSerializer.Framework.Writers.Values
{
    public class ValueWriterConduit : IObjectWriter, IBaseTypeMembersWriter
    {
        private IBaseTypeMembersWriter baseTypeMemberWriter;
        private IObjectWriter objectWriter;

        public void WriteMembers(object instance, ISerialiserNode parentNode, Type type)
        {
            baseTypeMemberWriter.WriteMembers(instance, parentNode, type);
        }

        public bool CanWriteMembers(object instance, Type type)
        {
            return baseTypeMemberWriter.CanWriteMembers(instance, type);
        }

        public bool CanWrite(object instance, Type referencedAsType)
        {
            return objectWriter.CanWrite(instance, referencedAsType);
        }

        public void Write(object instance, INodeWriter parentNode, Type referencedAsType)
        {
            objectWriter.Write(instance, parentNode, referencedAsType);
        }

        public void SetTarget(IObjectWriter targetObjectWriter, IBaseTypeMembersWriter baseMemberWriter)
        {
            objectWriter = targetObjectWriter;
            baseTypeMemberWriter = baseMemberWriter;
        }
    }
}