namespace PortfolioPoker.Domain.Validation
{
    public sealed class ValidationResult
    {
        public bool IsValid { get; }
        public string ErrorMessage { get; }

        private ValidationResult(bool isValid, string errorMessage)
        {
            IsValid = isValid;
            ErrorMessage = errorMessage;
        }

        public static ValidationResult Success()
        {
            return new ValidationResult(true, string.Empty);
        }

        public static ValidationResult Fail(string errorMessage)
        {
            return new ValidationResult(false, errorMessage);
        }
    }
}