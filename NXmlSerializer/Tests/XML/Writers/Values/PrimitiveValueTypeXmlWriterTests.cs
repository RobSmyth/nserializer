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

using NMock2;
using NSerializer.Framework.Document;
using NSerializer.Framework.Writers;
using NSerializer.XML.Document;
using NSerializer.XML.Document.Writers;
using NSerializer.XML.Writers.Values;
using NUnit.Framework;


namespace NSerializer.Tests.XML.Writers.Values
{
    [TestFixture]
    public class PrimitiveValueTypeXmlWriterTests : NXmlTestFixtureBase
    {
        private IDocumentWriter ownerDocument;
        private IObjectWriter writer;

        protected override void SetUp()
        {
            base.SetUp();

            ownerDocument = NewMock<IDocumentWriter>();

            writer = new PrimitiveValueTypeXmlWriter(ownerDocument);
        }

        [Test]
        public void CanWrite_ReturnsFalse_IfValueIsAnObject()
        {
            Assert.IsFalse(writer.CanWrite(this, GetType()));
        }

        [Test]
        public void CanWrite_ReturnsTrue_IfValueIsAnInteger()
        {
            Assert.IsTrue(writer.CanWrite(1, 1.GetType()));
        }

        [Test]
        public void CanWrite_ReturnsTrue_IfValueIsANullableInteger()
        {
            int? value = 1;
            Assert.IsTrue(writer.CanWrite(value, value.GetType()));
        }

        [Test]
        public void CanWrite_ReturnsTrue_IfValueIsAString()
        {
            Assert.IsTrue(writer.CanWrite("c", typeof (string)));
        }

        [Test]
        public void Write_BuiltsXmlNode_WithntegerValue()
        {
            var parentNode = NewMock<INodeWriter>();
            var valueNode = CreateXmlNode("primitive");

            Expect.Once.On(ownerDocument).Method("CreateElement").With("primitive", parentNode).Will(
                Return.Value(valueNode));
            Expect.Once.On(valueNode).Method("AddAttribute").With("type", "System.Int32");
            Expect.Once.On(valueNode).SetProperty("InnerText").To("1");
            Expect.Once.On(valueNode).Method("Dispose").WithNoArguments();

            writer.Write(1, parentNode, parentNode.GetType());
        }
    }
}