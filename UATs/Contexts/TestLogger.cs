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

// Project site: http://code.google.com/p/nserializer/

using System;
using NLog;
using NSerializer.Logging;


namespace NSerializer.UATs.Contexts
{
    public class TestLogger : ILogger
    {
        private readonly Logger logger;

        public TestLogger()
        {
            logger = LogManager.GetLogger("Tests");
        }

        public void Trace(string message)
        {
            logger.Trace(message);
        }

        public void Trace(object obj)
        {
            logger.Trace(obj);
        }

        public void Trace(IFormatProvider formatProvider, object obj)
        {
            logger.Trace(formatProvider, obj);
        }

        public void TraceException(string message, Exception exception)
        {
            logger.TraceException(message, exception);
        }

        public void Trace(IFormatProvider formatProvider, string message, params object[] args)
        {
            logger.Trace(formatProvider, message, args);
        }

        public void Trace(string message, params object[] args)
        {
            logger.Trace(message, args);
        }

        public void Trace(string message, object arg1, object arg2)
        {
            logger.Trace(message, arg1, arg2);
        }

        public void Trace(string message, object arg1, object arg2, object arg3)
        {
            logger.Trace(message, arg1, arg2, arg3);
        }

        public void Trace(IFormatProvider formatProvider, string message, bool argument)
        {
            logger.Trace(formatProvider, message, argument);
        }

        public void Trace(string message, bool argument)
        {
            logger.Trace(message, argument);
        }

        public void Trace(IFormatProvider formatProvider, string message, char argument)
        {
            logger.Trace(formatProvider, message, argument);
        }

        public void Trace(string message, char argument)
        {
            logger.Trace(message, argument);
        }

        public void Trace(IFormatProvider formatProvider, string message, byte argument)
        {
            logger.Trace(formatProvider, message, argument);
        }

        public void Trace(string message, byte argument)
        {
            logger.Trace(message, argument);
        }

        public void Trace(IFormatProvider formatProvider, string message, string argument)
        {
            logger.Trace(formatProvider, message, argument);
        }

        public void Trace(string message, string argument)
        {
            logger.Trace(message, argument);
        }

        public void Trace(IFormatProvider formatProvider, string message, int argument)
        {
            logger.Trace(formatProvider, message, argument);
        }

        public void Trace(string message, int argument)
        {
            logger.Trace(message, argument);
        }

        public void Trace(IFormatProvider formatProvider, string message, long argument)
        {
            logger.Trace(formatProvider, message, argument);
        }

        public void Trace(string message, long argument)
        {
            logger.Trace(message, argument);
        }

        public void Trace(IFormatProvider formatProvider, string message, float argument)
        {
            logger.Trace(formatProvider, message, argument);
        }

        public void Trace(string message, float argument)
        {
            logger.Trace(message, argument);
        }

        public void Trace(IFormatProvider formatProvider, string message, double argument)
        {
            logger.Trace(formatProvider, message, argument);
        }

        public void Trace(string message, double argument)
        {
            logger.Trace(message, argument);
        }

        public void Trace(IFormatProvider formatProvider, string message, decimal argument)
        {
            logger.Trace(formatProvider, message, argument);
        }

        public void Trace(string message, decimal argument)
        {
            logger.Trace(message, argument);
        }

        public void Trace(IFormatProvider formatProvider, string message, object argument)
        {
            logger.Trace(formatProvider, message, argument);
        }

        public void Trace(string message, object argument)
        {
            logger.Trace(message, argument);
        }

        public void Trace(IFormatProvider formatProvider, string message, sbyte argument)
        {
            logger.Trace(formatProvider, message, argument);
        }

        public void Trace(string message, sbyte argument)
        {
            logger.Trace(message, argument);
        }

        public void Trace(IFormatProvider formatProvider, string message, uint argument)
        {
            logger.Trace(formatProvider, message, argument);
        }

        public void Trace(string message, uint argument)
        {
            logger.Trace(message, argument);
        }

