using System.IO;
using System.Xml.Serialization;

namespace ApplicationConfiguration
{
	public class XMLActions
	{
		public static void SaveXmlFile<TXmlType>(TXmlType xmlToCreate, string fileName)
			where TXmlType : new()
		{
			using (FileStream stream = new FileStream(fileName, FileMode.Create))
			{
				XmlSerializer xml = new XmlSerializer(typeof(TXmlType));
				xml.Serialize(stream, xmlToCreate);
			}
		}

		public static TXmlType LoadXmlFile<TXmlType>(string fileName)
		{
			TXmlType xmlFile;

			using (FileStream stream = new FileStream(fileName, FileMode.Open))
			{
				XmlSerializer xml = new XmlSerializer(typeof(TXmlType));
				xmlFile = (TXmlType)xml.Deserialize(stream);
				stream.Close();
			}

			return xmlFile;
		}
	}
}
