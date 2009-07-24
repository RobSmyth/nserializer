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
using NSerializer.Framework.Writers;
using NSerializer.Framework.Writers.Values;
using NSerializer.XML.Document;
using NSerializer.XML.Document.Writers;
using NUnit.Framework;


namespace NSerializer.Tests.XML.Writers.Values
{
    [TestFixture]
    public class ClassWriterTests : MockingTestFixture
    {
        private IBaseTypeMembersWriter baseTypeMembersWriter;
        private IValuesCache cache;
        private IDocumentWriter document;
        private IMemberWriter memberWriter;
        private ITypeNamesCache typeNamesCache;
        private ClassWriter writer;

        protected override void SetUp()
        {
            document = NewMock<IDocumentWriter>();
            cache = NewMock<IValuesCache>();
            memberWriter = NewMock<IMemberWriter>();
            baseTypeMembersWriter = NewMock<IBaseTypeMembersWriter>();
            typeNamesCache = NewMock<ITypeNamesCache>();

            writer = new ClassWriter(document, cache, memberWriter, baseTypeMembersWriter, typeNamesCache);
        }

        [Test]
        public void CanWrite_ReturnsTrue_WhenObjectNotInCache()
        {
            var testObject = new TestClass1();
            Expect.Once.On(cache).Method("Contains").With(testObject).Will(Return.Value(false));
            Assert.IsTrue(writer.CanWrite(testObject, testObject.GetType()));
        }

        [Test]
        public void CanWrite_ReturnsFalse_WhenObjectInCache()
        {
            var testObject = new TestClass1();
            Expect.Once.On(cache).Method("Contains").With(testObject).Will(Return.Value(true));
            Assert.IsFalse(writer.CanWrite(testObject, testObject.GetType()));
        }

        [Test]
        public void Write_CachesTheObjectAndEnodesClassTypeObject()
        {
            var testObject = new TestClass1();
            var parentNode = NewMock<INodeWriter>();
            var classNode = NewMock<INodeWriter>();
            var membersNode = NewMock<INodeWriter>();

            StubTypeMapper();

            Expect.Once.On(document).Method("CreateClassElement").With(testObject.GetType().ToString(), 7, parentNode).Will(
                Return.Value(classNode));

            Expect.Once.On(document).Method("CreateMembersElement").With(classNode).Will(Return.Value(membersNode));
            Expect.Once.On(cache).Method("Add").With(testObject).Will(Return.Value(7));
            Expect.Once.On(memberWriter).Method("Write").With(testObject, membersNode, testObject.GetType());
            Expect.Once.On(baseTypeMembersWriter).Method("WriteMembers").With(testObject, classNode,
                                                                              testObject.GetType());
            Expect.Once.On(classNode).Method("Dispose").WithNoArguments();
            Expect.Once.On(membersNode).Method("Dispose").WithNoArguments();

            writer.Write(testObject, parentNode, parentNode.GetType());
        }

        private void StubTypeMapper()
        {
            StubTypeMapper(typeof (TestClass1));
            StubTypeMapper(typeof (int));
            StubTypeMapper(typeof (int[]));
        }

        private void StubTypeMapper(Type type)
        {
            Stub.On(typeNamesCache).Method("GetTypeName").With(type).Will(Return.Value(type.ToString()));
        }

        private class TestClass1
        {
        }
    }
}