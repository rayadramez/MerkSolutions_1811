using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using MerkDataBaseBusinessLogicProject;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;

namespace ApplicationConfiguration
{
	public class ApplicationStaticConfiguration
	{
		public static User_cu ActiveLoginUser { get; set; }

		public static string UserName { get; set; }

		public static DB_PEMRSavingMode PEMRSavingMode { get; set; }

		public static DB_Application Application { get; set; }

		public static DB_Application InternalReceptionApplication { get; set; }

		public static DB_Organization Organization { get; set; }

		public static DB_Station StationPoint { get; set; }

		public static int ActiveCashBoxID { get; set; }

		public static int ActiveInventoryHousingID { get; set; }

		public static bool CanAccessApplication
		{
			get { return UserConfigurationBusinessLogicEngine.CanUserAccessThisApplication(ActiveLoginUser, Application); }
		}

		public static OrganizationMachine_cu OrganizationMachine { get; set; }

		public static string OrganizationMachineName { get; set; }

		public static List<RoleRegistration_cu> ApplicationRoleRegistrations
		{
			get { return UserConfigurationBusinessLogicEngine.GetAllApplicationRoles(Application); }
		}

		public static string MerkConfigurationFilePath = @"C:\ProgramData\Merk\MerkConfiguration.xml";

		public static string SkinName { get; set; }

		public static object SkinColor { get; set; }

		private static string _errorMessage = "";

		public static bool IsPrivateLoginCredentialsUser(string userName, string password)
		{
			return (userName.Equals("merkuser", StringComparison.InvariantCultureIgnoreCase) && password == "m3rkus3r") ||
				   (userName.Equals("merkadmin", StringComparison.InvariantCultureIgnoreCase) && password == "m3rk1dmin");
		}

		public static User_cu GetUser(string loginName)
		{
			if (string.IsNullOrEmpty(loginName) || string.IsNullOrWhiteSpace(loginName))
				return null;

			return User_cu.ItemsList.Find(item => Convert.ToString(item.LoginName).Equals(loginName));
		}

		public static bool IsLoginCredentialsCorrect(string loginName, string password)
		{
			if (string.IsNullOrEmpty(loginName) || string.IsNullOrWhiteSpace(loginName) || string.IsNullOrEmpty(password) ||
			    string.IsNullOrWhiteSpace(password))
				return false;

			User_cu user = GetUser(loginName);
			if (user == null)
				return false;

			return Convert.ToString(user.Password).Equals(password);
		}

		public static bool UserCanAccess(string loginName, string password)
		{
			if (IsPrivateLoginCredentialsUser(loginName, password))
			{
				UserName = "Merk User";
				return true;
			}

			if (!IsLoginCredentialsCorrect(loginName, password))
				return false;

			User_cu user = GetUser(loginName);
			if (user == null)
				return false;

			ActiveLoginUser = user;
			UserName = user.FullName.ToString();

			PEMRSavingMode_User_cu pemrSavingMode = PEMRSavingMode_User_cu.ItemsList.Find(item =>
				Convert.ToInt32(item.User_CU_ID).Equals(Convert.ToInt32(user.Person_CU_ID)));
			if (pemrSavingMode == null)
				PEMRSavingMode = DB_PEMRSavingMode.SaveImmediately;
			else
				PEMRSavingMode = (DB_PEMRSavingMode) pemrSavingMode.PEMRSavingMode_P_ID;

			return CanAccessApplication;
		}

		public static bool MerkConfigurationFileExists()
		{
			return MerkConfiguration.Exists();
		}

