using System.IO;
using System.Xml.Serialization;

namespace ApplicationConfiguration
{
	[XmlRoot(ElementName = "MerkStaticConfiguration")]
	public class MerkConfiguration
	{
		[XmlElement(ElementName = "DBServer")]
		public string DBServer { get; set; }

		[XmlElement(ElementName = "MerkDBName")]
		public string MerkDBName { get; set; }

		[XmlElement(ElementName = "OrganizationID")]
		public string OrganizationID { get; set; }

		[XmlElement(ElementName = "InventoryHousingID")]
		public string InventoryHousingID { get; set; }

		[XmlElement(ElementName = "CashBoxID")]
		public string CashBoxID { get; set; }

		public static bool Exists()
		{
			return File.Exists(ApplicationStaticConfiguration.MerkConfigurationFilePath);
		}

		public void SaveMerkConfigurationFile(string file)
		{
			using (FileStream stream = new FileStream(file, FileMode.Create))
			{
				XmlSerializer xml = new XmlSerializer(typeof(MerkConfiguration));
				xml.Serialize(stream, this);
			}
		}
	}
}
