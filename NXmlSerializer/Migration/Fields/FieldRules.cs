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

using NSerializer.Migration.Types;


namespace NSerializer.Migration.Fields
{
    internal class FieldRules : IFieldRules
    {
        private readonly IFieldDefinition fieldDefinition;
        private readonly ITypeMigrationRules typeMigrationRules;

        public FieldRules(IFieldDefinition fieldDefinition, ITypeMigrationRules typeMigrationRules)
        {
            this.fieldDefinition = fieldDefinition;
            this.typeMigrationRules = typeMigrationRules;
        }

        public ITypeMigrationRules Ignore()
        {
            fieldDefinition.AddAction(new IgnoreFieldAction());
            return typeMigrationRules;
        }

        public ITypeMigrationRules RenamedTo(string newFieldName)
        {
            fieldDefinition.AddAction(new RenameFieldAction(newFieldName));
            return typeMigrationRules;
        }

        public ITypeMigrationRules SetTo(object value)
        {
            fieldDefinition.AddAction(new SetFieldAction(value));
            return typeMigrationRules;
        }
    }
}