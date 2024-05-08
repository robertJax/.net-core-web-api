using System;
namespace web_api.Logging
{
	public class LogToServer : IMyLogger
	{
		public void Log(string message)
		{
			Console.WriteLine(message);
			Console.WriteLine("Logto Server memory");
		}
	}
}

