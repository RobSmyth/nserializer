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
using System.IO;
using System.Text;
using NUnit.Framework;


namespace NSerializer.XmlSerializer.Tests
{
    /// <summary>
    /// Integration tests using System.Xml.Serialization.XmlSerializer.
    /// </summary>
    [TestFixture]
    public class DoubleTests
    {
        private StringBuilder stringBuilder;
        private StringWriter stringWriter;

        [SetUp]
        public void SetUp()
        {
            stringBuilder = new StringBuilder();
            stringWriter = new StringWriter(stringBuilder);
        }

        [Test]
        public void Value_1_23_DoesSerialize()
        {
            double writtenValue = 1.23;

            System.Xml.Serialization.XmlSerializer writer = new System.Xml.Serialization.XmlSerializer(typeof (double));
            writer.Serialize(stringWriter, writtenValue);
            stringWriter.Close();

            StringReader stringReader = new StringReader(stringBuilder.ToString());
            System.Xml.Serialization.XmlSerializer reader = new System.Xml.Serialization.XmlSerializer(typeof (double));
            double readValue = (double) reader.Deserialize(stringReader);

            Assert.AreEqual(writtenValue, readValue);
        }

        [Test]
        public void MaxValue_DoesSerialize()
        {
            double writtenValue = double.MaxValue;

            System.Xml.Serialization.XmlSerializer writer = new System.Xml.Serialization.XmlSerializer(typeof (double));
            writer.Serialize(stringWriter, writtenValue);
            stringWriter.Close();

            StringReader stringReader = new StringReader(stringBuilder.ToString());
            System.Xml.Serialization.XmlSerializer reader = new System.Xml.Serialization.XmlSerializer(typeof (double));
            double readValue = (double) reader.Deserialize(stringReader);

            Assert.AreEqual(writtenValue, readValue);
        }

        [Test]
        public void Nan_DoesSerialize()
        {
            double writtenValue = double.NaN;

            System.Xml.Serialization.XmlSerializer writer = new System.Xml.Serialization.XmlSerializer(typeof (double));
            writer.Serialize(stringWriter, writtenValue);
            stringWriter.Close();

            StringReader stringReader = new StringReader(stringBuilder.ToString());
            System.Xml.Serialization.XmlSerializer reader = new System.Xml.Serialization.XmlSerializer(typeof (double));
            double readValue = (double) reader.Deserialize(stringReader);

            Assert.AreEqual(writtenValue, readValue);
        }

        [Test]
        public void PositiveInfinity_DoesSerialize()
        {
            double writtenValue = double.PositiveInfinity;

            System.Xml.Serialization.XmlSerializer writer = new System.Xml.Serialization.XmlSerializer(typeof (double));
            writer.Serialize(stringWriter, writtenValue);
            stringWriter.Close();

            StringReader stringReader = new StringReader(stringBuilder.ToString());
            System.Xml.Serialization.XmlSerializer reader = new System.Xml.Serialization.XmlSerializer(typeof (double));
            double readValue = (double) reader.Deserialize(stringReader);

            Assert.AreEqual(writtenValue, readValue);
        }

        [Test]
        [ExpectedException(typeof (OverflowException))]
        public void MaxValue_ThrowsException_WhenParsing()
        {
            double readValue = double.Parse(double.MaxValue.ToString());
            Assert.AreEqual(double.MaxValue, readValue);
        }

        [Test]
        [ExpectedException(typeof (OverflowException))]
        public void MinValue_ThrowsException_WhenParsing()
        {
            double readValue = double.Parse(double.MinValue.ToString());
            Assert.AreEqual(double.MinValue, readValue);
        }

        [Test]
        public void NanValue_ThrowsException_WhenParsing()
        {
            double readValue = double.Parse(double.NaN.ToString());
            Assert.AreEqual(double.NaN, readValue);
        }
    }
}