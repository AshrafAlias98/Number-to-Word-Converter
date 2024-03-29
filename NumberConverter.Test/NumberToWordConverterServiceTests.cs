using NumberConverter.Services;

namespace NumberConverter.Test
{
    public class NumberToWordConverterServiceTests
    {
        private NumberToWordConverterService _numberToWordConverterService;

        public NumberToWordConverterServiceTests()
        {
            _numberToWordConverterService = new NumberToWordConverterService();
        }

        [Fact]
        public void ConvertNumberToWords_InputValidString_ReturnValidResult()
        {
            // Arrange
            string number = "123.45";

            // Act
            string result = _numberToWordConverterService.ConvertNumberToWords(number);

            // Assert
            Assert.Equal("ONE HUNDRED AND TWENTY-THREE DOLLARS AND FORTY-FIVE CENTS", result);
        }

        [Fact]
        public void ConvertNumberToWords_InputInvalidString_ReturnErrorMessage()
        {
            // Arrange
            string number = "abc.45";

            // Act
            string result = _numberToWordConverterService.ConvertNumberToWords(number);

            // Assert
            Assert.Equal("Invalid number", result);
        }

        [Fact]
        public void ConvertNumberToWords_InputEmptyString_ReturnErrorMessage()
        {
            // Arrange
            string number = "";

            // Act
            string result = _numberToWordConverterService.ConvertNumberToWords(number);

            // Assert
            Assert.Equal("Invalid number", result);
        }

        [Fact]
        public void ConvertNumberToWords_InputWholeNumberString_ReturnValidResult()
        {
            // Arrange
            string number = "123";

            // Act
            string result = _numberToWordConverterService.ConvertNumberToWords(number);

            // Assert
            Assert.Equal("ONE HUNDRED AND TWENTY-THREE DOLLARS", result);
        }

        [Fact]
        public void ConvertNumberToWords_InputDecimalNumberString_ReturnValidResult()
        {
            // Arrange
            string number = "0.45";

            // Act
            string result = _numberToWordConverterService.ConvertNumberToWords(number);

            // Assert
            Assert.Equal("FORTY-FIVE CENTS", result);
        }

        [Fact]
        public void ConvertNumberToWords_InputWholeAndDecimalNumberString_ReturnValidResult()
        {
            // Arrange
            string number = "123.45";

            // Act
            string result = _numberToWordConverterService.ConvertNumberToWords(number);

            // Assert
            Assert.Equal("ONE HUNDRED AND TWENTY-THREE DOLLARS AND FORTY-FIVE CENTS", result);
        }

        [Fact]
        public void ConvertNumberToWords_InputNumberWithLeadingZerosString_ReturnValidResult()
        {
            // Arrange
            string number = "0050.99";

            // Act
            string result = _numberToWordConverterService.ConvertNumberToWords(number);

            // Assert
            Assert.Equal("FIFTY DOLLARS AND NINETY-NINE CENTS", result);
        }

        [Fact]
        public void ConvertNumberToWords_InputNumberWithTrailingZerosString_ReturnValidResult()
        {
            // Arrange
            string number = "100.5000";

            // Act
            string result = _numberToWordConverterService.ConvertNumberToWords(number);

            // Assert
            Assert.Equal("ONE HUNDRED DOLLARS AND FIFTY CENTS", result);
        }

        [Fact]
        public void ConvertNumberToWords_InputMinimumNumberString_ReturnValidResult()
        {
            // Arrange
            string number = "0";

            // Act
            string result = _numberToWordConverterService.ConvertNumberToWords(number);

            // Assert
            Assert.Equal("ZERO", result);
        }

