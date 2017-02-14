using System;

using Xamarin.Forms;

namespace HttpListenerSample
{
	public class App : Application
	{
		public App()
		{
			MainPage = new MainView()
			{
				BindingContext = new MainViewModel()
			};
		}

	}
}
