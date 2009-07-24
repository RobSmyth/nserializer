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
using System.IO;
using System.Xml;
using NSerializer.Framework.Document;


namespace NSerializer.XML.Document.Writers
{
    public class NXmlDocumentWriter : IDocumentWriter
    {
        private const string rootNodeName = "NSerializer";
        private readonly XmlDocument document = new XmlDocument();
        private readonly Version version = new Version(4, 0);
        private TextWriter streamWriter;
        public INodeWriter RootNode { get; private set; }

        public void EndWrite()
        {
            RootNode.Dispose();
        }

        public void BeginWrite(TextWriter writer)
        {
            streamWriter = writer;
            Initialize();
        }

        public INodeWriter CreateElement(string name, ISerialiserNode parentNode)
        {
            var newbornElement = document.CreateElement(name);
            parentNode.AppendChild(name);

            var newborn = new NXmlNodeWriter(parentNode.Depth + 1, name);

            newborn.BeginWrite(streamWriter);
            return newborn;
        }

        public INodeWriter CreateObjectRefernceElement(ISerialiserNode parentNode)
        {
            return CreateElement("objref", parentNode);
        }

        public INodeWriter CreateClassElement(string typeName, int objectID, ISerialiserNode parentNode)
        {
            var classNode = CreateBaseClassElement(typeName, parentNode);
            classNode.AddAttribute("ID", objectID.ToString());
            return classNode;
        }

        public INodeWriter CreateAppObjectElement(string typeName, int objectID, ISerialiserNode parentNode)
        {
            var classNode = CreateTypedElement("appObject", typeName, parentNode);
            classNode.AddAttribute("ID", objectID.ToString());
            return classNode;
        }

        public INodeWriter CreateBaseClassElement(string typeName, ISerialiserNode parentNode)
        {
            return CreateTypedElement("c", typeName, parentNode);
        }

        public INodeWriter CreateNullValueElement(ISerialiserNode parentNode)
        {
            return CreateElement("null", parentNode);
        }

        public INodeWriter CreateListElement(string typeName, int valueID, ISerialiserNode parentNode)
        {
            var node = CreateListElement(typeName, parentNode);
            node.AddAttribute("ID", valueID.ToString());
            return node;
        }

        public INodeWriter CreateListElement(string typeName, ISerialiserNode parentNode)
        {
            return CreateTypedElement("list", typeName, parentNode);
        }

        public INodeWriter CreateValueElement(string typeName, ISerialiserNode parentNode)
        {
            return CreateTypedElement("value", typeName, parentNode);
        }

        public INodeWriter CreateArrayElement(string typeName, int valueID, ISerialiserNode parentNode)
        {
            var node = CreateTypedElement("array", typeName, parentNode);
            node.AddAttribute("ID", valueID.ToString());
            return node;
        }

        public INodeWriter CreateMembersElement(ISerialiserNode parentNode)
        {
            return CreateElement("members", parentNode);
        }

        public INodeWriter CreateItemsElement(INodeWriter parentNode)
        {
            return CreateElement("items", parentNode);
        }

        public INodeWriter CreateTypedElement(string name, string typeName, ISerialiserNode parentNode)
        {
            return new NXmlTypedElementWriter(CreateElement(name, parentNode), typeName, streamWriter,
                                              parentNode.Depth + 1);
        }

        [Obsolete]
        public XmlAttribute CreateAttribute(string name)
        {
            return null;
        }

        private void Initialize()
        {
            RootNode = new NXmlNodeWriter(0, rootNodeName);
            if (streamWriter != null)
            {
                RootNode.BeginWrite(streamWriter);
            }
            RootNode.AddAttribute("version", version.ToString());
        }
    }
}