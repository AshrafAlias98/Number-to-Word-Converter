using System.Reflection.Metadata.Ecma335;

namespace NumberConverter.Services
{
    public class NumberToWordConverterService : INumberToWordConverterService
    {
        public string ConvertNumberToWords(string input)
        {
            // Define arrays/dictionaries for word representations.
            string[] ones = { "", "ONE", "TWO", "THREE", "FOUR", "FIVE", "SIX", "SEVEN", "EIGHT", "NINE" };
            string[] teens = { "TEN", "ELEVEN", "TWELVE", "THIRTEEN", "FOURTEEN", "FIFTEEN", "SIXTEEN", "SEVENTEEN",
                "EIGHTEEN", "NINETEEN" };
            string[] tens = { "", "", "TWENTY", "THIRTY", "FORTY", "FIFTY", "SIXTY", "SEVENTY", "EIGHTY", "NINETY" };
            string[] thousands = { "", "THOUSAND", "MILLION", "BILLION", "TRILLION", "QUADRILLION", "QUINTILLION",
                "SEXTILLION", "SEPTILLION", "OCTILLION", "NONILLION", "DECILLOIN", "UNDECILLOIN", "DUODECILLOIN",
                "TREDECILLOIN", "QUATTUORDECILLOIN", "QUINDECILLOIN", "SEXDECILLOIN", "SEPTDECILLOIN", "OCTODECILLOIN",
                "NOVEMDECILLOIN", "VIGINTILLOIN" };

            const int supportedDigits = 64;

            // Validate input.
            if (!string.IsNullOrWhiteSpace(ValidateInput(input)))
                return ValidateInput(input);

            input = input.Trim();
            bool isNegative = false;
            if (double.Parse(input) < 0)
            {
                isNegative = true;
                input = input.Replace("-", "");
            }

            // Split number into integer and decimal parts.
            string[] parts = input.Split('.');
            string integerPart = parts[0];
            string decimalPart = parts.Length > 1 ? parts[1] : "";
            string result = "";

            if (integerPart.Length > supportedDigits)
                return "Unsupported number";

            // Get appropriate currency representation.
            var currencyUnit = GetCurrencyUnit(integerPart);
            var currencySubUnit = GetCurrencySubUnit(decimalPart);

            // Convert integer part.
            int groupIndex = 0;
            while (integerPart.Length > 0)
            {
                int group = int.Parse(integerPart.Substring(Math.Max(0, integerPart.Length - 3)));
                integerPart = integerPart.Remove(Math.Max(0, integerPart.Length - 3));
                string groupWords = ConvertGroupToWords(group, ones, teens, tens);
                if (groupWords != "")
                {
                    if (groupIndex > 0)
                        result = groupWords + " " + thousands[groupIndex] + " " + result;
                    else
                        result = groupWords + " " + result;
                }
                groupIndex++;
            }

            // Convert decimal part.
            if (string.IsNullOrWhiteSpace(result) && string.IsNullOrWhiteSpace(decimalPart))
            {
                result += "ZERO";
            }
            else if (string.IsNullOrWhiteSpace(result))
            {
                string decimalWords = ConvertGroupToWords(RoundUpSecondDigitAndTrim(decimalPart), ones, teens, tens);
                result += decimalWords + $" {currencySubUnit}";
            }
            else if (!string.IsNullOrEmpty(decimalPart))
            {
                string decimalWords = ConvertGroupToWords(RoundUpSecondDigitAndTrim(decimalPart), ones, teens, tens);
                result += $"{currencyUnit} AND " + decimalWords + $" {currencySubUnit}";
            }
            else
            {
                result += $"{currencyUnit}";
            }

            // Include negative (if applicable).
            if (isNegative)
                return "NEGATIVE " + result.Trim();

            return result.Trim();
        }

        private string ConvertGroupToWords(int group, string[] ones, string[] teens, string[] tens)
        {
            string groupWords = "";

            // Extract individual digits.
            int hundreds = group / 100;
            int tensUnits = group % 100;

            // Convert hundreds place.
            if (hundreds > 0)
            {
                groupWords += ones[hundreds] + " HUNDRED";
                if (tensUnits > 0)
                    groupWords += " AND ";
            }

            // Convert tens and units place.
            if (tensUnits >= 20)
            {
                int tensDigit = tensUnits / 10;
                int unitsDigit = tensUnits % 10;
                groupWords += tens[tensDigit];
                if (unitsDigit > 0)
                    groupWords += "-" + ones[unitsDigit];
            }
            else if (tensUnits >= 10)
            {
                groupWords += teens[tensUnits - 10];
            }
            else if (tensUnits > 0)
            {
                groupWords += ones[tensUnits];
            }

            return groupWords;
        }

        private int RoundUpSecondDigitAndTrim(string input)
        {
            input += "0";

            if (input.Length <= 2) 
                return int.Parse(input);

            int firstTwoDigits = int.Parse(input.Substring(0, 2)); 
            int thirdDigit = int.Parse(input.Substring(2, 1));
            return thirdDigit < 5 ? firstTwoDigits : firstTwoDigits+1;
        }

        private string ValidateInput(string input)
        {
            if (string.IsNullOrWhiteSpace(input) || !double.TryParse(input, out _))
                return "Invalid number";

            if (input == "0")
                return "ZERO";

            return string.Empty;
        }

        private string GetCurrencyUnit(string input)
        {
            const string singularCurrencyUnit = "DOLLAR";
            const string pluralCurrencyUnit = "DOLLARS";

            if (input.Length == 1 && int.Parse(input) == 1)
                return singularCurrencyUnit;

            return pluralCurrencyUnit;
        }

        private string GetCurrencySubUnit(string input)
        {
            const string singularCurrencySubUnit = "CENT";
            const string pluralCurrencySubUnit = "CENTS";

            if (RoundUpSecondDigitAndTrim(input) == 1)
                return singularCurrencySubUnit;

            return pluralCurrencySubUnit;
        }
    }
}