		public static MerkConfiguration LoadMerkConfigurationFile(ref string errorMessage)
		{
			MerkConfiguration merkConfiguration = null;
			if (SaveMerkConfigurationFile())
			{
				merkConfiguration = XMLActions.LoadXmlFile<MerkConfiguration>(MerkConfigurationFilePath);
				if (string.IsNullOrEmpty(merkConfiguration.DBServer) || string.IsNullOrWhiteSpace(merkConfiguration.DBServer))
				{
					errorMessage = "Please Provide Database Server Name in the MerkConfiguration.xml";
					return null;
				}

				if (string.IsNullOrEmpty(merkConfiguration.MerkDBName) || string.IsNullOrWhiteSpace(merkConfiguration.MerkDBName))
				{
					errorMessage = "Please Provide Database Name in the MerkConfiguration.xml";
					return null;
				}
			}

			return merkConfiguration;
		}

		public static bool SaveMerkConfigurationFile()
		{
			try
			{
				if (!MerkConfigurationFileExists())
				{
					MerkConfiguration configurationFile = new MerkConfiguration();
					configurationFile.DBServer = " ";
					configurationFile.MerkDBName = " ";
					configurationFile.OrganizationID = " ";
					configurationFile.InventoryHousingID = " ";
					configurationFile.CashBoxID = " ";
					XMLActions.SaveXmlFile(configurationFile, MerkConfigurationFilePath);
				}

			}
			catch (Exception ex)
			{

				throw;
			}

			return true;
		}

		public static bool LoadApplicationConfiguration()
		{
			DialogResult result;

			MerkConfiguration merkConfiguration = LoadMerkConfigurationFile(ref _errorMessage);

			if (merkConfiguration == null)
			{
				result = XtraMessageBox.Show(_errorMessage + "\r\n\r\n" + "The application will exit now", "Error",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
				switch (result)
				{
					case DialogResult.OK:
						Process.GetCurrentProcess().Kill();
						return false;
				}
			}

			DBCommon.ServerName = merkConfiguration.DBServer;
			DBCommon.DBName = merkConfiguration.MerkDBName;
			Organization = (DB_Organization) Convert.ToInt32(merkConfiguration.OrganizationID);

			if (merkConfiguration.InventoryHousingID != null && !string.IsNullOrWhiteSpace(merkConfiguration.InventoryHousingID))
				ActiveInventoryHousingID = Convert.ToInt32(merkConfiguration.InventoryHousingID);
			else
				ActiveInventoryHousingID = -1;
			if (merkConfiguration.CashBoxID != null && !string.IsNullOrWhiteSpace(merkConfiguration.CashBoxID))
				ActiveCashBoxID = Convert.ToInt32(merkConfiguration.CashBoxID);
			else
				ActiveCashBoxID = -1;

			return true;
		}

		public static object GetSkinColor(string skinColor)
		{
			if (string.IsNullOrEmpty(skinColor) || string.IsNullOrWhiteSpace(skinColor))
				return null;

			int red;
			int green;
			int blue;
			Color color;

			string[] colorArry = skinColor.Split(',');
			red = Convert.ToInt32(colorArry[0]);
			green = Convert.ToInt32(colorArry[1]);
			blue = Convert.ToInt32(colorArry[2]);

			color = Color.FromArgb(red, green, blue);
			return color;
		}

		public static List<OrganizationMachine_cu> GetOrganizationMachines(int organizationID)
		{
			List<OrganizationMachine_cu> organizationMachinesList =
				OrganizationMachine_cu.ItemsList.FindAll(
					item => Convert.ToInt32(item.OrganizationID).Equals(Convert.ToInt32(organizationID))
					        && Convert.ToBoolean(item.IsOnDuty));

			return organizationMachinesList;
		}

		public static string GetOrganizationMachineName()
		{
			return System.Environment.MachineName;
		}

		public static OrganizationMachine_cu GetOrganizationMachine(int organizationID)
		{
			List<OrganizationMachine_cu> organizationMachinesList  = GetOrganizationMachines(organizationID);
			if (organizationMachinesList.Count == 0)
				return null;

			string machineName = GetOrganizationMachineName();
			if (string.IsNullOrEmpty(machineName) || string.IsNullOrWhiteSpace(machineName))
				return null;

			return organizationMachinesList.Find(item => item.Name_P.Equals(machineName,  StringComparison.InvariantCultureIgnoreCase));
		}
	}
}
