using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using WIA;

namespace CommonControlLibrary
{
	public enum ImageFormat
	{
		PNG,
		JPEG
	}

	public class WIAScannerEngine
	{
		public static string PNGFormat = "{B96B3CAF-0728-11D3-9D7B-0000F81EF32E}";
		public static string JPEGFormat = "{B96B3CAE-0728-11D3-9D7B-0000F81EF32E}";

		/// <summary>
		/// Adjusts the settings of the scanner with the providen parameters.
		/// </summary>
		/// <param name="scannnerItem">Scanner Item</param>
		/// <param name="scanResolutionDPI">Provide the DPI resolution that should be used e.g 150</param>
		/// <param name="scanStartLeftPixel"></param>
		/// <param name="scanStartTopPixel"></param>
		/// <param name="scanWidthPixels"></param>
		/// <param name="scanHeightPixels"></param>
		/// <param name="brightnessPercents"></param>
		/// <param name="contrastPercents">Modify the contrast percent</param>
		/// <param name="colorMode">Set the color mode</param>
		private static void AdjustScannerSettings(IItem scannnerItem, int scanResolutionDPI, int scanStartLeftPixel,
			int scanStartTopPixel, int scanWidthPixels, int scanHeightPixels, int brightnessPercents,
			int contrastPercents, int colorMode)
		{
			const string WIA_SCAN_COLOR_MODE = "6146";
			const string WIA_HORIZONTAL_SCAN_RESOLUTION_DPI = "6147";
			const string WIA_VERTICAL_SCAN_RESOLUTION_DPI = "6148";
			const string WIA_HORIZONTAL_SCAN_START_PIXEL = "6149";
			const string WIA_VERTICAL_SCAN_START_PIXEL = "6150";
			const string WIA_HORIZONTAL_SCAN_SIZE_PIXELS = "6151";
			const string WIA_VERTICAL_SCAN_SIZE_PIXELS = "6152";
			const string WIA_SCAN_BRIGHTNESS_PERCENTS = "6154";
			const string WIA_SCAN_CONTRAST_PERCENTS = "6155";
			SetWIAProperty(scannnerItem.Properties, WIA_HORIZONTAL_SCAN_RESOLUTION_DPI, scanResolutionDPI);
			SetWIAProperty(scannnerItem.Properties, WIA_VERTICAL_SCAN_RESOLUTION_DPI, scanResolutionDPI);
			SetWIAProperty(scannnerItem.Properties, WIA_HORIZONTAL_SCAN_START_PIXEL, scanStartLeftPixel);
			SetWIAProperty(scannnerItem.Properties, WIA_VERTICAL_SCAN_START_PIXEL, scanStartTopPixel);
			SetWIAProperty(scannnerItem.Properties, WIA_HORIZONTAL_SCAN_SIZE_PIXELS, scanWidthPixels);
			SetWIAProperty(scannnerItem.Properties, WIA_VERTICAL_SCAN_SIZE_PIXELS, scanHeightPixels);
			SetWIAProperty(scannnerItem.Properties, WIA_SCAN_BRIGHTNESS_PERCENTS, brightnessPercents);
			SetWIAProperty(scannnerItem.Properties, WIA_SCAN_CONTRAST_PERCENTS, contrastPercents);
			SetWIAProperty(scannnerItem.Properties, WIA_SCAN_COLOR_MODE, colorMode);
		}

		/// <summary>
		/// Modify a WIA property
		/// </summary>
		/// <param name="properties"></param>
		/// <param name="propName"></param>
		/// <param name="propValue"></param>
		private static void SetWIAProperty(IProperties properties, object propName, object propValue)
		{
			Property prop = properties.get_Item(ref propName);
			prop.set_Value(ref propValue);
		}

		public static List<string> GetScannerDevices()
		{
			List<string> scannerDevicesList = new List<string>();

			WIA.DeviceManager manager = new WIA.DeviceManager();
			foreach (WIA.DeviceInfo managerDeviceInfo in manager.DeviceInfos)
				scannerDevicesList.Add(managerDeviceInfo.DeviceID);

			return scannerDevicesList;
		}

