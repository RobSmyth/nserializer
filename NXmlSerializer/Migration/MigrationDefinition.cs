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
using System.Collections.Generic;
using NSerializer.Exceptions;
using NSerializer.Framework.Types;
using NSerializer.Migration.Types;


namespace NSerializer.Migration
{
    internal class MigrationDefinition : IMigrationDefinition
    {
        private readonly SortedList<IVersionQualifier, IMigrationDefinition> childDefinitions =
            new SortedList<IVersionQualifier, IMigrationDefinition>(new VersionQualifierComparer());

        private readonly Version fromVersion;

        private readonly SortedList<IVersionQualifier, IMigrationDefinition> outOfScopeChildDefinitions =
            new SortedList<IVersionQualifier, IMigrationDefinition>(new VersionQualifierComparer());

        private readonly IMigrationDefinition parentDefinition;
        private readonly Dictionary<Type, ITypeDefinition> typeDefinitions = new Dictionary<Type, ITypeDefinition>();
        private readonly IComparer<Version> versionComparer = new VersionComparer();

        public MigrationDefinition(IMigrationDefinition parentDefinition, Version fromVersion)
        {
            this.parentDefinition = parentDefinition;
            this.fromVersion = fromVersion;
        }

        public ITypeFinder GetTypeMigrator(ITypeFinder typeFinder)
        {
            return new MigrationTypeFinder(typeFinder, this);
        }

        public ITypeNameMapper GetTypeNameMapper()
        {
            return new TypeNameMapper(this);
        }

        public bool HasTypeDefinition<T>()
        {
            return HasTypeDefinition(typeof (T));
        }

        public bool HasTypeDefinition(Type soughtType)
        {
            return typeDefinitions.ContainsKey(soughtType);
        }

        public void CreateTypeDefinition<T>(ITypeDefinition parentTypeDefinition)
        {
            typeDefinitions.Add(typeof (T), new TypeDefinition<T>(parentTypeDefinition));
        }

        public ITypeDefinition GetTypeDefinition<T>()
        {
            return HasTypeDefinition<T>() ? typeDefinitions[typeof (T)] : parentDefinition.GetTypeDefinition<T>();
        }

        public ITypeDefinition GetTypeDefinition(string name)
        {
            ITypeDefinition foundDefinition = null;

            foreach (var item in typeDefinitions)
            {
                if (item.Value.Matches(name))
                {
                    var childDefinition = parentDefinition.GetTypeDefinition(name) ?? new NullTypeDefinition(name);
                    foundDefinition = new AggregateTypeDefinition(item.Value, childDefinition);
                    break;
                }
            }

            if (foundDefinition == null)
            {
                foundDefinition = parentDefinition.GetTypeDefinition(name);
            }

            return foundDefinition;
        }

        public ITypeDefinition GetTypeDefinition(Type type)
        {
            return GetTypeDefinition(type.Name);
        }

        public void AddChild(IVersionQualifier qualifier, IMigrationDefinition childDefinition)
        {
            var childList =
                (versionComparer.Compare(fromVersion, qualifier.StartVersion) >= 0)
                    ? childDefinitions
                    : outOfScopeChildDefinitions;

            try
            {
                childList.Add(qualifier, childDefinition);
            }
            catch (ArgumentException exception)
            {
                throw new MigrationConfigurationException(
                    string.Format("Duplicate version {0} scopes", qualifier.StartVersion), exception);
            }
        }

        public void AddRules(IMigrationRulesBuilder migrationRulesBuilder)
        {
            IMigrationRules migrationRules = new MigrationRules(this, fromVersion);
            migrationRulesBuilder.Build(migrationRules);
        }
    }
}