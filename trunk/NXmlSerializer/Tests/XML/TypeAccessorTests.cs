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
using NSerializer.Framework;
using NSerializer.Types;
using NUnit.Framework;


namespace NSerializer.Tests.XML
{
    [TestFixture]
    public class TypeAccessorTests : MockingTestFixture
    {
        protected override void SetUp()
        {
        }

        [Test]
        public void GetMembers_OnlyReturnsPrivateFields_IfSerializeOptionIsFields()
        {
            var type = typeof (ClassB);
            var typeAccessor = new TypeAccessor(type, null);

            var members = typeAccessor.GetMembers();

            Assert.AreEqual(2, members.Length);
            Assert.AreEqual("fieldA", members[0].Name);
            Assert.AreEqual("fieldB", members[1].Name);
        }

        [Test]
        public void GetMembers_DoesNotReturnIgnoredPrivateFields_IfSerializeOptionIsFields()
        {
            var type = typeof (ClassC);
            var typeAccessor = new TypeAccessor(type, null);

            var members = typeAccessor.GetMembers();

            Assert.AreEqual(1, members.Length);
            Assert.AreEqual("fieldB", members[0].Name);
        }

        [Test]
        public void GetMembers_DoesNotReturnEventPrivateFields_IfSerializeOptionIsFields()
        {
            var type = typeof (ClassD);
            var typeAccessor = new TypeAccessor(type, null);

            var members = typeAccessor.GetMembers();

            Assert.AreEqual(1, members.Length);
            Assert.AreEqual("fieldB", members[0].Name);
        }

#pragma warning disable 169
#pragma warning disable 67

        private class ClassB
        {
            private int fieldA;
            private int fieldB;

            public int PropertyA
            {
                get { return 0; }
            }
        }

        private class ClassC
        {
            [NSerializerIgnore] private int fieldA;
            private int fieldB;
        }

        private class ClassD
        {
            public delegate void EventDelegate();

            [NSerializerIgnore] private int fieldA;
            private int fieldB;

            private event EventDelegate Event;
        }

        private class ClassE1 : ClassE2
        {
            private readonly int fieldE1;

            public ClassE1(int value) : base(value)
            {
                fieldE1 = value;
            }

            public int FieldE1
            {
                get { return fieldE1; }
            }
        }

        private abstract class ClassE2
        {
            private readonly int fieldE2;

            public ClassE2(int value)
            {
                fieldE2 = value;
            }

            public int FieldE2
            {
                get { return fieldE2; }
            }
        }

        private class ClassF : List<int>
        {
        }

#pragma warning restore 67
#pragma warning restore 169
    }
}