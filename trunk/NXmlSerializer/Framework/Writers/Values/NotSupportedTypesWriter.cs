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
using System.Reflection;
using NSerializer.Exceptions;
using NSerializer.XML.Document.Writers;


namespace NSerializer.Framework.Writers.Values
{
    public class NotSupportedTypesWriter : IObjectWriter
    {
        private readonly Dictionary<Type, int> notSupportedTypes = new Dictionary<Type, int>();

        public NotSupportedTypesWriter()
        {
            notSupportedTypes.Add(typeof (Pointer), 0);
        }

        public bool CanWrite(object instance, Type referencedAsType)
        {
            if (instance != null && notSupportedTypes.ContainsKey(instance.GetType()))
            {
                throw new NSerializerException(
                    string.Format("Error serilizing object. The serializer does not support objects of type '{0}'.",
                                  instance.GetType()));
            }
            return false;
        }

        public void Write(object instance, INodeWriter parentNode, Type referencedAsType)
        {
        }
    }
}