        public void Trace(IFormatProvider formatProvider, string message, ulong argument)
        {
            logger.Trace(formatProvider, message, argument);
        }

        public void Trace(string message, ulong argument)
        {
            logger.Trace(message, argument);
        }

        public void Debug(string message)
        {
            logger.Debug(message);
        }

        public void Debug(object obj)
        {
            logger.Debug(obj);
        }

        public void Debug(IFormatProvider formatProvider, object obj)
        {
            logger.Debug(formatProvider, obj);
        }

        public void DebugException(string message, Exception exception)
        {
            logger.DebugException(message, exception);
        }

        public void Debug(IFormatProvider formatProvider, string message, params object[] args)
        {
            logger.Debug(formatProvider, message, args);
        }

        public void Debug(string message, params object[] args)
        {
            logger.Debug(message, args);
        }

        public void Debug(string message, object arg1, object arg2)
        {
            logger.Debug(message, arg1, arg2);
        }

        public void Debug(string message, object arg1, object arg2, object arg3)
        {
            logger.Debug(message, arg1, arg2, arg3);
        }

        public void Debug(IFormatProvider formatProvider, string message, bool argument)
        {
            logger.Debug(formatProvider, message, argument);
        }

        public void Debug(string message, bool argument)
        {
            logger.Debug(message, argument);
        }

        public void Debug(IFormatProvider formatProvider, string message, char argument)
        {
            logger.Debug(formatProvider, message, argument);
        }

        public void Debug(string message, char argument)
        {
            logger.Debug(message, argument);
        }

        public void Debug(IFormatProvider formatProvider, string message, byte argument)
        {
            logger.Debug(formatProvider, message, argument);
        }

        public void Debug(string message, byte argument)
        {
            logger.Debug(message, argument);
        }

        public void Debug(IFormatProvider formatProvider, string message, string argument)
        {
            logger.Debug(formatProvider, message, argument);
        }

        public void Debug(string message, string argument)
        {
            logger.Debug(message, argument);
        }

        public void Debug(IFormatProvider formatProvider, string message, int argument)
        {
            logger.Debug(formatProvider, message, argument);
        }

        public void Debug(string message, int argument)
        {
            logger.Debug(message, argument);
        }

        public void Debug(IFormatProvider formatProvider, string message, long argument)
        {
            logger.Debug(formatProvider, message, argument);
        }

        public void Debug(string message, long argument)
        {
            logger.Debug(message, argument);
        }

        public void Debug(IFormatProvider formatProvider, string message, float argument)
        {
            logger.Debug(formatProvider, message, argument);
        }

        public void Debug(string message, float argument)
        {
            logger.Debug(message, argument);
        }

        public void Debug(IFormatProvider formatProvider, string message, double argument)
        {
            logger.Debug(formatProvider, message, argument);
        }

        public void Debug(string message, double argument)
        {
            logger.Debug(message, argument);
        }

        public void Debug(IFormatProvider formatProvider, string message, decimal argument)
        {
            logger.Debug(formatProvider, message, argument);
        }

        public void Debug(string message, decimal argument)
        {
            logger.Debug(message, argument);
        }

        public void Debug(IFormatProvider formatProvider, string message, object argument)
        {
            logger.Debug(formatProvider, message, argument);
        }

        public void Debug(string message, object argument)
        {
            logger.Debug(message, argument);
        }

        public void Debug(IFormatProvider formatProvider, string message, sbyte argument)
        {
            logger.Debug(formatProvider, message, argument);
        }

        public void Debug(string message, sbyte argument)
        {
            logger.Debug(message, argument);
        }

        public void Debug(IFormatProvider formatProvider, string message, uint argument)
        {
            logger.Debug(formatProvider, message, argument);
        }

        public void Debug(string message, uint argument)
        {
            logger.Debug(message, argument);
        }

        public void Debug(IFormatProvider formatProvider, string message, ulong argument)
        {
            logger.Debug(formatProvider, message, argument);
        }

