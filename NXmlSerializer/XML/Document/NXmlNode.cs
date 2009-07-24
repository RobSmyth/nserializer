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
using System.Diagnostics;
using System.Xml;
using NSerializer.Framework.Document;


namespace NSerializer.XML.Document
{
    public class NXmlNode : INXmlNode
    {
        private readonly ISerialiserDocument ownerDocument;
        private readonly XmlNode xmlElement;

        public NXmlNode(ISerialiserDocument ownerDocument, XmlNode xmlElement)
        {
            this.ownerDocument = ownerDocument;
            this.xmlElement = xmlElement;
        }

        public ISerialiserDocument OwnerDocument
        {
            get { return ownerDocument; }
        }

        public XmlNode Element
        {
            get { return xmlElement; }
        }

        public string Name
        {
            get { return xmlElement.Name; }
        }

        public IAttributeCollection Attributes
        {
            get { return new NXmlAttributeCollection(xmlElement.Attributes); }
        }

        public string InnerText
        {
            get { return xmlElement.InnerText; }
            set { xmlElement.InnerText = value; }
        }

        public ISerialiserNode[] ChildNodes
        {
            get
            {
                var childNodes = new List<ISerialiserNode>();
                foreach (XmlNode xmlNode in xmlElement.ChildNodes)
                {
                    childNodes.Add(new NXmlNode(ownerDocument, xmlNode));
                }
                return childNodes.ToArray();
            }
        }

        public ISerialiserNode AppendChild(string name)
        {
            var child = ownerDocument.CreateElement(name);
            AppendChild(child);
            return child;
        }

        public void AppendChild(ISerialiserNode node)
        {
            var child = (INXmlNode) node;
            xmlElement.AppendChild(child.Element);
        }

        public void AppendToDocument(ISerialiserDocument document)
        {
            Debug.Assert(document == ownerDocument);
            document.AppendChild(xmlElement);
        }

        public void AddAttribute(string name, int value)
        {
            AddAttribute(name, value.ToString());
        }

        public void AddAttribute(string name, Type value)
        {
            AddAttribute(name, value.Name);
        }

        public void AddAttribute(string name, string value)
        {
            var attribute = ownerDocument.CreateAttribute(name);
            attribute.Value = value;
            xmlElement.Attributes.Append(attribute);
        }

        public string GetAttribute(string name)
        {
            return xmlElement.Attributes[name].Value;
        }

        public int GetIntegerAttribute(string name)
        {
            return int.Parse(GetAttribute(name));
        }

        public ISerialiserNode[] SelectNodes(string xpath)
        {
            var selectedNodes = xmlElement.SelectNodes(xpath);

            var nxmlNodes = new List<ISerialiserNode>();
            foreach (XmlNode xmlNode in selectedNodes)
            {
                nxmlNodes.Add(new NXmlNode(ownerDocument, xmlNode));
            }

            return nxmlNodes.ToArray();
        }

        public ISerialiserNode SelectSingleNode(string xpath)
        {
            var node = xmlElement.SelectSingleNode(xpath);
            return node == null ? null : new NXmlNode(ownerDocument, node);
        }
    }
}