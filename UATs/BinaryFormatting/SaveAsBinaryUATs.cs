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
using System.Diagnostics;
using NSerializer.XML.Document;
using NSerializer.XML.Document.Writers;
using NUnit.Framework;


namespace NSerializer.UATs.BinaryFormatting
{
    [TestFixture]
    [Ignore("Performance test - spike")]
    public class SaveAsBinaryUATs :  SerializeContext
    {
        [Test]
        public void MemoryUsageSpike_Binary_WriteOnly()
        {
            var source = GetSource();

            IDocumentWriter document = new NXmlDocumentWriter();
            var xmlWriter = new NXmlWriter(document, null, null);
            xmlWriter.Write(source, null);//>>>
        }

        [Test]
        public void MemoryUsageSpike_Xml()
        {
            var source = GetSource();

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var xml = SerializeAsXml(source);

            stopwatch.Stop();

            Console.WriteLine("XML    {0} bytes in {1} seconds", xml.Length, stopwatch.Elapsed.Seconds);
        }

        private B[] GetSource()
        {
            var source = new B[2/*00*/];
            for (var index = 0; index < source.Length; index++)
            {
                source[index] = new B(new C[2/*10*/]);
            }
            return source;
        }

        private class A
        {}

        private class B
        {
            private C[] c;
    
            public B(C[] c)
            {
                this.c = c;
                for (var index = 0; index < c.Length; index++)
                {
                    c[index] = new C();
                }
            }

            public C[] C
            {
                get { return c; }
            }
        }

        private class C
        {
            private UInt32[] data;

            public C()
            {
                Data = new UInt32[5/*00*/];
                for (var index = 0; index < Data.Length; index++)
                {
                    Data[index] = 1000;
                }
            }

            public UInt32[] Data
            {
                get { return data; }
                set { data = value; }
            }
        }
    }
}