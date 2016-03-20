using System;
using System.Xml.Linq;
using System.Linq;


namespace RxMobile.Livedoor
{
	public class City
	{
		public string id {get;set; }
		public string title {get;set; }
		public string source {get;set; }

		public static City FromXml(XElement e){
			return new City {
				id = e.Attribute("id").Value,
				title = e.Attribute("title").Value,
				source = e.Attribute("source").Value,
			};
		}

		public override string ToString ()
		{
			return string.Format (
				"[City: id={0}, title={1}, source={2}]", 
				id, title, source);
		}
	}
}

