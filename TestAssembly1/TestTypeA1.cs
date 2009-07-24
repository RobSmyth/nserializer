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

using NSerializer.TestAssembly2;


namespace NSerializer.TestAssembly1
{
    /// <summary>
    /// The sole purpose of this class is to exercise the reference to a
    /// type in a referenced test types assembly. At run-time .Net will
    /// only list referenced assemblies as those that are actually used.
    /// </summary>
    public class TestTypeA1
    {
        public TestTypeA2 referencedType;
    }
}