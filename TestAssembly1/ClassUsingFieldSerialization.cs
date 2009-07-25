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


namespace NSerializer.TestAssembly1
{
    public class ClassUsingFieldSerialization
    {
        private readonly int fieldB = 3;
        private readonly Guid fieldC = new Guid("{02312D4A-93DA-4310-B5D7-4A25FD8F1DE8}");
        private readonly TimeSpan fieldD = TimeSpan.FromMinutes(99);
        private readonly DateTime fieldE = new DateTime(2000, 1, 1, 1, 1, 1);
        private readonly Dictionary<string, int> fieldF = new Dictionary<string, int>();
        private int fieldA = 7;

        public ClassUsingFieldSerialization(int fieldB)
        {
            this.fieldB = fieldB;
            fieldD = TimeSpan.FromMinutes(1.25);
            fieldE = new DateTime(2008, 12, 31, 23, 59, 59);
            fieldF.Add("abc", 42);
        }

        public int Total
        {
            get { return fieldA + fieldB; }
        }

        public Guid FieldC
        {
            get { return fieldC; }
        }

        public TimeSpan FieldD
        {
            get { return fieldD; }
        }

        public DateTime FieldE
        {
            get { return fieldE; }
        }

        public Dictionary<string, int> FieldF
        {
            get { return fieldF; }
        }

        public void ChangeValues(int seed)
        {
            fieldA *= seed;
        }
    }
}