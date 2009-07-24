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

using NSerializer.Framework;


namespace NSerializer.TestAssembly1
{
    public class SerializableClassWithAnInterface : IMyInterface
    {
        private int propertyA;

        /// <summary>
        /// Constructor that will be used when deserializing
        /// </summary>
        public SerializableClassWithAnInterface()
        {
            propertyA = 0;
        }

        /// <summary>
        /// Constructor for test code to make object with different property value
        /// </summary>
        /// <param name="dummyParam"></param>
        public SerializableClassWithAnInterface(bool dummyParam)
        {
            propertyA = 6;
        }

        public string PropertyB
        {
            get { return @"A second property"; }
            set { }
        }

        public string PropertyC
        {
            get { return @"A get only property that ought not be serialized."; }
        }

        #region IMyInterface Members

        public int PropertyA
        {
            get { return propertyA; }
            set { propertyA = value; }
        }

        #endregion

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            SerializableClassWithAnInterface otherObj = (SerializableClassWithAnInterface) obj;
            return
                PropertyA == otherObj.PropertyA &&
                PropertyB == otherObj.PropertyB &&
                PropertyC == otherObj.PropertyC;
        }
    }
}