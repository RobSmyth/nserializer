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
using System.Runtime.Remoting;
using System.Text;


namespace NSerializer.Wrappers
{
    internal class StreamWriter : IStreamWriter
    {
        private readonly System.IO.StreamWriter writer;

        public StreamWriter(System.IO.StreamWriter writer)
        {
            this.writer = writer;
        }

        public IFormatProvider FormatProvider
        {
            get { return writer.FormatProvider; }
        }

        public string NewLine
        {
            get { return writer.NewLine; }
            set { writer.NewLine = value; }
        }

        public bool AutoFlush
        {
            get { return writer.AutoFlush; }
            set { writer.AutoFlush = value; }
        }

        public Stream BaseStream
        {
            get { return writer.BaseStream; }
        }

        public Encoding Encoding
        {
            get { return writer.Encoding; }
        }

        public object GetLifetimeService()
        {
            return writer.GetLifetimeService();
        }

        public object InitializeLifetimeService()
        {
            return writer.InitializeLifetimeService();
        }

        public ObjRef CreateObjRef(Type requestedType)
        {
            return writer.CreateObjRef(requestedType);
        }

        public void Dispose()
        {
            writer.Dispose();
        }

        public void Write(bool value)
        {
            writer.Write(value);
        }

        public void Write(int value)
        {
            writer.Write(value);
        }

        public void Write(uint value)
        {
            writer.Write(value);
        }

        public void Write(long value)
        {
            writer.Write(value);
        }

        public void Write(ulong value)
        {
            writer.Write(value);
        }

        public void Write(float value)
        {
            writer.Write(value);
        }

        public void Write(double value)
        {
            writer.Write(value);
        }

        public void Write(decimal value)
        {
            writer.Write(value);
        }

        public void Write(object value)
        {
            writer.Write(value);
        }

        public void Write(string format, object arg0)
        {
            writer.Write(format, arg0);
        }

        public void Write(string format, object arg0, object arg1)
        {
            writer.Write(format, arg0, arg1);
        }

        public void Write(string format, object arg0, object arg1, object arg2)
        {
            writer.Write(format, arg0, arg1, arg2);
        }

        public void Write(string format, params object[] arg)
        {
            writer.Write(format, arg);
        }

        public void WriteLine()
        {
            writer.WriteLine();
        }

        public void WriteLine(char value)
        {
            writer.WriteLine(value);
        }

        public void WriteLine(char[] buffer)
        {
            writer.WriteLine(buffer);
        }

        public void WriteLine(char[] buffer, int index, int count)
        {
            writer.WriteLine(buffer, index, count);
        }

        public void WriteLine(bool value)
        {
            writer.WriteLine(value);
        }

        public void WriteLine(int value)
        {
            writer.WriteLine(value);
        }

        public void WriteLine(uint value)
        {
            writer.WriteLine(value);
        }

        public void WriteLine(long value)
        {
            writer.WriteLine(value);
        }

        public void WriteLine(ulong value)
        {
            writer.WriteLine(value);
        }

        public void WriteLine(float value)
        {
            writer.WriteLine(value);
        }

        public void WriteLine(double value)
        {
            writer.WriteLine(value);
        }

        public void WriteLine(decimal value)
        {
            writer.WriteLine(value);
        }

        public void WriteLine(string value)
        {
            writer.WriteLine(value);
        }

        public void WriteLine(object value)
        {
            writer.WriteLine(value);
        }

        public void WriteLine(string format, object arg0)
        {
            writer.WriteLine(format, arg0);
        }

        public void WriteLine(string format, object arg0, object arg1)
        {
            writer.WriteLine(format, arg0, arg1);
        }

        public void WriteLine(string format, object arg0, object arg1, object arg2)
        {
            writer.WriteLine(format, arg0, arg1, arg2);
        }

        public void WriteLine(string format, params object[] arg)
        {
            writer.WriteLine(format, arg);
        }

        public void Close()
        {
            writer.Close();
        }

        public void Flush()
        {
            writer.Flush();
        }

        public void Write(char value)
        {
            writer.Write(value);
        }

        public void Write(char[] buffer)
        {
            writer.Write(buffer);
        }

        public void Write(char[] buffer, int index, int count)
        {
            writer.Write(buffer, index, count);
        }

        public void Write(string value)
        {
            writer.Write(value);
        }
    }
}