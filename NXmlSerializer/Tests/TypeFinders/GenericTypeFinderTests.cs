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

using System.Collections.Generic;
using NMock2;
using NSerializer.Framework.Types;
using NSerializer.TestAssembly1;
using NSerializer.Types;
using NSerializer.XML.Readers.Members;
using NUnit.Framework;


namespace NSerializer.Tests.TypeFinders
{
    [TestFixture]
    public class GenericTypeFinderTests : MockingTestFixture
    {
        private GenericTypeFinder finder;
        private ITypeFinder typeFinder;
        private ITypesCache typesCache;

        protected override void SetUp()
        {
            typesCache = NewMock<ITypesCache>();
            typeFinder = NewMock<ITypeFinder>();
            finder = new GenericTypeFinder(typesCache, typeFinder);
        }

        [Test]
        public void ReturnsNullForNonGenericType()
        {
            Assert.IsNull(finder.Get("NSerializer.Tests.TypeHandling.GenericTypeFinderTests"));
        }

        [Test]
        public void CanFindGenericListTypeInReferencedAssemblyAndAddsTypeToCache()
        {
            var expectedType =
                typeof (List<SerializableClassWithProperties>);

            Expect.Once.On(typeFinder).Method("Get").With("System.Collections.Generic.List`1").Will(
                Return.Value(new DataType(typeof(List<>), null)));
            Expect.Once.On(typeFinder).Method("Get").With(
                "NSerializer.Tests.XML.Targets.SerializableClassWithProperties").Will(
                Return.Value(new DestinationType<SerializableClassWithProperties>(null)));
            ;
            Expect.Once.On(typesCache).Method("Add").With(
                "System.Collections.Generic.List`1[[NSerializer.Tests.XML.Targets.SerializableClassWithProperties]]",
                expectedType);

            Assert.AreEqual(expectedType,
                            finder.Get(
                                "System.Collections.Generic.List`1[[NSerializer.Tests.XML.Targets.SerializableClassWithProperties]]").GetTargetType());
        }
    }
}