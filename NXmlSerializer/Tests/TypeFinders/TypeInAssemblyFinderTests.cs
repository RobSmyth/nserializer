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
using NSerializer.Types;
using NUnit.Framework;


namespace NSerializer.Tests.TypeFinders
{
    [TestFixture]
    public class TypeInAssemblyFinderTests : MockingTestFixture
    {
        private TypeInAssemblyFinder finder;
        private ITypesCache typesCache;

        protected override void SetUp()
        {
            typesCache = NewMock<ITypesCache>();
            finder = new TypeInAssemblyFinder(GetType().Assembly, typesCache);
        }

        [Test]
        public void DoesNotFindTypeInReferencedAssembly()
        {
            Assert.IsNull(finder.Get("System.Int32"));
        }

        [Test]
        public void FindsTypeInSeedAssemblyAndAddsToCache()
        {
            Expect.Once.On(typesCache).Method("Add").With("NSerializer.Tests.TypeFinders.TypeInAssemblyFinderTests",
                                                          GetType());
            Assert.IsNotNull(finder.Get("NSerializer.Tests.TypeFinders.TypeInAssemblyFinderTests"));
        }
    }
}