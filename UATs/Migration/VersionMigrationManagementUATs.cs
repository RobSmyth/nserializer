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

// Project site: http://code.google.com/p/nserializer/

using System;
using System.Linq;
using NSerializer.Exceptions;
using NSerializer.Migration;
using NSerializer.UATs.Contexts;
using NUnit.Framework;
using System.Collections.Generic;


namespace NSerializer.UATs.Migration
{
    [TestFixture]
    public class VersionMigrationManagementUATs : SerializeContext
    {
        [Test]
        public void NoMigrationRequiredIfCurrentVersion()
        {
            var xmlText = SerializeAsXml(new List<object> { new MyTypeA_V2() });
            var destination = ReadXmlText<List<object>>(xmlText, null, null, new MigrationRulesBuilder())[0];
            Assert.AreEqual(typeof(MyTypeA_V2), destination.GetType());
        }

        [Test]
        [Ignore("work in progress")]
        public void MigratesPriorVersion()
        {
            var xmlText = SerializeAsXml(new List<object> { new MyTypeA_V1() });

            //Console.WriteLine(xmlText);//>>>
            xmlText = xmlText.Replace("version value=\"2.0.0.0\"", "version value=\"1.4.0.0\"");
            Assert.IsTrue(xmlText.Contains("version value=\"1.4.0.0\""));

            var destination = ReadXmlText<List<object>>(xmlText, null, null, new MigrationRulesBuilder())[0];
            Assert.AreEqual(typeof(MyTypeA_V2), destination.GetType());
        }

        [Test]
        [Ignore("work in progress")]
        public void ThrowsExceptionIfBeforeFirstSupportedVersion()
        {
            var xmlText = SerializeAsXml(new List<object> { new MyTypeA_V2() });

            xmlText = xmlText.Replace("version value=\"2.0.0.0\"", "version value=\"1.1.0.0\"");
            Assert.IsTrue(xmlText.Contains("version value=\"1.1.0.0\""));

            Assert.Throws <FileVersionNotSupportedException>(()=>ReadXmlText<List<object>>(xmlText, null, null, new MigrationRulesBuilder()));
        }

        private class MyTypeA_V1
        {}

        private class MyTypeA_V2
        {}

        private class MigrationRulesBuilder : IMigrationRulesBuilder
        {
            private readonly IMigrationRulesBuilder[] childRulesBuilders;

            public MigrationRulesBuilder(params IMigrationRulesBuilder[] childRulesBuilders)
            {
                this.childRulesBuilders = childRulesBuilders;
            }

            public void Build(IMigrationRules rules)
            {
                rules.From(new Version(1, 4))
                    .MigrateUsing(new From1V4MigrationRulesBuilder())
                    .AllPriorVersions().NotSupported();

                rules.From(new Version(1, 5))
                    .NoMigrationRequired();

                childRulesBuilders.ToList().ForEach(builder => builder.Build(rules));
            }
        }

        private class From1V4MigrationRulesBuilder : IMigrationRulesBuilder
        {
            public void Build(IMigrationRules rules)
            {
                rules.ForType<MyTypeA_V2>()
                    .MatchesTypeName("NSerializer.UATs.Migration.VersionMigrationManagementUATs+MyTypeA_V1");
            }
        }
    }
}