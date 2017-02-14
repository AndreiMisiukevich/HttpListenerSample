using System;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using HttpListenerSample.iOS;

[assembly: Xamarin.Forms.Dependency(typeof(IosHttpInfoService))]
namespace HttpListenerSample.iOS
{
	public class IosHttpInfoService : IHttpInfoService
	{
		public string IpAddress
		{
			get
			{
				foreach (var netInterface in NetworkInterface.GetAllNetworkInterfaces())
				{
					if (netInterface.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 ||
						netInterface.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
					{
						foreach (var addrInfo in netInterface.GetIPProperties().UnicastAddresses)
						{
							if (addrInfo.Address.AddressFamily == AddressFamily.InterNetwork)
							{
								return addrInfo.Address.ToString();
							}
						}
					}
				}

				return null;
			}
		}
	}
}
