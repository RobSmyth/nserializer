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
using System.Collections;
using System.Collections.Generic;
using NMock2;
using NSerializer.Framework;
using NSerializer.Framework.Document;
using NSerializer.Framework.Writers;
using NSerializer.Framework.Writers.Values;
using NSerializer.Types;
using NSerializer.XML.Document;
using NSerializer.XML.Document.Writers;
using NUnit.Framework;


namespace NSerializer.Tests.XML.Writers.Values
{
    [TestFixture]
    public class ArrayOfObjectsWriterTests : NXmlTestFixtureBase
    {
        private IObjectWriter objectWriter;
        private IDocumentWriter ownerDocument;
        private ITypeNamesCache typeNamesCache;
        private IValuesCache valueCache;
        private IObjectWriter writer;

        protected override void SetUp()
        {
            base.SetUp();

            ownerDocument = NewMock<IDocumentWriter>();
            valueCache = NewMock<IValuesCache>();
            objectWriter = NewMock<IObjectWriter>();
            typeNamesCache = NewMock<ITypeNamesCache>();

            writer = new ArrayOfObjectsWriter(ownerDocument, valueCache, objectWriter, typeNamesCache);
        }

        [Test]
        public void CanWrite_ReturnsFalse_IfValueIsAList()
        {
            object value = new ArrayList();
            Assert.IsFalse(writer.CanWrite(value, value.GetType()));

            value = new List<int>();
            Assert.IsFalse(writer.CanWrite(value, value.GetType()));
        }

        [Test]
        public void CanWrite_ReturnsTrue_IfAnArray()
        {
            object value = new[] {1, 3, 7};
            Assert.IsTrue(writer.CanWrite(value, value.GetType()));
        }

        [Test]
        public void Write_BuiltsXmlNodesAndAddsObjectToCache_IfAnArray()
        {
            var parentNode = NewMock<INodeWriter>();
            var arrayNode = CreateXmlNode("array");
            var itemsNode = CreateXmlNode("items");

            object value = new[] {1, 3};

            StubTypeMapper();

            Expect.Once.On(valueCache).Method("Add").With(value).Will(Return.Value(23));

            Expect.Once.On(ownerDocument).Method("CreateArrayElement").With(value.GetType().ToString(), 23, parentNode).Will(
                Return.Value(arrayNode));
            Expect.Once.On(ownerDocument).Method("CreateItemsElement").With(arrayNode).Will(Return.Value(itemsNode));

            Expect.Once.On(itemsNode).Method("Dispose").WithNoArguments();
            Expect.Once.On(arrayNode).Method("Dispose").WithNoArguments();

            Expect.Once.On(objectWriter).Method("Write").With(1, itemsNode, typeof (Int32));
            Expect.Once.On(objectWriter).Method("Write").With(3, itemsNode, typeof (Int32));

            writer.Write(value, parentNode, value.GetType());
        }

        private void StubTypeMapper()
        {
            StubTypeMapper(typeof (int));
            StubTypeMapper(typeof (int[]));
        }

        private void StubTypeMapper(Type type)
        {
            Stub.On(typeNamesCache).Method("GetTypeName").With(type).Will(Return.Value(type.ToString()));
        }
    }
}