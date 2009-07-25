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
using NUnit.Framework;


namespace NSerializer.UATs
{
    public class TimeLimit : IDisposable
    {
        private readonly DateTime startTime;
        private readonly TimeSpan timeLimit;

        public TimeLimit(TimeSpan timeLimit)
        {
            startTime = DateTime.Now;
            this.timeLimit = timeLimit;
        }

        public void Dispose()
        {
            var elapsedTime = DateTime.Now - startTime;
            Assert.IsTrue(elapsedTime <= timeLimit, "Time limit exceeded. Took {0} seconds.", elapsedTime.TotalSeconds);
        }
    }
}