using System;
using System.Xml.Linq;

using RestSharp.Portable;
using RestSharp.Portable.HttpClient;
//using RestSharp.Portable.Deserializers;

using System.Threading.Tasks;
//using Newtonsoft.Json;
//using Newtonsoft.Json.Linq;
//using Newtonsoft.Json.Serialization;

using System.Reactive;
using System.Reactive.Linq;
using Akavache;
//using Akavache.Sqlite3;

namespace RxMobile
{
	public class Rest 
	{
		public static async Task<T> Get<T>(string base_url, string path){
			using (var client = new RestClient (base_url)) {
				var req = new RestRequest (path, Method.GET);
				var res = await client.Execute<T> (req);
				return res.Data;
			}
		}

		public static async Task<string> GetString(string base_url, string path){
			using (var client = new RestClient (base_url)) {
				var res = await client.Execute(new RestRequest(path, Method.GET));
				return 
					System.Text.Encoding.UTF8.GetString (res.RawBytes, 0, res.RawBytes.Length);
			}
		}

		public static async Task<XDocument> GetXml(string base_url, string path){
			return  XDocument.Parse (await GetString (base_url, path));
		}

		public static IObservable<string> LoadXmlString(string base_url, string path){
			var url = base_url + path;
			var res = BlobCache.LocalMachine.GetOrFetchObject<string> (
				url, 
				() => Rest.GetString (base_url, path),
				DateTimeOffset.Now.AddHours (12));
			return res;
		}

		public static IObservable<XDocument> LoadXml(string base_url , string path){
			var str = LoadXmlString (base_url, path);
			return str.Select (s => XDocument.Parse (s));
		}
	}
}

