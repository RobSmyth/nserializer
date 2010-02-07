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


namespace NSerializer.Logging
{
    public class NullLogger : ILogger
    {
        public string Name
        {
            get { return string.Empty; }
        }

        public bool IsTraceEnabled
        {
            get { return false; }
        }

        public bool IsDebugEnabled
        {
            get { return false; }
        }

        public bool IsInfoEnabled
        {
            get { return false; }
        }

        public bool IsWarnEnabled
        {
            get { return false; }
        }

        public bool IsErrorEnabled
        {
            get { return false; }
        }

        public bool IsFatalEnabled
        {
            get { return false; }
        }

        public void Trace(string message)
        {
        }

        public void Trace(object obj)
        {
        }

        public void Trace(IFormatProvider formatProvider, object obj)
        {
        }

        public void TraceException(string message, Exception exception)
        {
        }

        public void Trace(IFormatProvider formatProvider, string message, params object[] args)
        {
        }

        public void Trace(string message, params object[] args)
        {
        }

        public void Trace(string message, object arg1, object arg2)
        {
        }

        public void Trace(string message, object arg1, object arg2, object arg3)
        {
        }

        public void Trace(IFormatProvider formatProvider, string message, bool argument)
        {
        }

        public void Trace(string message, bool argument)
        {
        }

        public void Trace(IFormatProvider formatProvider, string message, char argument)
        {
        }

        public void Trace(string message, char argument)
        {
        }

        public void Trace(IFormatProvider formatProvider, string message, byte argument)
        {
        }

        public void Trace(string message, byte argument)
        {
        }

        public void Trace(IFormatProvider formatProvider, string message, string argument)
        {
        }

        public void Trace(string message, string argument)
        {
        }

        public void Trace(IFormatProvider formatProvider, string message, int argument)
        {
        }

        public void Trace(string message, int argument)
        {
        }

        public void Trace(IFormatProvider formatProvider, string message, long argument)
        {
        }

        public void Trace(string message, long argument)
        {
        }

        public void Trace(IFormatProvider formatProvider, string message, float argument)
        {
        }

        public void Trace(string message, float argument)
        {
        }

        public void Trace(IFormatProvider formatProvider, string message, double argument)
        {
        }

        public void Trace(string message, double argument)
        {
        }

        public void Trace(IFormatProvider formatProvider, string message, decimal argument)
        {
        }

        public void Trace(string message, decimal argument)
        {
        }

        public void Trace(IFormatProvider formatProvider, string message, object argument)
        {
        }

        public void Trace(string message, object argument)
        {
        }

        public void Trace(IFormatProvider formatProvider, string message, sbyte argument)
        {
        }

        public void Trace(string message, sbyte argument)
        {
        }

        public void Trace(IFormatProvider formatProvider, string message, uint argument)
        {
        }

        public void Trace(string message, uint argument)
        {
        }

        public void Trace(IFormatProvider formatProvider, string message, ulong argument)
        {
        }

        public void Trace(string message, ulong argument)
        {
        }

        public void Debug(string message)
        {
        }

        public void Debug(object obj)
        {
        }

        public void Debug(IFormatProvider formatProvider, object obj)
        {
        }

        public void DebugException(string message, Exception exception)
        {
        }

        public void Debug(IFormatProvider formatProvider, string message, params object[] args)
        {
        }

        public void Debug(string message, params object[] args)
        {
        }

        public void Debug(string message, object arg1, object arg2)
        {
        }

        public void Debug(string message, object arg1, object arg2, object arg3)
        {
        }

        public void Debug(IFormatProvider formatProvider, string message, bool argument)
        {
        }

        public void Debug(string message, bool argument)
        {
        }

        public void Debug(IFormatProvider formatProvider, string message, char argument)
        {
        }

        public void Debug(string message, char argument)
        {
        }

        public void Debug(IFormatProvider formatProvider, string message, byte argument)
        {
        }

        public void Debug(string message, byte argument)
        {
        }

        public void Debug(IFormatProvider formatProvider, string message, string argument)
        {
        }

        public void Debug(string message, string argument)
        {
        }

        public void Debug(IFormatProvider formatProvider, string message, int argument)
        {
        }

        public void Debug(string message, int argument)
        {
        }

        public void Debug(IFormatProvider formatProvider, string message, long argument)
        {
        }

        public void Debug(string message, long argument)
        {
        }

        public void Debug(IFormatProvider formatProvider, string message, float argument)
        {
        }

        public void Debug(string message, float argument)
        {
        }

        public void Debug(IFormatProvider formatProvider, string message, double argument)
        {
        }

        public void Debug(string message, double argument)
        {
        }

        public void Debug(IFormatProvider formatProvider, string message, decimal argument)
        {
        }

        public void Debug(string message, decimal argument)
        {
        }

        public void Debug(IFormatProvider formatProvider, string message, object argument)
        {
        }

        public void Debug(string message, object argument)
        {
        }

