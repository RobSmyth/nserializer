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


namespace NSerializer.XML.Readers
{
    public class XmlStreamReader
    {
        private const string delimiters = "<>";
        private const string endOfLineDelimiters = "\n\r";
        private readonly int bytesPerChar = Encoding.ASCII.GetBytes(new[] {'A'}).Length;
        private readonly Stream inputStream;

        public XmlStreamReader(Stream inputStream)
        {
            this.inputStream = inputStream;
        }

        public long Position
        {
            get { return inputStream.Position; }
        }

        public void Seek(int numberOfCharacters, SeekOrigin seekOrigin)
        {
            inputStream.Seek(bytesPerChar*numberOfCharacters, seekOrigin);
        }

        public bool Seek(string soughtText)
        {
            var found = false;

            var sougthTextBytes = soughtText.Length*bytesPerChar;

            while (true)
            {
                var buffer = new byte[sougthTextBytes*2];

                var readBytesCount = inputStream.Read(buffer, 0, buffer.Length);
                if (readBytesCount < sougthTextBytes)
                {
                    break;
                }

                var text = Encoding.ASCII.GetString(buffer);

                if (text.Contains(soughtText))
                {
                    found = true;
                    inputStream.Seek(-(readBytesCount - text.IndexOf(soughtText)),
                                     SeekOrigin.Current);
                    break;
                }

                if (readBytesCount != sougthTextBytes)
                {
                    inputStream.Seek(-sougthTextBytes, SeekOrigin.Current);
                }
            }

            return found;
        }

        public bool Seek(char soughtChar, char delimiter)
        {
            var found = false;

            while (true)
            {
                var buffer = new byte[bytesPerChar];

                if (inputStream.Read(buffer, 0, buffer.Length) < bytesPerChar)
                {
                    break;
                }

                var text = Encoding.ASCII.GetString(buffer);

                if (text[0] == delimiter)
                {
                    break;
                }

                if (text[0] == soughtChar)
                {
                    found = true;
                    inputStream.Seek(-1, SeekOrigin.Current);
                    break;
                }
            }

            return found;
        }

        public bool SeekFromEnd(string soughtText)
        {
            var found = false;

            var sougthTextBytes = bytesPerChar*soughtText.Length;

            inputStream.Seek(-sougthTextBytes, SeekOrigin.End);

            var buffer = new byte[sougthTextBytes*2];

            while (true)
            {
                inputStream.Seek(-sougthTextBytes*2, SeekOrigin.Current);

                if (inputStream.Read(buffer, 0, sougthTextBytes) == 0)
                {
                    break;
                }

                var text = Encoding.ASCII.GetString(buffer);
                if (text.Contains(soughtText))
                {
                    found = true;
                    inputStream.Seek(text.IndexOf(soughtText) - sougthTextBytes, SeekOrigin.Current);
                    break;
                }

                Array.Copy(buffer, 0, buffer, sougthTextBytes, sougthTextBytes);
            }

            return found;
        }

        public string ReadWord()
        {
            SkipDelimiters();

            var readWord = new StringBuilder();
            var charBuffer = new byte[bytesPerChar];

            while (inputStream.Read(charBuffer, 0, bytesPerChar) == bytesPerChar)
            {
                var readChar = Encoding.ASCII.GetString(charBuffer);
                if (!char.IsWhiteSpace(readChar, 0) && !delimiters.Contains(readChar))
                {
                    readWord.Append(Encoding.ASCII.GetString(charBuffer));
                }
                else
                {
                    inputStream.Seek(-1, SeekOrigin.Current);
                    break;
                }
            }

            return readWord.ToString();
        }

        public string ReadRestOfLine()
        {
            var lineBuffer = new StringBuilder();
            var charBuffer = new byte[bytesPerChar];

            while (inputStream.Read(charBuffer, 0, bytesPerChar) == bytesPerChar)
            {
                var readChar = Encoding.ASCII.GetString(charBuffer);
                if (endOfLineDelimiters.IndexOf(readChar) < 0)
                {
                    lineBuffer.Append(readChar);
                }
                else
                {
                    break;
                }
            }

            return lineBuffer.ToString();
        }

        public char Peek()
        {
            var charBuffer = new byte[bytesPerChar];
            inputStream.Read(charBuffer, 0, bytesPerChar);
            var readChar = Encoding.ASCII.GetString(charBuffer);
            inputStream.Seek(-bytesPerChar, SeekOrigin.Current);
            return readChar[0];
        }

        public string ReadInnerText()
        {
            var lineBuffer = new StringBuilder();
            var charBuffer = new byte[bytesPerChar];

            while (inputStream.Read(charBuffer, 0, bytesPerChar) == bytesPerChar)
            {
                var readChar = Encoding.ASCII.GetString(charBuffer);
                if (delimiters.IndexOf(readChar) < 0)
                {
                    lineBuffer.Append(readChar);
                }
                else
                {
                    inputStream.Seek(-bytesPerChar, SeekOrigin.Current);
                    break;
                }
            }

            return lineBuffer.ToString();
        }

        private void SkipDelimiters()
        {
            var charBuffer = new byte[bytesPerChar];

            while (inputStream.Read(charBuffer, 0, bytesPerChar) == bytesPerChar)
            {
                var readChar = Encoding.ASCII.GetString(charBuffer);
                if (!char.IsWhiteSpace(Encoding.ASCII.GetString(charBuffer), 0) && !delimiters.Contains(readChar))
                {
                    inputStream.Seek(-1, SeekOrigin.Current);
                    break;
                }
            }
        }
    }
}