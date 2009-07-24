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
using NSerializer.TestAssembly1;


namespace NSerializer.Tests.XML.Targets
{
    public class SerializableClassWithArrayProperty
    {
        private int[] propertyA;
        private SerializableClassWithoutProperties[] propertyB;

        /// <summary>
        /// Constructor used to deserialise the class.
        /// </summary>
        private SerializableClassWithArrayProperty()
        {
        }

        /// <summary>
        /// Constructor used to get test object of known state.
        /// </summary>
        /// <param name="dummyArg"></param>
        public SerializableClassWithArrayProperty(bool dummyArg)
        {
            propertyA = new int[] {1, 2, 3};
            propertyB = new SerializableClassWithoutProperties[]
                            {
                                new SerializableClassWithoutProperties(),
                                new SerializableClassWithoutProperties(),
                            };
        }

        public int[] PropertyA
        {
            get { return propertyA; }
            set { propertyA = value; }
        }

        public SerializableClassWithoutProperties[] PropertyB
        {
            get { return propertyB; }
            set { propertyB = value; }
        }
    }
}