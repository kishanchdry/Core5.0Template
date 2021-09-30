using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Common
{
	public static class Helper
	{
		private static string key = "apnafarmappkeykc";//"ud3f805tFsWmES+mzh856nnyQ25vSxV8";//
		private static int count = 0;
		private static int maxTries = 3;
		public static string Encrypt(string input)
		{
			return EncryptString(input);
		}
		public static string Encrypt(long input)
		{
			return EncryptString(string.Format("{0}", input));
		}

		public static string EncryptString(string input)
		{
			try
			{
				if (!string.IsNullOrWhiteSpace(input))
				{
					byte[] inputArray = UTF8Encoding.UTF8.GetBytes(input);
					TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
					tripleDES.Key = UTF8Encoding.UTF8.GetBytes(key);
					tripleDES.Mode = CipherMode.ECB;
					tripleDES.Padding = PaddingMode.PKCS7;
					ICryptoTransform cTransform = tripleDES.CreateEncryptor();
					byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
					tripleDES.Clear();
					return Convert.ToBase64String(resultArray, 0, resultArray.Length);
				}
			}
			catch (Exception) { }
			return string.Empty;
		}
		public static string Decrypt(string input)
		{
			try
			{
				if (!string.IsNullOrWhiteSpace(input))
				{
					byte[] inputArray = Convert.FromBase64String(input);
					TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
					tripleDES.Key = UTF8Encoding.UTF8.GetBytes(key);
					tripleDES.Mode = CipherMode.ECB;
					tripleDES.Padding = PaddingMode.PKCS7;
					ICryptoTransform cTransform = tripleDES.CreateDecryptor();
					byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
					tripleDES.Clear();
					return UTF8Encoding.UTF8.GetString(resultArray);
				}
			}
			catch (Exception)
			{
				if (++count >= maxTries)
				{
					count = 0;
					return "";
				}
                // handle exception
                //TODO
                //ex.Log();
				return Decrypt(string.Format("{0}", input).Replace(" ", "+"));
			}
			return string.Empty;
		}
	}
}
