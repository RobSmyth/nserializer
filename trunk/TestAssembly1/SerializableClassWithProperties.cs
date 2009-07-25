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

namespace NSerializer.TestAssembly1
{
    public class SerializableClassWithProperties
    {
        private int propertyA;

        /// <summary>
        /// Constructor used to deserialize the class
        /// </summary>
        public SerializableClassWithProperties()
        {
            propertyA = 0;
        }

        /// <summary>
        /// Constructor for test code
        /// </summary>
        public SerializableClassWithProperties(bool dummyArg)
        {
            propertyA = 3;
        }

        public SerializableClassWithProperties(int propertyA)
        {
            this.propertyA = propertyA;
        }

        public int PropertyA
        {
            get { return propertyA; }
            set { propertyA = value; }
        }

        public string PropertyB
        {
            get { return @"have a nice day {.\/><%$ )"; }
            set { }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            var otherObj = (SerializableClassWithProperties) obj;
            return
                PropertyA == otherObj.PropertyA &&
                PropertyB == otherObj.PropertyB;
        }
    }
}