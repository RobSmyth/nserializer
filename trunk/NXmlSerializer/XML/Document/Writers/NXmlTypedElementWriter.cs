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
using NSerializer.XML.Document.Writers;


namespace NSerializer.XML.Document
{
    internal class NXmlTypedElementWriter : INodeWriter
    {
        private readonly int depth;
        private readonly INodeWriter node;

        public NXmlTypedElementWriter(INodeWriter node, string typeName, TextWriter streamWriter, int depth)
        {
            this.node = node;
            this.depth = depth;
            node.AddAttribute("type", typeName);
        }

        public string Name
        {
            get { return node.Name; }
        }

        public string InnerText
        {
            set { node.InnerText = value; }
        }

        public int Depth
        {
            get { return depth; }
        }

        public void AddAttribute(string attributeName, int value)
        {
            node.AddAttribute(attributeName, value);
        }

        public void AddAttribute(string name, Type value)
        {
            node.AddAttribute(name, value);
        }

        public void AddAttribute(string name, string value)
        {
            node.AddAttribute(name, value);
        }

        public void AppendChild(string name)
        {
            node.AppendChild(name);
        }

        public void BeginWrite(TextWriter streamWriter)
        {
            node.BeginWrite(streamWriter);
        }

        public void Dispose()
        {
            node.Dispose();
        }
    }
}