        public void Debug(string message, ulong argument)
        {
            logger.Debug(message, argument);
        }

        public void Info(string message)
        {
            logger.Info(message);
        }

        public void Info(object obj)
        {
            logger.Info(obj);
        }

        public void Info(IFormatProvider formatProvider, object obj)
        {
            logger.Info(formatProvider, obj);
        }

        public void InfoException(string message, Exception exception)
        {
            logger.InfoException(message, exception);
        }

        public void Info(IFormatProvider formatProvider, string message, params object[] args)
        {
            logger.Info(formatProvider, message, args);
        }

        public void Info(string message, params object[] args)
        {
            logger.Info(message, args);
        }

        public void Info(string message, object arg1, object arg2)
        {
            logger.Info(message, arg1, arg2);
        }

        public void Info(string message, object arg1, object arg2, object arg3)
        {
            logger.Info(message, arg1, arg2, arg3);
        }

        public void Info(IFormatProvider formatProvider, string message, bool argument)
        {
            logger.Info(formatProvider, message, argument);
        }

        public void Info(string message, bool argument)
        {
            logger.Info(message, argument);
        }

        public void Info(IFormatProvider formatProvider, string message, char argument)
        {
            logger.Info(formatProvider, message, argument);
        }

        public void Info(string message, char argument)
        {
            logger.Info(message, argument);
        }

        public void Info(IFormatProvider formatProvider, string message, byte argument)
        {
            logger.Info(formatProvider, message, argument);
        }

        public void Info(string message, byte argument)
        {
            logger.Info(message, argument);
        }

        public void Info(IFormatProvider formatProvider, string message, string argument)
        {
            logger.Info(formatProvider, message, argument);
        }

        public void Info(string message, string argument)
        {
            logger.Info(message, argument);
        }

        public void Info(IFormatProvider formatProvider, string message, int argument)
        {
            logger.Info(formatProvider, message, argument);
        }

        public void Info(string message, int argument)
        {
            logger.Info(message, argument);
        }

        public void Info(IFormatProvider formatProvider, string message, long argument)
        {
            logger.Info(formatProvider, message, argument);
        }

        public void Info(string message, long argument)
        {
            logger.Info(message, argument);
        }

        public void Info(IFormatProvider formatProvider, string message, float argument)
        {
            logger.Info(formatProvider, message, argument);
        }

        public void Info(string message, float argument)
        {
            logger.Info(message, argument);
        }

        public void Info(IFormatProvider formatProvider, string message, double argument)
        {
            logger.Info(formatProvider, message, argument);
        }

        public void Info(string message, double argument)
        {
            logger.Info(message, argument);
        }

        public void Info(IFormatProvider formatProvider, string message, decimal argument)
        {
            logger.Info(formatProvider, message, argument);
        }

        public void Info(string message, decimal argument)
        {
            logger.Info(message, argument);
        }

        public void Info(IFormatProvider formatProvider, string message, object argument)
        {
            logger.Info(formatProvider, message, argument);
        }

        public void Info(string message, object argument)
        {
            logger.Info(message, argument);
        }

        public void Info(IFormatProvider formatProvider, string message, sbyte argument)
        {
            logger.Info(formatProvider, message, argument);
        }

        public void Info(string message, sbyte argument)
        {
            logger.Info(message, argument);
        }

        public void Info(IFormatProvider formatProvider, string message, uint argument)
        {
            logger.Info(formatProvider, message, argument);
        }

        public void Info(string message, uint argument)
        {
            logger.Info(message, argument);
        }

        public void Info(IFormatProvider formatProvider, string message, ulong argument)
        {
            logger.Info(formatProvider, message, argument);
        }

        public void Info(string message, ulong argument)
        {
            logger.Info(message, argument);
        }

        public void Warn(string message)
        {
            logger.Warn(message);
        }

        public void Warn(object obj)
        {
            logger.Warn(obj);
        }

        public void Warn(IFormatProvider formatProvider, object obj)
        {
            logger.Warn(formatProvider, obj);
        }

