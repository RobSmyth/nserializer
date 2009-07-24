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
using System.IO;
using System.Xml;
using NSerializer.BinaryFormatting;
using NSerializer.Exceptions;
using NSerializer.Framework.Document;


namespace NSerializer.XML.Document
{
    public class NXmlDocument : ISerialiserDocument
    {
        private const string rootNodeName = "NSerializer";
        private readonly XmlDocument document = new XmlDocument();
        private readonly ISerialiserNode rootNode;
        private readonly Version version = new Version(4, 0);

        public NXmlDocument()
        {
            document.RemoveAll();

            rootNode = new NXmlNode(this, document.CreateElement(rootNodeName));
            var attribute = document.CreateAttribute("version");
            attribute.Value = version.ToString();
            rootNode.Attributes.Append(attribute);
            AppendChild(rootNode);
        }

        public ISerialiserNode MetadataNode
        {
            get { return Nodes[1]; }
        }

        public ISerialiserNode PayloadNode
        {
            get { return Nodes[0]; }
        }

        public ISerialiserNode RootNode
        {
            get { return rootNode; }
        }

        public void SaveAsXml(TextWriter textWriter)
        {
            document.Save(textWriter);
        }

        public void SaveAsBinary(BinaryWriter binaryWriter)
        {
            var data = SaveAsBinary(rootNode);
            binaryWriter.Write(data);
        }

        private byte[] SaveAsBinary(ISerialiserNode node)
        {
            return new BinaryNode(node).GetBytes();
        }


        public void AppendChild(XmlNode element)
        {
            document.AppendChild(element);
        }

        public ISerialiserNode CreateElement(string name)
        {
            return new NXmlNode(this, document.CreateElement(name));
        }

        public ISerialiserNode CreateObjectRefernceElement()
        {
            return new NXmlNode(this, document.CreateElement("objref"));
        }

        public ISerialiserNode CreateClassElement(string typeName, int objectID)
        {
            var classNode = CreateBaseClassElement(typeName);
            classNode.AddAttribute("ID", objectID.ToString());
            return classNode;
        }

        public ISerialiserNode CreateAppObjectElement(string typeName, int objectID)
        {
            var classNode = CreateTypedElement("appObject", typeName);
            classNode.AddAttribute("ID", objectID.ToString());
            return classNode;
        }

        public ISerialiserNode CreateBaseClassElement(string typeName)
        {
            return CreateTypedElement("c", typeName);
        }

        public ISerialiserNode CreateNullValueElement()
        {
            return CreateElement("null");
        }

        public ISerialiserNode CreateListElement(string typeName, int valueID)
        {
            var node = CreateListElement(typeName);
            node.AddAttribute("ID", valueID.ToString());
            return node;
        }

        public ISerialiserNode CreateListElement(string typeName)
        {
            return CreateTypedElement("list", typeName);
        }

        public ISerialiserNode CreateValueElement(string typeName)
        {
            return CreateTypedElement("value", typeName);
        }

        public ISerialiserNode CreateArrayElement(string typeName, int valueID)
        {
            var node = CreateTypedElement("array", typeName);
            node.AddAttribute("ID", valueID.ToString());
            return node;
        }

        public ISerialiserNode CreateMembersElement()
        {
            return CreateElement("members");
        }

        public ISerialiserNode CreateItemsElement()
        {
            return CreateElement("items");
        }

        public ISerialiserNode CreateTypedElement(string name, string typeName)
        {
            return new NXmlTypedElement(CreateElement(name), typeName);
        }

        public XmlAttribute CreateAttribute(string name)
        {
            return document.CreateAttribute(name);
        }

        public void Load(XmlReader xmlReader)
        {
            try
            {
                document.Load(xmlReader);
            }
            catch (XmlException)
            {
                throw;
            }
            catch (Exception exception)
            {
                throw new UnableToReadXMLStreamException("Error loading XML.", exception);
            }
        }

        private ISerialiserNode[] Nodes
        {
            get
            {
                var xmlNodesList = document.SelectNodes(string.Format("{0}/*", rootNode.Name));
                var nxmlNodes = new List<ISerialiserNode>();
                foreach (XmlNode xmlNode in xmlNodesList)
                {
                    nxmlNodes.Add(new NXmlNode(this, xmlNode));
                }
                return nxmlNodes.ToArray();
            }
        }

        private void AppendChild(ISerialiserNode node)
        {
            node.AppendToDocument(this);
        }
    }
}