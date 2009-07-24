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
using NSerializer.Migration.Fields;
using NSerializer.Migration.Types;


namespace NSerializer
{
    internal class NullTypeDefinition : ITypeDefinition
    {
        public void SetAlias(string alias)
        {
        }

        public void AddName(string matchingTypeName)
        {
        }

        public bool HasFieldDefinition(string fieldName)
        {
            return true;
        }

        public void CreateFieldDefinition(string fieldName, IFieldDefinition parentFieldDefinition)
        {
            throw new InvalidOperationException();
        }

        public IFieldDefinition FindFieldDefinition(string fieldName)
        {
            return new NullFieldDefinition();
        }

        public bool Matches(string name)
        {
            throw new NotImplementedException();
        }

        public string GetTypeName()
        {
            throw new NotImplementedException();
        }

        public IFieldDefinition GetFieldDefinition(string name)
        {
            throw new NotImplementedException();
        }

        public string GetMappedName()
        {
            throw new NotImplementedException();
        }
    }
}