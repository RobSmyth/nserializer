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

using NSerializer.Logging;


namespace NSerializer.Migration
{
    internal class MigrationScopeRulesVerb : IMigrationRulesVerb
    {
        private readonly IMigrationDefinition definition;
        private readonly IMigrationRules rules;
        private readonly IVersionQualifier versionQualifier;
        private readonly ILogger logger;

        public MigrationScopeRulesVerb(IMigrationRules rules, IMigrationDefinition definition, IVersionQualifier versionQualifier, ILogger logger)
        {
            this.rules = rules;
            this.definition = definition;
            this.versionQualifier = versionQualifier;
            this.logger = logger;
        }

        public IMigrationRules MigrateUsing(IMigrationRulesBuilder rulesBuilder)
        {
            IMigrationRules newRules =
                new MigrationRules(new MigrationDefinition(definition, versionQualifier.StartVersion, logger),
                                   versionQualifier.StartVersion, logger);
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
            // to do
            return rules;
        }
    }
}