        public void Debug(IFormatProvider formatProvider, string message, sbyte argument)
        {
        }

        public void Debug(string message, sbyte argument)
        {
        }

        public void Debug(IFormatProvider formatProvider, string message, uint argument)
        {
        }

        public void Debug(string message, uint argument)
        {
        }

        public void Debug(IFormatProvider formatProvider, string message, ulong argument)
        {
        }

        public void Debug(string message, ulong argument)
        {
        }

        public void Info(string message)
        {
        }

        public void Info(object obj)
        {
        }

        public void Info(IFormatProvider formatProvider, object obj)
        {
        }

        public void InfoException(string message, Exception exception)
        {
        }

        public void Info(IFormatProvider formatProvider, string message, params object[] args)
        {
        }

        public void Info(string message, params object[] args)
        {
        }

        public void Info(string message, object arg1, object arg2)
        {
        }

        public void Info(string message, object arg1, object arg2, object arg3)
        {
        }

        public void Info(IFormatProvider formatProvider, string message, bool argument)
        {
        }

        public void Info(string message, bool argument)
        {
        }

        public void Info(IFormatProvider formatProvider, string message, char argument)
        {
        }

        public void Info(string message, char argument)
        {
        }

        public void Info(IFormatProvider formatProvider, string message, byte argument)
        {
        }

        public void Info(string message, byte argument)
        {
        }

        public void Info(IFormatProvider formatProvider, string message, string argument)
        {
        }

        public void Info(string message, string argument)
        {
        }

        public void Info(IFormatProvider formatProvider, string message, int argument)
        {
        }

        public void Info(string message, int argument)
        {
        }

        public void Info(IFormatProvider formatProvider, string message, long argument)
        {
        }

        public void Info(string message, long argument)
        {
        }

        public void Info(IFormatProvider formatProvider, string message, float argument)
        {
        }

        public void Info(string message, float argument)
        {
        }

        public void Info(IFormatProvider formatProvider, string message, double argument)
        {
        }

        public void Info(string message, double argument)
        {
        }

        public void Info(IFormatProvider formatProvider, string message, decimal argument)
        {
        }

        public void Info(string message, decimal argument)
        {
        }

        public void Info(IFormatProvider formatProvider, string message, object argument)
        {
        }

        public void Info(string message, object argument)
        {
        }

        public void Info(IFormatProvider formatProvider, string message, sbyte argument)
        {
        }

        public void Info(string message, sbyte argument)
        {
        }

        public void Info(IFormatProvider formatProvider, string message, uint argument)
        {
        }

        public void Info(string message, uint argument)
        {
        }

        public void Info(IFormatProvider formatProvider, string message, ulong argument)
        {
        }

        public void Info(string message, ulong argument)
        {
        }

        public void Warn(string message)
        {
        }

        public void Warn(object obj)
        {
        }

        public void Warn(IFormatProvider formatProvider, object obj)
        {
        }

        public void WarnException(string message, Exception exception)
        {
        }

        public void Warn(IFormatProvider formatProvider, string message, params object[] args)
        {
        }

        public void Warn(string message, params object[] args)
        {
        }

        public void Warn(string message, object arg1, object arg2)
        {
        }

        public void Warn(string message, object arg1, object arg2, object arg3)
        {
        }

        public void Warn(IFormatProvider formatProvider, string message, bool argument)
        {
        }

        public void Warn(string message, bool argument)
        {
        }

        public void Warn(IFormatProvider formatProvider, string message, char argument)
        {
        }

        public void Warn(string message, char argument)
        {
        }

        public void Warn(IFormatProvider formatProvider, string message, byte argument)
        {
        }

        public void Warn(string message, byte argument)
        {
        }

        public void Warn(IFormatProvider formatProvider, string message, string argument)
        {
        }

        public void Warn(string message, string argument)
        {
        }

        public void Warn(IFormatProvider formatProvider, string message, int argument)
        {
        }

        public void Warn(string message, int argument)
        {
        }

        public void Warn(IFormatProvider formatProvider, string message, long argument)
        {
        }

        public void Warn(string message, long argument)
        {
        }

        public void Warn(IFormatProvider formatProvider, string message, float argument)
        {
        }

        public void Warn(string message, float argument)
        {
        }

        public void Warn(IFormatProvider formatProvider, string message, double argument)
        {
        }

        public void Warn(string message, double argument)
        {
        }

        public void Warn(IFormatProvider formatProvider, string message, decimal argument)
        {
        }

        public void Warn(string message, decimal argument)
        {
        }

        public void Warn(IFormatProvider formatProvider, string message, object argument)
        {
        }

        public void Warn(string message, object argument)
        {
        }

        public void Warn(IFormatProvider formatProvider, string message, sbyte argument)
        {
        }

        public void Warn(string message, sbyte argument)
        {
        }

        public void Warn(IFormatProvider formatProvider, string message, uint argument)
        {
        }

        public void Warn(string message, uint argument)
        {
        }

