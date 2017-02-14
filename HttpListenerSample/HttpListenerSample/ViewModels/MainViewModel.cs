using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using SimpleHttpServer.Service;
using Xamarin.Forms;
using System.Reflection;
using System.IO;
using System.Net;
using System.Collections.Generic;
using ISimpleHttpServer.Model;

namespace HttpListenerSample
{
	public class MainViewModel : INotifyPropertyChanged
	{
		private const int Port = 8000;
		private readonly HttpListener _httpListener;
		private readonly IHttpInfoService _httpInfoService;
		private readonly Assembly _currentAssembly;

		public event PropertyChangedEventHandler PropertyChanged;

		public MainViewModel()
		{
			_httpListener = new HttpListener(TimeSpan.FromSeconds(30));
			_httpInfoService = DependencyService.Get<IHttpInfoService>();
			_currentAssembly = typeof(MainViewModel).GetTypeInfo().Assembly;
		}

		public string IpAddress => _httpInfoService?.IpAddress;

		public async Task StartListener()
		{
			await _httpListener.StartTcpRequestListener(Port).ConfigureAwait(false);
			_httpListener.HttpRequestObservable.Subscribe(OnHttpRequest, OnHttpError);
		}

		public void StopListener()
		{
			_httpListener.StopTcpRequestListener();
		}

		protected void OnPropertyChanged([CallerMemberNameAttribute]string name = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}

		private async void OnHttpRequest(IHttpRequest req)
		{
			using (var memoryStream = new MemoryStream())
			{
				using(var fileStream = _currentAssembly.GetManifestResourceStream($"{_currentAssembly.GetName().Name}.Resources.test.png"))
				{
					fileStream.CopyTo(memoryStream);
				}

				var res = new HttpReponse
				{
					StatusCode = (int)HttpStatusCode.OK,
					ResponseReason = HttpStatusCode.OK.ToString(),
					Headers = new Dictionary<string, string>
					{
						{"Date", DateTime.UtcNow.ToString("r")},
						{"Content-Type", "image/png"}
					},
					Body = memoryStream
				};

				await _httpListener.HttpReponse(req, res);
			}
		}

		private void OnHttpError(Exception err)
		{

		}
	}
}
