using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Extractor.Common
{
	public class ValidationViewModelBase : ViewModelBase, System.ComponentModel.IDataErrorInfo
	{
		protected ValidationViewModelBase()
		{
			var metadataTypeAttr = this.GetType().GetCustomAttributes(typeof(MetadataTypeAttribute), false);
			if (metadataTypeAttr.Length == 1)
				this.metadataType = (metadataTypeAttr[0] as MetadataTypeAttribute).MetadataClassType;
			else
				this.metadataType = null;
		}

		private Type metadataType;

		public string Error
		{
			get { return null; }
		}

		public string this[string columnName]
		{
			get
			{
				try
				{
					if (this.metadataType != null)
						return this.ValidateProperty(this.metadataType, columnName);
					return validate(columnName);
				}
				catch
				{
					throw;
				}
			}
		}

		protected virtual string validate(string columnName)
		{
			return "";
		}
	}
}
