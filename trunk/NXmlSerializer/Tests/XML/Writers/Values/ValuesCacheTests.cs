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

using NSerializer.Framework;
using NUnit.Framework;


namespace NSerializer.Tests.Writers.Values
{
    [TestFixture]
    public class ValuesCacheTests : MockingTestFixture
    {
        private ValuesCache cache;

        protected override void SetUp()
        {
            cache = new ValuesCache();
        }

        [Test]
        public void Contains_ReturnsFalse_Initially()
        {
            Assert.IsFalse(cache.Contains(this));
        }

        [Test]
        public void Contains_ReturnsTrue_IfObjectAddedToCache()
        {
            ClassA objA = new ClassA();
            cache.Add(objA);
            Assert.IsTrue(cache.Contains(objA));
        }

        [Test]
        public void Contains_ReturnsFalse_ForObjectNotInCache()
        {
            ClassA objA = new ClassA();
            ClassA objB = new ClassA();
            cache.Add(objA);
            Assert.IsFalse(cache.Contains(objB));
        }

        [Test]
        public void Add_ReturnsUniqueId_ForEachObjectAdded()
        {
            ClassA objA = new ClassA();
            ClassA objB = new ClassA();
            int idA = cache.Add(objA);
            int idB = cache.Add(objB);
            Assert.AreNotEqual(idA, idB);
            Assert.AreEqual(idA, cache.GetID(objA));
            Assert.AreEqual(idB, cache.GetID(objB));
        }

        private class ClassA
        {
        }
    }
}