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


namespace NSerializer.Migration
{
    internal class VersionComparer : IComparer<Version>
    {
        public int Compare(Version x, Version y)
        {
            int result;

            if (x.Major != y.Major)
            {
                result = x.Major - y.Major;
            }
            else if (x.Minor != y.Minor)
            {
                result = x.Minor - y.Minor;
            }
            else if (x.Build != y.Build)
            {
                result = x.Build - y.Build;
            }
            else
            {
                result = x.Revision - y.Revision;
            }

            return result;
        }
    }
}