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
using System.Collections.Generic;


namespace NSerializer.Migration.Fields
{
    internal class FieldDefinition : IFieldDefinition
    {
        private string fieldName;
        private readonly List<string> aliases = new List<string>();
        private bool renamed;

        public FieldDefinition(IFieldDefinition parentDefinition, string fieldName)
        {
            this.fieldName = fieldName;
        }

        public void Rename(string newName)
        {
            if (renamed)
            {
                throw new InvalidOperationException("Attempt to rename field twice in same scope");
            }
            renamed = true;
            aliases.Add(fieldName);
            fieldName = newName;
        }

        public object Convert(object value)
        {
            return value;
        }

        public void AddAction(IFieldAction action)
        {
            // obsolete
        }

        public void SetField(object instance, object value)
        {
            throw new NotImplementedException();
        }

        public string Name
        {
            get { return fieldName; }
        }

        public bool Ignored { get; set;}

        public bool Matches(string name)
        {
            return name == fieldName || aliases.Contains(name);
        }
    }
}