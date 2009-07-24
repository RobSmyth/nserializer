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

using NSerializer.Exceptions;
using NSerializer.Framework;
using NUnit.Framework;


namespace NSerializer.Tests
{
    [TestFixture]
    public class NullInstanceRepositoryTests : MockingTestFixture
    {
        private NullInstanceRepository repository;

        protected override void SetUp()
        {
            repository = new NullInstanceRepository();
        }

        [Test]
        [ExpectedException(typeof (UnableToReadXMLTextException),
            ExpectedMessage =
                "External instance repository required to read type 'NSerializer.Tests.NullInstanceRepositoryTests'."
            )]
        public void GetInstance_ThrowsException()
        {
            repository.Get(GetType());
        }
    }
}