using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HttpListenerSample.WinPhone.PlatformSpecific;
using Xamarin.Forms;

[assembly: Dependency(typeof(WinPhoneHttpInfoService))]

namespace HttpListenerSample.WinPhone.PlatformSpecific
{
    public class WinPhoneHttpInfoService : IHttpInfoService
    {
        public string IpAddress
        {
            get
            {
                try
                {
                    var ipAddress = new List<string>();
                    var hosts = Windows.Networking.Connectivity.NetworkInformation.GetHostNames().ToList();
                    foreach (var host in hosts)
                    {
                        var ip = host.DisplayName;
                        ipAddress.Add(ip);
                    }

                    return ipAddress.LastOrDefault();
                }
                catch
                {
                    return null;
                }
            }
        }
    }
}