        public void WarnException(string message, Exception exception)
        {
            logger.WarnException(message, exception);
        }

        public void Warn(IFormatProvider formatProvider, string message, params object[] args)
        {
            logger.Warn(formatProvider, message, args);
        }

        public void Warn(string message, params object[] args)
        {
            logger.Warn(message, args);
        }

        public void Warn(string message, object arg1, object arg2)
        {
            logger.Warn(message, arg1, arg2);
        }

        public void Warn(string message, object arg1, object arg2, object arg3)
        {
            logger.Warn(message, arg1, arg2, arg3);
        }

        public void Warn(IFormatProvider formatProvider, string message, bool argument)
        {
            logger.Warn(formatProvider, message, argument);
        }

        public void Warn(string message, bool argument)
        {
            logger.Warn(message, argument);
        }

        public void Warn(IFormatProvider formatProvider, string message, char argument)
        {
            logger.Warn(formatProvider, message, argument);
        }

        public void Warn(string message, char argument)
        {
            logger.Warn(message, argument);
        }

        public void Warn(IFormatProvider formatProvider, string message, byte argument)
        {
            logger.Warn(formatProvider, message, argument);
        }

        public void Warn(string message, byte argument)
        {
            logger.Warn(message, argument);
        }

        public void Warn(IFormatProvider formatProvider, string message, string argument)
        {
            logger.Warn(formatProvider, message, argument);
        }

        public void Warn(string message, string argument)
        {
            logger.Warn(message, argument);
        }

        public void Warn(IFormatProvider formatProvider, string message, int argument)
        {
            logger.Warn(formatProvider, message, argument);
        }

        public void Warn(string message, int argument)
        {
            logger.Warn(message, argument);
        }

        public void Warn(IFormatProvider formatProvider, string message, long argument)
        {
            logger.Warn(formatProvider, message, argument);
        }

        public void Warn(string message, long argument)
        {
            logger.Warn(message, argument);
        }

        public void Warn(IFormatProvider formatProvider, string message, float argument)
        {
            logger.Warn(formatProvider, message, argument);
        }

        public void Warn(string message, float argument)
        {
            logger.Warn(message, argument);
        }

        public void Warn(IFormatProvider formatProvider, string message, double argument)
        {
            logger.Warn(formatProvider, message, argument);
        }

        public void Warn(string message, double argument)
        {
            logger.Warn(message, argument);
        }

        public void Warn(IFormatProvider formatProvider, string message, decimal argument)
        {
            logger.Warn(formatProvider, message, argument);
        }

        public void Warn(string message, decimal argument)
        {
            logger.Warn(message, argument);
        }

        public void Warn(IFormatProvider formatProvider, string message, object argument)
        {
            logger.Warn(formatProvider, message, argument);
        }

        public void Warn(string message, object argument)
        {
            logger.Warn(message, argument);
        }

        public void Warn(IFormatProvider formatProvider, string message, sbyte argument)
        {
            logger.Warn(formatProvider, message, argument);
        }

        public void Warn(string message, sbyte argument)
        {
            logger.Warn(message, argument);
        }

        public void Warn(IFormatProvider formatProvider, string message, uint argument)
        {
            logger.Warn(formatProvider, message, argument);
        }

        public void Warn(string message, uint argument)
        {
            logger.Warn(message, argument);
        }

        public void Warn(IFormatProvider formatProvider, string message, ulong argument)
        {
            logger.Warn(formatProvider, message, argument);
        }

        public void Warn(string message, ulong argument)
        {
            logger.Warn(message, argument);
        }

        public void Error(string message)
        {
            logger.Error(message);
        }

        public void Error(object obj)
        {
            logger.Error(obj);
        }

        public void Error(IFormatProvider formatProvider, object obj)
        {
            logger.Error(formatProvider, obj);
        }

        public void ErrorException(string message, Exception exception)
        {
            logger.ErrorException(message, exception);
        }

