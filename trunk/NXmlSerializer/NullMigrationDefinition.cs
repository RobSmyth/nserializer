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
using NSerializer.Migration;
using NSerializer.Migration.Types;
using NSerializer.TypeFinders;


namespace NSerializer
{
    internal class NullMigrationDefinition : IMigrationDefinition
    {
        public ITypeMigrator GetTypeMigrator(ITypeFinder typeFinder)
        {
            throw new InvalidOperationException();
        }

        public bool HasTypeDefinition<T>()
        {
            return false;
        }

        public void CreateTypeDefinition<T>(ITypeDefinition parentTypeDefinition)
        {
            throw new NotImplementedException();
        }

        public void AddChild(IVersionQualifier qualifier, IMigrationDefinition childDefinition)
        {
            throw new InvalidOperationException();
        }

        public ITypeDefinition GetTypeDefinition(string name)
        {
            return null;
        }

        public ITypeDefinition GetTypeDefinition<T>()
        {
            return null;
        }

        public ITypeDefinition GetTypeDefinition(Type type)
        {
            return null;
        }

        public ITypeNameMapper GetTypeNameMapper()
        {
            throw new NotImplementedException();
        }

        public bool HasTypeDefinition(Type soughtType)
        {
            return false;
        }
    }
}