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
using NSerializer.Exceptions;
using NSerializer.XML.Readers;


namespace NSerializer.Framework.Readers.Values
{
    public class ObjectReferenceReader : IObjectReader
    {
        private readonly IReadObjectsCache readObjects;

        public ObjectReferenceReader(IReadObjectsCache readObjects)
        {
            this.readObjects = readObjects;
        }

        public bool CanRead(INXmlElementReader nodeReader)
        {
            return nodeReader.Name == "objref";
        }

        public object Get(INXmlElementReader nodeReader)
        {
            var objectID = nodeReader.Attributes.GetInteger("ID");
            object readObject;
            try
            {
                readObject = readObjects.Get(objectID);
            }
            catch (Exception exception)
            {
                throw new UnableToReadXMLTextException(
                    string.Format(
                        "Exception thrown while reading referenced object with ID {0}. No object with that ID in the read objects cache.",
                        objectID),
                    exception);
            }
            return readObject;
        }
    }
}