		public static string GetScannerDevice()
		{
			WIA.DeviceManager manager = new WIA.DeviceManager();
			foreach (DeviceInfo info in manager.DeviceInfos)
			{
				if (info.Type == WiaDeviceType.ScannerDeviceType)
					return info.DeviceID;
			}

			return null;
		}

		public static WIA.Device ConnectToScanner(string scannerDeviceID)
		{
			WIA.DeviceManager manager = new WIA.DeviceManager();
			WIA.Device device = null;

			foreach (WIA.DeviceInfo info in manager.DeviceInfos)
				if (info.DeviceID == scannerDeviceID)
				{
					device = info.Connect();
					break;
				}

			return device;
		}

		public static Image ScanFile(string folderName, string fileName, ImageFormat imageFormat, bool overrideIfExists,
			ref string savedPath, ref string errorMessage)
		{
			WIA.ICommonDialog wiaCommonDialog = new WIA.CommonDialog();
			Device device = ConnectToScanner(GetScannerDevice());
			if (device == null)
				return null;

			Image scannedImage = null;

			try
			{
				WIA.ImageFile image = null;
				WIA.Item item = device.Items[1] as WIA.Item;
				switch (imageFormat)
				{
					case ImageFormat.JPEG:
						image = (WIA.ImageFile) wiaCommonDialog.ShowTransfer(item, JPEGFormat);
						break;
					case ImageFormat.PNG:
						image = (WIA.ImageFile) wiaCommonDialog.ShowTransfer(item, PNGFormat);
						break;
				}

				if (image != null)
				{
					string path = FileManager.GetServerDirectoryPath(DB_ServerDirectory.ScanDirectory);
					string fullPath = "";

					string serverDirectorBaseFile =
						FileManager.GetServerDirectoryName(DB_ServerDirectory.ScanDirectory);
					if (!Directory.Exists(Path.Combine(path, serverDirectorBaseFile)))
						Directory.CreateDirectory(Path.Combine(path, serverDirectorBaseFile));
					if (!Directory.Exists(Path.Combine(Path.Combine(path, serverDirectorBaseFile), folderName)))
						Directory.CreateDirectory(Path.Combine(Path.Combine(path, serverDirectorBaseFile), folderName));

					switch (imageFormat)
					{
						case ImageFormat.JPEG:
							fullPath = Path.Combine(Path.Combine(Path.Combine(path, serverDirectorBaseFile), folderName), fileName + ".jpg");
							break;
						case ImageFormat.PNG:
							fullPath = Path.Combine(Path.Combine(Path.Combine(path, serverDirectorBaseFile), folderName), fileName + ".png");
							break;
					}

					if (File.Exists(Path.GetFullPath(fullPath)))
						if (overrideIfExists)
							File.Delete(Path.GetFullPath(fullPath));
						else
						{
							errorMessage = "File is already exists";
							return null;
						}

					try
					{
						image.SaveFile(Path.GetFullPath(fullPath));
					}
					catch (Exception e)
					{
						errorMessage = "Network error";
						return null;
					}
					
					savedPath = Path.GetFullPath(fullPath);
					scannedImage = Image.FromFile(Path.GetFullPath(fullPath));
				}
			}
			catch (COMException e)
			{
				// Convert the error code to UINT
				uint errorCode = (uint) e.ErrorCode;

				// See the error codes
				if (errorCode == 0x80210006)
				{
					errorMessage = "The scanner is busy or isn't ready";
				}
				else if (errorCode == 0x80210064)
				{
					errorMessage = "The scanning process has been canceled.";
				}
				else if (errorCode == 0x8021000C)
				{
					errorMessage = "There is an incorrect setting on the WIA device.";
				}
				else if (errorCode == 0x80210005)
				{
					errorMessage = "The device is offline. Make sure the device is powered on and connected to the PC.";
				}
				else if (errorCode == 0x80210001)
				{
					errorMessage = "An unknown error has occurred with the WIA device.";
				}
			}

			return scannedImage;
		}
	}
}
