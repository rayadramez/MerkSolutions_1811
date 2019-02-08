using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Windows.Forms;
using MerkDataBaseBusinessLogicProject;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;

namespace CommonControlLibrary
{
	public enum FileSelectionFilter
	{
		All = 1,
		Images = 2,
	}

	public enum ReportTemplateType
	{
		Header_A4 = 1,
		Footer_A4 = 2,
		Header_A5 = 3,
		Footer_A5 = 4
	}

	public class FileManager
	{
		public static string A4PortraitHeader
		{
			get { return "A4PortraitHeader.docx"; }
		}

		public static string A4PortraitFooter
		{
			get { return "A4PortraitFooter.docx"; }
		}

		public static string A5PortraitHeader
		{
			get { return "A5PortraitHeader.docx"; }
		}

		public static string A5PortraitFooter
		{
			get { return "A5PortraitFooter.docx"; }
		}

		public static String[] FileDialoge(string folderName, string destinationPath, bool isMultiSelectionEnabled,
			FileSelectionFilter fileSelectionFilter, ref string errorMessage, string prefixFileName = "")
		{
			OpenFileDialog fileDialog = new OpenFileDialog();
			fileDialog.Filter = GetImagesFilter();
			fileDialog.Multiselect = true;
			fileDialog.ShowDialog();

			return fileDialog.FileNames;
		}

		public static List<string> FileDialogeAndCopy(string folderName, string destinationPath, bool isMultiSelectionEnabled,
			FileSelectionFilter fileSelectionFilter, ref string errorMessage, string prefixFileName = "")
		{
			OpenFileDialog fileDialog = new OpenFileDialog();
			fileDialog.Filter = GetImagesFilter();
			fileDialog.Multiselect = true;
			fileDialog.ShowDialog();

			return CopyFiles(folderName, destinationPath, fileDialog.FileNames, ref errorMessage, prefixFileName);
		}

		public static List<string> CopyFiles(string folderName, string destinationPath, string[] sourceFilePaths,
			ref string errorMessage, string prefixFileName = "")
		{
			if (!String.IsNullOrEmpty(destinationPath) && !Directory.Exists(destinationPath))
				Directory.CreateDirectory(destinationPath);

			List<string> copiedFiles = new List<string>();
			foreach (string filePath in sourceFilePaths)
			{
				string fileName = Path.GetFileName(filePath);
				string newFileName = "";
				if (!String.IsNullOrEmpty(prefixFileName) && !Directory.Exists(prefixFileName))
					newFileName = prefixFileName + "_" + fileName;
				else
					newFileName = fileName;

				string serverDirectorBaseFile =
					GetServerDirectoryName(DB_ServerDirectory.ScanDirectory);
				if (!Directory.Exists(Path.Combine(destinationPath, serverDirectorBaseFile)))
					Directory.CreateDirectory(Path.Combine(destinationPath, serverDirectorBaseFile));
				if (!Directory.Exists(Path.Combine(Path.Combine(destinationPath, serverDirectorBaseFile), folderName)))
					Directory.CreateDirectory(Path.Combine(Path.Combine(destinationPath, serverDirectorBaseFile),
						folderName));

				string destinationFilePath =
					Path.Combine(Path.Combine(Path.Combine(destinationPath, serverDirectorBaseFile), folderName),
						newFileName);

				try
				{
					File.Copy(filePath, destinationFilePath, false);
					copiedFiles.Add(Path.GetFullPath(destinationFilePath));
				}
				catch (Exception e)
				{
					errorMessage = e.Message;
				}
			}

			return copiedFiles;
		}

		public static string GetImagesFilter()
		{
			StringBuilder allImageExtensions = new StringBuilder();
			string separator = "";
			ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
			Dictionary<string, string> images = new Dictionary<string, string>();
			foreach (ImageCodecInfo codec in codecs)
			{
				allImageExtensions.Append(separator);
				allImageExtensions.Append(codec.FilenameExtension);
				separator = ";";
				images.Add(string.Format("{0} Files: ({1})", codec.FormatDescription, codec.FilenameExtension),
					codec.FilenameExtension);
			}
			StringBuilder sb = new StringBuilder();
			if (allImageExtensions.Length > 0)
			{
				sb.AppendFormat("{0}|{1}", "All Images", allImageExtensions.ToString());
			}

			foreach (KeyValuePair<string, string> image in images)
			{
				sb.AppendFormat("|{0}|{1}", image.Key, image.Value);
			}
			return sb.ToString();
		}

		public static string GetServerDirectoryPath(DB_ServerDirectory serverDirectory)
		{
			ServerDirectory_p serviceDirectory = ServerDirectory_p.ItemsList.Find(item =>
				Convert.ToInt32(item.ID).Equals(Convert.ToInt32(serverDirectory)));
			if (serviceDirectory == null)
				return string.Empty;
			return serviceDirectory.Path;
		}

		public static string GetServerDirectoryName(DB_ServerDirectory serverDirectory)
		{
			ServerDirectory_p serviceDirectory = ServerDirectory_p.ItemsList.Find(item =>
				Convert.ToInt32(item.ID).Equals(Convert.ToInt32(serverDirectory)));
			if (serviceDirectory == null)
				return string.Empty;
			return serviceDirectory.Name_P;
		}

		public static Image GetImageFromPath(string fullPath, ref string errorMessage)
		{
			try
			{
				return Image.FromFile(fullPath);
			}
			catch (Exception e)
			{
				errorMessage = e.Message;
			}

			return null;
		}

		public static string GetReportTemplateFullPath(ReportTemplateType reportTemplateType)
		{
			string serverDirectoryName = GetServerDirectoryName(DB_ServerDirectory.OrganizationTemplates);
			string fullPath = GetServerDirectoryPath(DB_ServerDirectory.OrganizationTemplates);
			if (!Directory.Exists(fullPath))
				Directory.CreateDirectory(fullPath);
			if (!Directory.Exists(Path.Combine(fullPath, serverDirectoryName)))
				Directory.CreateDirectory(Path.Combine(fullPath, serverDirectoryName));
			switch (reportTemplateType)
			{
				case ReportTemplateType.Header_A4:
					fullPath = Path.Combine(Path.Combine(fullPath, serverDirectoryName), A4PortraitHeader);
					break;
				case ReportTemplateType.Footer_A4:
					fullPath = Path.Combine(Path.Combine(fullPath, serverDirectoryName), A4PortraitFooter);
					break;
				case ReportTemplateType.Header_A5:
					fullPath = Path.Combine(Path.Combine(fullPath, serverDirectoryName), A5PortraitHeader);
					break;
			}

			return fullPath;
		}
	}
}
