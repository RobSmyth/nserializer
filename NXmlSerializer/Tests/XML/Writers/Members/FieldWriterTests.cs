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

using System.Reflection;
using NSerializer.Framework.Writers;
using NSerializer.XML.Writers.Members;
using NUnit.Framework;


namespace NSerializer.Tests.XML.Writers.Members
{
    [TestFixture]
    public class FieldWriterTests : MemberWriterTestFixture
    {
        private int fieldA;

        [Test]
        public void CanWrite_ReturnsTrue_IfAFieldInfo()
        {
            MemberInfo fieldInfo =
                GetType().GetField("fieldA", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            Assert.IsNotNull(fieldInfo);

            Assert.IsTrue(writer.CanWrite(ownerDocument, fieldInfo));
        }

        [Test]
        public void CanWrite_ReturnsFalse_IfAPropertyInfo()
        {
            MemberInfo fieldInfo =
                GetType().GetProperty("PropertyA", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            Assert.IsNotNull(fieldInfo);

            Assert.IsFalse(writer.CanWrite(ownerDocument, fieldInfo));
        }

        protected override IValueMemberWriter GetWriter()
        {
            return new FieldWriter(ownerDocument, objectWriter);
        }

#pragma warning disable 169

        public int PropertyA
        {
            get { return 0; }
        }
#pragma warning restore 169
    }
}