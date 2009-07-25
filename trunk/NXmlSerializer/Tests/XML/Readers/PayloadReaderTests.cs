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
using System.Text;
using NMock2;
using NSerializer.Framework;
using NSerializer.Framework.Document;
using NSerializer.Types;
using NSerializer.XML.Document;
using NSerializer.XML.Document.Writers;
using NSerializer.XML.Readers;
using NUnit.Framework;


namespace NSerializer.Tests.XML.Readers
{
    [TestFixture]
    public class PayloadReaderTests : MockingTestFixture
    {
        private PayloadReader reader;
        private ITypeFinder typeFinder;

        protected override void SetUp()
        {
            typeFinder = NewMock<ITypeFinder>();

            reader = new PayloadReader(typeFinder, new NullApplicationObjectRepository(),
                                       new NullDocumentObjectRepository());
        }

        [Test]
        public void Test01()
        {
            var value = new A("My name");

            var xmlResult = new StringBuilder();
            IDocumentWriter document = new NXmlDocumentWriter();
            var xmlWriter = new NXmlWriter(document, null, null);
            xmlWriter.Write(value, new StringWriter(xmlResult));

            var xml = xmlResult.ToString();

            var stream = new MemoryStream(Encoding.ASCII.GetBytes(xml));

            Stub.On(typeFinder).Method("Get").With(typeof (Payload).FullName).Will(Return.Value(typeof (Payload)));
            Stub.On(typeFinder).Method("Get").With("System.String[]").Will(Return.Value(typeof (String[])));
            Stub.On(typeFinder).Method("Get").With("_0").Will(Return.Value(typeof (Payload)));
            Stub.On(typeFinder).Method("Get").With("_1").Will(Return.Value(typeof (A)));

            var payload = reader.Read(new XmlStreamReader(stream));

            Assert.IsNotNull(payload);
            Assert.AreEqual("My name", ((A) payload.Target).Name);
        }

        private class A
        {
            private readonly string name;

            public A(string name)
            {
                this.name = name;
            }

            public string Name
            {
                get { return name; }
            }
        }
    }
}