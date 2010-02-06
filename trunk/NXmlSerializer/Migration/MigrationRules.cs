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
using NSerializer.Exceptions;
using NSerializer.Migration.Types;


namespace NSerializer.Migration
{
    public class MigrationRules : IMigrationRules
    {
        private readonly Version fromVersion;
        private readonly IMigrationDefinition definition;
        private readonly VersionComparer versionComparer = new VersionComparer();

        internal MigrationRules(IMigrationDefinition definition, Version fromVersion)
        {
            this.definition = definition;
            this.fromVersion = fromVersion;
        }

        public void NotSupported(string message)
        {
            throw new MigrationConfigurationException("Cannot mark a rules definition as not supported when it has rules.");
        }

        ITypeMigrationRules ITypeMigrationRulesVerb.ForType<T>()
        {
            if (!definition.HasTypeDefinition<T>())
            {
                definition.CreateTypeDefinition<T>(null);
            }
            return new TypeMigrationRules<T>(definition.GetTypeDefinition<T>());
        }

        IMigrationRulesVerb IMigrationRules.From(int major, int minor)
        {
            return ((IMigrationRules) this).From(new Version(major, minor));
        }

        IMigrationRulesVerb IMigrationRules.From(int major, int minor, int build)
        {
            return ((IMigrationRules)this).From(new Version(major, minor, build));
        }

        IMigrationRulesVerb IMigrationRules.From(int major, int minor, int build, int revision)
        {
            return ((IMigrationRules)this).From(new Version(major, minor, build, revision));
        }

        IMigrationRulesVerb IMigrationRules.From(Version version)
        {
            if (versionComparer.Compare(version, fromVersion) > 0)
            {
                //throw new MigrationConfigurationException(
                //    string.Format("Cannot add rules for version {0} within rules for version {1}. Must be earlier version.", version, fromVersion));
            }

            var versionQualifier = new FromVersionQualifier(version);
            definition.AddChild(versionQualifier, new MigrationDefinition(definition, version));
            return new MigrationScopeRulesVerb(this, definition, versionQualifier);
        }

        IMigrationRulesVerb IMigrationRules.AllPriorVersions()
        {
            var versionQualifier = new PriorToAndIncludingVersionQualifier();
            return new MigrationScopeRulesVerb(this, definition, versionQualifier);
        }
    }
}