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
using System.Text;
using System.Text.RegularExpressions;
using NSerializer.Types;
using NSerializer.XML.Readers;
using NUnit.Framework;
using System.IO;
using NMock2;
using NSerializer.Framework.Document;


namespace NSerializer.Tests.XML.Readers
{
    [TestFixture]
    public class MetaDataReaderTests : NXmlTestFixtureBase
    {
        private MetaDataReader reader;
        private ITypeFinder typeFinder;

        protected override void SetUp()
        {
            typeFinder = NewMock<ITypeFinder>();

            reader = new MetaDataReader(typeFinder);
        }

        [Test]
        public void Test01()
        {
            var xml =
                @"
<NSerializer version='4.0'>
	<c type='_0' ID='0'>
		<members>
			<f name='target'>
				<array type='_1' ID='1'>
					<items>
						<c type='_2' ID='2'>
							<members>
								<f name='c'>
									<array type='_3' ID='3'>
										<items>
											<c type='_4' ID='4'>
												<members>
													<f name='data'>
														<array type='_5' ID='5'>
															<items>
																<UInt32 value='1000' />
																<UInt32 value='1000' />
																<UInt32 value='1000' />
																<UInt32 value='1000' />
																<UInt32 value='1000' />
															</items>
														</array>
													</f>
												</members>
											</c>
											<c type='_4' ID='6'>
												<members>
													<f name='data'>
														<array type='_5' ID='7'>
															<items>
																<UInt32 value='1000' />
																<UInt32 value='1000' />
																<UInt32 value='1000' />
																<UInt32 value='1000' />
																<UInt32 value='1000' />
															</items>
														</array>
													</f>
												</members>
											</c>
										</items>
									</array>
								</f>
							</members>
						</c>
						<c type='_2' ID='8'>
							<members>
								<f name='c'>
									<array type='_3' ID='9'>
										<items>
											<c type='_4' ID='10'>
												<members>
													<f name='data'>
														<array type='_5' ID='11'>
															<items>
																<UInt32 value='1000' />
																<UInt32 value='1000' />
																<UInt32 value='1000' />
																<UInt32 value='1000' />
																<UInt32 value='1000' />
															</items>
														</array>
													</f>
												</members>
											</c>
											<c type='_4' ID='12'>
												<members>
													<f name='data'>
														<array type='_5' ID='13'>
															<items>
																<UInt32 value='1000' />
																<UInt32 value='1000' />
																<UInt32 value='1000' />
																<UInt32 value='1000' />
																<UInt32 value='1000' />
															</items>
														</array>
													</f>
												</members>
											</c>
										</items>
									</array>
								</f>
							</members>
						</c>
					</items>
				</array>
			</f>
		</members>
	</c>
	<c type='NSerializer.Framework.Document.MetaData' ID='0'>
		<members>
			<f name='dateTimeCreated'>
				<datetime value='20/07/2009 12:51:58 PM' />
			</f>
			<f name='machineName'>
				<string value='ROBERT-PC' />
			</f>
			<f name='serializerVersion'>
				<version value='1.0.1.585' />
			</f>
			<f name='targetVersion'>
				<version value='1.0.0.0' />
			</f>
			<f name='typeNames'>
				<array type='System.String[]' ID='1'>
					<items>
						<string value='NSerializer.Framework.Document.Payload' />
						<string value='NSerializer.UATs.BinaryFormatting.SaveAsBinaryUATs+B[]' />
						<string value='NSerializer.UATs.BinaryFormatting.SaveAsBinaryUATs+B' />
						<string value='NSerializer.UATs.BinaryFormatting.SaveAsBinaryUATs+C[]' />
						<string value='NSerializer.UATs.BinaryFormatting.SaveAsBinaryUATs+C' />
						<string value='System.UInt32[]' />
					</items>
				</array>
			</f>
		</members>
	</c>
</NSerializer>"
                    .Replace("'", "\"");

            var stream = new MemoryStream(Encoding.ASCII.GetBytes(xml));

            Expect.Once.On(typeFinder).Method("Get").With("NSerializer.Framework.Document.MetaData").Will(Return.Value(typeof(MetaData)));
            Expect.Once.On(typeFinder).Method("Get").With("System.String[]").Will(Return.Value(typeof(System.String[])));

            var metaData = reader.Read(new XmlStreamReader(stream));

            Assert.IsNotNull(metaData);
            Assert.AreEqual("ROBERT-PC", metaData.MachineName);
            Assert.AreEqual(6, metaData.TypeNames.Length);
            Assert.AreEqual("NSerializer.UATs.BinaryFormatting.SaveAsBinaryUATs+C", metaData.TypeNames[4]);
        }

        [Test]
        public void Spike()
        {
            var regex = new Regex("(?<attributeAssignment> (?<name>[^\\s=]+) =\" (?<value>.+?) \" )", RegexOptions.IgnorePatternWhitespace);

            var matches = regex.Matches(" name='myName'  type='myType'>".Replace("'", "\""));

            Assert.AreEqual(2, matches.Count);
            Assert.AreEqual("name=\"myName\"", matches[0].Value);
            Assert.AreEqual("name", matches[0].Groups["name"].Value);
            Assert.AreEqual("myName", matches[0].Groups["value"].Value);

            Assert.AreEqual("type=\"myType\"", matches[1].Value);
            Assert.AreEqual("type", matches[1].Groups["name"].Value);
            Assert.AreEqual("myType", matches[1].Groups["value"].Value);
        }
    }
}