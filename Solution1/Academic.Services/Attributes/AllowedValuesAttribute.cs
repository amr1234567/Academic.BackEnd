using System.ComponentModel.DataAnnotations;

namespace Academic.Services.Attributes
{
    public class AllowedValuesAttribute : ValidationAttribute
    {
        private readonly Type _enumType;

        public AllowedValuesAttribute(Type enumType)
        {
            if (!enumType.IsEnum)
            {
                throw new ArgumentException("AllowedValuesAttribute requires an enum type.");
            }

            _enumType = enumType;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || Enum.IsDefined(_enumType, value))
            {
                return ValidationResult.Success;
            }

            var allowedValues = string.Join(", ", Enum.GetNames(_enumType));
            return new ValidationResult($"The value '{value}' is not valid. Allowed values are: {allowedValues}.");
        }
    }

}
