using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Extractor.Common
{
	internal static class ValidationExtension
	{
		public static string ValidateProperty(this object source, Type metadataType, string propertyName)
		{
			var sourceType = source.GetType();
			var provider = new AssociatedMetadataTypeTypeDescriptionProvider(sourceType, metadataType);
			try
			{
				if (string.IsNullOrEmpty(propertyName))
				{
					return string.Empty;
				}
				if (sourceType.GetProperty(propertyName) == null)
				{
					return string.Empty;
				}
				if (sourceType != metadataType)
				{
					TypeDescriptor.AddProvider(provider, sourceType);
				}
				var propertyValue = sourceType.GetProperty(propertyName).GetValue(source, null);
				var validationContext = new ValidationContext(source, null, null);
				validationContext.MemberName = propertyName;
				var validationResults = new List<ValidationResult>();

				Validator.TryValidateProperty(propertyValue, validationContext, validationResults);

				if (validationResults.Count > 0)
				{
					return validationResults[0].ErrorMessage;
				}
				else
				{
					return string.Empty;
				}
			}
			catch (Exception ex)
			{
				return ex.Message;
			}
			finally
			{
				TypeDescriptor.RemoveProvider(provider, sourceType);
			}
		}

		public static string ValidateProperty<MetadataType>(this object source, string propertyName)
		{
			return ValidateProperty(source, typeof(MetadataType), propertyName);
		}

		public static List<string> ValidateAllProperties(this object source, Type metadataType)
		{
			var targetType = source.GetType();
			var typeProvider = new AssociatedMetadataTypeTypeDescriptionProvider(targetType, metadataType);
			try
			{
				if (targetType != metadataType)
				{
					TypeDescriptor.AddProvider(typeProvider, targetType);
				}

				var validationContext = new ValidationContext(source, null, null);
				var validationResults = new List<ValidationResult>();
				bool result = Validator.TryValidateObject(source, validationContext, validationResults, true);
				if (validationResults.Count > 0)
				{
					return validationResults.Select(item => item.ErrorMessage).ToList();
				}
				return null;
			}
			catch
			{
				return null;
			}
			finally
			{
				TypeDescriptor.RemoveProvider(typeProvider, targetType);
			}
		}

		public static List<string> ValidateAllProperties<MetadataType>(this object source)
		{
			return ValidateAllProperties(source, typeof(MetadataType));
		}
	}
}
