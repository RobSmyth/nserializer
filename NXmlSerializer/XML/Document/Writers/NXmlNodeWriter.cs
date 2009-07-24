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
using NSerializer.Framework.Document;


namespace NSerializer.XML.Document.Writers
{
    public class NXmlNodeWriter : INodeWriter
    {
        private readonly List<ISerialiserNode> childNodes = new List<ISerialiserNode>();
        private readonly int depth;
        private readonly string name;
        private bool attributesMode;
        private TextWriter streamWriter;

        public NXmlNodeWriter(int depth, string name)
        {
            this.depth = depth;
            this.name = name;
        }

        public string Name
        {
            get { return name; }
        }

        public string InnerText
        {
            get { throw new InvalidOperationException(); }
            set
            {
                NotInHeader();
                streamWriter.Write(string.Format("\t{0}", value));
            }
        }

        public int Depth
        {
            get { return depth; }
        }

        public void BeginWrite(TextWriter writer)
        {
            streamWriter = writer;
            attributesMode = true;
            if (streamWriter != null)
            {
                WriteIndent();
                streamWriter.Write(string.Format("<{0}", name));
            }
        }

        public void AddAttribute(string attributeName, int value)
        {
            AddAttribute(attributeName, value.ToString());
        }

        public void AddAttribute(string attributeName, Type value)
        {
            AddAttribute(attributeName, value.Name);
        }

        public void AddAttribute(string attributeName, string value)
        {
            if (streamWriter != null)
            {
                streamWriter.Write(string.Format(" {0}=\"{1}\"", attributeName, value));
            }
        }

        public void AppendChild(string nodeName)
        {
            NotInHeader();
            childNodes.Add(new NXmlNodeWriter(depth + 1, nodeName));
        }

        public void Dispose()
        {
            if (attributesMode && streamWriter != null)
            {
                attributesMode = false;
                streamWriter.WriteLine(" />");
            }
            else
            {
                WriteIndent();
                streamWriter.WriteLine(string.Format("</{0}>", name));
            }
            streamWriter = null;
        }

        private void NotInHeader()
        {
            if (attributesMode && streamWriter != null)
            {
                attributesMode = false;
                streamWriter.WriteLine(">");
            }
        }

        private void WriteIndent()
        {
            if (streamWriter != null)
            {
                for (var count = 0; count < depth; count++)
                {
                    streamWriter.Write("\t");
                }
            }
        }
    }
}