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


namespace NSerializer.Tests.TypeFinders
{
    [TestFixture]
    public class PassOnceNameFilterTests : MockingTestFixture
    {
        private PassOnceNameFilter filter;

        protected override void SetUp()
        {
            filter = new PassOnceNameFilter();
        }

        [Test]
        public void Pass_ReturnsTrue_OnFirstCallWithName()
        {
            Assert.IsTrue(filter.Pass("abc"));
            Assert.IsTrue(filter.Pass(" abc"));
            Assert.IsTrue(filter.Pass("Abc"));
            Assert.IsTrue(filter.Pass("ABC"));
            Assert.IsTrue(filter.Pass("a bc"));
            Assert.IsTrue(filter.Pass(".abc"));
            Assert.IsTrue(filter.Pass(""));
        }

        [Test]
        public void Pass_ReturnsFalse_OnSecondCallWithName()
        {
            Assert.IsTrue(filter.Pass("abc"));
            Assert.IsFalse(filter.Pass("abc"));

            Assert.IsTrue(filter.Pass(" abc"));
            Assert.IsFalse(filter.Pass(" abc"));

            Assert.IsTrue(filter.Pass("ABC"));
            Assert.IsFalse(filter.Pass("ABC"));

            Assert.IsTrue(filter.Pass(""));
            Assert.IsFalse(filter.Pass(""));

            Assert.IsTrue(filter.Pass(".abc"));
            Assert.IsFalse(filter.Pass(".abc"));
        }
    }
}