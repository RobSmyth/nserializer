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
using System.Collections.Generic;


namespace NSerializer.Migration.Fields
{
    internal class FieldDefinition : IFieldDefinition
    {
        private readonly List<IFieldAction> migrationActions = new List<IFieldAction>();

        public FieldDefinition(IFieldDefinition parentDefinition)
        {
        }

        public void AddAction(IFieldAction action)
        {
            if (migrationActions.Contains(action))
            {
                throw new MigrationConfigurationException("Cannot add action twice to smae field definition.");
            }
            migrationActions.Add(action);
        }

        public void SetField(object instance, object value)
        {
            throw new NotImplementedException();
        }
    }
}