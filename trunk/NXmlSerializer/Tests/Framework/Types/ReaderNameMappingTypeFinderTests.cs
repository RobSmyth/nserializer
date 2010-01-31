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

// Project site: http://code.google.com/p/nserializer/

using NMock2;
using NSerializer.Framework.Types;
using NSerializer.XML.Readers.Members;
using NUnit.Framework;


namespace NSerializer.Tests.Framework.Types
{
    [TestFixture]
    public class ReaderNameMappingTypeFinderTests : MockingTestFixture
    {
        private ReaderNameAliasingTypeFinder finder;
        private string[] names;
        private ITypeFinder typeFinder;

        protected override void SetUp()
        {
            typeFinder = NewMock<ITypeFinder>();
            names = new[]
                        {
                            "NSerializer.Tests.Framework.Types.ReaderNameMappingTypeFinderTests+MyTypeA", "!0[]",
                            "NSerializer.Tests.Framework.Types.ReaderNameMappingTypeFinderTests+MyTypeC"
                        };
            finder = new ReaderNameAliasingTypeFinder(typeFinder, names);
        }

        [Test]
        public void Get()
        {
            var typeA1 = NewMock<IDataType>();
            Expect.Once.On(typeFinder).Method("GetType").With(
                "NSerializer.Tests.Framework.Types.ReaderNameMappingTypeFinderTests+MyTypeA").Will(Return.Value(typeA1));
            Assert.AreSame(typeA1, finder.GetType("!0"));

            var typeA2 = NewMock<IDataType>();
            var typeA2_Array = NewMock<IDataType>();
            Stub.On(typeA2).Method("MakeArrayType").WithNoArguments().Will(Return.Value(typeA2_Array));
            Expect.Once.On(typeFinder).Method("GetType").With(
                "NSerializer.Tests.Framework.Types.ReaderNameMappingTypeFinderTests+MyTypeA").Will(Return.Value(typeA2));
            Assert.AreSame(typeA2_Array, finder.GetType("!1"));

            var typeC = NewMock<IDataType>();
            Expect.Once.On(typeFinder).Method("GetType").With(
                "NSerializer.Tests.Framework.Types.ReaderNameMappingTypeFinderTests+MyTypeC").Will(Return.Value(typeC));
            Assert.AreSame(typeC, finder.GetType("!2"));
        }

        private class MyTypeA
        {
        }

        private class MyTypeC
        {
        }
    }
}