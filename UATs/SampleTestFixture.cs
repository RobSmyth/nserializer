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

using System.IO;
using System.Reflection;
using System.Text;
using NSerializer.XML.Document;
using NSerializer.XML.Document.Writers;
using NUnit.Framework;


namespace NSerializer.UATs
{
    [TestFixture]
    public class SampleTestFixture
    {
        private Assembly typeSeedAssembly;

        [SetUp]
        public void SetUp()
        {
            typeSeedAssembly = typeof (MyClassUnderTest).Assembly;
        }

        [Test]
        public void CanSerialize()
        {
            var sourceObject = new MyClassUnderTest(3); // the class you are testing

            var xmlText = Serialize(sourceObject);
            var readObject = ReadXmlText<MyClassUnderTest>(xmlText);

            Assert.AreEqual(3, readObject.A); // something you can validate on the derserialized object
        }

        private static string Serialize(object value)
        {
            var xmlResult = new StringBuilder();
            IDocumentWriter document = new NXmlDocumentWriter();
            var xmlWriter = new NXmlWriter(document, null, null);
            xmlWriter.Write(value, new StringWriter(xmlResult));
            return xmlResult.ToString();
        }

        private T ReadXmlText<T>(string xmlText)
        {
            var stringReader = new StringReader(xmlText);
            var reader = new NXmlReader(typeSeedAssembly);
            return reader.Read<T>(GetStream(xmlText));
        }

        protected MemoryStream GetStream(string xmlText)
        {
            return new MemoryStream(Encoding.ASCII.GetBytes(xmlText));
        }
    }

    public class MyClassUnderTest
    {
        private int a;

        public MyClassUnderTest(int a)
        {
            this.a = a;
        }

        public int A
        {
            get { return a; }
        }
    }
}