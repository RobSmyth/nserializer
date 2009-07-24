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
using System.Collections.Generic;
using NSerializer.Framework;
using NSerializer.Framework.Document;
using NSerializer.Framework.Writers;
using NSerializer.XML.Document;


namespace NSerializer.XML.Writers.Members
{
    public class MemberWriter : IMemberWriter
    {
        private readonly List<IValueMemberWriter> writers;

        public MemberWriter(IDocumentWriter ownerDocument, IObjectWriter objectWriter)
        {
            writers = new List<IValueMemberWriter>();
            writers.Add(new FieldWriter(ownerDocument, objectWriter));
        }

        public void Write(object owningObject, ISerialiserNode parentNode, Type type)
        {
            var typeAccessor = new TypeAccessor(type, null);

            var memberInfos = typeAccessor.GetMembers();
            foreach (var memberInfo in memberInfos)
            {
                if (memberInfo.GetCustomAttributes(typeof (NSerializerIgnoreAttribute), false).Length != 1)
                {
                    foreach (var writer in writers)
                    {
                        if (writer.CanWrite(owningObject, memberInfo))
                        {
                            writer.Write(owningObject, parentNode, memberInfo);
                            break;
                        }
                    }
                }
            }
        }
    }
}