        public void Warn(IFormatProvider formatProvider, string message, ulong argument)
        {
        }

        public void Warn(string message, ulong argument)
        {
        }

        public void Error(string message)
        {
        }

        public void Error(object obj)
        {
        }

        public void Error(IFormatProvider formatProvider, object obj)
        {
        }

        public void ErrorException(string message, Exception exception)
        {
        }

        public void Error(IFormatProvider formatProvider, string message, params object[] args)
        {
        }

        public void Error(string message, params object[] args)
        {
        }

        public void Error(string message, object arg1, object arg2)
        {
        }

        public void Error(string message, object arg1, object arg2, object arg3)
        {
        }

        public void Error(IFormatProvider formatProvider, string message, bool argument)
        {
        }

        public void Error(string message, bool argument)
        {
        }

        public void Error(IFormatProvider formatProvider, string message, char argument)
        {
        }

        public void Error(string message, char argument)
        {
        }

        public void Error(IFormatProvider formatProvider, string message, byte argument)
        {
        }

        public void Error(string message, byte argument)
        {
        }

        public void Error(IFormatProvider formatProvider, string message, string argument)
        {
        }

        public void Error(string message, string argument)
        {
        }

        public void Error(IFormatProvider formatProvider, string message, int argument)
        {
        }

        public void Error(string message, int argument)
        {
        }

        public void Error(IFormatProvider formatProvider, string message, long argument)
        {
        }

        public void Error(string message, long argument)
        {
        }

        public void Error(IFormatProvider formatProvider, string message, float argument)
        {
        }

        public void Error(string message, float argument)
        {
        }

        public void Error(IFormatProvider formatProvider, string message, double argument)
        {
        }

        public void Error(string message, double argument)
        {
        }

        public void Error(IFormatProvider formatProvider, string message, decimal argument)
        {
        }

        public void Error(string message, decimal argument)
        {
        }

        public void Error(IFormatProvider formatProvider, string message, object argument)
        {
        }

        public void Error(string message, object argument)
        {
        }

        public void Error(IFormatProvider formatProvider, string message, sbyte argument)
        {
        }

        public void Error(string message, sbyte argument)
        {
        }

        public void Error(IFormatProvider formatProvider, string message, uint argument)
        {
        }

        public void Error(string message, uint argument)
        {
        }

        public void Error(IFormatProvider formatProvider, string message, ulong argument)
        {
        }

        public void Error(string message, ulong argument)
        {
        }

        public void Fatal(string message)
        {
        }

        public void Fatal(object obj)
        {
        }

        public void Fatal(IFormatProvider formatProvider, object obj)
        {
        }

        public void FatalException(string message, Exception exception)
        {
        }

        public void Fatal(IFormatProvider formatProvider, string message, params object[] args)
        {
        }

        public void Fatal(string message, params object[] args)
        {
        }

        public void Fatal(string message, object arg1, object arg2)
        {
        }

        public void Fatal(string message, object arg1, object arg2, object arg3)
        {
        }

        public void Fatal(IFormatProvider formatProvider, string message, bool argument)
        {
        }

        public void Fatal(string message, bool argument)
        {
        }

        public void Fatal(IFormatProvider formatProvider, string message, char argument)
        {
        }

        public void Fatal(string message, char argument)
        {
        }

        public void Fatal(IFormatProvider formatProvider, string message, byte argument)
        {
        }

        public void Fatal(string message, byte argument)
        {
        }

        public void Fatal(IFormatProvider formatProvider, string message, string argument)
        {
        }

        public void Fatal(string message, string argument)
        {
        }

        public void Fatal(IFormatProvider formatProvider, string message, int argument)
        {
        }

        public void Fatal(string message, int argument)
        {
        }

        public void Fatal(IFormatProvider formatProvider, string message, long argument)
        {
        }

        public void Fatal(string message, long argument)
        {
        }

        public void Fatal(IFormatProvider formatProvider, string message, float argument)
        {
        }

        public void Fatal(string message, float argument)
        {
        }

        public void Fatal(IFormatProvider formatProvider, string message, double argument)
        {
        }

        public void Fatal(string message, double argument)
        {
        }

        public void Fatal(IFormatProvider formatProvider, string message, decimal argument)
        {
        }

        public void Fatal(string message, decimal argument)
        {
        }

        public void Fatal(IFormatProvider formatProvider, string message, object argument)
        {
        }

        public void Fatal(string message, object argument)
        {
        }

        public void Fatal(IFormatProvider formatProvider, string message, sbyte argument)
        {
        }

        public void Fatal(string message, sbyte argument)
        {
        }

        public void Fatal(IFormatProvider formatProvider, string message, uint argument)
        {
        }

        public void Fatal(string message, uint argument)
        {
        }

        public void Fatal(IFormatProvider formatProvider, string message, ulong argument)
        {
        }

        public void Fatal(string message, ulong argument)
        {
        }
    }
}