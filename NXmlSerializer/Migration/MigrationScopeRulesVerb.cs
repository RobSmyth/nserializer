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

namespace NSerializer.Migration
{
    internal class MigrationScopeRulesVerb : IMigrationRulesVerb
    {
        private readonly IMigrationDefinition definition;
        private readonly IMigrationRules rules;
        private readonly IVersionQualifier versionQualifier;

        public MigrationScopeRulesVerb(IMigrationRules rules, IMigrationDefinition definition,
                                       IVersionQualifier versionQualifier)
        {
            this.rules = rules;
            this.definition = definition;
            this.versionQualifier = versionQualifier;
        }

        public IMigrationRules MigrateUsing(IMigrationRulesBuilder rulesBuilder)
        {
            IMigrationRules newRules =
                new MigrationRules(new MigrationDefinition(definition, versionQualifier.StartVersion),
                                   versionQualifier.StartVersion);
            rulesBuilder.Build(newRules);
            return rules;
        }

        public IMigrationRules NotSupported()
        {
            definition.AddChild(versionQualifier, new NotSupportedMigrationDefinition());
            return rules;
        }

        public IMigrationRules NoMigrationRequired()
        {
            return rules;
        }
    }
}