        public void Error(IFormatProvider formatProvider, string message, params object[] args)
        {
            logger.Error(formatProvider, message, args);
        }

        public void Error(string message, params object[] args)
        {
            logger.Error(message, args);
        }

        public void Error(string message, object arg1, object arg2)
        {
            logger.Error(message, arg1, arg2);
        }

        public void Error(string message, object arg1, object arg2, object arg3)
        {
            logger.Error(message, arg1, arg2, arg3);
        }

        public void Error(IFormatProvider formatProvider, string message, bool argument)
        {
            logger.Error(formatProvider, message, argument);
        }

        public void Error(string message, bool argument)
        {
            logger.Error(message, argument);
        }

        public void Error(IFormatProvider formatProvider, string message, char argument)
        {
            logger.Error(formatProvider, message, argument);
        }

        public void Error(string message, char argument)
        {
            logger.Error(message, argument);
        }

        public void Error(IFormatProvider formatProvider, string message, byte argument)
        {
            logger.Error(formatProvider, message, argument);
        }

        public void Error(string message, byte argument)
        {
            logger.Error(message, argument);
        }

        public void Error(IFormatProvider formatProvider, string message, string argument)
        {
            logger.Error(formatProvider, message, argument);
        }

        public void Error(string message, string argument)
        {
            logger.Error(message, argument);
        }

        public void Error(IFormatProvider formatProvider, string message, int argument)
        {
            logger.Error(formatProvider, message, argument);
        }

        public void Error(string message, int argument)
        {
            logger.Error(message, argument);
        }

        public void Error(IFormatProvider formatProvider, string message, long argument)
        {
            logger.Error(formatProvider, message, argument);
        }

        public void Error(string message, long argument)
        {
            logger.Error(message, argument);
        }

        public void Error(IFormatProvider formatProvider, string message, float argument)
        {
            logger.Error(formatProvider, message, argument);
        }

        public void Error(string message, float argument)
        {
            logger.Error(message, argument);
        }

        public void Error(IFormatProvider formatProvider, string message, double argument)
        {
            logger.Error(formatProvider, message, argument);
        }

        public void Error(string message, double argument)
        {
            logger.Error(message, argument);
        }

        public void Error(IFormatProvider formatProvider, string message, decimal argument)
        {
            logger.Error(formatProvider, message, argument);
        }

        public void Error(string message, decimal argument)
        {
            logger.Error(message, argument);
        }

        public void Error(IFormatProvider formatProvider, string message, object argument)
        {
            logger.Error(formatProvider, message, argument);
        }

        public void Error(string message, object argument)
        {
            logger.Error(message, argument);
        }

        public void Error(IFormatProvider formatProvider, string message, sbyte argument)
        {
            logger.Error(formatProvider, message, argument);
        }

        public void Error(string message, sbyte argument)
        {
            logger.Error(message, argument);
        }

        public void Error(IFormatProvider formatProvider, string message, uint argument)
        {
            logger.Error(formatProvider, message, argument);
        }

        public void Error(string message, uint argument)
        {
            logger.Error(message, argument);
        }

        public void Error(IFormatProvider formatProvider, string message, ulong argument)
        {
            logger.Error(formatProvider, message, argument);
        }

        public void Error(string message, ulong argument)
        {
            logger.Error(message, argument);
        }

        public void Fatal(string message)
        {
            logger.Fatal(message);
        }

        public void Fatal(object obj)
        {
            logger.Fatal(obj);
        }

        public void Fatal(IFormatProvider formatProvider, object obj)
        {
            logger.Fatal(formatProvider, obj);
        }

        public void FatalException(string message, Exception exception)
        {
            logger.FatalException(message, exception);
        }

        public void Fatal(IFormatProvider formatProvider, string message, params object[] args)
        {
            logger.Fatal(formatProvider, message, args);
        }

        public void Fatal(string message, params object[] args)
        {
            logger.Fatal(message, args);
        }

