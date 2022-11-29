using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreAPI
{
	public class LoginSecurity
	{
		public static bool Login(string username, string password)
		{
			if (username.ToLower() == "balaakm90" && password == "Bala@123")
			{
				return true;
			}
			else
			{
				return false;
			}
		}
	}
}
