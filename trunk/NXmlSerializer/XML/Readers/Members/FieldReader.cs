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
using System.Reflection;
using NSerializer.Exceptions;
using NSerializer.Framework.Document;
using NSerializer.Framework.Readers;


namespace NSerializer.XML.Readers.Members
{
    public class FieldReader : IMemberReader
    {
        private readonly IObjectReader objectReader;

        public FieldReader(IObjectReader objectReader)
        {
            this.objectReader = objectReader;
        }

        public bool CanRead(INXmlElementReader nodeReader)
        {
            return nodeReader.Name == "f";
        }

        public IMemberValue Read(INXmlElementReader nodeReader, IDataType type)
        {
            var fieldName = nodeReader.Attributes.Get("name");

            var fieldInfo = type.GetField(fieldName,
                                          BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            if (fieldInfo == null)
            {
                throw new UnableToReadXMLTextException(
                    string.Format("Unable to find field '{0}'.", fieldName));
            }

            IMemberValue value;
            using (var valueNode = nodeReader.GetNextChildNode())
            {
                value = new FieldValue(fieldInfo, objectReader.Get(valueNode));
            }

            return value;
        }
    }
}