using System;
namespace web_api.Logging
{
	public class LogToFile : IMyLogger
	{
		public void Log(string message)
		{
			Console.WriteLine(message);
			Console.WriteLine("LogTofile");
		}
	}
}

