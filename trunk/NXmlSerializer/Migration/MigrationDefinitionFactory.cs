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
using NDependencyInjection.Attributes;
using NSerializer.Framework.Document;


namespace NSerializer.Migration
{
    internal class MigrationDefinitionFactory
    {
        private readonly Version payloadVersion;
        private readonly IMigrationRulesBuilder rulesBuilder;

        [InjectionConstructor]
        public MigrationDefinitionFactory(MetaData metaData, IMigrationRulesBuilder rulesBuilder)
            : this(metaData.PayloadVersion, rulesBuilder)
        {
        }

        public MigrationDefinitionFactory(Version payloadVersion, IMigrationRulesBuilder rulesBuilder)
        {
            this.payloadVersion = payloadVersion;
            this.rulesBuilder = rulesBuilder;
        }

        public IMigrationDefinition Create()
        {
            var migrationDefinition = new MigrationDefinition(new NullMigrationDefinition(), payloadVersion);
            migrationDefinition.AddRules(rulesBuilder);
            return migrationDefinition;
        }
    }
}