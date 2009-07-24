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
using System.Xml;
using NSerializer.Framework.Document;


namespace NSerializer.XML.Document
{
    public class NXmlTypedElement : INXmlNode
    {
        private readonly ISerialiserNode node;

        public NXmlTypedElement(ISerialiserNode node, string typeName)
        {
            this.node = node;
            node.AddAttribute("type", typeName);
        }

        public string Name
        {
            get { return node.Name; }
        }

        public IAttributeCollection Attributes
        {
            get { return node.Attributes; }
        }

        public string InnerText
        {
            get { return node.InnerText; }
            set { node.InnerText = value; }
        }

        public XmlNode Element
        {
            get { return ((INXmlNode) node).Element; }
        }

        public ISerialiserNode[] ChildNodes
        {
            get { return node.ChildNodes; }
        }

        public ISerialiserNode AppendChild(string name)
        {
            return node.AppendChild(name);
        }

        public void AppendChild(ISerialiserNode childNode)
        {
            node.AppendChild(childNode);
        }

        public void AppendToDocument(ISerialiserDocument document)
        {
            node.AppendToDocument(document);
        }

        public void AddAttribute(string name, int value)
        {
            node.AddAttribute(name, value);
        }

        public void AddAttribute(string name, Type value)
        {
            node.AddAttribute(name, value);
        }

        public void AddAttribute(string name, string value)
        {
            node.AddAttribute(name, value);
        }

        public string GetAttribute(string name)
        {
            return node.GetAttribute(name);
        }

        public ISerialiserNode SelectSingleNode(string xpath)
        {
            return node.SelectSingleNode(xpath);
        }

        public ISerialiserNode[] SelectNodes(string xpath)
        {
            return node.SelectNodes(xpath);
        }

        public int GetIntegerAttribute(string name)
        {
            return node.GetIntegerAttribute(name);
        }
    }
}