using System;
namespace HttpListenerSample
{
	public interface IHttpInfoService
	{
		string IpAddress { get; }
	}
}
