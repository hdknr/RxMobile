using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Xamarin.Forms;

using System.Linq;
using System.Reactive.Linq;

namespace RxMobile
{
	public partial class HomePage : ContentPage
	{
		public HomePage ()
		{
			InitializeComponent ();

			LoadData ();
		}

		void LoadData(){
			var source = new ObservableCollection<Livedoor.City>();
			CityList.ItemsSource = source;
			Livedoor.Weather.Load ().Subscribe (w => {
//				CityList.ItemsSource  = new ObservableCollection<Livedoor.City>(w.Cities);
				foreach(var i in w.Cities ){
					source.Add(i);
				}
			});


		}
	}
}

