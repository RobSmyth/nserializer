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

using System.Collections.Generic;
using NSerializer.Framework.Document;
using NSerializer.Framework.Readers;


namespace NSerializer.XML.Readers.Members
{
    public class MemberReader : IMemberReader
    {
        private readonly List<IMemberReader> memberReaders = new List<IMemberReader>();

        public MemberReader(params IMemberReader[] readers)
        {
            memberReaders.AddRange(readers);
        }

        public bool CanRead(INXmlElementReader nodeReader)
        {
            var canRead = false;
            foreach (var typeMemberReader in memberReaders)
            {
                canRead = typeMemberReader.CanRead(nodeReader);
                if (canRead)
                {
                    break;
                }
            }
            return canRead;
        }

        public IMemberValue Read(INXmlElementReader nodeReader, IDataType type)
        {
            var memberValues = new List<IMemberValue>();
            foreach (var typeMemberReader in memberReaders)
            {
                if (typeMemberReader.CanRead(nodeReader))
                {
                    memberValues.Add(typeMemberReader.Read(nodeReader, type));
                    break;
                }
            }

            return new MembersValue(memberValues.ToArray());
        }
    }
}