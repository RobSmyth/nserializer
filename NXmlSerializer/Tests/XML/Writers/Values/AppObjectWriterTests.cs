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
using NSerializer.Framework;
using NSerializer.Framework.Document;
using NSerializer.Framework.Writers.Values;
using NSerializer.Types;
using NSerializer.XML.Document;
using NSerializer.XML.Document.Writers;
using NUnit.Framework;


namespace NSerializer.Tests.XML.Writers.Values
{
    [TestFixture]
    public class AppObjectWriterTests : MockingTestFixture
    {
        private IValuesCache cache;
        private IDocumentWriter document;
        private IInstanceRepository repository;
        private ITypeNamesCache typeNamesCache;
        private AppObjectWriter writer;

        protected override void SetUp()
        {
            repository = NewMock<IInstanceRepository>();
            cache = NewMock<IValuesCache>();
            document = NewMock<IDocumentWriter>();
            typeNamesCache = NewMock<ITypeNamesCache>();

            writer = new AppObjectWriter(document, repository, cache, typeNamesCache);
        }

        [Test]
        public void CanWrite_ReturnsTrue_IfExternalInstanceCanProvide()
        {
            var testObject = new TestClass1();
            Expect.Once.On(repository).Method("HasType").With(testObject.GetType()).Will(Return.Value(true));
            Assert.IsTrue(writer.CanWrite(testObject, testObject.GetType()));
        }

        [Test]
        public void CanWrite_ReturnsFalse_IfExternalInstanceCannotProvide()
        {
            var testObject = new TestClass1();
            Expect.Once.On(repository).Method("HasType").With(testObject.GetType()).Will(Return.Value(false));
            Assert.IsFalse(writer.CanWrite(testObject, testObject.GetType()));
        }

        [Test]
        public void Write_GetInstanceFromRepository()
        {
            var testObject = new TestClass1();
            var parentNode = NewMock<INodeWriter>();
            var classNode = NewMock<INodeWriter>();

            StubTypeMapper(typeof (int));

            Expect.Once.On(cache).Method("Add").With(testObject).Will(Return.Value(7));
            Expect.Once.On(document).Method("CreateAppObjectElement").With("System.Int32", 7, parentNode).Will(
                Return.Value(classNode));
            Expect.Once.On(classNode).Method("Dispose").WithNoArguments();

            writer.Write(testObject, parentNode, typeof (int));
        }

        private void StubTypeMapper(Type type)
        {
            Expect.Once.On(typeNamesCache).Method("GetTypeName").With(type).Will(Return.Value(type.ToString()));
        }

        private class TestClass1
        {
        }
    }
}