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

using NSerializer.TypeFinders;
using NUnit.Framework;


namespace NSerializer.Tests.TypeFinders
{
    [TestFixture]
    public class TypeNameDemanglerTests : MockingTestFixture
    {
        protected override void SetUp()
        {
        }

        [Test]
        public void ToString_GivesTypeNameOfInteger()
        {
            TypeNameDemangler typeNameDemangler = new TypeNameDemangler(7.GetType());
            Assert.AreEqual("System.Int32", typeNameDemangler.ToString());
        }

        [Test]
        public void ToString_GivesTypeNameOfString()
        {
            TypeNameDemangler typeNameDemangler = new TypeNameDemangler("77".GetType());
            Assert.AreEqual("System.String", typeNameDemangler.ToString());
        }

        [Test]
        public void ToString_GivesNameOfApplicationNestedClassInstance()
        {
            TypeNameDemangler typeNameDemangler = new TypeNameDemangler(typeof (ClassA));
            Assert.AreEqual("NSerializer.Tests.TypeFinders.TypeNameDemanglerTests+ClassA",
                            typeNameDemangler.ToString());
        }

        [Test]
        public void ToString_GivesNameOfApplicationNestedClassType()
        {
            TypeNameDemangler typeNameDemangler = new TypeNameDemangler(typeof (ClassA));
            Assert.AreEqual("NSerializer.Tests.TypeFinders.TypeNameDemanglerTests+ClassA",
                            typeNameDemangler.ToString());
        }

        [Test]
        public void ToString_GivesNameOfApplicationGenericClassType()
        {
            TypeNameDemangler typeNameDemangler = new TypeNameDemangler(typeof (ClassB<int>));
            Assert.AreEqual("NSerializer.Tests.TypeFinders.TypeNameDemanglerTests+ClassB`1[[System.Int32]]",
                            typeNameDemangler.ToString());
        }

        [Test]
        public void ToString_GivesNameOfApplicationGenericClassTypeThatHasTwoGenericTypes()
        {
            TypeNameDemangler typeNameDemangler = new TypeNameDemangler(typeof (ClassC<string[], bool>));
            Assert.AreEqual(
                "NSerializer.Tests.TypeFinders.TypeNameDemanglerTests+ClassC`2[[System.String[]],[System.Boolean]]",
                typeNameDemangler.ToString());
        }

        [Test]
        public void ToString_GivesNameOfIntegerArray()
        {
            int[] testArray = new int[0];
            TypeNameDemangler typeNameDemangler = new TypeNameDemangler(testArray.GetType());
            Assert.AreEqual("System.Int32[]", typeNameDemangler.ToString());
        }

        [Test]
        public void ToString_GivesNameOfIntegerTwoDimensionArray()
        {
            int[][] testArray = new int[0][];
            TypeNameDemangler typeNameDemangler = new TypeNameDemangler(testArray.GetType());
            Assert.AreEqual("System.Int32[][]", typeNameDemangler.ToString());
        }

        [Test]
        public void ToString_GivesNameOfAnArrayOfGenericTypes()
        {
            ClassB<int>[] testArray = new ClassB<int>[0];
            TypeNameDemangler typeNameDemangler = new TypeNameDemangler(testArray.GetType());
            Assert.AreEqual("NSerializer.Tests.TypeFinders.TypeNameDemanglerTests+ClassB`1[[System.Int32]][]",
                            typeNameDemangler.ToString());
        }

        [Test]
        public void ToString_GivesAliasNameIfTypeHasAttribute()
        {
            TypeNameDemangler typeNameDemangler = new TypeNameDemangler(typeof (ClassD));
            Assert.AreEqual("ADifferentName", typeNameDemangler.ToString());
        }

        private class ClassA
        {
        }

        private class ClassB<T>
        {
        }

        private class ClassC<T1, T2>
        {
        }

        [SerializeName("ADifferentName")]
        private class ClassD
        {
        }
    }
}