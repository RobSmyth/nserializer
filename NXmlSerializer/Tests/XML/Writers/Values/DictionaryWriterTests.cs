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
using NMock2;
using NSerializer.Framework;
using NSerializer.Framework.Document;
using NSerializer.Framework.Writers;
using NSerializer.XML.Document;
using NSerializer.XML.Document.Writers;
using NSerializer.XML.Writers.Values;
using NUnit.Framework;


namespace NSerializer.Tests.XML.Writers.Values
{
    [TestFixture]
    public class DictionaryWriterTests : NXmlTestFixtureBase
    {
        private IObjectWriter objectWriter;
        private IDocumentWriter ownerDocument;
        private ITypeNamesCache typeNamesCache;
        private IValuesCache valuesCache;
        private DictionaryWriter writer;

        protected override void SetUp()
        {
            base.SetUp();

            ownerDocument = NewMock<IDocumentWriter>();
            valuesCache = NewMock<IValuesCache>();
            objectWriter = NewMock<IObjectWriter>();
            typeNamesCache = NewMock<ITypeNamesCache>();

            writer = new DictionaryWriter(ownerDocument, valuesCache, objectWriter, typeNamesCache);
        }

        [Test]
        public void CanWrite_IsFalse_IfAnInteger()
        {
            var value = 1;
            Assert.IsFalse(writer.CanWrite(value, value.GetType()));
        }

        [Test]
        public void CanWrite_IsTrue_IfAGenericDictionary()
        {
            var value = new Dictionary<string, int>();
            Assert.IsTrue(writer.CanWrite(value, value.GetType()));
        }

        [Test]
        public void Write_BuiltsXmlNode_IfAGenericDictionary()
        {
            var parentNode = NewMock<INodeWriter>();
            var dictNode = CreateXmlNode("topNode");
            var itemsNode = CreateXmlNode("dictionary");

            var value = new Dictionary<string, int>();
            value.Add("first key", 11);

            StubTypeMapper();

            Expect.Once.On(valuesCache).Method("Add").With(value).Will(Return.Value(7));

            Expect.Once.On(ownerDocument).Method("CreateTypedElement").With("dict", value.GetType().ToString(),
                                                                            parentNode).Will(
                Return.Value(dictNode));
            Expect.Once.On(dictNode).Method("AddAttribute").With("ID", "7");
            Expect.Once.On(dictNode).Method("Dispose").WithNoArguments();
            Expect.Once.On(itemsNode).Method("Dispose").WithNoArguments();

            Expect.Once.On(ownerDocument).Method("CreateElement").With("items", dictNode).Will(Return.Value(itemsNode));

            Expect.Once.On(objectWriter).Method("Write").With("first key", itemsNode, typeof (string));
            Expect.Once.On(objectWriter).Method("Write").With(11, itemsNode, typeof (Int32));

            Expect.Once.On(itemsNode).Method("AddAttribute").With("count", 1);

            writer.Write(value, parentNode, value.GetType());
        }

        private void StubTypeMapper()
        {
            StubTypeMapper(typeof (Dictionary<string, int>));
            StubTypeMapper(typeof (int));
            StubTypeMapper(typeof (int[]));
        }

        private void StubTypeMapper(Type type)
        {
            Stub.On(typeNamesCache).Method("GetTypeName").With(type).Will(Return.Value(type.ToString()));
        }
    }
}