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

using System.IO;
using NSerializer.Exceptions;
using NSerializer.TestAssembly1;
using NSerializer.UATs.Contexts;
using NUnit.Framework;


namespace NSerializer.UATs
{
    [TestFixture]
    public class ReaderUATs : SerializeContext
    {
        [SetUp]
        public override void SetUp()
        {
            typeSeedAssembly = typeof (SerializableClassWithProperties).Assembly;
        }

        [Test]
        [ExpectedException(typeof (UnableToReadXMLTextException))]
        public void ThrowsExceptionIfReadingEmptyXmlString()
        {
            var xmlText = string.Empty;
            var stringReader = new StringReader(xmlText);

            var reader = new NXmlReader(typeSeedAssembly);

            reader.Read<object>(GetStream(xmlText));
        }

        [Test]
        [ExpectedException(typeof (UnableToReadXMLTextException))]
        public void ThrowsExceptionIfReadingNullStream()
        {
            var reader = new NXmlReader(typeSeedAssembly);

            reader.Read<object>(null);
        }

        [Test]
        [ExpectedException(typeof (UnableToReadXMLTextException))]
        public void ThrowsExceptionOnInvalidXmlStringFormat()
        {
            ReadXmlText<object>("invalid xml content");
        }
    }
}