        public void Fatal(string message, object arg1, object arg2)
        {
            logger.Fatal(message, arg1, arg2);
        }

        public void Fatal(string message, object arg1, object arg2, object arg3)
        {
            logger.Fatal(message, arg1, arg2, arg3);
        }

        public void Fatal(IFormatProvider formatProvider, string message, bool argument)
        {
            logger.Fatal(formatProvider, message, argument);
        }

        public void Fatal(string message, bool argument)
        {
            logger.Fatal(message, argument);
        }

        public void Fatal(IFormatProvider formatProvider, string message, char argument)
        {
            logger.Fatal(formatProvider, message, argument);
        }

        public void Fatal(string message, char argument)
        {
            logger.Fatal(message, argument);
        }

        public void Fatal(IFormatProvider formatProvider, string message, byte argument)
        {
            logger.Fatal(formatProvider, message, argument);
        }

        public void Fatal(string message, byte argument)
        {
            logger.Fatal(message, argument);
        }

        public void Fatal(IFormatProvider formatProvider, string message, string argument)
        {
            logger.Fatal(formatProvider, message, argument);
        }

        public void Fatal(string message, string argument)
        {
            logger.Fatal(message, argument);
        }

        public void Fatal(IFormatProvider formatProvider, string message, int argument)
        {
            logger.Fatal(formatProvider, message, argument);
        }

        public void Fatal(string message, int argument)
        {
            logger.Fatal(message, argument);
        }

        public void Fatal(IFormatProvider formatProvider, string message, long argument)
        {
            logger.Fatal(formatProvider, message, argument);
        }

        public void Fatal(string message, long argument)
        {
            logger.Fatal(message, argument);
        }

        public void Fatal(IFormatProvider formatProvider, string message, float argument)
        {
            logger.Fatal(formatProvider, message, argument);
        }

        public void Fatal(string message, float argument)
        {
            logger.Fatal(message, argument);
        }

        public void Fatal(IFormatProvider formatProvider, string message, double argument)
        {
            logger.Fatal(formatProvider, message, argument);
        }

        public void Fatal(string message, double argument)
        {
            logger.Fatal(message, argument);
        }

        public void Fatal(IFormatProvider formatProvider, string message, decimal argument)
        {
            logger.Fatal(formatProvider, message, argument);
        }

        public void Fatal(string message, decimal argument)
        {
            logger.Fatal(message, argument);
        }

        public void Fatal(IFormatProvider formatProvider, string message, object argument)
        {
            logger.Fatal(formatProvider, message, argument);
        }

        public void Fatal(string message, object argument)
        {
            logger.Fatal(message, argument);
        }

        public void Fatal(IFormatProvider formatProvider, string message, sbyte argument)
        {
            logger.Fatal(formatProvider, message, argument);
        }

        public void Fatal(string message, sbyte argument)
        {
            logger.Fatal(message, argument);
        }

        public void Fatal(IFormatProvider formatProvider, string message, uint argument)
        {
            logger.Fatal(formatProvider, message, argument);
        }

        public void Fatal(string message, uint argument)
        {
            logger.Fatal(message, argument);
        }

        public void Fatal(IFormatProvider formatProvider, string message, ulong argument)
        {
            logger.Fatal(formatProvider, message, argument);
        }

        public void Fatal(string message, ulong argument)
        {
            logger.Fatal(message, argument);
        }

        public string Name
        {
            get { return logger.Name; }
        }

        public bool IsTraceEnabled
        {
            get { return logger.IsTraceEnabled; }
        }

        public bool IsDebugEnabled
        {
            get { return logger.IsDebugEnabled; }
        }

        public bool IsInfoEnabled
        {
            get { return logger.IsInfoEnabled; }
        }

        public bool IsWarnEnabled
        {
            get { return logger.IsWarnEnabled; }
        }

        public bool IsErrorEnabled
        {
            get { return logger.IsErrorEnabled; }
        }

        public bool IsFatalEnabled
        {
            get { return logger.IsFatalEnabled; }
        }
    }
}