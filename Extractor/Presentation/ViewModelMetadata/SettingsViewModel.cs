using System;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace Extractor.Presentation.ViewModel
{
	[MetadataType(typeof(SettingsViewModelMetadata))]
	public partial class SettingsViewModel
	{
		internal class SettingsViewModelMetadata
		{
			[CustomValidation(typeof(SettingsViewModelValidationUtils), "ValidateOFDRRootFolder")]
			public string OFDRRootFolder { get; set; }
			[CustomValidation(typeof(SettingsViewModelValidationUtils), "ValidateDATFilePath")]
			public string DATFilePath { get; set; }
			[CustomValidation(typeof(SettingsViewModelValidationUtils), "ValidateOutputFolder")]
			public string OutputFolder { get; set; }
			[CustomValidation(typeof(SettingsViewModelValidationUtils), "ValidateMaxUnpackingParallelismCount")]
			public string MaxUnpackingParallelismCount { get; set; }
		}

		public static class SettingsViewModelValidationUtils
		{
			public static ValidationResult ValidateOFDRRootFolder(string value, ValidationContext context)
			{
				ValidationResult validationResult;

				if (string.IsNullOrEmpty(value))
				{
					validationResult = new ValidationResult("Required");
				}
				else
				{
					DirectoryInfo dir = new DirectoryInfo(value);
					if (!dir.Exists)
					{
						validationResult = new ValidationResult("Path does not exists");
					}
					else
					{
						var searchResult = dir.GetFiles("ofdr.exe,win000.nfs");
						if (searchResult.Length == 2)
						{
							validationResult = ValidationResult.Success;
						}
						else
						{
							validationResult = new ValidationResult("Invalid OFDR installer folder");
						}
					}
				}

				var ofdrRootFolder = (context.ObjectInstance as SettingsViewModel).ofdrRootFolder;
				ofdrRootFolder.SetValidationMessage(validationResult);

				return validationResult;
			}

			public static ValidationResult ValidateDATFilePath(string value, ValidationContext context)
			{
				ValidationResult validationResult;

				if (string.IsNullOrEmpty(value))
				{
					validationResult = new ValidationResult("Required");
				}
				else
				{
					FileInfo fi = new FileInfo(value);
					if (fi.Exists)
					{
						validationResult = ValidationResult.Success;
					}
					else
					{
						validationResult = new ValidationResult("File does not exists");
					}
				}

				var datFilePath = (context.ObjectInstance as SettingsViewModel).datFilePath;
				datFilePath.SetValidationMessage(validationResult);

				return validationResult;
			}

			public static ValidationResult ValidateOutputFolder(string value, ValidationContext context)
			{
				ValidationResult validationResult;

				if (string.IsNullOrEmpty(value))
				{
					validationResult = new ValidationResult("Required");
				}
				else
				{
					DirectoryInfo dir = new DirectoryInfo(value);
					if (!dir.Exists)
					{
						validationResult = new ValidationResult("Path does not exists");
					}
					else
					{
						validationResult = ValidationResult.Success;
					}
				}

				var outputFolder = (context.ObjectInstance as SettingsViewModel).outputFolder;
				outputFolder.SetValidationMessage(validationResult);

				return validationResult;
			}

			public static ValidationResult ValidateMaxUnpackingParallelismCount(string value, ValidationContext context)
			{
				var maxUnpackingParallelismCount = (context.ObjectInstance as SettingsViewModel).maxUnpackingParallelismCount;

				ValidationResult validationResult;

				if (maxUnpackingParallelismCount.HasFormatError)
				{
					validationResult = new ValidationResult("should be numeric");
				}
				else if (string.IsNullOrWhiteSpace(value))
				{
					validationResult = new ValidationResult("required");
				}
				else if (maxUnpackingParallelismCount.Value <= 0)
				{
					validationResult = new ValidationResult("must greater than 0");
				}
				else if (maxUnpackingParallelismCount.Value > 5)
				{
					validationResult = new ValidationResult("cannot greater than 5");
				}
				else
				{
					validationResult = ValidationResult.Success;
				}

				maxUnpackingParallelismCount.SetValidationMessage(validationResult);

				return validationResult;
			}
		}
	}
}
