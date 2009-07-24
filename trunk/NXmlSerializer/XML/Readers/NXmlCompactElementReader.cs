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


namespace NSerializer.XML.Readers
{
    public class NXmlCompactElementReader : INXmlElementReader
    {
        public NXmlCompactElementReader(NXmlNodeAttributes attributes, string name)
        {
            Attributes = attributes;
            Name = name;
        }

        public NXmlNodeAttributes Attributes { get; private set; }

        public string InnerText
        {
            get { throw new NotImplementedException(); }
        }

        public string Name { get; set; }

        public INXmlElementReader GetNextChildNode(string name)
        {
            return null;
        }

        public INXmlElementReader GetNextChildNode()
        {
            return null;
        }

        public string GetPayload()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
        }
    }
}