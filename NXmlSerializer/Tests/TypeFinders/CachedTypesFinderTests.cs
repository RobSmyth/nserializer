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

using NSerializer.Framework.Types;
using NUnit.Framework;


namespace NSerializer.Tests.TypeFinders
{
    [TestFixture]
    public class CachedTypesFinderTests : MockingTestFixture
    {
        private CachedTypesFinder finder;

        protected override void SetUp()
        {
            finder = new CachedTypesFinder();
        }

        [Test]
        public void Get_ReturnsNull_Initially()
        {
            Assert.IsNull(finder.Get("mynampesace.myType"));
        }

        [Test]
        public void Get_ReturnsType_AfterTypeAdded()
        {
            finder.Add("mynampesace.myType", GetType());
            Assert.AreEqual(GetType(), finder.Get("mynampesace.myType"));
        }
    }
}