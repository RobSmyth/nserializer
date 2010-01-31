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
using NSerializer.Exceptions;
using NSerializer.Framework.Types;
using NSerializer.XML.Readers.Members;
using NUnit.Framework;


namespace NSerializer.Tests.TypeFinders
{
    [TestFixture]
    public class TypeFindersTests : MockingTestFixture
    {
        private TypeFinder finder;
        private ITypeFinder typeFinderA;
        private ITypeFinder typeFinderB;

        protected override void SetUp()
        {
            typeFinderA = NewMock<ITypeFinder>();
            typeFinderB = NewMock<ITypeFinder>();

            finder = new TypeFinder(typeFinderA, typeFinderB);
        }

        [Test]
        public void Get_ThrowsException_IfUnknownTypeName()
        {
            Expect.Once.On(typeFinderA).Method("GetType").Will(Return.Value(new DataType(null)));
            Expect.Once.On(typeFinderB).Method("GetType").Will(Return.Value(new DataType(null)));

            Assert.Throws<UnableToReadXMLTextException>(() => finder.GetType("mynamespace.nonExistingType"), "Unable to find type 'mynamespace.nonExistingType'.");
        }

        [Test]
        public void Get_ReturnsFoundType_IfFirstFinderFindsType()
        {
            Expect.Once.On(typeFinderA).Method("GetType").Will(Return.Value(new DataType(GetType())));

            Assert.AreEqual(GetType(), finder.GetType("mynamespace.nonExistingType").GetTargetType());
        }

        [Test]
        public void Get_ReturnsFoundType_IfFirstFinderDoesNotFindTypeButSecondFinderDoes()
        {
            Expect.Once.On(typeFinderA).Method("GetType").Will(Return.Value(new DataType(null)));
            Expect.Once.On(typeFinderB).Method("GetType").Will(Return.Value(new DataType(GetType())));

            Assert.AreEqual(GetType(), finder.GetType("mynamespace.nonExistingType").GetTargetType());
        }
    }
}