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
using NSerializer.Exceptions;
using NSerializer.Framework;
using NSerializer.Framework.Readers;
using NSerializer.Types;


namespace NSerializer.XML.Readers.Values
{
    public class ClassReader : IObjectReader, IBaseTypeMembersReader
    {
        private readonly IBaseTypeMembersReader baseClassMembersReader;
        private readonly IDocumentObjectsRepository docObjectRepository;
        private readonly IMemberReader memberReader;
        private readonly IObjectReader objectReader;
        private readonly IReadObjectsCache readObjects;
        private readonly ITypeFinder typeFinder;

        public ClassReader(IReadObjectsCache readObjects, IMemberReader memberReader, ITypeFinder typeFinder,
                           IDocumentObjectsRepository docObjectRepository, IBaseTypeMembersReader baseClassMembersReader,
                           IObjectReader objectReader)
        {
            this.readObjects = readObjects;
            this.memberReader = memberReader;
            this.typeFinder = typeFinder;
            this.docObjectRepository = docObjectRepository;
            this.baseClassMembersReader = baseClassMembersReader;
            this.objectReader = objectReader;
        }

        public bool CanRead(INXmlElementReader nodeReader)
        {
            return nodeReader.Name == "c";
        }

        public object Get(INXmlElementReader nodeReader)
        {
            var typeName = nodeReader.Attributes.Get("type");
            var type = typeFinder.Get(typeName);

            var typeAccessor = new TypeAccessor(type, docObjectRepository);
            var instance = typeAccessor.GetInstance();

            readObjects.Add(nodeReader.Attributes.GetInteger("ID"), instance);

            ReadMembers(instance, nodeReader, type);

            return instance;
        }

        public void ReadMembers(object instance, INXmlElementReader nodeReader, Type type)
        {
            using (var membersNodeReader = nodeReader.GetNextChildNode("members"))
            {
                if (membersNodeReader == null)
                {
                    throw new NXmlReaderFormatException("Missing class members node.");
                }

                ReadFields(instance, membersNodeReader, type);
            }

            var baseNodeReader = nodeReader.GetNextChildNode();
            if (baseNodeReader != null)
            {
                if (baseNodeReader.Name != "base")
                {
                    throw new NXmlReaderFormatException(
                        string.Format("Found a {0} node when expecting a base node.", baseNodeReader.Name));
                }

                using (var baseClassTypeNode = baseNodeReader.GetNextChildNode())
                {
                    baseClassMembersReader.ReadMembers(instance, baseClassTypeNode, type.BaseType);
                }

                baseNodeReader.Dispose();
            }
        }

        private void ReadFields(object instance, INXmlElementReader membersNodeReader, Type type)
        {
            INXmlElementReader fieldReader;
            while ((fieldReader = membersNodeReader.GetNextChildNode("f")) != null)
            {
                if (fieldReader.Name != "f")
                {
                    throw new NXmlReaderFormatException(
                        string.Format("Invalid node '{0}' found when expecting a field node", fieldReader.Name));
                }

                try
                {
                    var members = memberReader.Read(fieldReader, type);
                    members.SetValue(instance);
                }
                catch (Exception exception)
                {
                    var memberName = fieldReader.Attributes.Get("name");
                    throw new NSerializerException(
                        string.Format("Error reading member '{0}.{1}", type.FullName, memberName),
                        exception);
                }

                fieldReader.Dispose();
            }
        }
    }
}