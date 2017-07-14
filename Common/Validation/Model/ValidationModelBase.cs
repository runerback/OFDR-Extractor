using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Extractor.Common.Validation
{
	public abstract class ValidationModelBase<T>
	{
		private string stringValue;
		/// <summary>
		/// get or set string value
		/// </summary>
		public string StringValue
		{
			get { return this.stringValue; }
			set
			{
				if (value != this.stringValue)
				{
					this.stringValue = value;
					this.hasFormatError = this.convert(value, out this.value);
				}
			}
		}

		private T value;
		/// <summary>
		/// get converted value
		/// </summary>
		public T Value
		{
			get { return this.value; }
		}

		protected bool hasFormatError;
		public bool HasFormatError
		{
			get { return this.hasFormatError; }
		}

		public override string ToString()
		{
			return this.stringValue;
		}

		/// <summary>
		/// convert from string value to T value
		/// </summary>
		/// <param name="value">string value</param>
		/// <param name="result">conversion result</param>
		/// <returns>if convert successed, return true; otherwise, return false</returns>
		protected abstract bool convert(string value, out T result);

		//ValidationResult.Success: We want this to be readonly since we're just returning [null] -----Justification
		private ValidationResult validationResult = new ValidationResult("");

		public string ErrorMessage
		{
			get { return this.validationResult.ErrorMessage; }
		}

		public bool IsValid
		{
			get { return string.IsNullOrEmpty(validationResult.ErrorMessage); }
		}

		public void SetValidationMessage(string errorMessage)
		{
			if (string.IsNullOrWhiteSpace(errorMessage))
				this.validationResult.ErrorMessage = "";
			else
				this.validationResult.ErrorMessage = errorMessage.Trim();
		}

		public void SetValidationMessage(ValidationResult validationResult)
		{
			if (validationResult == null || string.IsNullOrWhiteSpace(validationResult.ErrorMessage))
				this.validationResult.ErrorMessage = "";
			else
				this.validationResult.ErrorMessage = validationResult.ErrorMessage;
		}
	}
}
