using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Extractor.Common.Dialog
{
	public static class FileSystemDialog
	{
		public static string ChooseFolder(string title = "choose folder")
		{
			CommonOpenFileDialog dialog = new CommonOpenFileDialog()
			{
				Title = title,
				AllowPropertyEditing = false,
				EnsureFileExists = false,
				EnsurePathExists = true,
				IsFolderPicker = true,
				Multiselect = false
			};
			if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
			{
				return dialog.FileName;
			}
			return null;
		}

		public static List<string> ChooseFolders(string title = "choose folders")
		{
			CommonOpenFileDialog dialog = new CommonOpenFileDialog()
			{
				Title = title,
				AllowPropertyEditing = false,
				EnsureFileExists = false,
				EnsurePathExists = true,
				IsFolderPicker = true,
				Multiselect = true
			};
			if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
			{
				if (dialog.FileNames != null)
				{
					var paths = dialog.FileNames.ToList();
					if (paths.Count != 0)
					{
						return paths;
					}
				}
			}
			return null;
		}

		public static string ChooseFile(string title = "choose file")
		{
			CommonOpenFileDialog dialog = new CommonOpenFileDialog()
			{
				Title = title,
				AllowPropertyEditing = false,
				EnsureFileExists = false,
				EnsurePathExists = true,
				IsFolderPicker = false,
				Multiselect = false
			};
			if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
			{
				return dialog.FileName;
			}
			return null;
		}
	}
}
