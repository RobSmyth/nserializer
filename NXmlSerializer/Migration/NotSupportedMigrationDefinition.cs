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
using NSerializer.Framework.Types;
using NSerializer.Migration.Types;


namespace NSerializer.Migration
{
    internal class NotSupportedMigrationDefinition : IMigrationDefinition
    {
        public ITypeFinder GetTypeMigrator(ITypeFinder typeFinder)
        {
            throw new InvalidOperationException();
        }

        public bool HasTypeDefinition<T>()
        {
            throw new InvalidOperationException();
        }

        public bool HasTypeDefinition(Type soughtType)
        {
            throw new InvalidOperationException();
        }

        public void CreateTypeDefinition<T>(ITypeDefinition parentTypeDefinition)
        {
            throw new InvalidOperationException("Cannot add types to a 'not supported' scope");
        }

        public ITypeDefinition GetTypeDefinition<T>()
        {
            throw new InvalidOperationException();
        }

        public void AddChild(IVersionQualifier qualifier, IMigrationDefinition childDefinition)
        {
            throw new InvalidOperationException();
        }

        public ITypeDefinition GetTypeDefinition(string name)
        {
            throw new NotImplementedException();
        }

        public ITypeDefinition GetTypeDefinition(Type type)
        {
            throw new NotImplementedException();
        }

        public ITypeNameMapper GetTypeNameMapper()
        {
            throw new NotImplementedException();
        }
    }
}