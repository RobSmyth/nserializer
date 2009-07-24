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
using NSerializer.TestAssembly1;
using NSerializer.TypeFinders;
using NUnit.Framework;


namespace NSerializer.Tests.TypeFinders
{
    [TestFixture]
    public class TypeInReferencedAssemblyFinderTests : MockingTestFixture
    {
        private Assembly seedAssembly;
        private ITypesCache typesCache;

        protected override void SetUp()
        {
            seedAssembly = GetType().Assembly;

            typesCache = NewMock<ITypesCache>();
        }

        [Test]
        public void CanFindSystemTypeAndCachesTypeFound()
        {
            ITypeFinder finder = new TypeInReferencedAssemblyFinder(seedAssembly, typesCache);
            Expect.Once.On(typesCache).Method("Add").With("System.Int32", typeof (Int32));
            Assert.AreEqual(typeof (Int32), finder.Get("System.Int32"));
        }

        [Test]
        public void DoesNotFindTypeInSeedAssembly()
        {
            ITypeFinder finder = new TypeInReferencedAssemblyFinder(seedAssembly, typesCache);
            Assert.IsNull(finder.Get(GetType().FullName));
        }

        [Test]
        public void CanFindTypeInDirectlyReferencedAssembly()
        {
            Stub.On(typesCache).Method("Add").WithAnyArguments();
            ITypeFinder finder = new TypeInReferencedAssemblyFinder(seedAssembly, typesCache);
            Assert.IsNotNull(finder.Get("NSerializer.TestAssembly1.TestTypeA1"));
        }

        [Test]
        public void CanFindTypeInSecondReferencedAssembly()
        {
            Stub.On(typesCache).Method("Add").WithAnyArguments();
            ITypeFinder finder = new TypeInReferencedAssemblyFinder(seedAssembly, typesCache);
            Assert.IsNotNull(finder.Get("NSerializer.TestAssembly2.TestTypeA2"));
        }

        [Test]
        public void CanFindTypeInThirdReferencedAssemblyQuickly()
        {
            Stub.On(typesCache).Method("Add").WithAnyArguments();

            DateTime startTime = DateTime.Now;

            ITypeFinder finder = new TypeInReferencedAssemblyFinder(seedAssembly, typesCache);
            Assert.IsNotNull(finder.Get("NSerializer.TestAssembly3.TestTypeA3"));

            TimeSpan expiredTime = DateTime.Now - startTime;
            Assert.IsTrue(expiredTime < TimeSpan.FromSeconds(2));
            // generous time allowance for test reliability, really a sanity check
        }

        [Test]
        public void CanFindArrayType()
        {
            Stub.On(typesCache).Method("Add").WithAnyArguments();

            ITypeFinder finder = new TypeInReferencedAssemblyFinder(seedAssembly, typesCache);

            Type type = finder.Get("NSerializer.TestAssembly1.TestTypeA1[]");
            Assert.IsNotNull(type);
            Assert.IsTrue(type.IsArray);
        }

        [Test]
        public void CanFindStringType()
        {
            Type soughtType = typeof (string);
            CanFindType(soughtType);
        }

        [Test]
        public void CanFindEnumType()
        {
            Type soughtType = typeof (TestEnumA);
            CanFindType(soughtType);
        }

        private void CanFindType(Type soughtType)
        {
            Stub.On(typesCache).Method("Add").WithAnyArguments();

            ITypeFinder finder = new TypeInReferencedAssemblyFinder(seedAssembly, typesCache);

            Assert.AreEqual(soughtType, finder.Get(soughtType.FullName));
        }
    }
}