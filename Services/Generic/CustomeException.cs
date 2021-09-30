using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Generic
{
	public class CustomException : Exception
	{
		public CustomException(string message) : base(message)
		{
		}
		public CustomException(Exception ex, string message) : base(message)
		{
			if (ex != null)
			{
				ex.Log();
			}
		}
	}
}