        [Fact]
        public void ConvertNumberToWords_InputMaximumNumberString_ReturnValidResult()
        {
            // Arrange
            string number = "9999999999999999999999999999999999999999999999999999999999999999.99";

            // Act
            string result = _numberToWordConverterService.ConvertNumberToWords(number);

            // Assert
            Assert.Equal("NINE VIGINTILLOIN NINE HUNDRED AND NINETY-NINE NOVEMDECILLOIN NINE HUNDRED " +
                "AND NINETY-NINE OCTODECILLOIN NINE HUNDRED AND NINETY-NINE SEPTDECILLOIN NINE HUNDRED " +
                "AND NINETY-NINE SEXDECILLOIN NINE HUNDRED AND NINETY-NINE QUINDECILLOIN NINE HUNDRED " +
                "AND NINETY-NINE QUATTUORDECILLOIN NINE HUNDRED AND NINETY-NINE TREDECILLOIN NINE HUNDRED " +
                "AND NINETY-NINE DUODECILLOIN NINE HUNDRED AND NINETY-NINE UNDECILLOIN NINE HUNDRED AND " +
                "NINETY-NINE DECILLOIN NINE HUNDRED AND NINETY-NINE NONILLION NINE HUNDRED AND NINETY-NINE " +
                "OCTILLION NINE HUNDRED AND NINETY-NINE SEPTILLION NINE HUNDRED AND NINETY-NINE SEXTILLION " +
                "NINE HUNDRED AND NINETY-NINE QUINTILLION NINE HUNDRED AND NINETY-NINE QUADRILLION NINE " +
                "HUNDRED AND NINETY-NINE TRILLION NINE HUNDRED AND NINETY-NINE BILLION NINE HUNDRED AND " +
                "NINETY-NINE MILLION NINE HUNDRED AND NINETY-NINE THOUSAND NINE HUNDRED AND NINETY-NINE " +
                "DOLLARS AND NINETY-NINE CENTS", result);
        }

        [Fact]
        public void ConvertNumberToWords_InputMinimumDecimalNumberString_ReturnValidResult()
        {
            // Arrange
            string number = "0.01";

            // Act
            string result = _numberToWordConverterService.ConvertNumberToWords(number);

            // Assert
            Assert.Equal("ONE CENT", result);
        }

        [Fact]
        public void ConvertNumberToWords_InputMaximumDecimalNumberString_ReturnValidResult()
        {
            // Arrange
            string number = "0.99";

            // Act
            string result = _numberToWordConverterService.ConvertNumberToWords(number);

            // Assert
            Assert.Equal("NINETY-NINE CENTS", result);
        }

        [Fact]
        public void ConvertNumberToWords_InputDecimalWithOneNonZeroDigitString_ReturnValidResult()
        {
            // Arrange
            string number = "0.1";

            // Act
            string result = _numberToWordConverterService.ConvertNumberToWords(number);

            // Assert
            Assert.Equal("TEN CENTS", result);
        }

        [Fact]
        public void ConvertNumberToWords_InputLargeDecimalWithoutRoundingString_ReturnValidResult()
        {
            // Arrange
            string number = "123.45123";

            // Act
            string result = _numberToWordConverterService.ConvertNumberToWords(number);

            // Assert
            Assert.Equal("ONE HUNDRED AND TWENTY-THREE DOLLARS AND FORTY-FIVE CENTS", result);
        }

        [Fact]
        public void ConvertNumberToWords_InputLargeDecimalWithRoundingString_ReturnValidResult()
        {
            // Arrange
            string number = "123.45678";

            // Act
            string result = _numberToWordConverterService.ConvertNumberToWords(number);

            // Assert
            Assert.Equal("ONE HUNDRED AND TWENTY-THREE DOLLARS AND FORTY-SIX CENTS", result);
        }

        [Fact]
        public void ConvertNumberToWords_InputNegativeNumberString_ReturnValidResult()
        {
            // Arrange
            string number = "-123.45";

            // Act
            string result = _numberToWordConverterService.ConvertNumberToWords(number);

            // Assert
            Assert.Equal("NEGATIVE ONE HUNDRED AND TWENTY-THREE DOLLARS AND FORTY-FIVE CENTS", result);
        }

        [Fact]
        public void ConvertNumberToWords_InputUnsupportedNumberString_ReturnErrorMessage()
        {
            // Arrange
            string number = "99999999999999999999999999999999999999999999999999999999999999999.99";

            // Act
            string result = _numberToWordConverterService.ConvertNumberToWords(number);

            // Assert
            Assert.Equal("Unsupported number", result);
        }
    }
}