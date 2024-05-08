using System;
namespace web_api.Logging
{
	public class LogToDB : IMyLogger
	{
		public void Log(string message)
		{
			Console.WriteLine(message);
			Console.WriteLine("LogtoDB");
		}
	}
}

