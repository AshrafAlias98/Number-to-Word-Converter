namespace NumberConverter.Services
{
    public class NumberToWordConverterService : INumberToWordConverterService
    {
        public string ConvertNumberToWords(string input)
        {
            const int supportedDigits = 64;

            // Sanitize input.
            if (string.IsNullOrWhiteSpace(input) || !double.TryParse(input, out _))
                return $"Invalid number - {input}";

            if (input == "0")
                return "ZERO";

            input = input.Trim();
            bool isNegative = false;
            if (double.Parse(input) < 0)
            {
                isNegative = true;
                input = input.Replace("-", "");
            }

            // Define arrays/dictionaries for word representations.
            string[] ones = { "", "ONE", "TWO", "THREE", "FOUR", "FIVE", "SIX", "SEVEN", "EIGHT", "NINE" };
            string[] teens = { "TEN", "ELEVEN", "TWELVE", "THIRTEEN", "FOURTEEN", "FIFTEEN", "SIXTEEN", "SEVENTEEN", 
                "EIGHTEEN", "NINETEEN" };
            string[] tens = { "", "", "TWENTY", "THIRTY", "FORTY", "FIFTY", "SIXTY", "SEVENTY", "EIGHTY", "NINETY" };
            string[] thousands = { "", "THOUSAND", "MILLION", "BILLION", "TRILLION", "QUADRILLION", "QUINTILLION", 
                "SEXTILLION", "SEPTILLION", "OCTILLION", "NONILLION", "DECILLOIN", "UNDECILLOIN", "DUODECILLOIN", 
                "TREDECILLOIN", "QUATTUORDECILLOIN", "QUINDECILLOIN", "SEXDECILLOIN", "SEPTDECILLOIN", "OCTODECILLOIN", 
                "NOVEMDECILLOIN", "VIGINTILLOIN" };

            // Split number into integer and decimal parts.
            string[] parts = input.Split('.');
            string integerPart = parts[0];
            string decimalPart = parts.Length > 1 ? parts[1] : "";

            if (integerPart.Length > supportedDigits)
            {
                return $"Unsupported number.";
            }

            string result = "";

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
                result += decimalWords + " CENTS";
            }
            else if (!string.IsNullOrEmpty(decimalPart))
            {
                string decimalWords = ConvertGroupToWords(RoundUpSecondDigitAndTrim(decimalPart), ones, teens, tens);
                result += "DOLLARS AND " + decimalWords + " CENTS";
            }
            else
            {
                result += "DOLLARS";
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

        private int RoundUpSecondDigitAndTrim(string number)
        {
            number += "0";

            if (number.Length <= 2) 
                return int.Parse(number);

            int firstTwoDigits = int.Parse(number.Substring(0, 2)); 
            int thirdDigit = int.Parse(number.Substring(2, 1));
            return thirdDigit < 5 ? firstTwoDigits : firstTwoDigits+1;
        }
    }
}
