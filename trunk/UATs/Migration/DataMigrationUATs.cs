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
using System.Collections.Generic;
using System.Linq;
using NSerializer.Exceptions;
using NSerializer.Migration;
using NSerializer.UATs.Contexts;
using NSerializer.UATs.Migration.Migration;
using NUnit.Framework;


namespace NSerializer.UATs.Migration
{
    [TestFixture]
    public class DataMigrationUATs : SerializeContext
    {
        [Test]
        public void TypeNameChange()
        {
            var xmlText = SerializeAsXml(new List<object> {new MyTypeA_V1()});

            var destination = ReadXmlText<List<object>>(xmlText, null, null, new MigrationRulesBuilder())[0];
            Assert.AreEqual(typeof (MyTypeA_V2), destination.GetType());
        }

        [Test]
        public void TypeNamespaceChange()
        {
            var xmlText = SerializeAsXml(new List<object> {new MyTypeB_V1()});

            var destination = ReadXmlText<List<object>>(xmlText, null, null, new MigrationRulesBuilder())[0];
            Assert.AreEqual(typeof(MyTypeB_V2), destination.GetType());
        }

        [Test]
        public void FieldNameChange()
        {
            var xmlText = SerializeAsXml(new List<object> { new MyTypeC_V1(7) });

            var destination = ReadXmlText<List<object>>(xmlText, null, null, new MigrationRulesBuilder(new FileNameChangeMigrationBuilder()))[0];
            Assert.AreEqual(typeof(MyTypeC_V2), destination.GetType());
        }

        [Test]
        public void FieldNameChange_Failure()
        {
            var xmlText = SerializeAsXml(new List<object> { new MyTypeC_V1(7) });
            Assert.Throws<NSerializerException>(() => ReadXmlText<List<object>>(xmlText, null, null, new MigrationRulesBuilder()));
        }

        [Test]
        public void DeletedField()
        {
            var xmlText = SerializeAsXml(new List<object> { new MyTypeD_V1(17) });

            var destination = ReadXmlText<List<object>>(xmlText, null, null, new MigrationRulesBuilder(new FileNameChangeMigrationBuilder()))[0];
            Assert.AreEqual(typeof(MyTypeD_V2), destination.GetType());
        }

        [Test]
        public void DeletedField_Failure()
        {
            var xmlText = SerializeAsXml(new List<object> { new MyTypeD_V1(3) });
            Assert.Throws<NSerializerException>(() => ReadXmlText<List<object>>(xmlText, null, null, new MigrationRulesBuilder()));
        }

        private class MigrationRulesBuilder : IMigrationRulesBuilder
        {
            private readonly IMigrationRulesBuilder[] childRulesBuilders;

            public MigrationRulesBuilder(params IMigrationRulesBuilder[] childRulesBuilders)
            {
                this.childRulesBuilders = childRulesBuilders;
            }

            public void Build(IMigrationRules rules)
            {
                rules.ForType<MyTypeA_V2>()
                    .MatchesTypeName("NSerializer.UATs.Migration.DataMigrationUATs+MyTypeA_V1");

                rules.ForType<MyTypeB_V2>()
                    .MatchesTypeName("NSerializer.UATs.Migration.DataMigrationUATs+MyTypeB_V1");

                rules.ForType<MyTypeC_V2>()
                    .MatchesTypeName("NSerializer.UATs.Migration.DataMigrationUATs+MyTypeC_V1");

                rules.ForType<MyTypeD_V2>()
                    .MatchesTypeName("NSerializer.UATs.Migration.DataMigrationUATs+MyTypeD_V1");

                childRulesBuilders.ToList().ForEach(builder => builder.Build(rules));
            }
        }

        private class FileNameChangeMigrationBuilder : IMigrationRulesBuilder
        {
            public void Build(IMigrationRules rules)
            {
                rules.ForType<MyTypeC_V2>()
                    .Field("valueA").RenamedTo("valueAV2");

                rules.ForType<MyTypeD_V2>()
                    .Field("valueA").Ignore();
            }
        }

        private class MyTypeA_V1
        {
        }

        private class MyTypeB_V1
        {
        }

        private class MyTypeA_V2
        {
        }

        private class MyTypeC_V1
        {
            private readonly int valueA;

            public MyTypeC_V1(int valueA)
            {
                this.valueA = valueA;
            }
        }

        private class MyTypeC_V2
        {
            private readonly int valueAV2;

            public MyTypeC_V2(int valueAv2)
            {
                valueAV2 = valueAv2;
            }

            public int ValueAv2
            {
                get { return valueAV2; }
            }
        }

        private class MyTypeD_V1
        {
            private readonly int valueA;

            public MyTypeD_V1(int valueA)
            {
                this.valueA = valueA;
            }
        }

        private class MyTypeD_V2
        {
        }
    }


    namespace Migration
    {
        internal class MyTypeB_V2
        {
        }
    }
}
