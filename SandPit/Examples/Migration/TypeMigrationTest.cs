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

using NSerializer.Migration;
using NSerializer.UATs;
using NUnit.Framework;


namespace NSerializer.SandPit.Examples.Migration
{
    [TestFixture]
    public class TypeMigrationTest : SerializeContext
    {
        [Test]
        public void RootTypeNameChange()
        {
            var xmlText = SerializeAsXml(new A1());

            var readObject = ReadXmlText<A2>(xmlText, null, null, new MigrationBuilderA12());

            Assert.IsNotNull(readObject);
            Assert.AreEqual(1, readObject.FieldOne);
        }

        [Test]
        public void MemberTypeNameChange()
        {
            var xmlText = SerializeAsXml(new B1());

            var readObject = ReadXmlText<B2>(xmlText, null, null, new MigrationBuilderB12());

            Assert.IsNotNull(readObject);
            Assert.AreEqual(1, readObject.FieldOne.FieldOne);
        }

        [Test]
        public void TypeAlias_WriteReadSameType()
        {
            var xmlText = SerializeAsXml(new B1(), null, new MigrationBuilder_WithAliasB12());
            Assert.IsFalse(xmlText.Contains("B1"));
            Assert.IsTrue(xmlText.Contains("bonealias"));

            var readObject = ReadXmlText<B1>(xmlText, null, null, new MigrationBuilder_WithAliasB12());

            Assert.IsNotNull(readObject);
            Assert.AreEqual(1, readObject.FieldOne.FieldOne);
        }
    }

    internal class A1
    {
        public A1()
        {
            FieldOne = 1;
        }

        public int FieldOne { get; private set; }
    }

    internal class A2
    {
        public A2()
        {
            FieldOne = 10;
        }

        public int FieldOne { get; private set; }
    }

    internal class B1
    {
        public B1()
        {
            FieldOne = new A1();
        }

        public A1 FieldOne { get; private set; }
    }

    internal class B2
    {
        public B2()
        {
            FieldOne = new A2();
        }

        public A2 FieldOne { get; private set; }
    }

    internal class MigrationBuilderA12 : IMigrationRulesBuilder
    {
        public void Build(IMigrationRules rules)
        {
            rules.ForType<A2>().MatchesTypeName(typeof (A1).FullName);
        }
    }

    internal class MigrationBuilderB12 : IMigrationRulesBuilder
    {
        public void Build(IMigrationRules rules)
        {
            rules.ForType<A2>().MatchesTypeName(typeof (A1).FullName);
            rules.ForType<B2>()
                .UseAlias("btwoalias")
                .MatchesTypeName(typeof (B1).FullName);
        }
    }

    internal class MigrationBuilder_WithAliasB12 : IMigrationRulesBuilder
    {
        public void Build(IMigrationRules rules)
        {
            rules.ForType<B1>().UseAlias("bonealias");
        }
    }
}