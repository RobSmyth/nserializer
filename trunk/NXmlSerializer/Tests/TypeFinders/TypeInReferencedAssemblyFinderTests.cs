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
using System.Reflection;
using NMock2;
using NSerializer.Framework.Types;
using NSerializer.TestAssembly1;
using NSerializer.Types;
using NSerializer.XML.Readers.Members;
using NUnit.Framework;
using Is=NMock2.Is;


namespace NSerializer.Tests.TypeFinders
{
    [TestFixture]
    public class TypeInReferencedAssemblyFinderTests : MockingTestFixture
    {
        private Assembly seedAssembly;
        private ITypesCache typesCache;
        private ITypeFinder typeFinder;
        private IDataTypeFactory dataTypeFactory;
        private IDataType dataType;

        protected override void SetUp()
        {
            seedAssembly = GetType().Assembly;
            typeFinder = NewMock<ITypeFinder>();
            typesCache = NewMock<ITypesCache>();
            dataTypeFactory = NewMock<IDataTypeFactory>();
            dataType = NewMock<IDataType>();
        }

        [Test]
        public void CanFindSystemTypeAndCachesTypeFound()
        {
            Stub.On(dataTypeFactory).Method("Create").With(typeof(Int32)).Will(Return.Value(dataType));
            ITypeFinder finder = new TypeInReferencedAssemblyFinder(seedAssembly, typesCache, typeFinder, dataTypeFactory);
            Expect.Once.On(typesCache).Method("Add").With("System.Int32", typeof (Int32));
            Assert.AreSame(dataType, finder.GetType("System.Int32"));
        }

        [Test]
        public void DoesNotFindTypeInSeedAssembly()
        {
            Stub.On(dataTypeFactory).Method("Create").With(Is.Null).Will(Return.Value(dataType));
            ITypeFinder finder = new TypeInReferencedAssemblyFinder(seedAssembly, typesCache, typeFinder, dataTypeFactory);
            finder.GetType(GetType().FullName);
        }

        [Test]
        public void CanFindTypeInDirectlyReferencedAssembly()
        {
            Stub.On(dataTypeFactory).Method("Create").With(typeof(TestTypeA1)).Will(Return.Value(dataType));
            Stub.On(typesCache).Method("Add").WithAnyArguments();
            ITypeFinder finder = new TypeInReferencedAssemblyFinder(seedAssembly, typesCache, typeFinder, dataTypeFactory);
            Assert.AreSame(dataType, finder.GetType("NSerializer.TestAssembly1.TestTypeA1"));
        }

        [Test]
        public void CanFindTypeInSecondReferencedAssembly()
        {
            Stub.On(dataTypeFactory).Method("Create").WithAnyArguments().Will(Return.Value(dataType));
            Stub.On(typesCache).Method("Add").WithAnyArguments();
            ITypeFinder finder = new TypeInReferencedAssemblyFinder(seedAssembly, typesCache, typeFinder, dataTypeFactory);
            Assert.AreSame(dataType, finder.GetType("NSerializer.TestAssembly2.TestTypeA2"));
        }

        [Test]
        public void CanFindArrayType()
        {
            Stub.On(dataTypeFactory).Method("Create").With(typeof(TestTypeA1[])).Will(Return.Value(dataType));
            Stub.On(dataType).GetProperty("IsArray").Will(Return.Value(true));
            Stub.On(typesCache).Method("Add").WithAnyArguments();

            ITypeFinder finder = new TypeInReferencedAssemblyFinder(seedAssembly, typesCache, typeFinder, dataTypeFactory);

            var type = finder.GetType("NSerializer.TestAssembly1.TestTypeA1[]");
            Assert.IsNotNull(type);
            Assert.IsTrue(type.IsArray);
        }

        [Test]
        public void CanFindStringType()
        {
            var soughtType = typeof(string);
            CanFindType(soughtType);
        }

        [Test]
        public void CanFindEnumType()
        {
            var soughtType = typeof (TestEnumA);
            CanFindType(soughtType);
        }

        private void CanFindType(Type soughtType)
        {
            Stub.On(dataTypeFactory).Method("Create").With(soughtType).Will(Return.Value(dataType));
            Stub.On(typesCache).Method("Add").WithAnyArguments();

            ITypeFinder finder = new TypeInReferencedAssemblyFinder(seedAssembly, typesCache, typeFinder, dataTypeFactory);

            Assert.AreSame(dataType, finder.GetType(soughtType.FullName));
        }
    }
}