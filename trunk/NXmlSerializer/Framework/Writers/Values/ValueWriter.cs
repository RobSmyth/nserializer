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
using NSerializer.XML.Document;
using NSerializer.XML.Document.Writers;


namespace NSerializer.Framework.Writers.Values
{
    public class ValueWriter : IObjectWriter, IBaseTypeMembersWriter
    {
        private readonly IBaseTypeMembersWriter[] baseTypeMembersWriters;
        private readonly IDocumentWriter ownerDocument;
        private readonly IObjectWriter[] writers;

        public ValueWriter(IDocumentWriter ownerDocument, IObjectWriter[] objectWriters,
                           IBaseTypeMembersWriter[] baseTypeMembersWriters)
        {
            this.ownerDocument = ownerDocument;
            writers = objectWriters;
            this.baseTypeMembersWriters = baseTypeMembersWriters;
        }

        public bool CanWriteMembers(object instance, Type type)
        {
            var canWrite = false;
            type = type.BaseType;
            if (TypeIsSerializable(type))
            {
                foreach (var baseTypeMembersWriter in baseTypeMembersWriters)
                {
                    if (baseTypeMembersWriter.CanWriteMembers(instance, type))
                    {
                        canWrite = true;
                        break;
                    }
                }
            }
            return canWrite;
        }

        public void WriteMembers(object instance, ISerialiserNode parentNode, Type type)
        {
            type = type.BaseType;
            if (TypeIsSerializable(type))
            {
                foreach (var baseTypeMembersWriter in baseTypeMembersWriters)
                {
                    if (baseTypeMembersWriter.CanWriteMembers(instance, type))
                    {
                        using (var baseNode = ownerDocument.CreateElement("base", parentNode))
                        {
                            baseTypeMembersWriter.WriteMembers(instance, baseNode, type);
                        }
                        break;
                    }
                }
            }
        }

        public bool CanWrite(object instance, Type referencedAsType)
        {
            var canWrite = false;
            foreach (var xmlWriter in writers)
            {
                if (xmlWriter.CanWrite(instance, referencedAsType))
                {
                    canWrite = true;
                    break;
                }
            }
            return canWrite;
        }

        public void Write(object instance, INodeWriter parentNode, Type referencedAsType)
        {
            foreach (var writer in writers)
            {
                if (writer.CanWrite(instance, referencedAsType))
                {
                    writer.Write(instance, parentNode, referencedAsType);
                    break;
                }
            }
        }

        private static bool TypeIsSerializable(Type type)
        {
            return type != null && type != typeof (object);
        }
    }
}