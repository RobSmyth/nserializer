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
using System.Text.RegularExpressions;
using NSerializer.Types;


namespace NSerializer.Framework.Types
{
    public class TypeNameDemangler
    {
        private readonly Type type;

        public TypeNameDemangler(Type type)
        {
            this.type = type;
        }

        public override string ToString()
        {
            var typeName = type.FullName;
            if (typeName.Contains("`"))
            {
                var regex = new Regex(@",[^\[]*?\]");
                typeName = regex.Replace(typeName, "]");
            }
            else
            {
                var attributes =
                    (SerializeNameAttribute[]) type.GetCustomAttributes(typeof (SerializeNameAttribute), false);
                if (attributes.Length == 1)
                {
                    typeName = attributes[0].Alias;
                }
            }
            return typeName;
        }
    }
}