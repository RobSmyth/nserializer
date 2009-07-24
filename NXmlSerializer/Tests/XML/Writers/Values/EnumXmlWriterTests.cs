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
using NMock2;
using NSerializer.Framework.Document;
using NSerializer.Framework.Writers;
using NSerializer.Types;
using NSerializer.XML.Document;
using NSerializer.XML.Document.Writers;
using NSerializer.XML.Writers.Values;
using NUnit.Framework;


namespace NSerializer.Tests.XML.Writers.Values
{
    [TestFixture]
    public class EnumXmlWriterTests : NXmlTestFixtureBase
    {
        private IDocumentWriter ownerDocument;
        private ITypeNamesCache typeNamesCache;
        private IObjectWriter writer;

        protected override void SetUp()
        {
            base.SetUp();

            ownerDocument = NewMock<IDocumentWriter>();
            typeNamesCache = NewMock<ITypeNamesCache>();

            writer = new EnumXmlWriter(ownerDocument, typeNamesCache);
        }

        [Test]
        public void CanWrite_ReturnsFalse_IfValueIsAnInteger()
        {
            Assert.IsFalse(writer.CanWrite(2, typeof (int)));
        }

        [Test]
        public void CanWrite_ReturnsTrue_IfValueIsAnEnum()
        {
            Assert.IsTrue(writer.CanWrite(EnumA.ValueB, typeof (EnumA)));
        }

        [Test]
        public void Write_BuiltsXmlNode()
        {
            var parentNode = NewMock<INodeWriter>();
            var valueNode = CreateXmlNode("enum");

            StubTypeMapper();

            Expect.Once.On(ownerDocument).Method("CreateTypedElement").With("enum", EnumA.ValueB.GetType().ToString(), parentNode).
                Will(Return.Value(valueNode));
            Expect.Once.On(valueNode).SetProperty("InnerText").To("ValueB");
            Expect.Once.On(valueNode).Method("Dispose").WithNoArguments();

            writer.Write(EnumA.ValueB, parentNode, parentNode.GetType());
        }

        private void StubTypeMapper()
        {
            StubTypeMapper(typeof (EnumA));
            StubTypeMapper(typeof (int));
            StubTypeMapper(typeof (int[]));
        }

        private void StubTypeMapper(Type type)
        {
            Stub.On(typeNamesCache).Method("GetTypeName").With(type).Will(Return.Value(type.ToString()));
        }

        private enum EnumA
        {
            ValueA,
            ValueB
        }
    }
}