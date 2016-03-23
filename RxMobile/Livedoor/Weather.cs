using System;
using System.Linq;
using System.Collections.Generic;

using RestSharp.Portable;
using RestSharp.Portable.HttpClient;

using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Converters;
using System.Xml.Linq;

using Akavache;
using Akavache.Sqlite3;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;

namespace RxMobile.Livedoor
{
	public class Weather
	{
		static readonly string base_url = "http://weather.livedoor.com";
		static readonly string area_path = "/forecast/rss/primary_area.xml";

		public static string Url { get { return base_url + area_path; } }

		public XDocument Xml { get; private set; }

		public Weather ()
		{
			
		}

		public List<City> Cities {
			get {
				return Xml.Descendants ()
					.Where (i => i.Name == "city")
					.Select (c => City.FromXml (c)).ToList();
			}
		}

		public static IObservable<Weather> Load(){
			return Rest.LoadXml (base_url, area_path).Select (
				xml => new Weather{ Xml = xml }
			);
		}

		public static void LoadString(){
			Rest.Get<string> (base_url, area_path).ToObservable ()
				.Subscribe (
				i => {
					System.Diagnostics.Debug.WriteLine (i);
				},
				ex => {
					System.Diagnostics.Debug.WriteLine (ex.Message);
				}
			);
		}
	}

}

