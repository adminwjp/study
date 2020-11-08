using System.Globalization;
using System.Windows.Controls;

namespace Wpf.Common
{
	public class NotEmptyValidationRule : ValidationRule
	{
		public override ValidationResult Validate(object value, CultureInfo cultureInfo)
		{
			return string.IsNullOrWhiteSpace((value ?? "").ToString())
				? new ValidationResult(false, "值不能为空!")
				: ValidationResult.ValidResult;
		}
	}
}