using System;
using Xamarin.Forms;
using System.Threading.Tasks;
using SimpleHttpServer.Service;
namespace HttpListenerSample
{
	public class MainView : ContentPage
	{
		private MainViewModel _model;

		public MainView()
		{
			var ipAddressLabel = new Label 
			{ 
				VerticalOptions = LayoutOptions.CenterAndExpand, 
				HorizontalOptions = LayoutOptions.CenterAndExpand 
			};

			ipAddressLabel.SetBinding(Label.TextProperty, nameof(MainViewModel.IpAddress));

			Content = new StackLayout
			{
				Children = {
					ipAddressLabel
				}
			};
		}

		protected override void OnBindingContextChanged()
		{
			base.OnBindingContextChanged();
			_model = BindingContext as MainViewModel;
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			_model?.StartListener();
		}

		protected override void OnDisappearing()
		{
			base.OnDisappearing();
			_model?.StopListener();
		}

	}
}
