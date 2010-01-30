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
using NSerializer.Migration;
using NSerializer.UATs.Contexts;
using NUnit.Framework;
using TestNamespace;


namespace NSerializer.UATs
{
    [TestFixture]
    public class DataMigrationUATs : SerializeContext
    {
        [Test]
        public void TypeNameChange()
        {
            var source = new MyTypeA_V1();
            var xmlText = SerializeAsXml(new List<object> {source});

            var destination = ReadXmlText<List<object>>(xmlText, null, null, new MigrationRulesBuilder())[0];
            Assert.AreEqual(typeof (MyTypeA_V2), destination.GetType());
        }

        [Test]
        public void TypeNamespaceChange()
        {
            var source = new MyTypeB_V1();
            var xmlText = SerializeAsXml(new List<object> {source});

            var destination = ReadXmlText<List<object>>(xmlText, null, null, new MigrationRulesBuilder())[0];
            Assert.AreEqual(typeof (MyTypeB_V2), destination.GetType());
        }

        private class MigrationRulesBuilder : IMigrationRulesBuilder
        {
            public void Build(IMigrationRules rules)
            {
                rules.ForType<MyTypeA_V2>()
                    .MatchesTypeName("NSerializer.UATs.DataMigrationUATs+MyTypeA_V1");

                rules.ForType<MyTypeB_V2>()
                    .MatchesTypeName("NSerializer.UATs.DataMigrationUATs+MyTypeB_V1");
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
    }
}


namespace TestNamespace
{
    internal class MyTypeB_V2
    {
    }
}