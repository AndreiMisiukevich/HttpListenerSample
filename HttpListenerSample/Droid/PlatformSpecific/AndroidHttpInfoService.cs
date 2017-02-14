using System;
using System.Net;
using System.Net.Sockets;
using HttpListenerSample.Droid;
using Java.Net;

[assembly: Xamarin.Forms.Dependency(typeof(AndroidHttpInfoService))]
namespace HttpListenerSample.Droid
{
	public class AndroidHttpInfoService : IHttpInfoService
	{
		public string IpAddress
		{
			get
			{
				var host = Dns.GetHostEntry(Dns.GetHostName());
				foreach (var ip in host.AddressList)
				{
					if (ip.AddressFamily == AddressFamily.InterNetwork)
					{
						return ip.ToString();
					}
				}

				return null;
			}
		}
	}
}
