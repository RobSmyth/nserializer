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
using NSerializer.SandPit.Examples.Migration.SampleTypes;


namespace NSerializer.Migration.Tests
{
    public class MigrationBuilder : IMigrationRulesBuilder
    {
        public void Build(IMigrationRules rules)
        {
            rules.ForType<TypeA>().UseAlias("XYZServiceType");
            rules.ForType<TypeB>().UseAlias("ABCServiceType");

            rules
                .From(new Version(1, 2, 6)).MigrateUsing(new V010203Migrator())
                .From(new Version(1, 2, 5)).MigrateUsing(new V010202Migrator())
                .From(new Version(1, 2, 4)).NotSupported()
                .From(new Version(1, 2, 0)).MigrateUsing(new V010200Migrator())
                .AllPriorVersions().NotSupported();
        }
    }
}