using System;
using System.Diagnostics;
using System.Configuration;

namespace CimexUtility.Debugging
{
	public class SwitchableDebugWriter
	{
		public void Write(string message)
		{
			var value = ConfigurationManager.AppSettings["DebugWriteLineEnabled"];
			if (value == null) throw new ConfigurationErrorsException("Please configure the 'DebugWriteLineEnabled' [bool] appSetting.");
			if (Convert.ToBoolean(value)) Debug.WriteLine(message);
		}
	}
}