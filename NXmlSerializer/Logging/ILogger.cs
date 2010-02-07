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
    public interface ILogger
    {
        void Trace(string message);
        void Trace(object obj);
        void Trace(IFormatProvider formatProvider, object obj);
        void TraceException(string message, Exception exception);
        void Trace(IFormatProvider formatProvider, string message, params object[] args);
        void Trace(string message, params object[] args);
        void Trace(string message, object arg1, object arg2);
        void Trace(string message, object arg1, object arg2, object arg3);
        void Trace(IFormatProvider formatProvider, string message, bool argument);
        void Trace(string message, bool argument);
        void Trace(IFormatProvider formatProvider, string message, char argument);
        void Trace(string message, char argument);
        void Trace(IFormatProvider formatProvider, string message, byte argument);
        void Trace(string message, byte argument);
        void Trace(IFormatProvider formatProvider, string message, string argument);
        void Trace(string message, string argument);
        void Trace(IFormatProvider formatProvider, string message, int argument);
        void Trace(string message, int argument);
        void Trace(IFormatProvider formatProvider, string message, long argument);
        void Trace(string message, long argument);
        void Trace(IFormatProvider formatProvider, string message, float argument);
        void Trace(string message, float argument);
        void Trace(IFormatProvider formatProvider, string message, double argument);
        void Trace(string message, double argument);
        void Trace(IFormatProvider formatProvider, string message, decimal argument);
        void Trace(string message, decimal argument);
        void Trace(IFormatProvider formatProvider, string message, object argument);
        void Trace(string message, object argument);
        void Trace(IFormatProvider formatProvider, string message, sbyte argument);
        void Trace(string message, sbyte argument);
        void Trace(IFormatProvider formatProvider, string message, uint argument);
        void Trace(string message, uint argument);
        void Trace(IFormatProvider formatProvider, string message, ulong argument);
        void Trace(string message, ulong argument);
        void Debug(string message);
        void Debug(object obj);
        void Debug(IFormatProvider formatProvider, object obj);
        void DebugException(string message, Exception exception);
        void Debug(IFormatProvider formatProvider, string message, params object[] args);
        void Debug(string message, params object[] args);
        void Debug(string message, object arg1, object arg2);
        void Debug(string message, object arg1, object arg2, object arg3);
        void Debug(IFormatProvider formatProvider, string message, bool argument);
        void Debug(string message, bool argument);
        void Debug(IFormatProvider formatProvider, string message, char argument);
        void Debug(string message, char argument);
        void Debug(IFormatProvider formatProvider, string message, byte argument);
        void Debug(string message, byte argument);
        void Debug(IFormatProvider formatProvider, string message, string argument);
        void Debug(string message, string argument);
        void Debug(IFormatProvider formatProvider, string message, int argument);
        void Debug(string message, int argument);
        void Debug(IFormatProvider formatProvider, string message, long argument);
        void Debug(string message, long argument);
        void Debug(IFormatProvider formatProvider, string message, float argument);
        void Debug(string message, float argument);
        void Debug(IFormatProvider formatProvider, string message, double argument);
        void Debug(string message, double argument);
        void Debug(IFormatProvider formatProvider, string message, decimal argument);
        void Debug(string message, decimal argument);
        void Debug(IFormatProvider formatProvider, string message, object argument);
        void Debug(string message, object argument);
        void Debug(IFormatProvider formatProvider, string message, sbyte argument);
        void Debug(string message, sbyte argument);
        void Debug(IFormatProvider formatProvider, string message, uint argument);
        void Debug(string message, uint argument);
        void Debug(IFormatProvider formatProvider, string message, ulong argument);
        void Debug(string message, ulong argument);
        void Info(string message);
        void Info(object obj);
        void Info(IFormatProvider formatProvider, object obj);
        void InfoException(string message, Exception exception);
        void Info(IFormatProvider formatProvider, string message, params object[] args);
        void Info(string message, params object[] args);
        void Info(string message, object arg1, object arg2);
        void Info(string message, object arg1, object arg2, object arg3);
        void Info(IFormatProvider formatProvider, string message, bool argument);
        void Info(string message, bool argument);
        void Info(IFormatProvider formatProvider, string message, char argument);
        void Info(string message, char argument);
        void Info(IFormatProvider formatProvider, string message, byte argument);
        void Info(string message, byte argument);
        void Info(IFormatProvider formatProvider, string message, string argument);
        void Info(string message, string argument);
        void Info(IFormatProvider formatProvider, string message, int argument);
        void Info(string message, int argument);
        void Info(IFormatProvider formatProvider, string message, long argument);
        void Info(string message, long argument);
        void Info(IFormatProvider formatProvider, string message, float argument);
        void Info(string message, float argument);
        void Info(IFormatProvider formatProvider, string message, double argument);
        void Info(string message, double argument);
        void Info(IFormatProvider formatProvider, string message, decimal argument);
        void Info(string message, decimal argument);
        void Info(IFormatProvider formatProvider, string message, object argument);
        void Info(string message, object argument);
        void Info(IFormatProvider formatProvider, string message, sbyte argument);
        void Info(string message, sbyte argument);
        void Info(IFormatProvider formatProvider, string message, uint argument);
        void Info(string message, uint argument);
        void Info(IFormatProvider formatProvider, string message, ulong argument);
        void Info(string message, ulong argument);
        void Warn(string message);
        void Warn(object obj);
        void Warn(IFormatProvider formatProvider, object obj);
        void WarnException(string message, Exception exception);
        void Warn(IFormatProvider formatProvider, string message, params object[] args);
        void Warn(string message, params object[] args);
        void Warn(string message, object arg1, object arg2);
        void Warn(string message, object arg1, object arg2, object arg3);
        void Warn(IFormatProvider formatProvider, string message, bool argument);
        void Warn(string message, bool argument);
        void Warn(IFormatProvider formatProvider, string message, char argument);
        void Warn(string message, char argument);
        void Warn(IFormatProvider formatProvider, string message, byte argument);
        void Warn(string message, byte argument);
        void Warn(IFormatProvider formatProvider, string message, string argument);
        void Warn(string message, string argument);
        void Warn(IFormatProvider formatProvider, string message, int argument);
        void Warn(string message, int argument);
        void Warn(IFormatProvider formatProvider, string message, long argument);
        void Warn(string message, long argument);
        void Warn(IFormatProvider formatProvider, string message, float argument);
        void Warn(string message, float argument);
        void Warn(IFormatProvider formatProvider, string message, double argument);
        void Warn(string message, double argument);
        void Warn(IFormatProvider formatProvider, string message, decimal argument);
        void Warn(string message, decimal argument);
        void Warn(IFormatProvider formatProvider, string message, object argument);
        void Warn(string message, object argument);
        void Warn(IFormatProvider formatProvider, string message, sbyte argument);
        void Warn(string message, sbyte argument);
        void Warn(IFormatProvider formatProvider, string message, uint argument);
        void Warn(string message, uint argument);
        void Warn(IFormatProvider formatProvider, string message, ulong argument);
        void Warn(string message, ulong argument);
        void Error(string message);
        void Error(object obj);
        void Error(IFormatProvider formatProvider, object obj);
        void ErrorException(string message, Exception exception);
        void Error(IFormatProvider formatProvider, string message, params object[] args);
        void Error(string message, params object[] args);
        void Error(string message, object arg1, object arg2);
        void Error(string message, object arg1, object arg2, object arg3);
        void Error(IFormatProvider formatProvider, string message, bool argument);
        void Error(string message, bool argument);
        void Error(IFormatProvider formatProvider, string message, char argument);
        void Error(string message, char argument);
        void Error(IFormatProvider formatProvider, string message, byte argument);
        void Error(string message, byte argument);
        void Error(IFormatProvider formatProvider, string message, string argument);
        void Error(string message, string argument);
        void Error(IFormatProvider formatProvider, string message, int argument);
        void Error(string message, int argument);
        void Error(IFormatProvider formatProvider, string message, long argument);
        void Error(string message, long argument);
        void Error(IFormatProvider formatProvider, string message, float argument);
        void Error(string message, float argument);
        void Error(IFormatProvider formatProvider, string message, double argument);
        void Error(string message, double argument);
        void Error(IFormatProvider formatProvider, string message, decimal argument);
        void Error(string message, decimal argument);
        void Error(IFormatProvider formatProvider, string message, object argument);
        void Error(string message, object argument);
        void Error(IFormatProvider formatProvider, string message, sbyte argument);
        void Error(string message, sbyte argument);
        void Error(IFormatProvider formatProvider, string message, uint argument);
        void Error(string message, uint argument);
        void Error(IFormatProvider formatProvider, string message, ulong argument);
        void Error(string message, ulong argument);
        void Fatal(string message);
        void Fatal(object obj);
        void Fatal(IFormatProvider formatProvider, object obj);
        void FatalException(string message, Exception exception);
        void Fatal(IFormatProvider formatProvider, string message, params object[] args);
        void Fatal(string message, params object[] args);
        void Fatal(string message, object arg1, object arg2);
        void Fatal(string message, object arg1, object arg2, object arg3);
        void Fatal(IFormatProvider formatProvider, string message, bool argument);
        void Fatal(string message, bool argument);
        void Fatal(IFormatProvider formatProvider, string message, char argument);
        void Fatal(string message, char argument);
        void Fatal(IFormatProvider formatProvider, string message, byte argument);
        void Fatal(string message, byte argument);
        void Fatal(IFormatProvider formatProvider, string message, string argument);
        void Fatal(string message, string argument);
        void Fatal(IFormatProvider formatProvider, string message, int argument);
        void Fatal(string message, int argument);
        void Fatal(IFormatProvider formatProvider, string message, long argument);
        void Fatal(string message, long argument);
        void Fatal(IFormatProvider formatProvider, string message, float argument);
        void Fatal(string message, float argument);
        void Fatal(IFormatProvider formatProvider, string message, double argument);
        void Fatal(string message, double argument);
        void Fatal(IFormatProvider formatProvider, string message, decimal argument);
        void Fatal(string message, decimal argument);
        void Fatal(IFormatProvider formatProvider, string message, object argument);
        void Fatal(string message, object argument);
        void Fatal(IFormatProvider formatProvider, string message, sbyte argument);
        void Fatal(string message, sbyte argument);
        void Fatal(IFormatProvider formatProvider, string message, uint argument);
        void Fatal(string message, uint argument);
        void Fatal(IFormatProvider formatProvider, string message, ulong argument);
        void Fatal(string message, ulong argument);
        string Name { get; }
        bool IsTraceEnabled { get; }
        bool IsDebugEnabled { get; }
        bool IsInfoEnabled { get; }
        bool IsWarnEnabled { get; }
        bool IsErrorEnabled { get; }
        bool IsFatalEnabled { get; }
    }
}